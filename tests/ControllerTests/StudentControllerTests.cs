using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CRK.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CRK.Tests.ControllerTests;

// all the rows in the table must be deleted after each test run
public class StudentControllerTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();
    private readonly JsonSerializerOptions jsonSerializerOptions =
        new() { PropertyNameCaseInsensitive = true };
    public static TheoryData<Student> Data =>
        [
            new()
            {
                FirstName = "Jill",
                MiddleName = "James",
                LastName = "Joe",
                CetPercentile = 95.999,
                HscPercentage = 69.12,
                SscPercentage = 76.23,
                Dob = DateTime.Parse("2001-10-12", CultureInfo.InvariantCulture),
                YearOfAdmission = DateTime.Parse("2022-2-20", CultureInfo.InvariantCulture),
            },
            new()
            {
                FirstName = "Jack",
                LastName = "Sparrow",
                CetPercentile = 75.3134,
                HscPercentage = 39.39,
                SscPercentage = 66.77,
                Dob = DateTime.Parse("999-10-12", CultureInfo.InvariantCulture),
                YearOfAdmission = DateTime.Today,
            }
        ];

    [Theory]
    [MemberData(nameof(Data))]
    public async Task OnGetExpectedStudentMustBeRetured(Student student)
    {
        HttpResponseMessage postResponse = await _client.PostAsJsonAsync("/student", student);
        Assert.StrictEqual(HttpStatusCode.Created, postResponse.StatusCode);
        Stream responseStudentStream = await postResponse.Content.ReadAsStreamAsync();
        Student? responseStudent = await JsonSerializer.DeserializeAsync<Student>(
            responseStudentStream,
            jsonSerializerOptions
        );
        if (responseStudent == null)
        {
            Assert.Fail();
        }
        HttpResponseMessage response = await _client.GetAsync(
            "/student/" + responseStudent.Id.ToString()
        );
        Assert.StrictEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.Equivalent(responseStudent, student, strict: true);
    }
}
