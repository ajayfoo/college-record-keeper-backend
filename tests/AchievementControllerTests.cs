namespace CRK.Tests;

using System.Net.Http.Json;
using System.Text.Json;
using CRK.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

// all the rows in the table must be deleted after each test run
public class AchievementControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly ITestOutputHelper _output;
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Guid _achievementTypeId = Guid.Parse("2f2d6239-2e1c-4b39-8660-8eb76c2303ec");
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[]
            {
                new Achievement()
                {
                    Name = "Mr. Fresher",
                    Prize = "Goodies",
                    Level = "College",
                    AchievementTypeId = Guid.Parse("2f2d6239-2e1c-4b39-8660-8eb76c2303ec"),
                    Year = DateTime.Parse("2022-2-20").ToUniversalTime()
                }
            },
            new object[]
            {
                new Achievement()
                {
                    Name = "World Chess Champion",
                    Prize = "Gold Medal",
                    Level = "International",
                    AchievementTypeId = Guid.Parse("2f2d6239-2e1c-4b39-8660-8eb76c2303ec"),
                    Year = DateTime.Parse("2022-9-10").ToUniversalTime()
                }
            },
        };

    public AchievementControllerTests(
        WebApplicationFactory<Program> factory,
        ITestOutputHelper outputHelper
    )
    {
        _factory = factory;
        _output = outputHelper;
    }

    [Theory(Skip = "WIP")]
    [MemberData(nameof(Data))]
    public async Task OnPost_NewAchievementDataMustBeAdded(Achievement achievement)
    {
        var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/achievement", achievement);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task OnGet_ExpectedAchievementMustBeRetured()
    {
        var client = _factory.CreateClient();
        AchievementType achievementType = new AchievementType()
        {
            Label = "Sports",
            Id = Guid.Parse("2f2d6239-2e1c-4b39-8660-8eb76c2303ec"),
        };
        var achievementTypePostResponse = await client.PostAsJsonAsync(
            "/achievementtype",
            achievementType
        );
        achievementTypePostResponse.EnsureSuccessStatusCode();
        Achievement expectedAchievement = new Achievement()
        {
            Name = "World Chess Champion",
            Prize = "Gold Medal",
            AchievementTypeId = Guid.Parse("2f2d6239-2e1c-4b39-8660-8eb76c2303ec"),
            Level = "International",
            Year = DateTime.Parse("2022-9-10").ToUniversalTime(),
        };
        var postResponse = await client.PostAsJsonAsync("/achievement", expectedAchievement);
        string content = await postResponse.Content.ReadAsStringAsync();
        _output.WriteLine(content);
        postResponse.EnsureSuccessStatusCode();
        var responseAchievementStream = await postResponse.Content.ReadAsStreamAsync();
        var responseAchievement = await JsonSerializer.DeserializeAsync<Achievement>(
            responseAchievementStream,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
        );
        if (responseAchievement == null)
        {
            Assert.Fail();
        }
        var response = await client.GetAsync("/achievement/" + responseAchievement.Id.ToString());
        response.EnsureSuccessStatusCode();
        Assert.Equivalent(responseAchievement, expectedAchievement, strict: true);
    }
}