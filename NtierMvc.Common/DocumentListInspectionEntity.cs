using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NtierMvc.Common
{
    public class DocumentListInspectionEntity
    {
        public int WorkItemId { get; set; }
        public int Id { get; set; }
        public string DocumentImageName { get; set; }
        public string DocumentImageNameUrl { get; set; }
        public FileUploadEntity DocumentImage { get; set; }
        public HttpPostedFileBase DocumentImageFile { get; set; }
        public string FormType { get; set; }
        public int UserId { get; set; }
    }
    public class DocumentListInspection
    {
        public int WorkItemId { get; set; }
        public string FormType { get; set; }
       public List<DocumentListInspectionEntity> objDocList { get; set; }
    }
}
