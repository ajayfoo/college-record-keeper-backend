using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CRK.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CRK.Tests.ControllerTests;

// all the rows in the table must be deleted after each test run
public class CompanyControllerTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();
    private readonly JsonSerializerOptions jsonSerializerOptions =
        new() { PropertyNameCaseInsensitive = true };
    public static TheoryData<Company> Data =>
        [
            new()
            {
                Name = "L&T",
                MiniumSalary = 50000,
                MaximumSalary = 90000,
                Year = 2022,
            },
            new()
            {
                Name = "Google",
                MiniumSalary = 90000,
                MaximumSalary = 120000,
                Year = 2023,
            }
        ];

    [Theory]
    [MemberData(nameof(Data))]
    public async Task OnGetExpectedCompanyMustBeRetured(Company company)
    {
        HttpResponseMessage postResponse = await _client.PostAsJsonAsync("/company", company);
        Assert.StrictEqual(HttpStatusCode.Created, postResponse.StatusCode);
        Stream responseCompanyStream = await postResponse.Content.ReadAsStreamAsync();
        Company? responseCompany = await JsonSerializer.DeserializeAsync<Company>(
            responseCompanyStream,
            jsonSerializerOptions
        );
        if (responseCompany == null)
        {
            Assert.Fail();
        }
        HttpResponseMessage response = await _client.GetAsync(
            "/company/" + responseCompany.Id.ToString()
        );
        Assert.StrictEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.Equivalent(responseCompany, company, strict: true);
    }
}
