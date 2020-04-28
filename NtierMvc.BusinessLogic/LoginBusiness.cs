using System;
using System.Collections.Generic;
using NtierMvc.Common;
using NtierMvc.DataAccess;
using NtierMvc.Model.Account;

namespace NtierMvc.BusinessLogic
{
    /// <summary>
    /// Purpose: Business Logic Class [LoginBusiness] for handling the business constrains on table [HR].[Login].
    /// </summary>
    public class LoginBusiness : IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;

        #endregion

        #region Class Methods

        public bool InsertEmployee(LoginEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new LoginRepository())
                {
                    bOpDoneSuccessfully = repository.Insert(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:LoginBusiness::InsertEmployee::Error occured.", ex);
            }
        }

        public bool UpdateEmployee(LoginEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new LoginRepository())
                {
                    bOpDoneSuccessfully = repository.Update(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:LoginBusiness::UpdateEmployee::Error occured.", ex);
            }
        }

        public bool DeleteEmployeeById(int empId)
        {
            try
            {
                using (var repository = new LoginRepository())
                {
                    return repository.DeleteById(empId);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:LoginBusiness::DeleteEmployeeById::Error occured.", ex);
            }
        }

        public LoginEntity SelectEmployeeById(int empId)
        {
            try
            {
                LoginEntity returnedEntity;
                using (var repository = new LoginRepository())
                {
                    returnedEntity = repository.SelectById(empId);
                    //if (returnedEntity != null)
                    //    returnedEntity.NetSalary = GetNetSalary(returnedEntity.GrossSalary, returnedEntity.Age);
                }

                return returnedEntity;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:LoginBusiness::SelectEmployeeById::Error occured.", ex);
            }
        }

        public List<LoginEntity> SelectAllLogin()
        {
            var returnedEntities = new List<LoginEntity>();

            try
            {
                using (var repository = new LoginRepository())
                {
                    foreach (var entity in repository.SelectAll())
                    {
                        //entity.NetSalary = GetNetSalary(entity.GrossSalary, entity.Age);
                        returnedEntities.Add(entity);
                    }
                }

                return returnedEntities;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:LoginBusiness::SelectAllLogin::Error occured.", ex);
            }
        }
        
        private decimal GetNetSalary(decimal grossSalary, int age)
        {
            var netSalary = grossSalary;

            if (age < 30)
            {
                //Deduct 50% from the Gross Salary
                netSalary = grossSalary - grossSalary * 0.5M;
            }
            else if (age < 40)
            {
                //Deduct 40% from the Gross Salary
                netSalary = grossSalary - grossSalary * 0.4M;
            }
            else if (age < 50)
            {
                //Deduct 30% from the Gross Salary
                netSalary = grossSalary - grossSalary * 0.3M;
            }
            else if (age < 60)
            {
                //Deduct 20% from the Gross Salary
                netSalary = grossSalary - grossSalary * 0.2M;
            }

            return Math.Round(netSalary, 2);
        }

        public LoginBusiness()
        {
            _loggingHandler = new LoggingHandler();
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
    }
}
