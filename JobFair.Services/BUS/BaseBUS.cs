using JobFair.DomainModels;
using System;

namespace JobFair.Services.BUS
{
    public class BaseBUS : IDisposable
    {
        protected DomainModel db;

        // Constructor empty
        public BaseBUS()
        {
            // Init obj
            db = new DomainModel();
            db.Database.CreateIfNotExists();
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            // Close connection
            if (db != null)
            {
                db.Dispose();
            }
        }
    }
}
