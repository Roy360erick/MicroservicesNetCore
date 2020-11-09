using System;
using AutoMapper;
using MicroserviceBooks.Application;
using MicroserviceBooks.Models;

namespace MicroserviceBooks.Test
{
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<Book, BookDto>();
        }
    }
}
