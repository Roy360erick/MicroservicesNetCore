using System;
using System.Collections.Generic;

namespace MicroserviceShoppingCart.Models
{
    public class SessionCart
    {
        public int SessionCartID { get; set; }
        public DateTime? CreateAt { get; set; }
        public virtual ICollection<SessionCartDetail> SessionCartDetails { get; set; }
    }
}
