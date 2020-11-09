using System;
namespace MicroserviceShoppingCart.Application
{
    public class ShoppingCartDetailDto
    {
        public Guid? BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime? PublicationDate { get; set; }
    }
}
