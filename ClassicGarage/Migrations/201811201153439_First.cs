namespace ClassicGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Repair", "Cost", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Repair", "Cost", c => c.Int(nullable: false));
        }
    }
}
