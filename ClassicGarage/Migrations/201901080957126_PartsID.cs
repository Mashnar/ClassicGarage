namespace ClassicGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PartsID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parts", "OwnerID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Parts", "OwnerID");
        }
    }
}
