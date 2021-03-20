using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoTaskCategories",
                columns: table => new
                {
                    TodoTaskCategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TodoTaskCategoryName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoTaskCategories", x => x.TodoTaskCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "TodoTaskPriorities",
                columns: table => new
                {
                    TodoTaskPriorityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TodoTaskPriorityName = table.Column<string>(type: "TEXT", nullable: false),
                    Sort = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoTaskPriorities", x => x.TodoTaskPriorityId);
                });

            migrationBuilder.CreateTable(
                name: "TodoTasks",
                columns: table => new
                {
                    TodoTaskId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TodoTaskName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    IsDone = table.Column<bool>(type: "INTEGER", nullable: false),
                    TodoTaskPriorityId = table.Column<int>(type: "INTEGER", nullable: false),
                    TodoTaskCategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoTasks", x => x.TodoTaskId);
                    table.ForeignKey(
                        name: "FK_TodoTasks_TodoTaskCategories_TodoTaskCategoryId",
                        column: x => x.TodoTaskCategoryId,
                        principalTable: "TodoTaskCategories",
                        principalColumn: "TodoTaskCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TodoTasks_TodoTaskPriorities_TodoTaskPriorityId",
                        column: x => x.TodoTaskPriorityId,
                        principalTable: "TodoTaskPriorities",
                        principalColumn: "TodoTaskPriorityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasks_IsDone",
                table: "TodoTasks",
                column: "IsDone");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasks_TodoTaskCategoryId",
                table: "TodoTasks",
                column: "TodoTaskCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasks_TodoTaskName",
                table: "TodoTasks",
                column: "TodoTaskName");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasks_TodoTaskPriorityId",
                table: "TodoTasks",
                column: "TodoTaskPriorityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoTasks");

            migrationBuilder.DropTable(
                name: "TodoTaskCategories");

            migrationBuilder.DropTable(
                name: "TodoTaskPriorities");
        }
    }
}
