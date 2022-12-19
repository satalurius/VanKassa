namespace VanKassa.Backend.Core.Services;

public class ImageService
{
    public string? CopyEmployeeImageToWebFolderAndGetCopyPath(string databaseImage)
    {
        try
        {
            var employeesDirectory = Path.Combine("wwwroot", "images", "employees");

            if (!Directory.Exists(employeesDirectory))
                Directory.CreateDirectory(employeesDirectory);

            var fileName = databaseImage.Split('/', '\\').Last();

            var newImagePath = Path.Combine(employeesDirectory, fileName);

            File.Copy(databaseImage, newImagePath , true);

            return Path.Combine("images", "employees", fileName);
        }
        catch (IOException iox)
        {
            // TODO: Логировать
            return null;
        }
    }
}