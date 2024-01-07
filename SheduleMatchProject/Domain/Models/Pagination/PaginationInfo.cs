using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Pagination
{
    public class PaginationInfo
    {
        public int PagesCount { get; set; }
        public int TotalCount { get; set; }
        public int FirstIndex { get; set; }
        public int LastIndex { get; set; }
        //public int CurrentPage { get; set; }
        //public int CurrentSize { get; set; }
    }
}
