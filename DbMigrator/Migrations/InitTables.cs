using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(20230819)]
    public class InitTables : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException("Only up");
        }

        public override void Up()
        {
            const string sql = @"

                CREATE TABLE short_link (

	                id UUID PRIMARY KEY,

	                full_url TEXT NOT NULL,

	                short_url TEXT NOT NULL,
	                created_date  TIME NOT NULL
                );

                CREATE TABLE tag (

	                id UUID PRIMARY KEY,

	                name text NOT NULL
                );

                CREATE TABLE link_tag (
	                short_link_id UUID  NOT NULL,
	                tag_id UUID  NOT NULL
                );

                ALTER TABLE link_tag ADD FOREIGN KEY (short_link_id) REFERENCES short_link;

                ALTER TABLE link_tag ADD FOREIGN KEY (tag_id) REFERENCES tag;


            ";

            Execute.Sql(sql);
        }
    }
}
