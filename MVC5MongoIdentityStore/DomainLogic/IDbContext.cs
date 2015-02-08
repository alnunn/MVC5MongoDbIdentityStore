using System;
using MongoIdentity.CustomIdentity;

namespace MongoIdentity.DomainLogic
{
    public interface IDbContext
    {
        MongoIdentity.CustomIdentity.Profile FindProfile(string id);
        MongoIdentity.CustomIdentity.Profile FindProfileByUserName(string UserName);
        void SaveProfile(MongoIdentity.CustomIdentity.Profile User);

        MongoIdentity.CustomIdentity.Role FindRole(string id);
        MongoIdentity.CustomIdentity.Role FindRoleByName(string name);
        void SaveRole(MongoIdentity.CustomIdentity.Role role);
        Profile FindProfileByEmail(string email);

        Profile FindProfileByLogin(Microsoft.AspNet.Identity.UserLoginInfo login);
    }
}
