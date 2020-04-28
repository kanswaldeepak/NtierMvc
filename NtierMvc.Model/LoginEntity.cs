using System;
using System.ComponentModel.DataAnnotations;

namespace NtierMvc.Model
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
        public int Id { get; set; }
        
        [Required(ErrorMessage = "You must enter an employee Gross Salary.")] 
        public string Password { get; set; }
        public DateTime LastLoginDate { get; set; }

        #endregion
    }
}
