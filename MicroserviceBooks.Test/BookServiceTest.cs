using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GenFu;
using MicroserviceBooks.Application;
using MicroserviceBooks.Models;
using MicroserviceBooks.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace MicroserviceBooks.Test
{
    public class BookServiceTest
    {
        private IEnumerable<Book> RepositoryTest()
        {
            A.Configure<Book>()
                .Fill(x => x.Title).AsArticleTitle()
                .Fill(x => x.BookID, () => { return new Guid(); });

            var list = A.ListOf<Book>(30);
            list[0].BookID = Guid.Empty;
            return list;
        }

        private Mock<DbContextBook> CreateContext()
        {
            var repository = RepositoryTest().AsQueryable();

            var dbSet = new Mock<DbSet<Book>>();
            dbSet.As<IQueryable<Book>>().Setup(x => x.Provider).Returns(repository.Provider);
            dbSet.As<IQueryable<Book>>().Setup(x => x.Expression).Returns(repository.Expression);
            dbSet.As<IQueryable<Book>>().Setup(x => x.ElementType).Returns(repository.ElementType);
            dbSet.As<IQueryable<Book>>().Setup(x => x.GetEnumerator()).Returns(repository.GetEnumerator());

            dbSet.As<IAsyncEnumerable<Book>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<Book>(repository.GetEnumerator()));

            dbSet.As<IQueryable<Book>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<Book>(repository.Provider))  ;

            var context = new Mock<DbContextBook>();
            context.Setup(x => x.Books).Returns(dbSet.Object);

            return context;
        }

        [Fact]
        public async void GetBookById()
        {
            //System.Diagnostics.Debugger.Launch();

            var mookContext = CreateContext();

            var mookMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mookMapper.CreateMapper();

            QueryFilter.Driver driver = new QueryFilter.Driver(mookContext.Object, mapper);

            QueryFilter.BookRequest request = new QueryFilter.BookRequest { BookID = Guid.Empty};

            var obj = await driver.Handle(request,new System.Threading.CancellationToken());

            Assert.NotNull(obj);
            Assert.True(obj.BookID.Equals(Guid.Empty));
        }

        [Fact]
        public async void GetBooks()
        {
            //System.Diagnostics.Debugger.Launch();

            var mookContext = CreateContext();

            var mookMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mookMapper.CreateMapper();

            Query.Driver driver = new Query.Driver(mookContext.Object, mapper);

            Query.BookRequest request = new Query.BookRequest();

            var list = await driver.Handle(request, new System.Threading.CancellationToken());


            Assert.True(list.Any());
        }

        [Fact]
        public async void SaveBook()
        {
            System.Diagnostics.Debugger.Launch();

            var options = new DbContextOptionsBuilder<DbContextBook>()
                .UseInMemoryDatabase(databaseName: "db_books_test")
                .Options;

            var context = new DbContextBook(options);


            New.RequestBook request = new New.RequestBook
            {
                Title = "Microservices Book",
                PublicationDate = DateTime.Now,
                AuthorGuid = Guid.Empty
            };

            New.Driver driver = new New.Driver(context);


            var result = await driver.Handle(request, new System.Threading.CancellationToken());

            Assert.True(result != null);
            
        }

    }
}
