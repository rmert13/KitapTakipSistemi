namespace KitapTakipSistemi.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kitaps",
                c => new
                    {
                        KitapId = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 150),
                        Yazar = c.String(nullable: false, maxLength: 100),
                        YayinYili = c.Int(nullable: false),
                        Stok = c.Int(nullable: false),
                        TurId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KitapId)
                .ForeignKey("dbo.Turs", t => t.TurId, cascadeDelete: true)
                .Index(t => t.TurId);
            
            CreateTable(
                "dbo.Turs",
                c => new
                    {
                        TurId = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.TurId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kitaps", "TurId", "dbo.Turs");
            DropIndex("dbo.Kitaps", new[] { "TurId" });
            DropTable("dbo.Turs");
            DropTable("dbo.Kitaps");
        }
    }
}
