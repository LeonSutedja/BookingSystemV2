using System.Data.Common;
using System.Data.Entity;
using Abp.EntityFramework;
using LeonSutedja.BookingSystem.Entities;

namespace LeonSutedja.BookingSystem.EntityFramework
{
    public class BookingSystemDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...
        public virtual IDbSet<Customer> Customers { get; set; } 

        public virtual IDbSet<Room> Rooms { get; set; }
         
        public virtual IDbSet<Schedule> Schedules { get; set; }

        public virtual IDbSet<Appointment> Appointments { get; set; } 

        //Example:
        //public virtual IDbSet<User> Users { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public BookingSystemDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in BookingSystemDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of BookingSystemDbContext since ABP automatically handles it.
         */
        public BookingSystemDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public BookingSystemDbContext(DbConnection connection) : base(connection, true)
        {
            
        }
    }
}
