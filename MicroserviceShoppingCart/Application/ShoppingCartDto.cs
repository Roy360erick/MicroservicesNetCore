using System;
using System.Collections.Generic;

namespace MicroserviceShoppingCart.Application
{
    public class ShoppingCartDto
    {
        public int ShoppingCartID { get; set; }
        public DateTime? CreateDate { get; set; }
        public List<ShoppingCartDetailDto> shoppingCartDetails { get; set; }
    }
}
