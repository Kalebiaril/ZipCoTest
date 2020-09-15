using System;

namespace TestProject.WebAPI.Data.Db
{
    public class Account
    {
        /// <summary>
        /// Accounts primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Second name
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Account creation date
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}
