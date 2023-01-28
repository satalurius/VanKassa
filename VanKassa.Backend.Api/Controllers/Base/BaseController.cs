using Microsoft.AspNetCore.Mvc;

namespace VanKassa.Backend.Api.Controllers.Base;

public abstract class BaseController<TService> : ControllerBase
{
    protected readonly TService Service;

    protected BaseController(TService service)
        => Service = service;
}