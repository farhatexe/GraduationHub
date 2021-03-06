﻿using System.Linq;
using DataTables.Mvc;

namespace GraduationHub.Web.Infrastructure
{
    public static class DataTablesRequestExtensions
    {
        /// <summary>
        /// Calculates the Sort String to be used in the query
        /// </summary>
        /// <param name="dataTablesRequest"></param>
        /// <returns></returns>
        public static string Sort(this IDataTablesRequest dataTablesRequest)
        {
            // Get Columns that are sorted:
            string[] columns = dataTablesRequest
                .Columns.Where(x => x.IsOrdered)
                .Select(c => string.Format("{0} {1}", c.Data, c.SortDirection.SqlValue()))
                .ToArray();

            return string.Join(", ", columns);
        }

        public static string SearchValues(this IDataTablesRequest dataTablesRequest)
        {
            string[] predicates = dataTablesRequest
                .Columns.Where(x => x.Searchable)
                .Select(c => string.Format("{0}.Contains(@0)", c.Data))
                .ToArray();

            return string.Join(" OR ", predicates);
        }

        public static bool HasSearchValues(this IDataTablesRequest dataTablesRequest)
        {
            return !string.IsNullOrWhiteSpace(dataTablesRequest.Search.Value);
        }
    }

    public static class ColumnOrderDirectionExtensions
    {
        /// <summary>
        /// Translates the OrderDirection to a Sql Clause
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static string SqlValue(this Column.OrderDirection direction)
        {
            return direction == Column.OrderDirection.Ascendant
                ? "ASC"
                : "DESC";
        }
    }
}