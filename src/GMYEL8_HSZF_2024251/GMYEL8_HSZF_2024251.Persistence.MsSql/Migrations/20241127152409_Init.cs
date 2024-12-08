using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMYEL8_HSZF_2024251.Persistence.MsSql.Migrations
{
	/// <inheritdoc />
	public partial class Init : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "TaxiCars",
				columns: table => new
				{
					LicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Driver = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TaxiCars", x => x.LicensePlate);
				});

			migrationBuilder.CreateTable(
				name: "Services",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					TaxiCarId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					From = table.Column<string>(type: "nvarchar(max)", nullable: false),
					To = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Distance = table.Column<int>(type: "int", nullable: false),
					PaidAmount = table.Column<int>(type: "int", nullable: false),
					FareStartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Services", x => x.Id);
					table.ForeignKey(
						name: "FK_Services_TaxiCars_TaxiCarId",
						column: x => x.TaxiCarId,
						principalTable: "TaxiCars",
						principalColumn: "LicensePlate",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Services_TaxiCarId",
				table: "Services",
				column: "TaxiCarId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Services");

			migrationBuilder.DropTable(
				name: "TaxiCars");
		}
	}
}
