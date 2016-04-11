using DataTables.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookApp.Helpers
{
    public static class DataTablesHelper
    {
        public static string GetSortingConfig(IOrderedEnumerable<Column> sortedColumns)
        {
            var sortingConfig = String.Empty;

            foreach (var column in sortedColumns)
            {
                sortingConfig += sortingConfig != String.Empty ? "," : "";
                sortingConfig += column.Data + " " + (column.SortDirection.ToString()[0] == 'D' ? "DESC" : "ASC");
            }

            return sortingConfig;
        }
    }
}
