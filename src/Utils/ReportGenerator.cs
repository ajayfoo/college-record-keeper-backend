namespace CRK.Utils;

using CRK.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

class ReportGenerator
{
    internal static byte[] Generate(Student student)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        string name = student.FirstName + ' ' + student.LastName;
        return Document
            .Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Inch);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(14));

                    page.Header().Text(name).SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .Column(x =>
                        {
                            x.Spacing(1);
                            x.Item()
                                .Text(
                                    $@"
																CET Percentile: {student.CetPercentile}
																HSC Percentage: {student.HscPercentage}
																SSC Percentage: {student.SscPercentage}
																"
                                );
                        });
                });
            })
            .GeneratePdf();
    }
}
