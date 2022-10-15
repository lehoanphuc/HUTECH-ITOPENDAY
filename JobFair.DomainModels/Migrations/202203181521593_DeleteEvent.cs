namespace JobFair.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EVENT", "IsDeleted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EVENT", "IsDeleted");
        }
    }
}
