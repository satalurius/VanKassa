using System.Runtime.InteropServices;
using Dapper;
using jsreport.Binary;
using jsreport.Local;
using jsreport.Types;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Exceptions;
using VanKassa.Domain.Models;

namespace VanKassa.Backend.Core.Services;

public class EmployeesPdfService : IEmployeesPdfService
{

    private readonly DapperDbContext dapperDbContext;

    public EmployeesPdfService(DapperDbContext dapperDbContext)
    {
        this.dapperDbContext = dapperDbContext;
    }

    public async Task<PdfReport> GenerateReportFromHtmlAsync(string html)
    {
        var binary = GetBinaryByOs();

        var rs = new LocalReporting()
            .KillRunningJsReportProcesses()
            .UseBinary(binary)
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

    public async Task<IReadOnlyList<PdfEmployeeDto>> GetEmployeesAsync()
    {
        var query =
            """
              WITH grouped_cte as (SELECT dbo.employee.user_id, fist_name, last_name, patronymic, dbo.outlet.city, dbo.outlet.street,
                 dbo.outlet.street_number, r.name, dbo.employee.fired
                            FROM dbo.employee_outlet
                                     JOIN dbo.employee ON employee_outlet.user_id = employee.user_id
                                     JOIN dbo.outlet ON employee_outlet.outlet_id = outlet.outlet_id
                            JOIN dbo.role r ON employee.role_id = r.role_id)
              SELECT 
                     string_agg(CONCAT(grouped_cte.city, ', ', grouped_cte.street, ', ', grouped_cte.street_number), '; ') Addresses,
                     name AS RoleName,
                     last_name AS LastName,
                     fist_name AS FirstName,
                     patronymic AS Patronymic
              FROM grouped_cte
              WHERE fired = false
              GROUP BY RoleName, LastName, FirstName, Patronymic
          """;

        try
        {
            using var dbConnection = dapperDbContext.CreateConnection();
            return (await dbConnection.QueryAsync<PdfEmployeeDto>(query)).ToList();
        }
        catch (InvalidOperationException)
        {
            // TODO: Подключить логгер
            throw new NotFoundException();
        }
    }

    private jsreport.Shared.IReportingBinary GetBinaryByOs()
    {
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return JsReportBinary.GetBinary();
        }
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return jsreport.Binary.OSX.JsReportBinary.GetBinary();
        }
        
        return jsreport.Binary.Linux.JsReportBinary.GetBinary();

    }
}