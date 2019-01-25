using System;
using System.IO;

namespace RestWithAspNetCoreUdemy.Bussines.Implementattions
{
    public class FileBussinesImp : IFileBussines
    {
        public byte[] GetPdfFile()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = $@"{path}\Files\RESTFULL-API.pdf";
            return File.ReadAllBytes(fullPath);
        }
    }
}
