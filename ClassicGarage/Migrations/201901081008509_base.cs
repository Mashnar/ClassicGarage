namespace ClassicGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _base : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Parts", "OwnerID");
            AddForeignKey("dbo.Parts", "OwnerID", "dbo.Owner", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parts", "OwnerID", "dbo.Owner");
            DropIndex("dbo.Parts", new[] { "OwnerID" });
        }
    }
}
