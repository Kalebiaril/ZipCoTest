using System;
using System.ComponentModel.DataAnnotations;

namespace TestProject.WebAPI.Models
{
    public class AccountModel
    {
        /// <summary>
        /// User
        /// </summary>
        [Required]
        public UserModel User { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [StringLength(60, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string FirstName { get; set; }

        /// <summary>
        /// Second name
        /// </summary>
        [StringLength(60, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string SecondName { get; set; }

        /// <summary>
        /// Account creation date
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}
