using Microsoft.EntityFrameworkCore.Migrations;

namespace VoBoo.Migrations
{
    public partial class NotNullPropertiesOfSameClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dictionaries_Languages_KnownLangId",
                table: "Dictionaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Translations_Words_TranslationId",
                table: "Translations");

            migrationBuilder.AlterColumn<long>(
                name: "TranslationId",
                table: "Translations",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "KnownLangId",
                table: "Dictionaries",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dictionaries_Languages_KnownLangId",
                table: "Dictionaries",
                column: "KnownLangId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Translations_Words_TranslationId",
                table: "Translations",
                column: "TranslationId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dictionaries_Languages_KnownLangId",
                table: "Dictionaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Translations_Words_TranslationId",
                table: "Translations");

            migrationBuilder.AlterColumn<long>(
                name: "TranslationId",
                table: "Translations",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "KnownLangId",
                table: "Dictionaries",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Dictionaries_Languages_KnownLangId",
                table: "Dictionaries",
                column: "KnownLangId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Translations_Words_TranslationId",
                table: "Translations",
                column: "TranslationId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
