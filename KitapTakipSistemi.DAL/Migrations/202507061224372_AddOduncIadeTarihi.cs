namespace KitapTakipSistemi.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOduncIadeTarihi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Oduncs", "OduncIadeTarihi", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Oduncs", "OduncIadeTarihi");
        }
    }
}
