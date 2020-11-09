using System;
namespace MicroserviceShoppingCart.RemoteModels
{
    public class BookRemote
    {
        public Guid BookID { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public Guid? AuthorGuid { get; set; }
    }
}
