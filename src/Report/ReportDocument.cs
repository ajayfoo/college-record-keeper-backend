using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CRK.Report;

public class ReportDocument(ReportModel model) : IDocument
{
    public ReportModel Model { get; } = model;

    public DocumentMetadata GetMetadata()
    {
        return DocumentMetadata.Default;
    }

    public DocumentSettings GetSettings()
    {
        return DocumentSettings.Default;
    }

    public void Compose(IDocumentContainer container)
    {
        _ = container.Page(page =>
        {
            page.Margin(50);

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
        });
    }

    private static string FormatDate(DateTime dateTime)
    {
        return dateTime.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
    }

    private void ComposeHeader(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

        container.Row(row =>
        {
            row.RelativeItem()
                .Column(column =>
                {
                    column
                        .Item()
                        .Text(t =>
                        {
                            _ = t.Span(
                                    Model.Student.FirstName
                                        + ' '
                                        + Model.Student.MiddleName
                                        + ' '
                                        + Model.Student.LastName
                                )
                                .Style(titleStyle);
                            _ = t.Span(" - ").FontSize(20).SemiBold();
                            _ = t.Span(Model.Student.YearOfAdmission + "").Style(titleStyle);
                        });
                    _ = column.Item().Text("Id: " + Model.Student.Id);
                });

            row.ConstantItem(80).Height(100).Placeholder();
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(col =>
        {
            col.Item()
                .Row(row =>
                {
                    row.RelativeItem()
                        .Column(column =>
                        {
                            column
                                .Item()
                                .BorderBottom(1)
                                .Text(text =>
                                {
                                    _ = text.Span("Bio").Bold().FontSize(18);
                                });
                            column.Spacing(5);
                            column
                                .Item()
                                .Text(text =>
                                {
                                    _ = text.Span("Date Of Birth: ").SemiBold();
                                    _ = text.Span(FormatDate(Model.Student.DateOfBirth));
                                });
                        });
                });
            col.Spacing(25);
            col.Item()
                .Row(row =>
                {
                    row.RelativeItem()
                        .Column(col =>
                        {
                            col.Item()
                                .BorderBottom(1)
                                .Text(t =>
                                {
                                    _ = t.Span("Academic").Bold().FontSize(18);
                                });
                            col.Spacing(5);
                            _ = col.Item().Text("CET Score: " + Model.Student.CetPercentile);
                            _ = col.Item().Text("HSC: " + Model.Student.HscPercentage);
                            _ = col.Item().Text("SSC: " + Model.Student.SscPercentage);
                            _ = col.Item().Text("Score: " + Model.Student.AcademicScore);
                        });
                    row.Spacing(25);
                    row.RelativeItem()
                        .Column(col =>
                        {
                            col.Item()
                                .BorderBottom(1)
                                .Text(t =>
                                {
                                    _ = t.Span("Placement").Bold().FontSize(18);
                                });
                            col.Spacing(5);
                            _ = col.Item()
                                .Text(
                                    "Status: "
                                        + (
                                            Model.Student.Employment.IsEmployed
                                                ? "Placed"
                                                : "Not placed"
                                        )
                                );

                            if (!Model.Student.Employment.IsEmployed)
                            {
                                return;
                            }
                            _ = col.Item()
                                .Text("Company: " + Model.Student.Employment.Company?.Name);
                            _ = col.Item().Text("Salary: " + Model.Student.Employment.Salary);
                            _ = col.Item()
                                .Text(
                                    "Tenure Start: "
                                        + FormatDate(
                                            Model.Student.Employment.TenureStart ?? DateTime.Now
                                        )
                                );
                            _ = col.Item()
                                .Text(
                                    "Tenure End: "
                                        + FormatDate(
                                            Model.Student.Employment.TenureEnd ?? DateTime.Now
                                        )
                                );
                        });
                });
            col.Item()
                .BorderBottom(1)
                .Text(t =>
                {
                    _ = t.Span("Achievement(s)").Bold().FontSize(18);
                });
            col.Item()
                .Border(1)
                .Table(tb =>
                {
                    tb.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });
                    _ = tb.Cell().Row(1).Column(1).Element(Heading).Text("#").SemiBold();
                    _ = tb.Cell().Row(1).Column(2).Element(Heading).Text("Name").SemiBold();
                    _ = tb.Cell().Row(1).Column(3).Element(Heading).Text("Type").SemiBold();
                    _ = tb.Cell().Row(1).Column(4).Element(Heading).Text("Level").SemiBold();
                    _ = tb.Cell().Row(1).Column(5).Element(Heading).Text("Prize").SemiBold();
                    _ = tb.Cell().Row(1).Column(6).Element(Heading).Text("Date").SemiBold();
                    foreach (
                        var item in Model.Student.Achievements.Select((value, i) => (value, i))
                    )
                    {
                        _ = tb.Cell().Element(Plain).Text(item.i + 1 + "");
                        _ = tb.Cell().Element(Plain).Text(item.value.Name);
                        _ = tb.Cell().Element(Plain).Text(item.value.AchievementType.Label);
                        _ = tb.Cell().Element(Plain).Text(item.value.AchievementLevel.Name);
                        _ = tb.Cell().Element(Plain).Text(item.value.Prize);
                        _ = tb.Cell().Element(Plain).Text(FormatDate(item.value.Date));
                    }
                    static IContainer Heading(IContainer container)
                    {
                        return container
                            .Border(1)
                            .Background(Colors.Grey.Lighten3)
                            .ShowOnce()
                            .MinHeight(25)
                            .AlignCenter()
                            .AlignMiddle();
                    }
                    static IContainer Plain(IContainer container)
                    {
                        return container
                            .Border(1)
                            .ShowOnce()
                            .MinHeight(25)
                            .AlignCenter()
                            .AlignMiddle();
                    }
                });
        });
    }
}
