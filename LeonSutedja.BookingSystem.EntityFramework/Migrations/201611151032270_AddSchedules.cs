namespace LeonSutedja.BookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSchedules : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        RoomId = c.Int(nullable: false),
                        IsFull = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Schedules", new[] { "RoomId" });
            DropTable("dbo.Schedules");
        }
    }
}
