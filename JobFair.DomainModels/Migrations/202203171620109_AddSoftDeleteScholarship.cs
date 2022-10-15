namespace JobFair.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSoftDeleteScholarship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SCHOLARSHIP_STUDENT", "IsDeleted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SCHOLARSHIP_STUDENT", "IsDeleted");
        }
    }
}
