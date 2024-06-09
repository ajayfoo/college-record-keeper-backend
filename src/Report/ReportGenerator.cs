using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace CRK.Report;

internal static class ReportGenerator
{
    internal static byte[] GenerateStudentReport()
    {
        QuestPDF.Settings.License = LicenseType.Community;
        var model = ReportDataSource.GetReportDetails();
        var document = new ReportDocument(model);
        return document.GeneratePdf();
    }
}
