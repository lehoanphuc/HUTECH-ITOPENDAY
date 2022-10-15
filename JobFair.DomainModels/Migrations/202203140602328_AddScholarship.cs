namespace JobFair.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScholarship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SCHOLARSHIP_STUDENT",
                c => new
                    {
                        IdReg = c.Int(nullable: false, identity: true),
                        StudentCode = c.String(maxLength: 15),
                        StudentName = c.String(maxLength: 100),
                        StudentEmail = c.String(maxLength: 100),
                        StudentPhone = c.String(maxLength: 10),
                        StudentActivities = c.String(maxLength: 500),
                        StudentRole = c.Short(),
                        StudentPoint = c.Double(),
                        TimeReg = c.DateTime(),
                    })
                .PrimaryKey(t => t.IdReg);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SCHOLARSHIP_STUDENT");
        }
    }
}
