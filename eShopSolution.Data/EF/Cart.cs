using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.EF
{
    public class Cart
    {
        public int Id { set; get; }
        
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }

        //1 giỏ hàng có nhiều sản phẩm
        public int ProductId { set; get; }
        public Product Product { get; set; }
    }
}
