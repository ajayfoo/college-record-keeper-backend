using CRK.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace CRK.Report;

internal static class ReportGenerator
{
    internal static byte[] GenerateStudentReport(Student student)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        ReportModel model = new() { Student = student };
        var document = new ReportDocument(model);
        return document.GeneratePdf();
    }
}
