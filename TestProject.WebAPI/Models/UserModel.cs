using System;
using System.ComponentModel.DataAnnotations;

namespace TestProject.WebAPI.Models
{    
    public class UserModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Users name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Users email  - Is Unique
        /// </summary>
        [Required]
        [EmailAddress] 
        public string Email { get; set; }

        /// <summary>
        /// Monthly salary of the user
        /// </summary>
        [Range(1000, Int32.MaxValue, ErrorMessage = "Salary should not be less than 1000$")]
        public int MonthlySalary { get; set; }

        /// <summary>
        /// Monthly expeses of the user
        /// </summary>
        [Range(0, Int32.MaxValue)]
        public int MonthlyExpenses { get; set; }
    }
}
