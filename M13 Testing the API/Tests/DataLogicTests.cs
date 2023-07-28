using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PubAPI;
using PublisherData;
using PublisherDomain;
using System;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class DataLogicTests
    {
        [TestMethod]
        public void CanGetAnAuthorById()
        {
            //Arrange (set up builder & seed data)
            var builder = new DbContextOptionsBuilder<PubContext>();
            builder.UseInMemoryDatabase("CanGetAnAuthorById");
            int seededId = SeedOneAuthor(builder.Options);
            //Act (call the method)
            using (var context = new PubContext(builder.Options))
            {
                var bizlogic = new DataLogic(context);
                //When working in EFCore7, I moved Result ot here not in assertion
                var authorRetrieved = bizlogic.GetAuthorById(seededId).Result;
                //Assert (check the results)
                Assert.AreEqual(seededId, authorRetrieved.AuthorId);
            };
        }
        private int SeedOneAuthor(DbContextOptions<PubContext> options)
        {
            using (var seedcontext = new PubContext(options))
            {
                var author = new Author { FirstName="a", LastName="b"};
                seedcontext.Authors.Add(author);
                // savechanges seems to be needed now for InMemory with EFCore7. I can't find issue in GitHub about this
                //also note, I may remove use if InMemory for EFCore8 update to course because team is now more
                //adamant about saying it's NOT RECOMMENDED
                seedcontext.SaveChanges();
                return author.AuthorId;
            }
        }
    }
}