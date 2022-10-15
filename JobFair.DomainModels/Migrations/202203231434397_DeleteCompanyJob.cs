namespace JobFair.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCompanyJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.COMPANY_JOB", "IsDeleted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.COMPANY_JOB", "IsDeleted");
        }
    }
}
