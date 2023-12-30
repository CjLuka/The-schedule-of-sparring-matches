using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Pagination
{
    public class ModelPagination
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public ModelPagination(int page, int size)
        {
            Page = page;
            Size = size;
        }
    }
}
