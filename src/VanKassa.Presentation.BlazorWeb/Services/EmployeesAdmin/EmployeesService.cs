using System.Net.Http.Json;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.ViewModels;
using VanKassa.Presentation.BlazorWeb.Shared.Data.Base;
using VanKassa.Presentation.BlazorWeb.Shared.Exceptions;

namespace VanKassa.Presentation.BlazorWeb.Services.EmployeesAdmin;

public class EmployeesService : ServiceBase
{
    public EmployeesService(HttpClient httpClient, IMapper mapper, IConfiguration config) : base(httpClient, mapper, config)
    {
        WebApiAddress += "/employees";
    }

    public async Task<PageEmployeesViewModel?> GetEmployeesAsync(EmployeesPageParameters pageParameters)
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

            var uri = QueryHelpers.AddQueryString(WebApiAddress + "/all", query);
            
            var pageEmp = await HttpClient.GetFromJsonAsync<PageEmployeesDto>(uri);

            return Mapper.Map<PageEmployeesViewModel>(pageEmp);
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
            var uri = $"{WebApiAddress}/delete";

            var json = JsonConvert.SerializeObject(deleteIds);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            await HttpClient.PostAsync(uri, httpContent);
        }
        catch (HttpRequestException)
        {
            throw new QueryException();
        }
    }

}