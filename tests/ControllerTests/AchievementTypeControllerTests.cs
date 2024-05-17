using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CRK.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CRK.Tests.ControllerTests;

// all the rows in the table must be deleted after each test run
public class AchievementTypeControllerTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();
    private readonly string uriStr = "/achievementtype";
    private readonly JsonSerializerOptions jsonSerializerOptions =
        new() { PropertyNameCaseInsensitive = true };
    public static TheoryData<AchievementType> Data =>
        [new() { Label = "Sports" }, new() { Label = "Extra Curricular" },];

    [Theory]
    [MemberData(nameof(Data))]
    public async Task OnGetExpectedAchievementTypeMustBeRetured(AchievementType achievementType)
    {
        HttpResponseMessage postResponse = await _client.PostAsJsonAsync(uriStr, achievementType);
        Assert.StrictEqual(HttpStatusCode.Created, postResponse.StatusCode);
        Stream responseAchievementTypeStream = await postResponse.Content.ReadAsStreamAsync();
        AchievementType? responseAchievementType =
            await JsonSerializer.DeserializeAsync<AchievementType>(
                responseAchievementTypeStream,
                jsonSerializerOptions
            );
        if (responseAchievementType == null)
        {
            Assert.Fail();
        }
        HttpResponseMessage response = await _client.GetAsync(
            uriStr + "/" + responseAchievementType.Id.ToString()
        );
        Assert.StrictEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.Equivalent(responseAchievementType, achievementType, strict: true);
    }
}
