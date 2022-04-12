using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class ImageUploadDTO
    {
        public int ID { get; set; }
        public string ImageFor { get; set; }
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public string ImgPath { get; set; }
    }
}
