using FluentMigrator;

namespace DAL.Migration
{
    [Migration(20212208220000)]
    public class Migration_20212208220000 : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("category")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("parentid").AsInt64().Nullable();
        }

        public override void Down()
        {
            Delete.Table("category");
        }
    }
}