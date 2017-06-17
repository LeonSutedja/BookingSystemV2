using System.Data.Entity.Migrations;

namespace LeonSutedja.BookingSystem.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BookingSystem.EntityFramework.BookingSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BookingSystem";
        }

        protected override void Seed(BookingSystem.EntityFramework.BookingSystemDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...
        }
    }
}
