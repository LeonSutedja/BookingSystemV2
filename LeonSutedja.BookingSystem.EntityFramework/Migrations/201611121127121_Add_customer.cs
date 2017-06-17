namespace LeonSutedja.BookingSystem.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Add_customer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 40),
                        LastName = c.String(nullable: false, maxLength: 40),
                        DateOfBirth = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 100),
                        MobileNo = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
