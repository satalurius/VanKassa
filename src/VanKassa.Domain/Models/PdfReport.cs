namespace VanKassa.Domain.Models;

public class PdfReport
{
    public Stream FileStream { get; set; }
    public string ContentType { get; set; }
    public string Name { get; set; }

    public PdfReport(Stream fileStream, string contentType, string name)
    {
        FileStream = fileStream;
        ContentType = contentType;
        Name = name;
    }
}