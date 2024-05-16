namespace CRK.Tests;

using System.Net.Http.Json;
using System.Text.Json;
using CRK.Models;
using Microsoft.AspNetCore.Mvc.Testing;

// all the rows in the table must be deleted after each test run
public class CompanyControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[]
            {
                new Company()
                {
                    Name = "L&T",
                    MiniumSalary = 50000,
                    MaximumSalary = 90000,
                    Year = 2022,
                }
            },
            new object[]
            {
                new Company()
                {
                    Name = "Google",
                    MiniumSalary = 90000,
                    MaximumSalary = 120000,
                    Year = 2023,
                }
            },
        };

    public CompanyControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [MemberData(nameof(Data))]
    public async Task OnGet_ExpectedCompanyMustBeRetured(Company company)
    {
        var client = _factory.CreateClient();
        var postResponse = await client.PostAsJsonAsync("/company", company);
        postResponse.EnsureSuccessStatusCode();
        var responseCompanyStream = await postResponse.Content.ReadAsStreamAsync();
        var responseCompany = await JsonSerializer.DeserializeAsync<Company>(
            responseCompanyStream,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
        );
        if (responseCompany == null)
        {
            Assert.Fail();
        }
        var response = await client.GetAsync("/company/" + responseCompany.Id.ToString());
        response.EnsureSuccessStatusCode();
        Assert.Equivalent(responseCompany, company, strict: true);
    }
}
