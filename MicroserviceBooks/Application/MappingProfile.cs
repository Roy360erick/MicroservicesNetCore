using System;
using AutoMapper;
using MicroserviceBooks.Models;

namespace MicroserviceBooks.Application
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
        }
    }
}
