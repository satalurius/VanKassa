using System.Runtime.InteropServices;
using jsreport.Binary;
using jsreport.Local;
using jsreport.Types;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Models;

namespace VanKassa.Backend.Core.Services;

public class EmployeesPdfService : IEmployeesPdfService
{
    public async Task<PdfReport> GenerateReportFromHtmlAsync(string html)
    {
        var rs = new LocalReporting()
            .KillRunningJsReportProcesses()
            .UseBinary(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? JsReportBinary.GetBinary()
                : jsreport.Binary.Linux.JsReportBinary.GetBinary())
            .Configure(cfg => cfg.AllowedLocalFilesAccess().FileSystemStore().BaseUrlAsWorkingDirectory())
            .AsUtility()
            .Create();

        var report = await rs.RenderAsync(new RenderRequest
        {
            Template = new Template
            {
                Recipe = Recipe.ChromePdf,
                Engine = Engine.JsRender,
                Content = html,
                Chrome = new Chrome
                {
                    MarginTop = "10",
                    MarginBottom = "10",
                    MarginLeft = "50",
                    MarginRight = "50"
                }
            }
        });
        
        var reportName = $"Отчет-по-Сотрудникам-{DateTime.Now.ToShortDateString()}.pdf";

        return new PdfReport(report.Content, report.Meta.ContentType, reportName);
    }
}