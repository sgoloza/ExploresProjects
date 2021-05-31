using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Web;
namespace InternetExplores.Models
{
    public class StudentFiles
    {
        public int FileID { get; set; }
        public string FileName { get; set; }
        public string FileDescription { get; set; }
        public string FileLocation { get; set; }
        //public HttpPostedFileBase UploadFile { get; set; }
    }
}
