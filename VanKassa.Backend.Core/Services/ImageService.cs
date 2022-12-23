using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Models.SettingsModels;
using Exception = System.Exception;

namespace VanKassa.Backend.Core.Services;

public class ImageService
{
    private readonly IConfiguration _configuration;
    private readonly string _imagesPath;
    public ImageService(IConfiguration configuration)
    {
        _configuration = configuration;

        _imagesPath = GetImagesPath();
        CreateImageFolderIfNotExist();
    }

    private string GetImagesPath()
    {
        var imageSettings = _configuration.GetSection(SettingsConstants.DbImageSettingsName)
            .Get<ImageSettings>();

        if (imageSettings is null)
            throw new InvalidOperationException("ImagePath settings not found");

        return imageSettings.BaseFolder;
    }

    private void CreateImageFolderIfNotExist()
    {
        if (!Directory.Exists(_imagesPath))
            Directory.CreateDirectory(_imagesPath);
    }

    /// <summary>
    /// Копирует путь в wwwroot каталог, чтобы получить из веб проекта.
    /// </summary>
    /// <param name="databaseImage"></param>
    /// <returns></returns>
    public string? CopyEmployeeImageToWebFolderAndGetCopyPath(string databaseImage)
    {
        try
        {
            var employeesDirectory = Path.Combine("wwwroot", "images", "employees");

            if (!Directory.Exists(employeesDirectory))
                Directory.CreateDirectory(employeesDirectory);

            var fileName = databaseImage.Split('/', '\\').Last();

            var newImagePath = Path.Combine(employeesDirectory, fileName);

            File.Copy(databaseImage, newImagePath, true);

            return Path.Combine("images", "employees", fileName);
        }
        catch (ArgumentException)
        {
            return null;
        }
        catch (IOException iox)
        {
            // TODO: Логировать
            return null;
        }
    }

    /// <summary>
    /// Записывает файл на сервер и возвращает путь до этого файла.
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public async Task<string> CopyImageFromUserToServerFolderAsync(IBrowserFile file)
    {
        try
        {

            //await using var fs = new FileStream()
            var imagePath = Path.Combine(_imagesPath, $"{Guid.NewGuid().ToString()}.jpg");
            
            await using var fs = new FileStream(imagePath, FileMode.OpenOrCreate);
            await file.OpenReadStream().CopyToAsync(fs);
            
            return imagePath;
        }
        catch (Exception ex)
        {
            // TODO: Логер
            return string.Empty;
        }
    }
}