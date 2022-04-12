using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublisherConsole;
using PublisherData;
using PublisherDomain;
using System;
using System.Collections.Generic;

namespace PubAppTest
{
    [TestClass]
    public class InMemoryTests
    {
        [TestMethod]
        public void CanInsertAuthorIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder<PubContext>();
            builder.UseInMemoryDatabase(
                Guid.NewGuid().ToString());

            using (var context = new PubContext(builder.Options))
            {
                var author = new Author { FirstName = "a", LastName = "b" };
                context.Authors.Add(author);

                Assert.AreEqual(EntityState.Added, context.Entry(author).State);
            }
        }

        [TestMethod]
        public void InsertAuthorsReturnsCorrectResultNumber()
        {
            var builder = new DbContextOptionsBuilder<PubContext>();
            builder.UseInMemoryDatabase(
                "InsertAuthorsReturnsCorrectResultNumber");
            var authorList = new Dictionary<string, string>
                { { "a" , "b" },
                  { "c" , "d" },
                  { "d" , "e" }
                };

            var dl = new DataLogic(new PubContext(builder.Options));
            var result = dl.ImportAuthors(authorList);
            Assert.AreEqual(authorList.Count, result);
        }
    }
}
