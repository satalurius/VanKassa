using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using VanKassa.Domain.Dtos;
using Microsoft.AspNetCore.WebUtilities;
using VanKassa.Presentation.BlazorWeb.Features.Shared.Exceptions;

namespace VanKassa.Presentation.BlazorWeb.Features.Admin.Employees;

public class EmployeesService
{
    private readonly HttpClient httpClient;

    private readonly string webApiAddress;
    
    public EmployeesService(HttpClient httpClient, IConfiguration configuration)
    {
        this.httpClient = httpClient;
        webApiAddress = configuration.GetConnectionString("ApiAddress") 
                        ?? throw new ArgumentNullException("Api address path does not exist");
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

            var uri = QueryHelpers.AddQueryString(webApiAddress + "/all", query);

            var pageEmp = await httpClient.GetFromJsonAsync<PageEmployeesDto>(uri);

            return pageEmp;
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    public async Task DeleteEmployeesAsync(IEnumerable<int> deleteIds)
    {
        try
        {
            var uri = $"{webApiAddress}/delete";

            var json = JsonConvert.SerializeObject(deleteIds);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            await httpClient.PostAsync(uri, httpContent);
        }
        catch (HttpRequestException)
        {
            throw new QueryException();
        }
    }
}