using System.Net.Http.Json;
using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Dtos.Employees.Requests;
using VanKassa.Domain.ViewModels;
using VanKassa.Presentation.BlazorWeb.Features.Shared.Data.Base;
using VanKassa.Shared.Data;

namespace VanKassa.Presentation.BlazorWeb.Features.Admin.EmployeesEditor;

public class EmployeeEditService : ServiceBase
{
    private readonly ImageConverter imageConverter;

    public EmployeeEditService(HttpClient httpClient, IMapper mapper,
        IConfiguration configuration, ImageConverter imageConverter)
        : base(httpClient, mapper, configuration)
    {
        this.imageConverter = imageConverter;

        WebApiAddress += "/employees/edit";
    }

    public async Task<EditedEmployeeViewModel> GetEditedEmployeeByIdAsync(int empId)
    {
        var query = new Dictionary<string, string>()
        {
            ["EmployeeId"] = empId.ToString()
        };

        var uri = QueryHelpers.AddQueryString(WebApiAddress, query);

        var empDto = await HttpClient.GetFromJsonAsync<EditedEmployeeDto>(uri)
                     ?? new EditedEmployeeDto();

        return Mapper.Map<EditedEmployeeDto, EditedEmployeeViewModel>(empDto);
    }

    public async Task<bool> SaveEmployee(EditedEmployeeViewModel savedEmployee, IBrowserFile? addedImage = null)
    {
        var uri = WebApiAddress + "/save";

        if (addedImage is not null)
        {
            await using var stream = addedImage.OpenReadStream();
            savedEmployee.Photo = await imageConverter.ConvertImageStreamToBase64Async(stream);
        }

        var requestDto = Mapper.Map<SavedEmployeeRequestDto>(savedEmployee);

        var result = await HttpClient.PostAsJsonAsync(uri, requestDto);

        return result.IsSuccessStatusCode;
    }

    public async Task<bool> SaveEditedEmployee(EditedEmployeeViewModel editedEmployee, IBrowserFile? addedImage = null)
    {
        if (addedImage is not null)
        {
            await using var stream = addedImage.OpenReadStream();

            editedEmployee.Photo = await imageConverter.ConvertImageStreamToBase64Async(stream);
        }

        var requestDto = Mapper.Map<ChangedEmployeeRequestDto>(editedEmployee);

        var result = await HttpClient.PutAsJsonAsync(WebApiAddress, requestDto);

        return result.IsSuccessStatusCode;
    }
}