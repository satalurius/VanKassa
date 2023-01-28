using System.Net.Http.Json;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.ViewModels;

namespace VanKassa.Presentation.BlazorWeb.Features.Admin.EmployeesEditor;

public class EmployeeEditService
{
    private readonly HttpClient httpClient;
    private readonly IMapper mapper;

    private readonly string webApiAddress;


    public EmployeeEditService(HttpClient httpClient, IConfiguration configuration, IMapper mapper)
    {
        this.httpClient = httpClient;
        this.mapper = mapper;
        webApiAddress = configuration.GetConnectionString("ApiAddress")
                        ?? throw new ArgumentNullException("Api address path does not exist");
    }

    public async Task<EditedEmployeeViewModel> GetEditedEmployeeByIdAsync(int empId)
    {
        var query = new Dictionary<string, string>()
        {
            ["EmployeeId"] = empId.ToString()
        };

        var uri = QueryHelpers.AddQueryString(webApiAddress + "/employees/edit", query);

        var empDto = await httpClient.GetFromJsonAsync<EditedEmployeeDto>(uri)
                     ?? new EditedEmployeeDto();

        return mapper.Map<EditedEmployeeDto, EditedEmployeeViewModel>(empDto);
    }
}
