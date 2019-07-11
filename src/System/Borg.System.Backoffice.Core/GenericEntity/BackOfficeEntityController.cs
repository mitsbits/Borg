using Borg.Framework.DAL;
using Borg.Framework.DAL.Inventories;
using Borg.Framework.DAL.Ordering;
using Borg.Framework.Modularity;
using Borg.Infrastructure.Core;
using Borg.Infrastructure.Core.Collections;
using Borg.Infrastructure.Core.DDD.Contracts;
using Borg.Infrastructure.Core.DDD.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Borg.System.Backoffice.Core.GenericEntity
{
    [BackOfficeEntityControllerName]
    public class BackOfficeEntityController<TEntity, TDbContext> : BackOfficeController where TEntity : class, IEntity, new() where TDbContext : DbContext
    {
        private static string HeaderColumnsCacheKey = $"{typeof(TEntity).Name}:{nameof(HeaderColumn)}Definition";

        private readonly IInventoryFacade<TEntity> inv;

        protected readonly DmlOperation mode;

        public BackOfficeEntityController(ILoggerFactory loggerFactory, IInventoryFacade<TEntity> inv, IUserSession userSession) : base(loggerFactory, userSession)
        {
            this.inv = Preconditions.NotNull(inv, nameof(inv));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var ColumnDefinition = GetHeaderColumnDefinition();

            var orderByDefinitions = GetOrderBy(ColumnDefinition);

            IPagedResult<TEntity> results = await inv.Find(x => true, Pager().Current, Pager().RowCount, OrderByInfo<TEntity>.DefaultSorter(), default);

            var model = new EntityGridViewModel<TEntity>()
            {
                Data = results,
                Title = typeof(TEntity).EntitySingular(),
                HeaderColumns = userSession.Setting<IEnumerable<HeaderColumn>>(HeaderColumnsCacheKey).ToList()
            };
            return View("~/Areas/Backoffice/Views/BackOfficeEntity/Index.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail()
        {
            if (typeof(TEntity).ImplementsInterface(typeof(IIdentifiable)))
            {
                var keys = new CompositeKey(Request.QueryString.Value);
                var hit = await inv.Get(keys);

                var model = new EditEntityViewModel<TEntity>() { Data = hit, DmlOperation = DmlOperation.Update };
                model.Title = ((IHaveTitle)hit).Title;

                return View("~/Areas/Backoffice/Views/BackOfficeEntity/Detail.cshtml", hit);
            };
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new EditEntityViewModel<TEntity>() { Data = await inv.Instance(), Title = "Create", DmlOperation = DmlOperation.Create };
            return View("~/Areas/Backoffice/Views/BackOfficeEntity/Detail.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return BadRequest();
        }

        [HttpPost]
        public IActionResult SetColumnSelection(EntityGridViewModel<TEntity> model)
        {
            if (mode != DmlOperation.Query)
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
            }

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
            var savedHeaderColumns = userSession.Setting<HeaderColumn[]>(HeaderColumnsCacheKey);
            if (savedHeaderColumns == null)
            {
                var orderdColumns = typeof(TEntity).GetProperties().Select((x, i) => new HeaderColumn(x))
                      .OrderByDescending(x => x.IsSimple).ThenBy(x => x.Title).Select((x, i) =>
                      {
                          var o = x;
                          o.Order = i;
                          return o;
                      }).ToArray();
                userSession.Setting<HeaderColumn[]>(HeaderColumnsCacheKey, orderdColumns.OrderBy(x => x.Order).ToArray());
            }
            return userSession.Setting<HeaderColumn[]>(HeaderColumnsCacheKey);
        }

        #endregion Private
    }
}