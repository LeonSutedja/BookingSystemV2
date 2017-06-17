using System;
using System.Data.Entity.Migrations;
using LeonSutedja.BookingSystem.Entities;
using LeonSutedja.BookingSystem.EntityFramework;

namespace LeonSutedja.BookingSystem.Tests
{
    public class BookingSystemInitialDataBuilder
    {
        public void Build(BookingSystemDbContext context)
        {
            context.Database.CreateIfNotExists();
            context.Customers.AddOrUpdate(
                p => p.Id, 
                new Customer("John", "Doe", new DateTime(1987, 11, 11)));
            context.SaveChanges();

            context.Rooms.AddOrUpdate(
                p => p.Id,
                new Room("DAVE", "David Room", "51 / 2 St Kilda Rd. 3155"));

            context.Schedules.AddOrUpdate(
                p => p.Id,
                new Schedule(
                    1, 
                    new DateTime(2016, 12, 12),
                    new DateTime(2016, 12, 12, 8, 0, 0),
                    new DateTime(2016, 12, 12, 18, 0, 0)));

            context.SaveChanges();
        }
    }
}