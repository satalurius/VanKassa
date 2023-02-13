using AutoMapper;
using VanKassa.Shared.Mappers;

namespace VanKassa.Backend.Core.Tests;

public static class AutoMapperCreator
{
    private static IMapper? mapper;

    public static IMapper Create()
    {
        if (mapper is null)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DtoViewModelProfiles());
            });
            mapper = mappingConfig.CreateMapper();
        }

        return mapper;
    }
}