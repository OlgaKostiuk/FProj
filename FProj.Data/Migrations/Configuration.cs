namespace FProj.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<FProj.Data.FProjContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FProj.Data.FProjContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (!context.User.Any())
            {
                string salt = Crypto.GenerateSalt();
                string password = Crypto.HashPassword("12345" + salt);
                UserAccount accountAdmin = new UserAccount()
                {
                    Email = "admin@gmail.com",
                    Password = password,
                    Salt = salt
                };

                context.UserAccount.Add(accountAdmin);
                context.SaveChanges();

                User admin = new User()
                {
                    FirtsName = "Admin",
                    Id = accountAdmin.Id,
                    IsDeleted = false,
                    LastName = "Admin",
                    Login = "Admin"
                };

                context.User.Add(admin);
                context.SaveChanges();
            }
        }
    }
}
