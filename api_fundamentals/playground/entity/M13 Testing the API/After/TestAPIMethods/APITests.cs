using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PubAPI;
using PublisherData;
using System.Net;
using System.Net.Http.Json;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace TestAPIMethods;

[TestClass]
public partial class APITests
{
 
    [TestMethod]
    public async Task ApiIsRunning()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var response = await client.GetStringAsync("/weatherforecast");
        Assert.AreEqual("[{\"date", response.Substring(0, 7));
    }

    //NOTE: there is a known whoopsie in EF9 around using the webApplicationFactory to
    //replace the DbContext instance using one database with another (SQLite in this case).
    //the team is aware. Here is the github issue: https://github.com/dotnet/efcore/issues/35126

    //This means that the two following tests will fail because the test thinks
    //that I have two instances of the dbcontext pointing to two different databases.
    //This is the same code from EF8 which still works as advertised.

    [TestMethod]
    public async Task CanRetrieveAnAuthorDTO()
    {   
        await using var application = new CustomWebApplicationFactory<Program>();
         CreateAndSeedDatabase(application);
        using var client = application.CreateClient();
        var authorDTO = await client.GetFromJsonAsync<AuthorDTO>("/api/author/1");
        Assert.IsInstanceOfType(authorDTO, typeof(AuthorDTO));
    }

    [TestMethod]
    public async Task CanInsertAnAuthor()
    {
        var authorDTO = new AuthorDTO(0, "John", "Doe");
        await using var application = new CustomWebApplicationFactory<Program>();
        CreateAndSeedDatabase(application);
        using var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/api/author/", authorDTO);
        var author = await response.Content.ReadFromJsonAsync<AuthorDTO>();
        Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        Assert.AreNotEqual(0, author.AuthorId);
    }

    private static void CreateAndSeedDatabase(WebApplicationFactory<Program> appFactory)
    {
    
        using (var scope = appFactory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<PubContext>();
            db.Database.EnsureCreated();
         }

    }
}
