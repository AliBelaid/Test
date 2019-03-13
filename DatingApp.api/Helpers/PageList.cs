using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.api.Helpers {
    public class PageList<T> : List<T> {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PageList (List<T> items, int count, int pageNumber, int pageSize) {
            TotalCount = count;
            TotalPages = (int) Math.Ceiling (count / (double) pageSize);
            CurrentPage = pageNumber;
            PageSize = pageSize;
            AddRange (items);
        }
        public static async Task<PageList<T>> CreateAsync (IQueryable<T> soures, int pageSize, int pageNumber) {
            var count = await soures.CountAsync ();
            var items = await soures.Skip ((pageNumber - 1) * pageSize).Take (pageSize).ToListAsync ();
            return new PageList<T> (items, count, pageNumber, pageSize);
        }
    }
}