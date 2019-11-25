using System;
using Xunit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.UnitTests
{
    public class TodoRepositoryShould
    {
        [Fact]
        public async Task GetExistingItem()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_GetExistingItem").Options;

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new TodoRepository(context);
                var item = new TodoItem
                {
                    Name = "Fake Item"
                };

                await repository.AddAsync(item);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new TodoRepository(context);
                var item = await repository.GetAsync(1);

                Assert.Equal(1, item.Id);
                Assert.Equal("Fake Item", item.Name);
            }
        }
    }
}
