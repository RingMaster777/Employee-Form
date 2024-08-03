using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;


public static class FileHelper
{
    public static async Task<string?> SaveFileAsync(IFormFile file, string uploadsFolder)
    {
        if (file == null || file.Length == 0)
            return null;


        // Server-side file type validation for image type
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(file.FileName).ToLower();

        if (!Array.Exists(allowedExtensions, e => e == extension))
        {
            return null;
        }

        // Create the directory if it does not exist
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        // generate unique file name
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

        // get file path
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return uniqueFileName;
    }


    public static async Task<bool> DeleteFolderAsync(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            try
            {
                // deletes the directory where the image resides
                Directory.Delete(folderPath, true);
                return true;
            }
            catch (IOException)
            {
                // Handle the exception as needed
                return false;
            }
        }
        return false;
    }
}
