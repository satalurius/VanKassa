using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.WebUtilities;
using VanKassa.Domain.Dtos.Employees;
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

    // TODO: Возвращать ViewModel
    public async Task<PageEmployeesDto?> GetEmployeesAsync(EmployeesPageParameters pageParameters)
    {
        try
        {
            var query = new Dictionary<string, string>
            {
                ["Page"] = pageParameters.Page.ToString(),
                ["PageSize"] = pageParameters.PageSize.ToString(),
                ["SortedColumn"] = pageParameters.SortedColumn.ToString(),
                ["SortDirection"] = pageParameters.SortDirection.ToString(),
            };

            if (!string.IsNullOrEmpty(pageParameters.FilterText))
            {
                query.Add("FilterText", pageParameters.FilterText);
            }

            var uri = QueryHelpers.AddQueryString(webApiAddress + "/employees/all", query);

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
            var uri = $"{webApiAddress}/employees/delete";

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