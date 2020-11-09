using System;
namespace MicroserviceAuthor.Application
{
    public class AuthorDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string AuthorGuid { get; set; }
    }
}
