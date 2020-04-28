using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NtierMvc.Common
{
    public class BaseEntity
    {
        // INTEGER SECTION
        public Int32 InstituteId { get; set; }
        public Int32 CreatedBy { get; set; }
        public Int32 ModifiedBy { get; set; }
        public int? WorkflowStatusId { get; set; }
        public Int32 HeadId { get; set; }
        public Int32 WorkItemId { get; set; }
        public Int32 RegistrationId { get; set; }
        public Int32 RoleId { get; set; }        

        //BOOLEAN SECTION
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        //STRING
        public string UserName { get; set; }

    }
    public class GetInfraNormsEntity
    {
        public List<DropDownEntity> ApplicationType { get; set; }
        public List<DropDownEntity> Sector { get; set; }
        public List<DropDownEntity> Trade { get; set; }

        public Int32 TradeMachinesMasterId { get; set; }
        public String NameOfItem { get; set; }
        public String ItemNameasperStandardizedbyERP { get; set; }
        public String QtyperUnitasperERP { get; set; }
        public String CostofMachine { get; set; }
        public String TradeName { get; set; }
        public String Utility { get; set; }
        public String DropDownValue { get; set; }


    }
    public class listentity
    {
        public Int32 TradeMachinesMasterId { get; set; }
        public String NameOfItem { get; set; }
        public String ItemNameasperStandardizedbyERP { get; set; }
        public String QtyperUnitasperERP { get; set; }
        public String CostofMachine { get; set; }
        public String TradeName { get; set; }
        public String Utility { get; set; }
    }

    public class listCourseNormEntity
    {
        public string CourseType { get; set; }
        public string CourseApprovedBy { get; set; }
        public string SchemeCode { get; set; }
        public Int32 CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string SectorName { get; set; }
        public string TypeOfTrade { get; set; }
        public Int32 DurationInYears { get; set; }
        public string SeparateWorkshopRequired { get; set; }
        public decimal WorkshopSpaceSqM { get; set; }
        public string RemarkforFloor { get; set; }
        public string SeparateClassRoomRequired { get; set; }
        public decimal ClassRoomSqM { get; set; }
        public decimal PowerRequiredinKW { get; set; }

        public decimal FundsRequiredForCivilWork { get; set; }
        public decimal FundsRequiredForMachineInfrastructure { get; set; }
        public decimal FundsRequiredForTools { get; set; }
        public decimal AgriculturalLandAcre { get; set; }
        public string SourceOfWaterSupply { get; set; }
        public decimal LabDemoSqM { get; set; }
        public decimal DrawingHallSqM { get; set; }
        public decimal ApproximateInvestment { get; set; }
        public string InfrastructureSheetIncluded { get; set; }
        public string CourseApprovedByFullName { get; set; }
        public Int32 TradeNormId { get; set; }

    }

    public class listInfraNormEntity
    {
        public Int32 CourseId { get; set; }
        public string CourseName { get; set; }
        public int TradeMachinesMasterId { get; set; }
        public string ItemName { get; set; }
        public string ItemNameasperStandardizedbyERP { get; set; }
        public decimal CostofMachine { get; set; }
        public string Utility { get; set; }
        public string DropDownValue { get; set; }
        public int TaxonomyId { get; set; }
        public int IntakePerBatch { get; set; }
        public int TradeId { get; set; }
        public int QtyperUnitasperERP { get; set; }
        public String IPAddress { get; set; }
        public int UpdatedBy { get; set; }
        public string Description { get; set; }
        public string GetInfraNormsXML(listInfraNormEntity objData)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(objData.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, objData);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }
    }

    public class AddInfraNormEntity
    {
        public List<listInfraNormEntity> lstInfraNormsDetails { get; set; }
        public listInfraNormEntity listInfraNormEntity { get; set; }
        public String DataTextField { get; set; }

    }

}
