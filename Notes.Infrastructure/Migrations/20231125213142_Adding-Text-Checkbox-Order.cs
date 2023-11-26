using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingTextCheckboxOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Label_LabelId",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteCheckbox_Note_NoteId",
                table: "NoteCheckbox");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteText_Note_NoteId",
                table: "NoteText");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteText",
                table: "NoteText");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteCheckbox",
                table: "NoteCheckbox");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Note",
                table: "Note");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Label",
                table: "Label");

            migrationBuilder.RenameTable(
                name: "NoteText",
                newName: "NoteTexts");

            migrationBuilder.RenameTable(
                name: "NoteCheckbox",
                newName: "NoteCheckboxes");

            migrationBuilder.RenameTable(
                name: "Note",
                newName: "Notes");

            migrationBuilder.RenameTable(
                name: "Label",
                newName: "Labels");

            migrationBuilder.RenameIndex(
                name: "IX_NoteText_NoteId",
                table: "NoteTexts",
                newName: "IX_NoteTexts_NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_NoteCheckbox_NoteId",
                table: "NoteCheckboxes",
                newName: "IX_NoteCheckboxes_NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_Note_LabelId",
                table: "Notes",
                newName: "IX_Notes_LabelId");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "NoteTexts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "NoteCheckboxes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteTexts",
                table: "NoteTexts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteCheckboxes",
                table: "NoteCheckboxes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Labels",
                table: "Labels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteCheckboxes_Notes_NoteId",
                table: "NoteCheckboxes",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Labels_LabelId",
                table: "Notes",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteTexts_Notes_NoteId",
                table: "NoteTexts",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteCheckboxes_Notes_NoteId",
                table: "NoteCheckboxes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Labels_LabelId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteTexts_Notes_NoteId",
                table: "NoteTexts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteTexts",
                table: "NoteTexts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteCheckboxes",
                table: "NoteCheckboxes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Labels",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "NoteTexts");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "NoteCheckboxes");

            migrationBuilder.RenameTable(
                name: "NoteTexts",
                newName: "NoteText");

            migrationBuilder.RenameTable(
                name: "Notes",
                newName: "Note");

            migrationBuilder.RenameTable(
                name: "NoteCheckboxes",
                newName: "NoteCheckbox");

            migrationBuilder.RenameTable(
                name: "Labels",
                newName: "Label");

            migrationBuilder.RenameIndex(
                name: "IX_NoteTexts_NoteId",
                table: "NoteText",
                newName: "IX_NoteText_NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_LabelId",
                table: "Note",
                newName: "IX_Note_LabelId");

            migrationBuilder.RenameIndex(
                name: "IX_NoteCheckboxes_NoteId",
                table: "NoteCheckbox",
                newName: "IX_NoteCheckbox_NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteText",
                table: "NoteText",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Note",
                table: "Note",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteCheckbox",
                table: "NoteCheckbox",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Label",
                table: "Label",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Label_LabelId",
                table: "Note",
                column: "LabelId",
                principalTable: "Label",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteCheckbox_Note_NoteId",
                table: "NoteCheckbox",
                column: "NoteId",
                principalTable: "Note",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteText_Note_NoteId",
                table: "NoteText",
                column: "NoteId",
                principalTable: "Note",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
