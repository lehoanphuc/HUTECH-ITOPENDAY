namespace JobFair.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMultiRoles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SCHOLARSHIP_STUDENT", "StudentRoles", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SCHOLARSHIP_STUDENT", "StudentRoles");
        }
    }
}
