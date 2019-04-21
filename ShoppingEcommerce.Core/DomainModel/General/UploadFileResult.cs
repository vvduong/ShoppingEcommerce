using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LacViet.HPS.Sureportal.Core
{
    public class UploadFileResult
    {
        public bool UploadSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string UploadedFileName { get; set; }
        public string UploadedFileUrl { get; set; }
    }
}