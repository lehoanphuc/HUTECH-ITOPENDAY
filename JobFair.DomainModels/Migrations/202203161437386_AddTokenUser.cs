namespace JobFair.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTokenUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.USER", "Token", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.USER", "Token");
        }
    }
}
