namespace LeonSutedja.BookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAppointment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ScheduleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ScheduleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.Appointments", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Appointments", new[] { "ScheduleId" });
            DropIndex("dbo.Appointments", new[] { "CustomerId" });
            DropTable("dbo.Appointments");
        }
    }
}
