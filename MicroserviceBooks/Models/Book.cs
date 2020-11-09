using System;
namespace MicroserviceBooks.Models
{
    public class Book
    {
        public Guid BookID { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public Guid? AuthorGuid { get; set; }
    }
}
