using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebSignalRFormAuthentication.Models
{
    public class DataContext : DbContext
    {
        public DataContext(): base("SignLRDemoFormAutTwo")
        {
        }
        public DbSet<Register> Registers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MyDbContextInitializer());

            base.OnModelCreating(modelBuilder);
        }
    }
    public class MyDbContextInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext dbContext)
        {
            // seed data
            Register register1 = new Register();
            register1.UserName = "abc01";
            register1.Password = "123456";
            register1.ConfirmPassword = "123456";
            dbContext.Registers.Add(register1);
            dbContext.SaveChanges();
            base.Seed(dbContext);
        }
    }
}