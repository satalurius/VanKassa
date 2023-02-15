using Moq;
using VanKassa.Backend.Core.Services;
using VanKassa.Backend.Core.Services.Interface;

namespace VanKassa.Backend.Core.Tests;

public static class MockImageServiceFactory
{
    public static Mock<IImageService> Create()
    {
        var imageService = new Mock<IImageService>();
        
        imageService.Setup(i => i.ConvertImageToBase64(It.IsAny<string>()))
            .Returns("");
        imageService.Setup(i => i.SaveBase64ToImageFile(It.IsAny<string>()))
            .Returns("");
        imageService.Setup(i => i.CopyEmployeeImageToWebFolderAndGetCopyPath(It.IsAny<string>()))
            .Returns("");

        return imageService;
    }
}