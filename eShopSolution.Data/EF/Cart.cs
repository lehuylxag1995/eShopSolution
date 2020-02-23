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
        public DateTime DateCreated { get; set; }
        public int ProductId { set; get; }
        public Product Product { get; set; }
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
