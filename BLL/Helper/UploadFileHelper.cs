using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace BlL.Helper
{
    public static class UploadFileHelper
    {
        public static string[] SaveFile(IFormFile FileUrl, string FolderPath)
        {
            string[] arr = new string[2];
            // Get Directory
            string FilePath = Directory.GetCurrentDirectory() + "/wwwroot/" + FolderPath;

            // Get File Name
            string FileName = Guid.NewGuid() + Path.GetFileName(FileUrl.FileName);
            arr[0] = FileName;
            // Merge The Directory With File Name
            string FinalPath = Path.Combine(FilePath, FileName);
            arr[1] = FinalPath;
            // Save Your File As Stream "Data Overtime"
            using (var Stream = new FileStream(FinalPath, FileMode.Create))
            {
                FileUrl.CopyTo(Stream);
            }
            return arr;
        }
        public static void RemoveFile(string Filepath)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "/wwwroot" + Filepath))
            {
                File.Delete(Directory.GetCurrentDirectory() + "/wwwroot" + Filepath);
            }
        }
    }
}
