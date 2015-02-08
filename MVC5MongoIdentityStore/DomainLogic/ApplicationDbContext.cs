using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MongoIdentity.DomainLogic
{
    public class ApplicationDbContext : MongoDbContext
    {
        public ApplicationDbContext(string connectionName)
            : base(connectionName)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext("mongodb://localhost");
        }
    }
}