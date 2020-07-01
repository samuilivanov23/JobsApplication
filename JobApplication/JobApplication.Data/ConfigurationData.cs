using System;

namespace JobApplication.Data
{
    /// <summary>
    /// This class holds the connection string for connecting to the data base
    /// </summary>
    public static class ConfigurationData
    {
        /// <summary>
        /// The connection string, used for connecting to the database
        /// The second one is for the other person, working on the project
        /// (we know we should have added it in the gitignore...)
        /// </summary>
        //public const string ConnectionString = "Server=GAS-LAPTOP; Database=JobApp; Integrated Security=true";
        public const string ConnectionString = "Server=SVI-LAPTOP; Database=JobApp; Integrated Security=true";
    }
}
