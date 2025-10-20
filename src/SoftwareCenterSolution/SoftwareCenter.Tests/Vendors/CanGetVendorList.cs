using Alba;

using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace SoftwareCenter.Tests.Vendors;

// https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-9.0&pivots=xunit
public class CanGetVendorList
{

    [Trait("Category", "System")]
    public async Task GivesASuccessStatusCodeAsync()
    {
        //var client = new HttpClient();
        //client.BaseAddress = new Uri("http://localhost:1137");

        //var response = await client.GetAsync("/vendors");

        //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GettingAllVendorsAsync()
    {
        // start ujp the api using my program.cs in memory
        var host = await AlbaHost.For<Program>();

        // Here's the scenario for the test
        await host.Scenario(api =>
        {
            // Get the vendors (no host or anything, it's internal)
            api.Get.Url("/vendors");
            // if it isn't this, fail.
            api.StatusCodeShouldBeOk();
        });
    }
}
