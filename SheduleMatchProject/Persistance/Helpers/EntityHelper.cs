using Domain.Models.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Helpers
{
    public static class EntityHelper
    {
        public static async Task<ListPaginated<T>> AddPagination<T>(this IQueryable<T> query, ModelPagination modelPagination)
        {
            var totalCount = await query.CountAsync();
            return new ListPaginated<T>
            {
                Items = await query
                .Skip((modelPagination.Page - 1) * modelPagination.Size)
                .Take(modelPagination.Size)
                .ToListAsync(),
                PaginationInfo = new PaginationInfo()
                {
                    TotalCount = totalCount,
                    PagesCount = (int)Math.Ceiling((decimal)totalCount / modelPagination.Size),
                    FirstIndex = totalCount == 0 ? 0 : modelPagination.Size * (modelPagination.Page - 1) + 1,
                    LastIndex = Math.Min(modelPagination.Page * modelPagination.Size, totalCount),
                    //CurrentPage = modelPagination.Page,
                    //CurrentSize = modelPagination.Size
                }

            };
            
        }
    }
}
