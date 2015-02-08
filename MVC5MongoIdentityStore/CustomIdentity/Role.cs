using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoIdentity.CustomIdentity
{
    public class Role: IRole<string>
    {
        public Role()
        {

        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public char Active
        {
            get;
            set;
        }
    }
}