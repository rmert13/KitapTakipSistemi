namespace KitapTakipSistemi.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OduncModelEklendi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Oduncs",
                c => new
                    {
                        OduncId = c.Int(nullable: false, identity: true),
                        KullaniciAdi = c.String(nullable: false, maxLength: 100),
                        KitapId = c.Int(nullable: false),
                        OduncTarihi = c.DateTime(nullable: false),
                        IadeEdildi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OduncId)
                .ForeignKey("dbo.Kitaps", t => t.KitapId, cascadeDelete: true)
                .Index(t => t.KitapId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Oduncs", "KitapId", "dbo.Kitaps");
            DropIndex("dbo.Oduncs", new[] { "KitapId" });
            DropTable("dbo.Oduncs");
        }
    }
}
