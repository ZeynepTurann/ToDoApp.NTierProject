using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ToDoAppNTierUI.Manager
{
    public static class FileManager
    {
        public static string GetUniqueNameAndSavePhotoToDisk(this IFormFile pictureFile, string imageName, IWebHostEnvironment webHostEnvironment)
        {
            string uniqueFileName = default;       
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");

            if (pictureFile is not null)
            {
                if (imageName != null)
                { 
                    string oldPath = Path.Combine(uploadsFolder, imageName);
                    File.Delete(oldPath);
                }

                uniqueFileName = $"{Guid.NewGuid()}_{pictureFile.FileName}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    pictureFile.CopyTo(fileStream);
                  
                }
            }

            return uniqueFileName;
        }

        ////public static void RemoveImageFromDisk(IWebHostEnvironment webHostEnvironment)
        ////{
        ////    if (!string.IsNullOrEmpty(imageName))
        ////    {
        ////        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
        ////        string filePath = Path.Combine(uploadsFolder, imageName);
        ////        File.Delete(filePath);
        ////    }
        ////}
    }
}
