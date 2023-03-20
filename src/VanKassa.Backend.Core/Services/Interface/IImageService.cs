namespace VanKassa.Backend.Core.Services.Interface;

public interface IImageService
{
    string? CopyEmployeeImageToWebFolderAndGetCopyPath(string databaseImage);
    string SaveBase64ToImageFile(string imageEncoded);
    string ConvertImageToBase64(string imagePath);
}