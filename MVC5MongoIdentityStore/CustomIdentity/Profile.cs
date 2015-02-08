using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Claims;
using MongoIdentity.Models;

namespace MongoIdentity.CustomIdentity
{
    
    public class Profile
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool ConfirmedEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Active { get; set; }
        public bool LockoutEnabled { get; set; }
        public int LockoutCount { get; set; }
        public DateTimeOffset LockoutEndDate { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string Telephone { get; set; }
        public bool TelephoneConfirmed { get; set; }
        public List<UserLoginInfo> LoginInfo { get; set; }
        public List<Claim> Claims { get; set; }
        public List<DeviceModel> Devices { get; set; }
 

        public Profile()
        {
            Claims = new List<Claim>();
            LoginInfo = new List<UserLoginInfo>();
        }
    }

    public class IdentityUser: IUser<string>
    {
        private Profile UserData;

        public IdentityUser()
        {
            UserData = new Profile();
        }

        public IdentityUser(Profile UserData)
        {
            this.UserData = UserData;
        }

        public string Id
        {
            get { return UserData._id; }
        }

        public string UserName
        {
            get
            {
                return UserData.UserName;
            }
            set
            {
                UserData.UserName = value ;
            }
        }

        public string PhoneNumber
        {
            get { return UserData.Phone; }
            set { UserData.Phone = value; }
        }


        public string Email
        {
            get
            {
                return UserData.Email;
            }
            set
            {
                UserData.Email = value;
            }
        }

        public bool ConfirmedEmail
        {
            get
            {
                return UserData.ConfirmedEmail;
            }
            set
            {
                UserData.ConfirmedEmail = value;
            }
        }

        public string PasswordHash { get { return UserData.Password; } set { UserData.Password = value; } }

        public Profile Profile { get { return UserData; } set { UserData = value; } }


        public System.Threading.Tasks.Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager UserManager)
        {
            ClaimsIdentity UserClaims = new ClaimsIdentity();
            UserClaims.AddClaims(Profile.Claims);
            
            return Task.FromResult<ClaimsIdentity>(UserClaims);
            
        }
    }


}