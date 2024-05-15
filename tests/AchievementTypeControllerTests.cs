namespace CRK.Tests;

using System.Net.Http.Json;
using System.Text.Json;
using CRK.Models;
using Microsoft.AspNetCore.Mvc.Testing;

// all the rows in the table must be deleted after each test run
public class AchievementTypeControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    readonly string uriStr = "/achievementtype";
    private readonly WebApplicationFactory<Program> _factory;
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { new AchievementType() { Label = "Sports" } },
            new object[] { new AchievementType() { Label = "Extra Curricular" } },
        };

    public AchievementTypeControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [MemberData(nameof(Data))]
    public async void OnPost_NewAchievementTypeDataMustBeAdded(AchievementType achievementType)
    {
        var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync(uriStr, achievementType);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async void OnGet_ExpectedAchievementTypeMustBeRetured()
    {
        var client = _factory.CreateClient();
        Guid id = Guid.NewGuid();
        AchievementType expectedAchievementType = new AchievementType() { Label = "Co-curricular" };
        var postResponse = await client.PostAsJsonAsync(uriStr, expectedAchievementType);
        var responseAchievementTypeStream = await postResponse.Content.ReadAsStreamAsync();
        var responseAchievementType = await JsonSerializer.DeserializeAsync<AchievementType>(
            responseAchievementTypeStream,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
        );
        if (responseAchievementType == null)
        {
            Assert.Fail();
        }
        var response = await client.GetAsync(uriStr + "/" + responseAchievementType.Id.ToString());
        response.EnsureSuccessStatusCode();
        Assert.Equivalent(responseAchievementType, expectedAchievementType, strict: true);
    }
}
