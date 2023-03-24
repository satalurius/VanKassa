using AutoMapper;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Dtos.Employees.Requests;
using VanKassa.Domain.Exceptions;
using VanKassa.Domain.ViewModels;
using VanKassa.Presentation.BlazorWeb.Services.Interfaces;
using VanKassa.Presentation.BlazorWeb.Shared.Data.Base;
using VanKassa.Presentation.BlazorWeb.Shared.Data.Constants;

namespace VanKassa.Presentation.BlazorWeb.Services.EmployeesAdmin;

public class EmployeesPdfReportService : ServiceBase
{

    public EmployeesPdfReportService(IHttpClientFactory httpClientFactory, IMapper mapper, IConfiguration config, ITokenService tokenService) 
        : base(httpClientFactory, mapper, config, tokenService)
    {
        WebApiAddress += ApiRoutes.EmployeesPdfReportRoute;
    }

    public async Task<IList<PdfEmployeeViewModel>> GetEmployeesAsync()
    {
        try
        {
            var empDto = await GetAsync<List<PdfEmployeeDto>>(WebApiAddress);

            return Mapper.Map<IList<PdfEmployeeViewModel>>(empDto);
        }
        catch (HttpRequestException)
        {
            return Array.Empty<PdfEmployeeViewModel>();
        }
    }

    public async Task<Stream> GenerateReportAsync(IList<PdfEmployeeViewModel> employees, IList<string> outlets)
    {
        var pdfEmployeesDto = Mapper.Map<List<PdfEmployeeDto>>(employees);

        var request = new GeneratePdfEmployeesRequest
        {
            EmployeesList = pdfEmployeesDto,
            Outlet = outlets
        };


        try
        {
            var response = await PostAsync(WebApiAddress, request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }

        }
        catch (HttpRequestException)
        {
            throw new NotFoundException();
        }
        throw new NotFoundException();
    }

}