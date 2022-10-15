namespace JobFair.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.USER", "IsDeleted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.USER", "IsDeleted");
        }
    }
}
