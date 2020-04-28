using System;
using System.Data;
using NtierMvc.DataAccess.Pool;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.Model.MRM;
using NtierMvc.Model;

namespace NtierMvc.BusinessLogic.Worker
{
    public class MRMWorker : IMRMWorker
    {
        Repository _repository = new Repository();

        //public EmployeeEntity DT2Cust(DataTable dtRecord)
        //{
        //    EmployeeEntity oCust = new EmployeeEntity();
        //    try
        //    {
        //        if (dtRecord.Rows.Count > 0)
        //        {
        //            oCust.UnitNo = Convert.ToString(dtRecord.Rows[0]["Id"]);
        //            oCust.UserInitial = Convert.ToString(dtRecord.Rows[0]["UserName"]);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
        //    }
        //    return oCust;
        //}

        public PRDetailEntity GetPRDetailsPopup(PRDetailEntity Model)
        {
            try
            {
                DataSet ds = _repository.GetPRDetailsPopup(Model);
                //Model = ExtractEmployeeDetailsEntity(ds, Model);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    DataRow dr1 = dt1.Rows[0];
                    //Model.Id = dt1.Rows[0]["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dt1.Rows[0]["Id"]);

                    Model.DeptId = dr1.IsNull("DeptId") ? 0 : Convert.ToInt32(dr1["DeptId"]);
                    Model.PRno = dr1.IsNull("PrNo") ? "" : Convert.ToString(dr1["PrNo"]);
                    Model.DeptName = dr1.IsNull("DeptName") ? "" : Convert.ToString(dr1["DeptName"]);
                    Model.PRSetno = dr1.IsNull("PRSetno") ? 0 : Convert.ToInt32(dr1["PRSetno"]);
                    
                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return Model;
        }

        public string SavePRDetailsList(BulkUploadEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SavePRDetailsList(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

    }

}
