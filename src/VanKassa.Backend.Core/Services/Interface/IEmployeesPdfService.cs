using VanKassa.Domain.Models;

namespace VanKassa.Backend.Core.Services.Interface;

public interface IEmployeesPdfService
{
    Task<PdfReport> GenerateReportFromHtmlAsync(string html);
}