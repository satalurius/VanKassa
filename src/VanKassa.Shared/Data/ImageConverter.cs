namespace VanKassa.Shared.Data;

public class ImageConverter
{
    public async Task<string> ConvertImageStreamToBase64Async(Stream imageStream)
    {
        using var ms = new MemoryStream();
        await imageStream.CopyToAsync(ms);
        
        var imageArray = ms.ToArray();
        
        return $"data:image/jpeg;base64,{Convert.ToBase64String(imageArray)}";
    }
}