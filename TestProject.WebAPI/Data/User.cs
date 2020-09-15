namespace TestProject.WebAPI.Data.Db
{
    public class User
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Users name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Users email  - Is Unique
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Monthly salary of the user
        /// </summary>
        public int MonthlySalary { get; set; }

        /// <summary>
        /// Monthly expeses of the user
        /// </summary>
        public int MonthlyExpenses { get; set; }
    }
}
