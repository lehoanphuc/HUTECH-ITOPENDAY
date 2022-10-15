namespace JobFair.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditStudentActivities : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SCHOLARSHIP_STUDENT", "StudentActivities", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SCHOLARSHIP_STUDENT", "StudentActivities", c => c.String(maxLength: 500));
        }
    }
}
