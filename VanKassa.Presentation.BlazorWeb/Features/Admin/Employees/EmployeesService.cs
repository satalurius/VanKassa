using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using VanKassa.Domain.Dtos;
using Microsoft.AspNetCore.WebUtilities;


namespace VanKassa.Presentation.BlazorWeb.Features.Admin.Employees;

public class EmployeesService
{
    private readonly HttpClient httpClient;

    // TODO: Брать из настроек
    private static readonly string WebApiAddress = "http://localhost:5289/api/employees";

    public EmployeesService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<PageEmployeesDto?> GetEmployeesAsync(EmployeesPageParameters pageParameters)
    {
        try
        {
            var query = new Dictionary<string, string>
            {
                ["page"] = pageParameters.Page.ToString(),
                ["page_size"] = pageParameters.PageSize.ToString(),
                ["sorted_column"] = pageParameters.SortedColumn.ToString(),
                ["sort_direction"] = pageParameters.SortDirection.ToString(),
                ["filter_text"] = pageParameters.FilterText
            };

            var uri = QueryHelpers.AddQueryString(WebApiAddress + "/all", query);

            var pageEmp = await httpClient.GetFromJsonAsync<PageEmployeesDto>(uri);

            return pageEmp;
        }
        catch (HttpRequestException ex)
        {
            return null;
        }
    }
}