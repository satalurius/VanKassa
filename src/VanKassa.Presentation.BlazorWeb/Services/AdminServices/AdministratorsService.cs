using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using VanKassa.Domain.Dtos.Admins;
using VanKassa.Domain.Dtos.Admins.Requests;
using VanKassa.Domain.ViewModels;
using VanKassa.Presentation.BlazorWeb.Services.Interfaces;
using VanKassa.Presentation.BlazorWeb.Shared.Data.Base;

namespace VanKassa.Presentation.BlazorWeb.Services.AdminServices
{
    public class AdministratorsService : ServiceBase
    {
        public AdministratorsService(IHttpClientFactory httpClientFactory, IMapper mapper, IConfiguration config, ITokenService tokenService) : base(httpClientFactory, mapper, config, tokenService)
        {
            WebApiAddress += "/administrators";
        }

        public async Task<IReadOnlyList<AdministratorViewModel>> GetAdministratorsAsync()
        {
            var uri = WebApiAddress + "/all";

            try
            {
                var administrators = await GetAsync<IReadOnlyList<AdministratorDto>>(uri);

                return Mapper.Map<IReadOnlyList<AdministratorViewModel>>(administrators);
            }
            catch (HttpRequestException)
            {
                return new List<AdministratorViewModel>();
            }
        }

        public async Task<bool> CreateAdminAsync(AdministratorViewModel createdAdmin)
        {
            var uri = WebApiAddress + "/create";

            try
            {
                var adminDto = Mapper.Map<CreateAdministratorRequest>(createdAdmin);

                return (await PostAsync(uri, adminDto)).IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task<bool> EditAdminAsync(AdministratorViewModel editedAdmin)
        {
            var uri = WebApiAddress + "/edit";

            try
            {
                var adminDto = Mapper.Map<ChangeAdministratorRequest>(editedAdmin);

                return (await PutAsync(uri, adminDto)).IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAdminAsync(AdministratorViewModel deletedAdmin)
        {
            var uri = WebApiAddress + "/delete";

            uri = QueryHelpers.AddQueryString(uri, "deleteId", deletedAdmin.AdminId.ToString());

            try
            {
                return (await DeleteAsync(uri)).IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

    }
}