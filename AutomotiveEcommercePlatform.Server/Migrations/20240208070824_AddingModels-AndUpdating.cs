using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomotiveEcommercePlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddingModelsAndUpdating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Traders",
                columns: table => new
                {
                    TraderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, computedColumnSql: "[FirstName] + ' ' + [LastName]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traders", x => x.TraderId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, computedColumnSql: "[FirstName] + ' ' + [LastName]"),
                    TraderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ApplicationUserId);
                    table.ForeignKey(
                        name: "FK_Users_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Traders_TraderId",
                        column: x => x.TraderId,
                        principalTable: "Traders",
                        principalColumn: "TraderId");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UserApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Users_UserApplicationUserId",
                        column: x => x.UserApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TraderRating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    TraderId = table.Column<int>(type: "int", nullable: false),
                    UserApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraderRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraderRating_Traders_TraderId",
                        column: x => x.TraderId,
                        principalTable: "Traders",
                        principalColumn: "TraderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraderRating_Users_UserApplicationUserId",
                        column: x => x.UserApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModelYear = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "Decimal(15,2)", nullable: false),
                    CarCategory = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CarImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InStock = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    TraderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_Traders_TraderId",
                        column: x => x.TraderId,
                        principalTable: "Traders",
                        principalColumn: "TraderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarReview_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarReview_Users_UserApplicationUserId",
                        column: x => x.UserApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => new { x.CarId, x.CartId });
                    table.UniqueConstraint("AK_Cart_CartId", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Cart_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cart_Users_CartId",
                        column: x => x.CartId,
                        principalTable: "Users",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarsInTheCart",
                columns: table => new
                {
                    CartId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarsInTheCart", x => new { x.CarId, x.CartId });
                    table.ForeignKey(
                        name: "FK_CarsInTheCart_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarsInTheCart_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarReview_CarId",
                table: "CarReview",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarReview_UserApplicationUserId",
                table: "CarReview",
                column: "UserApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OrderId",
                table: "Cars",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_TraderId",
                table: "Cars",
                column: "TraderId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsInTheCart_CartId",
                table: "CarsInTheCart",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserApplicationUserId",
                table: "Order",
                column: "UserApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TraderRating_TraderId",
                table: "TraderRating",
                column: "TraderId");

            migrationBuilder.CreateIndex(
                name: "IX_TraderRating_UserApplicationUserId",
                table: "TraderRating",
                column: "UserApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TraderId",
                table: "Users",
                column: "TraderId",
                unique: true,
                filter: "[TraderId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarReview");

            migrationBuilder.DropTable(
                name: "CarsInTheCart");

            migrationBuilder.DropTable(
                name: "TraderRating");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Traders");
        }
    }
}
