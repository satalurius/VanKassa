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
                mc.AddProfile(new MapProfiles());
            });
            mapper = mappingConfig.CreateMapper();
        }

        return mapper;
    }
}