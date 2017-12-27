namespace Repository.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockItem : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.StockItemListViewModels", newName: "StockItems");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.StockItems", newName: "StockItemListViewModels");
        }
    }
}
