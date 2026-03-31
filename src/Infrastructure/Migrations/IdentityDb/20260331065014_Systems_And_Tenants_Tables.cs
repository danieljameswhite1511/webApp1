using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.migrations.Identitydb
{
    /// <inheritdoc />
    public partial class SystemsAndTenantsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Systems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: false),
                    DeletedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: false),
                    DeletedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UriName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: false),
                    DeletedByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemUsers_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemTenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemId = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: false),
                    DeletedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SystemDefinitionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemTenants_Systems_SystemDefinitionId",
                        column: x => x.SystemDefinitionId,
                        principalTable: "Systems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SystemTenants_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemTenants_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemTenantUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemTenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    LastModifiedByUserId = table.Column<int>(type: "int", nullable: false),
                    DeletedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTenantUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemTenantUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemTenantUsers_SystemTenants_SystemTenantId",
                        column: x => x.SystemTenantId,
                        principalTable: "SystemTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemTenants_SystemDefinitionId",
                table: "SystemTenants",
                column: "SystemDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemTenants_SystemId",
                table: "SystemTenants",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemTenants_TenantId",
                table: "SystemTenants",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemTenantUsers_SystemTenantId",
                table: "SystemTenantUsers",
                column: "SystemTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemTenantUsers_UserId",
                table: "SystemTenantUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_SystemId",
                table: "SystemUsers",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_UserId",
                table: "SystemUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemTenantUsers");

            migrationBuilder.DropTable(
                name: "SystemUsers");

            migrationBuilder.DropTable(
                name: "SystemTenants");

            migrationBuilder.DropTable(
                name: "Systems");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
