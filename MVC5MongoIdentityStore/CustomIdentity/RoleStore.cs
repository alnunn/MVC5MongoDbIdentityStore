using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using MongoIdentity.CustomIdentity;
using MongoIdentity.DomainLogic;
using System.Threading.Tasks;

namespace MongoIdentity.CustomIdentity
{
    public class RoleStore: IRoleStore<Role,string>
    {
        IDbContext Database;

        public RoleStore(IDbContext database)
        {
            this.Database = database;
        }

        public System.Threading.Tasks.Task CreateAsync(Role role)
        {
            Database.SaveRole(role);
            return Task.FromResult<object>(null);
        }

        public System.Threading.Tasks.Task DeleteAsync(Role role)
        {
            role.Active = 'I';
            Database.SaveRole(role);
            return Task.FromResult<object>(null);
        }

        public System.Threading.Tasks.Task<Role> FindByIdAsync(string roleId)
        {
            return Task.FromResult<Role>(Database.FindRole(roleId));
        }

        public System.Threading.Tasks.Task<Role> FindByNameAsync(string roleName)
        {
            return Task.FromResult<Role>(Database.FindRoleByName(roleName));
        }

        public System.Threading.Tasks.Task UpdateAsync(Role role)
        {
            Database.SaveRole(role);
            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            Database = null;
        }
    }
}