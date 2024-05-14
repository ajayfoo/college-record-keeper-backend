namespace CRK.Tests;

using System.Net.Http.Json;
using CRK.Models;
using Microsoft.AspNetCore.Mvc.Testing;

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
                    Dob = DateTime.Parse("2001-10-12"),
                    YearOfAdmission = DateTime.Parse("2022-2-20")
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
                    Dob = DateTime.Parse("999-10-12"),
                    YearOfAdmission = DateTime.Today
                }
            },
        };

    public StudentControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [MemberData(nameof(Data))]
    public async void OnPost_NewStudentDataMustBeAdded(Student student)
    {
        var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/studen", student);
        response.EnsureSuccessStatusCode();
    }
}
