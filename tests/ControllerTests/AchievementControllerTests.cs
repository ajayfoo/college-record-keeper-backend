using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CRK.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CRK.Tests.ControllerTests;

// all the rows in the table must be deleted after each test run
public class AchievementControllerTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();
    private readonly JsonSerializerOptions jsonSerializerOptions =
        new() { PropertyNameCaseInsensitive = true };
    public static TheoryData<Achievement> Data =>
        [
            new()
            {
                Name = "Mr. Fresher",
                Prize = "Goodies",
                Level = "College",
                AchievementTypeId = Guid.Parse("2f2d6239-2e1c-4b39-8660-8eb76c2303ec"),
                Year = DateTime.Parse("2022-2-20", CultureInfo.InvariantCulture),
            },
            new()
            {
                Name = "World Chess Champion",
                Prize = "Gold Medal",
                Level = "International",
                AchievementTypeId = Guid.Parse("2f2d6239-2e1c-4b39-8660-8eb76c2303ec"),
                Year = DateTime.Parse("2022-9-10", CultureInfo.InvariantCulture),
            }
        ];

    [Theory(Skip = "WIP")]
    [MemberData(nameof(Data))]
    public async Task OnGetExpectedAchievementMustBeRetured(Achievement achievement)
    {
        AchievementType achievementType =
            new() { Label = "Sports", Id = Guid.Parse("2f2d6239-2e1c-4b39-8660-8eb76c2303ec"), };
        HttpResponseMessage achievementTypePostResponse = await _client.PostAsJsonAsync(
            "/achievementtype",
            achievementType
        );
        Assert.StrictEqual(HttpStatusCode.Created, achievementTypePostResponse.StatusCode);
        HttpResponseMessage postResponse = await _client.PostAsJsonAsync(
            "/achievement",
            achievement
        );
        Assert.StrictEqual(HttpStatusCode.Created, postResponse.StatusCode);
        Stream responseAchievementStream = await postResponse.Content.ReadAsStreamAsync();
        Achievement? responseAchievement = await JsonSerializer.DeserializeAsync<Achievement>(
            responseAchievementStream,
            jsonSerializerOptions
        );
        if (responseAchievement == null)
        {
            Assert.Fail();
        }
        HttpResponseMessage response = await _client.GetAsync(
            "/achievement/" + responseAchievement.Id.ToString()
        );
        Assert.StrictEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.Equivalent(responseAchievement, achievement, strict: true);
    }
}
