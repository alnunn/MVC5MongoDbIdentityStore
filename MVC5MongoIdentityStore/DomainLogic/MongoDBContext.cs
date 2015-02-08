using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoIdentity.CustomIdentity;
using MongoDB.Driver.Builders;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Owin;
using MongoIdentity.Models;

namespace MongoIdentity.DomainLogic
{
    public class MongoDbContext : IDbContext, IDisposable
    {
        public MongoDatabase Database { get; set; }
        private MongoClient Client;
        private MongoServer Server;

        public MongoDbContext(string Connection)
        {
            Client = new MongoClient(Connection);
            Server = Client.GetServer();
            Database = Server.GetDatabase("MongoIdentity");
        
        }

        public static MongoDbContext Create(IdentityFactoryOptions<MongoDbContext> options, IOwinContext context)
        {
            var Context = new MongoDbContext("mongodb://localhost");
            return Context;
        }

        public void SaveProfile(Profile User)
        {
            Database.GetCollection("Profiles").Save<Profile>(User);
        }

        public Profile FindProfile(string id)
        {
            var Search = Query.EQ("_id",ObjectId.Parse(id));
            var FoundProfile = Database.GetCollection<Profile>("Profiles").FindOne(Search);
            return FoundProfile;
        }

        public Profile FindProfileByUserName(string UserName)
        {
            var Search = Query.EQ("UserName", UserName);
            var FoundProfile = Database.GetCollection<Profile>("Profiles").FindOne(Search);
            return FoundProfile;

        }

        public void SaveRole(Role role)
        {
            Database.GetCollection("Roles").Save<Role>(role);
        }

        public Role FindRole(string id)
        {
            var Search = Query.EQ("Id", id);
            var FoundRole = Database.GetCollection<Role>("Roles").FindOne(Search);
            return FoundRole;
        }

        public Role FindRoleByName(string name)
        {
            var Search = Query.EQ("Name", name);
            var FoundRole = Database.GetCollection<Role>("Roles").FindOne(Search);
            return FoundRole;
        }

        public Profile FindProfileByEmail(string email)
        {
            var Search = Query.EQ("Email", email);
            var FoundRole = Database.GetCollection<Profile>("Profiles").FindOne(Search);
            return FoundRole;
        }

        public Profile FindProfileByLogin(UserLoginInfo userInfo)
        {
            var Search = Query.ElemMatch("LoginInfo",Query.And(Query.EQ("LoginProvider",userInfo.LoginProvider),Query.EQ("ProviderKey",userInfo.ProviderKey)));
            var FoundUser = Database.GetCollection<Profile>("Profiles").FindOne(Search);
            return FoundUser;
        }

        public void SaveDevices(IEnumerable<DeviceModel> Devices)
        {
            foreach (var Device in Devices)
            {
                Database.GetCollection("Devices").Save<DeviceModel>(Device);
            }
        }

        public IEnumerable<DeviceModel> GetDevicesByOwner(string OwnerId)
        {
            var Search = Query.EQ("OwnerId", OwnerId);
            var FoundDevices = Database.GetCollection<DeviceModel>("Devices").Find(Search);
            return FoundDevices;
        }


        public void Dispose()
        {
            Database = null;
        }
    }
}