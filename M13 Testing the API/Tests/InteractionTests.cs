using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PubAPI;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class InteractionTests
    {
        private readonly WebApplicationFactory<Program> _factory;

        public InteractionTests()
        {
            _factory = new CustomWebApplicationFactory<Program>();
        }

        [TestMethod]
        public async Task GetEndpointReturnsSuccessAndSomeDataFromTheDatabase()
        {
            // Arrange
            var client = _factory.CreateClient();
           
            // Act
            var response = await client.GetAsync("/api/authors");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObjectList = JsonSerializer.Deserialize<List<AuthorDTO>>(responseString);
            // Assert
            Assert.AreNotEqual(0, responseObjectList.Count);
        }
    }

}