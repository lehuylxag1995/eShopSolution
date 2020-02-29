using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.DTOs
{
    public class PageResult<T>
    {
        public List<T> Items;

        public int TotalPage;
    }
}
