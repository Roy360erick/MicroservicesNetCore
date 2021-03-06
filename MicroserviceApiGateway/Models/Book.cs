﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceApiGateway.Models
{
    public class Book
    {
        public Guid BookID { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public Guid? AuthorGuid { get; set; }
        public Author Author { get; set; }
    }
}
