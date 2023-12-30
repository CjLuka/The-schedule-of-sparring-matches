using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Pagination
{
    public class ListPaginated<T>
    {
        public List<T> Items { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
