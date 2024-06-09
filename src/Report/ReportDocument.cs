using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CRK.Report;

public class ReportDocument(ReportModel _model) : IDocument
{
    public ReportModel Model { get; } = _model;

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(50);

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
        });
    }

    void ComposeHeader(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

        container.Row(row =>
        {
            row.RelativeItem()
                .Column(column =>
                {
                    column
                        .Item()
                        .Text(
                            Model.Student.FirstName
                                + ' '
                                + Model.Student.MiddleName
                                + ' '
                                + Model.Student.LastName
                        )
                        .Style(titleStyle);

                    column
                        .Item()
                        .Text(text =>
                        {
                            text.Span("Date Of Birth: ").SemiBold();
                            text.Span($"{Model.Student.DateOfBirth:d}");
                        });

                    column
                        .Item()
                        .Text(text =>
                        {
                            text.Span("Id: ").SemiBold();
                            text.Span($"{Guid.NewGuid}");
                        });
                });

            row.ConstantItem(80).Height(100).Placeholder();
        });
    }

    void ComposeContent(IContainer container)
    {
        container
            .PaddingVertical(40)
            .Height(250)
            .Background(Colors.Grey.Lighten3)
            .AlignCenter()
            .AlignMiddle()
            .Text("Content")
            .FontSize(16);
    }
}
