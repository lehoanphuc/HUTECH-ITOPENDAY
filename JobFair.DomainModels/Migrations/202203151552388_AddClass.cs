namespace JobFair.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.STUDENT_EVENT", "StudentClass", c => c.String(maxLength: 50));
            AddColumn("dbo.SCHOLARSHIP_STUDENT", "StudentClass", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SCHOLARSHIP_STUDENT", "StudentClass");
            DropColumn("dbo.STUDENT_EVENT", "StudentClass");
        }
    }
}
