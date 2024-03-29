﻿using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Models.SettingsModels;
using Exception = System.Exception;

namespace VanKassa.Backend.Core.Services;

public class ImageService : IImageService
{
    private readonly IConfiguration configuration;
    private readonly string imagesPath;
    public ImageService(IConfiguration configuration)
    {
        this.configuration = configuration;

        imagesPath = GetImagesPath();
        CreateImageFolderIfNotExist();
    }

    private string GetImagesPath()
    {
        var imageSettings = configuration.GetSection(SettingsConstants.DbImageSettingsName)
            .Get<ImageSettings>();

        if (imageSettings is null)
            throw new InvalidOperationException("ImagePath settings not found");

        return imageSettings.BaseFolder;
    }

    private void CreateImageFolderIfNotExist()
    {
        if (!Directory.Exists(imagesPath))
            Directory.CreateDirectory(imagesPath);
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

    public string SaveBase64ToImageFile(string imageEncoded)
    {
        imageEncoded = imageEncoded.Substring(imageEncoded.LastIndexOf(",",
            StringComparison.Ordinal) + 1);
        
        var imageBytes = Convert.FromBase64String(imageEncoded);

        if (!Directory.Exists(imagesPath))
            Directory.CreateDirectory(imagesPath);

        var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty)
            .Substring(0, 8) + "-" + $"{DateTime.Now:yyyy-MM-dd_hh-mm-ss-tt}";
        var filePath = Path.Combine(imagesPath, fileName);

        using var stream = File.Create(filePath);
        stream.Write(imageBytes, 0, imageBytes.Length);

        return filePath;
    }

    public string ConvertImageToBase64(string imagePath)
    {
        try
        {
            var imageArray = File.ReadAllBytes(imagePath);
            return $"data:image/jpeg;base64,{Convert.ToBase64String(imageArray)}";
        }
        catch (ArgumentException)
        {
            return imagePath;
        }
        catch (IOException)
        {
            return imagePath;
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
            var imagePath = Path.Combine(imagesPath, $"{Guid.NewGuid().ToString()}.jpg");

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