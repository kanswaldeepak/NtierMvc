using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using NtierMvc.DataAccess.Pool;
using NtierMvc.BusinessLogic.Utility;
using System.Configuration;
using System.IO;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.Model.HR;
using NtierMvc.Common;
using NtierMvc.Model;
using System.Web;

namespace NtierMvc.BusinessLogic.Worker
{
    public class HrWorker : IHrWorker, IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;
        private Repository _repository;

        public HrWorker()
        {
            _loggingHandler = new LoggingHandler();
            _repository = new Repository();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            // Check to see if Dispose has already been called.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Dispose managed resources.
                    _loggingHandler = null;
                }
            }
            _bDisposed = true;
        }
        #endregion

        public string SaveEmployeeDetails(EmployeeEntity entity)
        {
            string result = "";
            try
            {
                result = _repository.SaveEmployeeDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public string SavePayrollDetails(PayrollEntity entity)
        {
            string result = "";
            try
            {
                result = _repository.SavePayrollDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public string SaveEmpLeaveDetails(LeaveManagement entity)
        {
            string result = "";
            try
            {
                result = _repository.SaveEmpLeaveDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public EmployeeEntity GetUserEmployeeDetails(string unitNo)
        {
            EmployeeEntity result = new EmployeeEntity();
            try
            {
                result = DT2Cust(_repository.GetUserEmpDetails(unitNo));
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public EmployeeEntity DT2Cust(DataTable dtRecord)
        {
            EmployeeEntity oCust = new EmployeeEntity();
            try
            {
                if (dtRecord.Rows.Count > 0)
                {
                    oCust.UnitNo = Convert.ToString(dtRecord.Rows[0]["Id"]);
                    oCust.UserInitial = Convert.ToString(dtRecord.Rows[0]["UserName"]);
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return oCust;
        }

        public string DeleteEmployeeDetail(int EmployeeId)
        {
            string msgCode = "";
            try
            {
                msgCode = _repository.DeleteEmployeeDetail(EmployeeId);
            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                throw;
            }
            return msgCode;
        }


        public EmployeeEntityDetails GetEmployeeDetails(int pageIndex, int pageSize, string SearchEmployeeNameId = null, string SearchDesignation = null, string SearchDepartment = null, string SearchEmpStatus = null)
        {
            try
            {
                EmployeeEntityDetails eED = new EmployeeEntityDetails();
                eED.ListEmployeeEnt = new List<EmployeeEntity>();
                DataSet ds = _repository.GetEmployeeDetails(pageIndex, pageSize, SearchEmployeeNameId, SearchDesignation, SearchDepartment, SearchEmpStatus);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    DataTable dt2 = ds.Tables[1];

                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                EmployeeEntity obj = new EmployeeEntity();

                                obj.Id = dr1.IsNull("Id") ? 0 : Convert.ToInt32(dr1["Id"]);
                                obj.EmpCode = dr1.IsNull("EMPCODE") ? "" : Convert.ToString(dr1["EMPCODE"]);
                                obj.EmpType = dr1.IsNull("EMPTYPE") ? "" : Convert.ToString(dr1["EMPTYPE"]);
                                obj.EmpName = dr1.IsNull("EmpName") ? string.Empty : Convert.ToString(dr1["EmpName"]);
                                obj.Dept = dr1.IsNull("Dept") ? string.Empty : Convert.ToString(dr1["Dept"]);
                                obj.DateOfBirth = dr1.IsNull("DOB") ? string.Empty : Convert.ToString(dr1["DOB"]);
                                obj.Unit = dr1.IsNull("Unit") ? string.Empty : Convert.ToString(dr1["Unit"]);
                                obj.DateOfJoining = dr1.IsNull("DOJ") ? string.Empty : Convert.ToString(dr1["DOJ"]);
                                obj.Designation = dr1.IsNull("Designation") ? string.Empty : Convert.ToString(dr1["Designation"]);
                                obj.mob1 = dr1.IsNull("MOB1") ? string.Empty : Convert.ToString(dr1["MOB1"]);
                                obj.email = dr1.IsNull("EMAIL") ? string.Empty : Convert.ToString(dr1["EMAIL"]);
                                obj.PresState = dr1.IsNull("PresState") ? string.Empty : Convert.ToString(dr1["PresState"]);
                                obj.EmergContPerson = dr1.IsNull("EmergContPerson") ? string.Empty : Convert.ToString(dr1["EmergContPerson"]);
                                obj.EmergContNo = dr1.IsNull("EmergContNo") ? string.Empty : Convert.ToString(dr1["EmergContNo"]);
                                obj.Status = dt1.Rows[0]["EmployeeStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EmployeeStatus"]);
                                //obj.EmpImage = dr1.IsNull("EmpImage") ? string.Empty : Convert.ToString(dr1["EmpImage"]);

                                eED.ListEmployeeEnt.Add(obj);
                            }
                        }

                        if (dt2.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt2.Rows)
                            {
                                eED.totalcount = dr2.IsNull("totalCount") ? 0 : Convert.ToInt32(dr2["totalCount"]);
                            }
                        }
                    }
                }
                return eED;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }


        public EmployeeEntity EmployeeDetailsPopup(EmployeeEntity Model)
        {
            try
            {
                DataSet ds = _repository.EmployeeDetailsPopup(Model);
                //Model = ExtractEmployeeDetailsEntity(ds, Model);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    //Model.Id = dt1.Rows[0]["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dt1.Rows[0]["Id"]);
                    Model.UserInitial = dt1.Rows[0]["UserInitial"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["UserInitial"]);
                    Model.UnitNo = dt1.Rows[0]["UnitNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["UnitNo"]);

                    Model.EmpCode = dt1.Rows[0]["EmpCode"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EmpCode"]);
                    Model.EmpType = dt1.Rows[0]["EmpType"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EmpType"]);
                    Model.Unit = dt1.Rows[0]["Unit"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Unit"]);
                    Model.Dept = dt1.Rows[0]["Dept"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Dept"]);
                    Model.IdCard = dt1.Rows[0]["IdCard"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["IdCard"]);
                    Model.EmpInitial = dt1.Rows[0]["EmpInitial"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EmpInitial"]);
                    Model.EmpName = dt1.Rows[0]["EmpName"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EmpName"]);
                    Model.GenderTitleID = dt1.Rows[0]["GenderTitle"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["GenderTitle"]);
                    Model.Gender = dt1.Rows[0]["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Gender"]);
                    Model.GaurdianName = dt1.Rows[0]["GUARDIANNAME"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["GUARDIANNAME"]);
                    Model.BloodGroup = dt1.Rows[0]["BloodGroup"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BloodGroup"]);
                    Model.BloodGroupType = dt1.Rows[0]["BloodGroupType"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BloodGroupType"]);

                    Model.Designation = dt1.Rows[0]["Designation"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Designation"]);
                    Model.DateOfBirth = dt1.Rows[0]["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["DOB"]);
                    Model.DateOfJoining = dt1.Rows[0]["DOJ"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["DOJ"]);
                    Model.PresAddress1 = dt1.Rows[0]["PresAddress1"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PresAddress1"]);
                    Model.PresAddress2 = dt1.Rows[0]["PresAddress2"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PresAddress2"]);
                    Model.PresCity = dt1.Rows[0]["PresCity"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PresCity"]);
                    Model.PresState = dt1.Rows[0]["PresState"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PresState"]);
                    Model.PresZipCode = dt1.Rows[0]["PresZipCode"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PresZipCode"]);
                    Model.PresCountry = dt1.Rows[0]["PresCountry"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PresCountry"]);

                    Model.PerAddress1 = dt1.Rows[0]["PerAddress1"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PerAddress1"]);
                    Model.PerAddress2 = dt1.Rows[0]["PerAddress2"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PerAddress2"]);
                    Model.PerCity = dt1.Rows[0]["PerCity"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PerCity"]);
                    Model.PerState = dt1.Rows[0]["PerState"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PerState"]);
                    Model.PerZipCode = dt1.Rows[0]["PerZipCode"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PerZipCode"]);
                    Model.PerCountry = dt1.Rows[0]["PerCountry"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PerCountry"]);

                    Model.mob1 = dt1.Rows[0]["mob1"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["mob1"]);
                    Model.mob2 = dt1.Rows[0]["mob2"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["mob2"]);
                    Model.email = dt1.Rows[0]["email"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["email"]);

                    Model.EmergContPerson = dt1.Rows[0]["EMERGCONTPERSON"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EMERGCONTPERSON"]);
                    Model.EmergContNo = dt1.Rows[0]["EMERGCONTNO"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EMERGCONTNO"]);
                    Model.EduDeg = dt1.Rows[0]["EDUDEG"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EDUDEG"]);
                    Model.EduPG = dt1.Rows[0]["EDUPG"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EDUPG"]);
                    Model.ProfQual = dt1.Rows[0]["PROFQUAL"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PROFQUAL"]);
                    Model.TechQual = dt1.Rows[0]["TECHQUAL"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["TECHQUAL"]);
                    Model.PassportNo = dt1.Rows[0]["PASSPORTNO"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PASSPORTNO"]);
                    Model.PassportDate = dt1.Rows[0]["PASSPORTDATE"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PASSPORTDATE"]);
                    Model.PassportExp = dt1.Rows[0]["PASSPORTEXP"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PASSPORTEXP"]);
                    Model.DlNo = dt1.Rows[0]["DLNO"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["DLNO"]);
                    Model.DlExp = dt1.Rows[0]["DLEXP"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["DLEXP"]);
                    Model.PanNo = dt1.Rows[0]["PANNO"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PANNO"]);
                    Model.AadharNo = dt1.Rows[0]["AadharNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["AadharNo"]);
                    Model.BankAcctNo = dt1.Rows[0]["BANKACCTNO"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BANKACCTNO"]);
                    Model.BankName = dt1.Rows[0]["BANKNAME"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BANKNAME"]);
                    Model.BankBranch = dt1.Rows[0]["BANKBRANCH"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BANKBRANCH"]);
                    Model.BankLoc = dt1.Rows[0]["BANKLOC"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BANKLOC"]);
                    Model.IFScode = dt1.Rows[0]["IFSCODE"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["IFSCODE"]);
                    Model.BankLoc = dt1.Rows[0]["BANKLOC"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BANKLOC"]);
                    
                    Model.EmpImage = dt1.Rows[0]["EmpImage"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EmpImage"]);
                    Model.Status = dt1.Rows[0]["EmployeeStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EmployeeStatus"]);
                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return Model;
        }

        public PayrollEntity GetEmpPayrollData(int EmpId, int Yr = 0, int mnth = 0)
        {
            PayrollEntity pModel = new PayrollEntity();
            try
            {
                DataSet ds = _repository.GetEmpPayrollData(EmpId, Yr, mnth);
                //Model = ExtractEmployeeDetailsEntity(ds, Model);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    //pModel.Id = dt1.Rows[0]["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dt1.Rows[0]["Id"]);
                    pModel.UserInitial = dt1.Rows[0]["UserInitial"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["UserInitial"]);
                    pModel.UnitNo = dt1.Rows[0]["UnitNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["UnitNo"]);

                    pModel.EmpCode = dt1.Rows[0]["EmpCode"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EmpCode"]);
                    pModel.EmpId = dt1.Rows[0]["EmpId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EmpId"]);
                    pModel.BasicPay = dt1.Rows[0]["BasicPay"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BasicPay"]);
                    pModel.HRA = dt1.Rows[0]["HRA"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["HRA"]);
                    pModel.Conv = dt1.Rows[0]["Conv"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Conv"]);
                    pModel.OtherAllow = dt1.Rows[0]["OtherAllow"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["OtherAllow"]);
                    pModel.CarAllow = dt1.Rows[0]["CarAllow"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CarAllow"]);
                    pModel.EPF = dt1.Rows[0]["EPF"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EPF"]);
                    pModel.PPF = dt1.Rows[0]["PPF"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PPF"]);
                    pModel.ESI = dt1.Rows[0]["ESI"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["ESI"]);
                    pModel.TDSAMT = dt1.Rows[0]["TDSAMT"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["TDSAMT"]);
                    pModel.leaveAdj = dt1.Rows[0]["leaveAdj"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["leaveAdj"]);
                    pModel.Absent = dt1.Rows[0]["Absent"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Absent"]);
                    pModel.LoanAmt = dt1.Rows[0]["LoanAmt"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["LoanAmt"]);
                    pModel.Adv = dt1.Rows[0]["Adv"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Adv"]);
                    pModel.NetPay = dt1.Rows[0]["NetPay"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["NetPay"]);

                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return pModel;
        }

        public LeaveManagementEntityDetails GetEmpLeaveList(int EmpId)
        {
            try
            {
                LeaveManagementEntityDetails objLeave = new LeaveManagementEntityDetails();
                objLeave.ListLeaveMgmt = new List<LeaveManagement>();
                DataSet ds = _repository.GetEmpLeaveList(EmpId);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    //DataTable dt2 = ds.Tables[1];

                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                LeaveManagement obj = new LeaveManagement();

                                obj.Id = dr1.IsNull("Id") ? 0 : Convert.ToInt32(dr1["Id"]);                               
                                //obj.EmpCode = dr1.IsNull("EMPCODE") ? "" : Convert.ToString(dr1["EMPCODE"]);                                
                                obj.EmpId = dr1.IsNull("EmpId") ? "" : Convert.ToString(dr1["EmpId"]);
                                obj.LeaveType = dr1.IsNull("LeaveType") ? "" : Convert.ToString(dr1["LeaveType"]);
                                obj.LeaveStartDate = dr1.IsNull("LeaveStartDate") ? "" : Convert.ToString(dr1["LeaveStartDate"]);
                                obj.LeaveEndDate = dr1.IsNull("LeaveEndDate") ? "" : Convert.ToString(dr1["LeaveEndDate"]);


                                //obj.EmpImage = dr1.IsNull("EmpImage") ? string.Empty : Convert.ToString(dr1["EmpImage"]);

                                objLeave.ListLeaveMgmt.Add(obj);
                            }
                        }

                        //if (dt2.Rows.Count > 0)
                        //{
                        //    foreach (DataRow dr2 in dt2.Rows)
                        //    {
                        //        //eED.totalcount = dr2.IsNull("totalCount") ? 0 : Convert.ToInt32(dr2["totalCount"]);
                        //    }
                        //}
                    }
                }

                return objLeave;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public string SaveExperienceDetailsList(BulkUploadEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveExperienceDetailsList(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public HRCertificatesEntity HRCertificates(int EmpId)
        {
            try
            {
                HRCertificatesEntity objC = new HRCertificatesEntity();

                DataSet ds = _repository.HRCertificates(EmpId);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];

                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            objC.EmpId = dt1.Rows[0]["EmpId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EmpId"]);
                            objC.EmpName = dt1.Rows[0]["EmpName"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EmpName"]);
                            objC.EmpCode = dt1.Rows[0]["EmpCode"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EmpCode"]);
                            objC.CertType = dt1.Rows[0]["CertType"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CertType"]);
                            objC.CertValue = dt1.Rows[0]["CertValue"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CertValue"]);

                        }
                    }
                }

                return objC;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }


        public string SaveEmpCertificates(HRCertificatesEntity entity)
        {
            string result = "";
            try
            {
                result = _repository.SaveEmpCertificates(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }


    }
}
