using System;
using AutoMapper;
using MicroserviceAuthor.Models;

namespace MicroserviceAuthor.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>();
        }
    }
}
