using System;
using System.ComponentModel.DataAnnotations;

namespace NtierMvc.Model.Account
{
    /// <summary>
    /// Purpose: Data Contract Entity Model Class [LoginEntity] for the table [HR].[Login].
    /// </summary>
    public class LoginEntity : IDisposable
    {
        #region Class Public Methods

        /// <summary>
        /// Purpose: Implements the IDispose interface.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Class Property Declarations

        [Required(ErrorMessage = "You must enter an User/EMail ID.")] 
        public string UserName { get; set; }
        [Required] 
        public string Password { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string ErrorMessage { get; set; }
        #endregion
    }
}
