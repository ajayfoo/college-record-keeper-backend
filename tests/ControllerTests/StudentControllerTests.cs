namespace CRK.Tests;

using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using CRK.Models;
using Microsoft.AspNetCore.Mvc.Testing;

// all the rows in the table must be deleted after each test run
public class StudentControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[]
            {
                new Student()
                {
                    FirstName = "Jill",
                    MiddleName = "James",
                    LastName = "Joe",
                    CetPercentile = 95.999,
                    HscPercentage = 69.12,
                    SscPercentage = 76.23,
                    Dob = DateTime.Parse("2001-10-12").ToUniversalTime(),
                    YearOfAdmission = DateTime.Parse("2022-2-20").ToUniversalTime(),
                }
            },
            new object[]
            {
                new Student()
                {
                    FirstName = "Jack",
                    LastName = "Sparrow",
                    CetPercentile = 75.3134,
                    HscPercentage = 39.39,
                    SscPercentage = 66.77,
                    Dob = DateTime.Parse("999-10-12").ToUniversalTime(),
                    YearOfAdmission = DateTime.Today.ToUniversalTime()
                }
            },
        };

    public StudentControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [MemberData(nameof(Data))]
    public async Task OnGet_ExpectedStudentMustBeRetured(Student student)
    {
        var client = _factory.CreateClient();
        var postResponse = await client.PostAsJsonAsync("/student", student);
        postResponse.EnsureSuccessStatusCode();
        var responseStudentStream = await postResponse.Content.ReadAsStreamAsync();
        var responseStudent = await JsonSerializer.DeserializeAsync<Student>(
            responseStudentStream,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
        );
        if (responseStudent == null)
        {
            Assert.Fail();
        }
        var response = await client.GetAsync("/student/" + responseStudent.Id.ToString());
        response.EnsureSuccessStatusCode();
        Assert.Equivalent(responseStudent, student, strict: true);
    }
}
