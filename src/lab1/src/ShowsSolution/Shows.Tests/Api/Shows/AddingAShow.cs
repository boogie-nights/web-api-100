using Alba;
using Shows.Api.Shows;
using Shows.Tests.Api.Fixtures;

namespace Shows.Tests.Api.Shows;

[Collection("SystemTestFixture")]
[Trait("Category", "SystemTest")]
public class AddingAShow(SystemTestFixture fixture)
{
    private readonly IAlbaHost _host = fixture.Host;

    [Fact]
    public async Task AddShowReturnsShowDetailsModel()
    {

        var host = _host;

        var showToAdd = new ShowCreateModel
        {
            Name = "Buffy The Vampire Slayer",
            Description = "A teenage girl inherits the powers of an ancient being called the Slayer. She and her gang of silly goofballs must take on all manner of evil doers to prevent a never ending list of apocalypses.",
            StreamingService = "Hulu"
        };

        var postResponse = await _host.Scenario(_ =>
        {
            _.Post.Json(showToAdd).ToUrl("/api/shows");
            _.StatusCodeShouldBe(201);

        });

        var postEntityReturned = postResponse.ReadAsJson<ShowDetailsModel>();

        Assert.NotNull(postEntityReturned);

        Assert.True(postEntityReturned.Id != Guid.Empty);
        Assert.True(postEntityReturned.CreatedAt != DateTime.MinValue);

        Assert.Equal(postEntityReturned.Name, showToAdd.Name);
        Assert.Equal(postEntityReturned.Description, showToAdd.Description);
        Assert.Equal(postEntityReturned.StreamingService, showToAdd.StreamingService);
    }

    [Fact]
    public async Task AddShowGrabsDataFromDatabase()
    {

        var host = _host;

        var showToAdd = new ShowCreateModel
        {
            Name = "Buffy The Vampire Slayer",
            Description = "A teenage girl inherits the powers of an ancient being called the Slayer. She and her gang of silly goofballs must take on all manner of evil doers to prevent a never ending list of apocalypses.",
            StreamingService = "Hulu"
        };

        var postResponse = await _host.Scenario(_ =>
        {
            _.Post.Json(showToAdd).ToUrl("/api/shows");
            _.StatusCodeShouldBe(201);

        });

        var postEntityReturned = postResponse.ReadAsJson<ShowDetailsModel>();

        Assert.NotNull(postEntityReturned);

        Assert.True(postEntityReturned.Id != Guid.Empty);
        Assert.True(postEntityReturned.CreatedAt != DateTime.MinValue);

        Assert.Equal(postEntityReturned.Name, showToAdd.Name);
        Assert.Equal(postEntityReturned.Description, showToAdd.Description);
        Assert.Equal(postEntityReturned.StreamingService, showToAdd.StreamingService);


        var getResponse = await host.Scenario(_ =>
        {
            _.Get.Url($"/api/shows/{postEntityReturned.Id}");
            _.StatusCodeShouldBeOk();
        });

        var getEntityReturned = getResponse.ReadAsJson<ShowDetailsModel>();

        Assert.NotNull(getEntityReturned);
        Assert.Equal(postEntityReturned.Id, getEntityReturned.Id);
        Assert.Equal(postEntityReturned.CreatedAt, getEntityReturned.CreatedAt);
        Assert.Equal(postEntityReturned.Name, getEntityReturned.Name);
        Assert.Equal(postEntityReturned.Description, getEntityReturned.Description);
        Assert.Equal(postEntityReturned.StreamingService, getEntityReturned.StreamingService);
    }

    [Fact]
    public async Task GetShowsWillReturnAllShows()
    {

        var host = _host;

        var showToAdd = new ShowCreateModel
        {
            Name = "Buffy The Vampire Slayer",
            Description = "A teenage girl inherits the powers of an ancient being called the Slayer. She and her gang of silly goofballs must take on all manner of evil doers to prevent a never ending list of apocalypses.",
            StreamingService = "Hulu"
        };

        var secondShowToAdd = new ShowCreateModel
        {
            Name = "Angel",
            Description = "David Boreanas plays David Boreanas in this sequel spinoff of Buffy the Vampire Slayer. This show plays fast and loose with established Buffy 'lore' and devolves into an absolute mess.",
            StreamingService = "Hulu"
        };

        // Add Buffy
        var postResponse = await _host.Scenario(_ =>
        {
            _.Post.Json(showToAdd).ToUrl("/api/shows");
            _.StatusCodeShouldBe(201);
        });

        var postEntityReturned = postResponse.ReadAsJson<ShowDetailsModel>();

        Assert.NotNull(postEntityReturned);

        Assert.True(postEntityReturned.Id != Guid.Empty);
        Assert.True(postEntityReturned.CreatedAt != DateTime.MinValue);

        Assert.Equal(postEntityReturned.Name, showToAdd.Name);
        Assert.Equal(postEntityReturned.Description, showToAdd.Description);
        Assert.Equal(postEntityReturned.StreamingService, showToAdd.StreamingService);

        var getResponse = await host.Scenario(_ =>
        {
            _.Get.Url($"/api/shows/{postEntityReturned.Id}");
            _.StatusCodeShouldBeOk();
        });

        var getEntityReturned = getResponse.ReadAsJson<ShowDetailsModel>();

        Assert.NotNull(getEntityReturned);
        Assert.Equal(postEntityReturned.Id, getEntityReturned.Id);
        Assert.Equal(postEntityReturned.CreatedAt, getEntityReturned.CreatedAt);
        Assert.Equal(postEntityReturned.Name, getEntityReturned.Name);
        Assert.Equal(postEntityReturned.Description, getEntityReturned.Description);
        Assert.Equal(postEntityReturned.StreamingService, getEntityReturned.StreamingService);

        // Add Angel
        var secondPostResponse = await _host.Scenario(_ =>
        {
            _.Post.Json(secondShowToAdd).ToUrl("/api/shows");
            _.StatusCodeShouldBe(201);
        });

        var secondPostEntityReturned = secondPostResponse.ReadAsJson<ShowDetailsModel>();

        Assert.NotNull(secondPostEntityReturned);

        Assert.True(secondPostEntityReturned.Id != Guid.Empty);
        Assert.True(secondPostEntityReturned.CreatedAt != DateTime.MinValue);

        Assert.Equal(secondPostEntityReturned.Name, secondShowToAdd.Name);
        Assert.Equal(secondPostEntityReturned.Description, secondShowToAdd.Description);
        Assert.Equal(secondPostEntityReturned.StreamingService, secondShowToAdd.StreamingService);

        var secondGetResponse = await host.Scenario(_ =>
        {
            _.Get.Url($"/api/shows/{secondPostEntityReturned.Id}");
            _.StatusCodeShouldBeOk();
        });

        var secondGetEntityReturned = secondGetResponse.ReadAsJson<ShowDetailsModel>();

        Assert.NotNull(secondGetEntityReturned);
        Assert.Equal(secondPostEntityReturned.Id, secondGetEntityReturned.Id);
        Assert.Equal(secondPostEntityReturned.CreatedAt, secondGetEntityReturned.CreatedAt);
        Assert.Equal(secondPostEntityReturned.Name, secondGetEntityReturned.Name);
        Assert.Equal(secondPostEntityReturned.Description, secondGetEntityReturned.Description);
        Assert.Equal(secondPostEntityReturned.StreamingService, secondGetEntityReturned.StreamingService);
    }
}