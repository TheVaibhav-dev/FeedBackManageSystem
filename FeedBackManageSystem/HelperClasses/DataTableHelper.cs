using FeedBackManageSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FeedBackManageSystem.HelperClasses
{
    public static class DataTableHelper
    {
        public static DataTableResult<T> ApplyDataTable<T>(IQueryable<T> query,DataTableAjaxPostModel model,Expression<Func<T, DateTime?>> dateColumn = null,DateTime? startDate = null,DateTime? endDate = null)
        {
            var totalRecords = query.Count();

            // Date Filter (optional)
            if (dateColumn != null)
            {
                if (startDate.HasValue)
                    query = query.Where(x => dateColumn.Compile()(x) >= startDate);

                if (endDate.HasValue)
                    query = query.Where(x => dateColumn.Compile()(x) <= endDate);
            }

            // Global Search
            var search = model.search?.value;
            if (!string.IsNullOrWhiteSpace(search))
            {
                var props = typeof(T).GetProperties()
                    .Where(p => p.PropertyType == typeof(string));

                foreach (var p in props)
                    query = query.Where(x =>
                        EF.Functions.Like(EF.Property<string>(x, p.Name), $"%{search}%"));
            }

            var filteredRecords = query.Count();

            // Sorting
            if (model.order?.Any() == true)
            {
                var colIndex = model.order[0].column;
                var colName = model.columns[colIndex].data;
                var dir = model.order[0].dir;

                query = dir == "asc"
                    ? query.OrderByDynamic(colName)
                    : query.OrderByDescendingDynamic(colName);
            }

            // Paging
            var data = query
                .Skip(model.start)
                .Take(model.length)
                .ToList();

            return new DataTableResult<T>
            {
                RecordsTotal = totalRecords,
                RecordsFiltered = filteredRecords,
                Data = data
            };
        }
    }



    // 🔹 Sorting helper
    public static class OrderByExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string property)
            => ApplyOrder(source, property, "OrderBy");

        public static IQueryable<T> OrderByDescendingDynamic<T>(this IQueryable<T> source, string property)
            => ApplyOrder(source, property, "OrderByDescending");

        private static IQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string method)
        {
            var param = Expression.Parameter(typeof(T), "x");
            var body = Expression.PropertyOrField(param, property);

            var selector = Expression.Lambda(body, param);

            return (IQueryable<T>)typeof(Queryable)
                .GetMethods()
                .Single(m => m.Name == method && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), body.Type)
                .Invoke(null, new object[] { source, selector })!;
        }
    }
}
