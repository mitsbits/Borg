﻿using Borg.Framework.DAL;
using Borg.Framework.DAL.Inventories;
using Borg.Framework.DAL.Ordering;
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
using System.Text;
using System.Threading.Tasks;

namespace Borg.System.Backoffice.Core.GenericEntity
{
    [BackOfficeEntityControllerName]
    public class BackOfficeEntityController<TEntity, TDbContext> : BackOfficeController where TEntity : class, IEntity, new() where TDbContext : DbContext
    {
        private static string HeaderColumnsCacheKey = $"{typeof(TEntity).Name}:{nameof(HeaderColumn)}Definition";

        protected readonly ILogger logger;
        private readonly IInventoryFacade<TEntity> inv;
        private readonly IUserSession userSession;

        protected readonly DmlOperation mode;

        public BackOfficeEntityController(ILoggerFactory loggerFactory, IInventoryFacade<TEntity> inv, IUserSession userSession)
        {
            logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(GetType());
            this.inv = Preconditions.NotNull(inv, nameof(inv));
            this.userSession = Preconditions.NotNull(userSession, nameof(userSession));
        }

        //private DmlOperation DetermineMode()
        //{
        //    if (ControllerContext.Action() == nameof(Detail)) return DmlOperation.Update;
        //    if (ControllerContext.Action() == nameof(Create)) return DmlOperation.Create;
        //    if (ControllerContext.Action() == nameof(Delete)) return DmlOperation.Delete;
        //    return DmlOperation.Update;
        //}

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
                var hit = this.inv.Find(predicate, 1, 1, OrderByInfo<TEntity>.DefaultSorter());
                return View("~/Areas/Backoffice/Views/BackOfficeEntity/Detail.cshtml", hit);
            };
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await inv.Instance();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
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

        #endregion Private
    }
}