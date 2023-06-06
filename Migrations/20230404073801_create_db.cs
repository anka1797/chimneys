using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chimneys.Migrations
{
    public partial class create_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Name_var",
                columns: table => new
                {
                    Id_var = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name_var = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Name_var", x => x.Id_var);
                });

            migrationBuilder.CreateTable(
                name: "Variant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Num_variants = table.Column<int>(type: "INTEGER", nullable: false),
                    num_wall = table.Column<int>(type: "INTEGER", nullable: false),
                    h = table.Column<double>(type: "REAL", nullable: false),
                    d_vn_s = table.Column<double>(type: "REAL", nullable: false),
                    d_vn_f = table.Column<double>(type: "REAL", nullable: false),
                    l1 = table.Column<double>(type: "REAL", nullable: false),
                    l2 = table.Column<double>(type: "REAL", nullable: false),
                    l3 = table.Column<double>(type: "REAL", nullable: false),
                    l4 = table.Column<double>(type: "REAL", nullable: false),
                    l5 = table.Column<double>(type: "REAL", nullable: false),
                    y1 = table.Column<double>(type: "REAL", nullable: false),
                    y2 = table.Column<double>(type: "REAL", nullable: false),
                    y3 = table.Column<double>(type: "REAL", nullable: false),
                    y4 = table.Column<double>(type: "REAL", nullable: false),
                    L = table.Column<double>(type: "REAL", nullable: false),
                    T = table.Column<double>(type: "REAL", nullable: false),
                    C_co2 = table.Column<double>(type: "REAL", nullable: false),
                    C_h2o = table.Column<double>(type: "REAL", nullable: false),
                    T_okr = table.Column<double>(type: "REAL", nullable: false),
                    V = table.Column<double>(type: "REAL", nullable: false),
                    h_2 = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variant", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Name_var");

            migrationBuilder.DropTable(
                name: "Variant");
        }
    }
}
