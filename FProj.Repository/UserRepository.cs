using System;
using FProj.Api;
using FProj.Data;
using FProj.Repository.Base;
using System.Data;
using System.Linq;
using System.Web.Helpers;

namespace FProj.Repository
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(FProjContext context) : base(context)
        {
        }

        public UserApi Default()
        {
            return new UserApi();
        }

        public UserApi Add(UserApi user, string password)
        {
            if (_dbContext.UserAccount.Any(x => x.Email == user.Email)) throw new Exception("Email has already been used.");

            var userData = ApiToData.UserApiToData(user);
            var salt = Crypto.GenerateSalt();
            var account = new UserAccount()
            {
                Email = user.Email,
                Password = Crypto.HashPassword(password) + salt,
                Salt = salt
            };
            _dbContext.UserAccount.Add(account);
            _dbContext.SaveChanges();

            userData.Id = account.Id;
            _dbContext.User.Add(userData);
            _dbContext.SaveChanges();

            return DataToApi.UserToApi(userData);
        }
    }
}
