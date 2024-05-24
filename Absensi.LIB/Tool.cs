using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absensi.LIB
{
    public class Tool
    {
        public static string SaveSignatureImage(IFormFile image)
        {
            //SAVING IN DIRECTORY D:/
            if (image != null)
            {
                //var fileName = Guid.NewGuid().ToString();
                var fileName = $"IMG-{DateTime.Now:yyyyMMdd-HHmmssfff}";
                var fileExtension = Path.GetExtension(image.FileName);
                var directoryPath = @"D:/Project_Image/PBL/AbsensiOnline";
                var filePath = Path.Combine(directoryPath, fileName + fileExtension);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                return fileName + fileExtension;
            }
            return null;
        }

    }
}
