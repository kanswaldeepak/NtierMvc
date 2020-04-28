using NtierMvc.Common;

namespace NtierMvc.Model.Application
{
    public class UploadDocumentEntity
    {
        public int RegistrationId { get; set; }
        public int DocumentId { get; set; }

        public long DocumentRowId { get; set; }

        public string DocShortName { get; set; }
        public string UserName { get; set; }
        public string Remarks { get; set; }
        public string IsCompulsory { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string IsUploaded { get; set; }
        public int UserId { get; set; }
        public FileUploadEntity docfile { get; set; }

        public string WorkCode { get; set; }
    }
}
