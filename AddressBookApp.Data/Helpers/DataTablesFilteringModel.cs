using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookApp.Data.Helpers
{
    public class DataTablesFilteringModel
    {
        public Dictionary<string, string> SearchConfig { get; set; };
        public string SortingConfig { get; set; }
        public int StartIndex { get; set; }
        public int ItemAmount { get; set; }
        
    }
}
