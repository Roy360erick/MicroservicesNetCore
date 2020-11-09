using System;
namespace MicroserviceShoppingCart.Models
{
    public class SessionCartDetail
    {
        public int SessionCartDetailID { get; set; }
        public DateTime CreateAt { get; set; }
        public string SelecctedItemID { get; set; }
        public int SessionCartID { get; set; }
        public virtual SessionCart SessionCart { get; set; }
    }
}
