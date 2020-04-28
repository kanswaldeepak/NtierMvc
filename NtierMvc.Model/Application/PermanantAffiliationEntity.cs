using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.Application
{
    public class PermanantAffiliationEntity
    {
        public int RegistrationId { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string IP { get; set; }
        public List<CheckModel> lstCheckModel { get; set; }
        public CheckModel checkModel { get; set; }

        public PermanantAffiliationEntity()
        {
            lstCheckModel = new List<CheckModel>();
            checkModel = new CheckModel();
        }
    }
    public class CheckModel
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public bool IsChecked
        {
            get;
            set;
        }
        public int InstituteCourseId
        {
            get;
            set;
        }
    }
}
