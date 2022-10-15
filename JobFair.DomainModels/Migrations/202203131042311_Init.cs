namespace JobFair.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.COMPANY",
                c => new
                    {
                        IdCompany = c.Int(nullable: false, identity: true),
                        ShowIndex = c.Int(),
                        CompanyName = c.String(maxLength: 50),
                        CompanyDescription = c.String(storeType: "ntext"),
                        CompanyVideo = c.String(maxLength: 500, unicode: false),
                        CompanyLink = c.String(maxLength: 255),
                        MeetingLink = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.IdCompany);
            
            CreateTable(
                "dbo.COMPANY_JOB",
                c => new
                    {
                        IdCompany = c.Int(nullable: false),
                        IdTitle = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdCompany, t.IdTitle })
                .ForeignKey("dbo.JOBTITLE", t => t.IdTitle)
                .ForeignKey("dbo.COMPANY", t => t.IdCompany)
                .Index(t => t.IdCompany)
                .Index(t => t.IdTitle);
            
            CreateTable(
                "dbo.JOBTITLE",
                c => new
                    {
                        IdTitle = c.Int(nullable: false, identity: true),
                        TitleName = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.IdTitle);
            
            CreateTable(
                "dbo.STUDENT_JOB",
                c => new
                    {
                        IdUser = c.Int(nullable: false),
                        IdCompany = c.Int(nullable: false),
                        IdTitle = c.Int(nullable: false),
                        DateApplied = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.IdUser, t.IdCompany, t.IdTitle })
                .ForeignKey("dbo.USER", t => t.IdUser)
                .ForeignKey("dbo.COMPANY_JOB", t => new { t.IdCompany, t.IdTitle })
                .Index(t => t.IdUser)
                .Index(t => new { t.IdCompany, t.IdTitle });
            
            CreateTable(
                "dbo.USER",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        IdCompany = c.Int(),
                        IdRole = c.Int(nullable: false),
                        Username = c.String(maxLength: 30, unicode: false),
                        Password = c.String(maxLength: 256, unicode: false),
                        Fullname = c.String(maxLength: 50),
                        ClassName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50, unicode: false),
                        Phone = c.String(maxLength: 11, unicode: false),
                    })
                .PrimaryKey(t => t.IdUser)
                .ForeignKey("dbo.COMPANY", t => t.IdCompany)
                .ForeignKey("dbo.ROLE", t => t.IdRole)
                .Index(t => t.IdCompany)
                .Index(t => t.IdRole);
            
            CreateTable(
                "dbo.ROLE",
                c => new
                    {
                        IdRole = c.Int(nullable: false),
                        RoleName = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.IdRole);
            
            CreateTable(
                "dbo.EVENT",
                c => new
                    {
                        IdEvent = c.Int(nullable: false, identity: true),
                        EventName = c.String(maxLength: 500),
                        EventDescription = c.String(maxLength: 500),
                        EventTime = c.DateTime(),
                        EventLocation = c.String(maxLength: 500),
                        IsShow = c.Boolean(),
                    })
                .PrimaryKey(t => t.IdEvent);
            
            CreateTable(
                "dbo.STUDENT_EVENT",
                c => new
                    {
                        IdReg = c.Int(nullable: false, identity: true),
                        IdEvent = c.Int(nullable: false),
                        StudentCode = c.String(maxLength: 50),
                        StudentName = c.String(maxLength: 50),
                        StudentPhone = c.String(maxLength: 50),
                        StudentEmail = c.String(maxLength: 50),
                        StudentQuestion = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.IdReg)
                .ForeignKey("dbo.EVENT", t => t.IdEvent)
                .Index(t => t.IdEvent);
            
            CreateTable(
                "dbo.SETTING",
                c => new
                    {
                        SettingKey = c.String(nullable: false, maxLength: 100, unicode: false),
                        SettingValue = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.SettingKey);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.STUDENT_EVENT", "IdEvent", "dbo.EVENT");
            DropForeignKey("dbo.COMPANY_JOB", "IdCompany", "dbo.COMPANY");
            DropForeignKey("dbo.STUDENT_JOB", new[] { "IdCompany", "IdTitle" }, "dbo.COMPANY_JOB");
            DropForeignKey("dbo.STUDENT_JOB", "IdUser", "dbo.USER");
            DropForeignKey("dbo.USER", "IdRole", "dbo.ROLE");
            DropForeignKey("dbo.USER", "IdCompany", "dbo.COMPANY");
            DropForeignKey("dbo.COMPANY_JOB", "IdTitle", "dbo.JOBTITLE");
            DropIndex("dbo.STUDENT_EVENT", new[] { "IdEvent" });
            DropIndex("dbo.USER", new[] { "IdRole" });
            DropIndex("dbo.USER", new[] { "IdCompany" });
            DropIndex("dbo.STUDENT_JOB", new[] { "IdCompany", "IdTitle" });
            DropIndex("dbo.STUDENT_JOB", new[] { "IdUser" });
            DropIndex("dbo.COMPANY_JOB", new[] { "IdTitle" });
            DropIndex("dbo.COMPANY_JOB", new[] { "IdCompany" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.SETTING");
            DropTable("dbo.STUDENT_EVENT");
            DropTable("dbo.EVENT");
            DropTable("dbo.ROLE");
            DropTable("dbo.USER");
            DropTable("dbo.STUDENT_JOB");
            DropTable("dbo.JOBTITLE");
            DropTable("dbo.COMPANY_JOB");
            DropTable("dbo.COMPANY");
        }
    }
}
