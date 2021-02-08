namespace ToDoList.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _TaskCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskCategoryEntities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TaskEntities", "Category_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.TaskEntities", "Category_Id");
            AddForeignKey("dbo.TaskEntities", "Category_Id", "dbo.TaskCategoryEntities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskEntities", "Category_Id", "dbo.TaskCategoryEntities");
            DropIndex("dbo.TaskEntities", new[] { "Category_Id" });
            DropColumn("dbo.TaskEntities", "Category_Id");
            DropTable("dbo.TaskCategoryEntities");
        }
    }
}
