using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using MongoIdentity.DomainLogic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;


namespace MongoIdentity.CustomIdentity
{
    public class ProfileStore : IUserStore<IdentityUser> ,IUserPasswordStore<IdentityUser>,IUserEmailStore<IdentityUser>,IUserLockoutStore<IdentityUser,string>, IUserTwoFactorStore<IdentityUser,string>, IUserPhoneNumberStore<IdentityUser, string>, IUserLoginStore<IdentityUser,string>, IUserClaimStore<IdentityUser>
    {

        private IDbContext DbContext;

        public ProfileStore()
        {
            DbContext = new MongoDbContext("mongodb://localhost");
        }

        public ProfileStore(IDbContext Context)
        {
            DbContext = Context;
        }


        public System.Threading.Tasks.Task CreateAsync(IdentityUser user)
        {
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }

        public System.Threading.Tasks.Task DeleteAsync(IdentityUser user)
        {
            user.Profile.Active = "I";
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }

        public System.Threading.Tasks.Task<IdentityUser> FindByIdAsync(string userId)
        {
            var FoundProfile = DbContext.FindProfile(userId);
            var UserProfile = new IdentityUser(FoundProfile);

            return Task.FromResult<IdentityUser>(UserProfile);
        }

        public System.Threading.Tasks.Task<IdentityUser> FindByNameAsync(string userName)
        {
            var FoundProfile = DbContext.FindProfileByUserName(userName);
            var UserProfile = new IdentityUser(FoundProfile);

            return Task.FromResult<IdentityUser>(UserProfile);
        }

        public System.Threading.Tasks.Task UpdateAsync(IdentityUser user)
        {
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            DbContext = null;
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            return Task.FromResult<string>(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }

        public Task<IdentityUser> FindByEmailAsync(string email)
        {
            var FoundProfile = DbContext.FindProfileByEmail(email);
            var User = new IdentityUser(FoundProfile);
            return Task.FromResult<IdentityUser>(User);
        }

        public Task<string> GetEmailAsync(IdentityUser user)
        {
            return Task.FromResult<string>(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {

            var FoundProfile = DbContext.FindProfileByEmail(user.Email);
            if(FoundProfile.Email == user.Email)
            {
                return Task.FromResult<bool>(true);
            }
            else
            {
                return Task.FromResult<bool>(false);
            }
        }

        public Task SetEmailAsync(IdentityUser user, string email)
        {
            user.Email = email;
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            user.ConfirmedEmail = confirmed;
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }

        public Task<int> GetAccessFailedCountAsync(IdentityUser user)
        {
            return Task.FromResult<int>(user.Profile.LockoutCount);
        }

        public Task<bool> GetLockoutEnabledAsync(IdentityUser user)
        {
            return Task.FromResult<bool>(user.Profile.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(IdentityUser user)
        {
            return Task.FromResult<DateTimeOffset>(user.Profile.LockoutEndDate);
        }

        public Task<int> IncrementAccessFailedCountAsync(IdentityUser user)
        {
            user.Profile.LockoutCount += 1;
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<int>(user.Profile.LockoutCount);
        }

        public Task ResetAccessFailedCountAsync(IdentityUser user)
        {
            user.Profile.LockoutCount = 0;
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }

        public Task SetLockoutEnabledAsync(IdentityUser user, bool enabled)
        {
            user.Profile.LockoutEnabled = true;
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }

        public Task SetLockoutEndDateAsync(IdentityUser user, DateTimeOffset lockoutEnd)
        {
            user.Profile.LockoutEndDate = lockoutEnd;
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }

        public Task<bool> GetTwoFactorEnabledAsync(IdentityUser user)
        {
            return Task.FromResult<bool>(user.Profile.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(IdentityUser user, bool enabled)
        {
            user.Profile.TwoFactorEnabled = enabled;
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }

        public Task<string> GetPhoneNumberAsync(IdentityUser user)
        {
            return Task.FromResult<string>(user.Profile.Telephone);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(IdentityUser user)
        {
            return Task.FromResult<bool>(user.Profile.TelephoneConfirmed);
        }

        public Task SetPhoneNumberAsync(IdentityUser user, string phoneNumber)
        {
            user.Profile.Telephone = phoneNumber;
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }

        public Task SetPhoneNumberConfirmedAsync(IdentityUser user, bool confirmed)
        {
            user.Profile.TelephoneConfirmed = confirmed;
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }

        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            user.Profile.LoginInfo.Add(login);
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }

        public Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
            var FoundProfile = DbContext.FindProfileByLogin(login);
            var NewFoundUser = new IdentityUser(FoundProfile);
            return Task.FromResult<IdentityUser>(NewFoundUser);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            return Task.FromResult<IList<UserLoginInfo>>(user.Profile.LoginInfo);
        }

        public Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            user.Profile.LoginInfo.Remove(login);
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }

        public Task AddClaimAsync(IdentityUser user, Claim claim)
        {
            user.Profile.Claims.Add(claim);
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            return Task.FromResult<IList<Claim>>(user.Profile.Claims);
        }

        public Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            user.Profile.Claims.Remove(claim);
            DbContext.SaveProfile(user.Profile);
            return Task.FromResult<object>(null);
        }
    }
}