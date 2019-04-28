using Borg.Framework.Cms;
using Borg.Framework.DAL;
using Borg.Framework.EF.Contracts;
using Borg.Framework.Modularity;
using Borg.Infrastructure.Core;
using Borg.Infrastructure.Core.Collections;
using Borg.Infrastructure.Core.DDD.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Borg.System.Backoffice.Lib
{
    [BackOfficeEntityControllerName]
    [Route("{area}/entity/{controller:genericController}/{action=Index}/")]
    public class BackOfficeEntityController<TEntity, TDbContext> : BackOfficeController where TEntity : class, IEntity, new() where TDbContext : DbContext
    {
        private static string HeaderColumnsCacheKey = $"{typeof(TEntity).Name}:{nameof(HeaderColumn)}Definition";

        protected readonly ILogger logger;
        private readonly IUnitOfWork<TDbContext> uow;
        private readonly IUserSession userSession;

        private string _entityPluralTitle;
        private string _entitySingularTitle;

        private string EntityPluralTitle()
        {
            if (!_entityPluralTitle.IsNullOrWhiteSpace()) return _entityPluralTitle;
            var attr = typeof(TEntity).GetCustomAttribute<GenericEntityAttribute>();
            if (attr != null)
            {
                if (attr.Plural.IsNullOrWhiteSpace())
                {
                    _entityPluralTitle = typeof(TEntity).Name.SplitCamelCaseToWords();
                }
                else
                {
                    _entityPluralTitle = attr.Plural;
                }
            }
            return _entityPluralTitle;
        }

        private string EntitySingularTitle()
        {
            if (!_entitySingularTitle.IsNullOrWhiteSpace()) return _entityPluralTitle;
            var attr = typeof(TEntity).GetCustomAttribute<GenericEntityAttribute>();
            if (attr != null)
            {
                if (attr.Singular.IsNullOrWhiteSpace())
                {
                    _entitySingularTitle = typeof(TEntity).Name.SplitCamelCaseToWords();
                }
                else
                {
                    _entitySingularTitle = attr.Singular;
                }
            }
            return _entitySingularTitle;
        }

        public BackOfficeEntityController(ILoggerFactory loggerFactory, IUnitOfWork<TDbContext> uow, IUserSession userSession)
        {
            logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
            Preconditions.NotNull(uow, nameof(uow));
            Preconditions.NotNull(userSession, nameof(userSession));
            this.uow = uow;
            this.userSession = userSession;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var ColumnDefinition = GetHeaderColumnDefinition();

            var orderByDefinitions = GetOrderBy(ColumnDefinition);

            IPagedResult<TEntity> results = await uow.QueryRepo<TEntity>().Find(x => true,
                Pager().Current, Pager().RowCount, orderByDefinitions);

            var model = new EntityGridViewModel<TEntity>()
            {
                Data = results,
                Title = EntityPluralTitle(),
                HeaderColumns = userSession.Setting<IEnumerable<HeaderColumn>>(HeaderColumnsCacheKey).ToList()
            };
            return View("~/Areas/Backoffice/Views/BackOfficeEntity/Index.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail()
        {

            if (typeof(TEntity).ImplementsInterface(typeof(IIdentifiable)))
            {
                var props = typeof(TEntity).GetProperties();
                var queryProps = new List<(string prop, object val)>();
                foreach (var quwryKeu in Request.Query.Keys)
                {
                    var prop = props.SingleOrDefault(x => x.Name.ToLower() == quwryKeu.ToLower());
                    if (prop != null)
                    {
                        queryProps.Add((prop: prop.Name, val: Request.Query[quwryKeu]));
                    }
                }

                var exprBuilder = new StringBuilder("x=> ");

                for (var i = 0; i < queryProps.Count(); i++)
                {
                    var prp = queryProps[i];
                    if (i > 0)
                    {
                        exprBuilder.Append(" && ");
                    }
                    exprBuilder.Append($"x.{prp.prop} == {prp.val}");
                }
                var options = ScriptOptions.Default.AddReferences(typeof(TEntity).Assembly);
                Expression<Func<TEntity, bool>> predicate = await CSharpScript.EvaluateAsync<Expression<Func<TEntity, bool>>>(exprBuilder.ToString(), options);
                var hit = this.uow.QueryRepo<TEntity>().Get(predicate);
                return View("~/Areas/Backoffice/Views/BackOfficeEntity/Detail.cshtml", hit);
            };
            return BadRequest();
        }


        [HttpPost]

        public IActionResult SetColumnSelection(EntityGridViewModel<TEntity> model)
        {
            userSession.Setting<IEnumerable<HeaderColumn>>(HeaderColumnsCacheKey);
            if (model.ReorderColumns.IsNullOrWhiteSpace())
            {
                return RedirectToRoute(new { action = "index" });
            }
            var colOrder = model.ReorderColumns.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var newOrder = colOrder.Select(x => int.Parse(x)).ToArray();
            var newHeaderColums = new List<HeaderColumn>();
            for (var i = 0; i < newOrder.Count(); i++)
            {
                var currentOrder = newOrder[i];
                var current = model.HeaderColumns.FirstOrDefault(x => x.Order == currentOrder);
                current.Order = i;
                newHeaderColums.Add(current);
            }
            userSession.Setting<IEnumerable<HeaderColumn>>(HeaderColumnsCacheKey, newHeaderColums);

            return RedirectToRoute(new { action = "index" });
        }


        #region Private


        private static List<OrderByInfo<TEntity>> GetOrderBy(IEnumerable<HeaderColumn> savedHeaderColumns)
        {
            List<OrderByInfo<TEntity>> sorts = new List<OrderByInfo<TEntity>>();
            foreach (var sortableHeader in savedHeaderColumns.Where(x => x.SortDirection != SortDirection.None))
            {
                var direction = sortableHeader.SortDirection == SortDirection.Ascending ? "ASC" : "DESC";
                var sort = new OrderByInfo<TEntity>($"{sortableHeader.Title}:{direction}");
                sorts.Add(sort);
            }
            return sorts;
        }

        private IEnumerable<HeaderColumn> GetHeaderColumnDefinition()
        {
            var savedHeaderColumns = userSession.Setting<IEnumerable<HeaderColumn>>(HeaderColumnsCacheKey);
            if (savedHeaderColumns == null)
            {
                var orderdColumns = typeof(TEntity).GetProperties().Select((x, i) => new HeaderColumn(x))
                      .OrderByDescending(x => x.IsSimple).ThenBy(x => x.Title).Select((x, i) =>
                      {
                          var o = x;
                          o.Order = i;
                          return o;
                      });
                userSession.Setting<IEnumerable<HeaderColumn>>(HeaderColumnsCacheKey, orderdColumns.OrderBy(x => x.Order).ToList());
            }
            return userSession.Setting<IEnumerable<HeaderColumn>>(HeaderColumnsCacheKey);
        }
        #endregion
    }
}