using System.Net.Http.Json;
using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Dtos.Employees.Requests;
using VanKassa.Domain.ViewModels;
using VanKassa.Shared.Data;

namespace VanKassa.Presentation.BlazorWeb.Features.Admin.EmployeesEditor;

public class EmployeeEditService
{
    private readonly HttpClient httpClient;
    private readonly IMapper mapper;

    private readonly string webApiAddress;
    private readonly ImageConverter imageConverter;

    public EmployeeEditService(HttpClient httpClient, IConfiguration configuration, IMapper mapper,
        ImageConverter imageConverter)
    {
        this.httpClient = httpClient;
        this.mapper = mapper;
        this.imageConverter = imageConverter;
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

    public async Task<bool> SaveEmployee(EditedEmployeeViewModel savedEmployee, IBrowserFile? addedImage = null)
    {
        var uri = webApiAddress + "/employees/edit/save";
        
        if (addedImage is not null)
        {
            await using var stream = addedImage.OpenReadStream();
            savedEmployee.Photo = await imageConverter.ConvertImageStreamToBase64Async(stream);
        }

        var requestDto = mapper.Map<SavedEmployeeRequestDto>(savedEmployee);

        var result = await httpClient.PostAsJsonAsync(uri, requestDto);

        return result.IsSuccessStatusCode;
    }

    public async Task<bool> SaveEditedEmployee(EditedEmployeeViewModel editedEmployee, IBrowserFile? addedImage = null)
    {
        var uri = webApiAddress + "/employees/edit";

        if (addedImage is not null)
        {
            await using var stream = addedImage.OpenReadStream();

            editedEmployee.Photo = await imageConverter.ConvertImageStreamToBase64Async(stream);
        }

        var requestDto = mapper.Map<ChangedEmployeeRequestDto>(editedEmployee);

        var result = await httpClient.PutAsJsonAsync(uri, requestDto);

        return result.IsSuccessStatusCode;
    }
}
