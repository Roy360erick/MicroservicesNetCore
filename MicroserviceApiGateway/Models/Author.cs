using System;

namespace MicroserviceApiGateway.Models
{
    public class Author
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string AuthorGuid { get; set; }
    }
}
