namespace KitapTakipSistemi.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFotoUrlToKitap : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kitaps", "FotoUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kitaps", "FotoUrl");
        }
    }
}
