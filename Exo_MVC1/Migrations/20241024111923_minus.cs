using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exo_MVC1.Migrations
{
    /// <inheritdoc />
    public partial class minus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activites_Compagnyes_id_compagnie",
                table: "Activites");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Activites_id_activite",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Compagnie_Utilisateurs_Compagnyes_id_compagnie",
                table: "Compagnie_Utilisateurs");

            migrationBuilder.DropForeignKey(
                name: "FK_Compagnie_Utilisateurs_Utilisateurs_id_utilisateur",
                table: "Compagnie_Utilisateurs");

            migrationBuilder.DropForeignKey(
                name: "FK_Pratiquants_Activites_id_activite",
                table: "Pratiquants");

            migrationBuilder.DropForeignKey(
                name: "FK_Pratiquants_Categories_id_categorie",
                table: "Pratiquants");

            migrationBuilder.DropForeignKey(
                name: "FK_Pratiquants_Sessions_id_session",
                table: "Pratiquants");

            migrationBuilder.DropForeignKey(
                name: "FK_Presences_Activites_id_activite",
                table: "Presences");

            migrationBuilder.DropForeignKey(
                name: "FK_Presences_Categories_id_categorie",
                table: "Presences");

            migrationBuilder.DropForeignKey(
                name: "FK_Presences_Pratiquants_id_pratiquant",
                table: "Presences");

            migrationBuilder.DropForeignKey(
                name: "FK_Presences_Sessions_id_session",
                table: "Presences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Utilisateurs",
                table: "Utilisateurs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadExcels",
                table: "UploadExcels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Presences",
                table: "Presences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pratiquants",
                table: "Pratiquants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Compagnie_Utilisateurs",
                table: "Compagnie_Utilisateurs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admins",
                table: "Admins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activites",
                table: "Activites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Compagnyes",
                table: "Compagnyes");

            migrationBuilder.RenameTable(
                name: "Utilisateurs",
                newName: "utilisateurs");

            migrationBuilder.RenameTable(
                name: "UploadExcels",
                newName: "uploadexcels");

            migrationBuilder.RenameTable(
                name: "Sessions",
                newName: "sessions");

            migrationBuilder.RenameTable(
                name: "Presences",
                newName: "presences");

            migrationBuilder.RenameTable(
                name: "Pratiquants",
                newName: "pratiquants");

            migrationBuilder.RenameTable(
                name: "Compagnie_Utilisateurs",
                newName: "compagnie_utilisateurs");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.RenameTable(
                name: "Admins",
                newName: "admins");

            migrationBuilder.RenameTable(
                name: "Activites",
                newName: "activites");

            migrationBuilder.RenameTable(
                name: "Compagnyes",
                newName: "compagnies");

            migrationBuilder.RenameIndex(
                name: "IX_Presences_id_session",
                table: "presences",
                newName: "IX_presences_id_session");

            migrationBuilder.RenameIndex(
                name: "IX_Presences_id_pratiquant",
                table: "presences",
                newName: "IX_presences_id_pratiquant");

            migrationBuilder.RenameIndex(
                name: "IX_Presences_id_categorie",
                table: "presences",
                newName: "IX_presences_id_categorie");

            migrationBuilder.RenameIndex(
                name: "IX_Presences_id_activite",
                table: "presences",
                newName: "IX_presences_id_activite");

            migrationBuilder.RenameIndex(
                name: "IX_Pratiquants_id_session",
                table: "pratiquants",
                newName: "IX_pratiquants_id_session");

            migrationBuilder.RenameIndex(
                name: "IX_Pratiquants_id_categorie",
                table: "pratiquants",
                newName: "IX_pratiquants_id_categorie");

            migrationBuilder.RenameIndex(
                name: "IX_Pratiquants_id_activite",
                table: "pratiquants",
                newName: "IX_pratiquants_id_activite");

            migrationBuilder.RenameIndex(
                name: "IX_Compagnie_Utilisateurs_id_utilisateur",
                table: "compagnie_utilisateurs",
                newName: "IX_compagnie_utilisateurs_id_utilisateur");

            migrationBuilder.RenameIndex(
                name: "IX_Compagnie_Utilisateurs_id_compagnie",
                table: "compagnie_utilisateurs",
                newName: "IX_compagnie_utilisateurs_id_compagnie");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_id_activite",
                table: "categories",
                newName: "IX_categories_id_activite");

            migrationBuilder.RenameIndex(
                name: "IX_Activites_id_compagnie",
                table: "activites",
                newName: "IX_activites_id_compagnie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_utilisateurs",
                table: "utilisateurs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_uploadexcels",
                table: "uploadexcels",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sessions",
                table: "sessions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_presences",
                table: "presences",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pratiquants",
                table: "pratiquants",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_compagnie_utilisateurs",
                table: "compagnie_utilisateurs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_admins",
                table: "admins",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_activites",
                table: "activites",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_compagnies",
                table: "compagnies",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_activites_compagnies_id_compagnie",
                table: "activites",
                column: "id_compagnie",
                principalTable: "compagnies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_categories_activites_id_activite",
                table: "categories",
                column: "id_activite",
                principalTable: "activites",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_compagnie_utilisateurs_compagnies_id_compagnie",
                table: "compagnie_utilisateurs",
                column: "id_compagnie",
                principalTable: "compagnies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_compagnie_utilisateurs_utilisateurs_id_utilisateur",
                table: "compagnie_utilisateurs",
                column: "id_utilisateur",
                principalTable: "utilisateurs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pratiquants_activites_id_activite",
                table: "pratiquants",
                column: "id_activite",
                principalTable: "activites",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pratiquants_categories_id_categorie",
                table: "pratiquants",
                column: "id_categorie",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pratiquants_sessions_id_session",
                table: "pratiquants",
                column: "id_session",
                principalTable: "sessions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_presences_activites_id_activite",
                table: "presences",
                column: "id_activite",
                principalTable: "activites",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_presences_categories_id_categorie",
                table: "presences",
                column: "id_categorie",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_presences_pratiquants_id_pratiquant",
                table: "presences",
                column: "id_pratiquant",
                principalTable: "pratiquants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_presences_sessions_id_session",
                table: "presences",
                column: "id_session",
                principalTable: "sessions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_activites_compagnies_id_compagnie",
                table: "activites");

            migrationBuilder.DropForeignKey(
                name: "FK_categories_activites_id_activite",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_compagnie_utilisateurs_compagnies_id_compagnie",
                table: "compagnie_utilisateurs");

            migrationBuilder.DropForeignKey(
                name: "FK_compagnie_utilisateurs_utilisateurs_id_utilisateur",
                table: "compagnie_utilisateurs");

            migrationBuilder.DropForeignKey(
                name: "FK_pratiquants_activites_id_activite",
                table: "pratiquants");

            migrationBuilder.DropForeignKey(
                name: "FK_pratiquants_categories_id_categorie",
                table: "pratiquants");

            migrationBuilder.DropForeignKey(
                name: "FK_pratiquants_sessions_id_session",
                table: "pratiquants");

            migrationBuilder.DropForeignKey(
                name: "FK_presences_activites_id_activite",
                table: "presences");

            migrationBuilder.DropForeignKey(
                name: "FK_presences_categories_id_categorie",
                table: "presences");

            migrationBuilder.DropForeignKey(
                name: "FK_presences_pratiquants_id_pratiquant",
                table: "presences");

            migrationBuilder.DropForeignKey(
                name: "FK_presences_sessions_id_session",
                table: "presences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_utilisateurs",
                table: "utilisateurs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_uploadexcels",
                table: "uploadexcels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sessions",
                table: "sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_presences",
                table: "presences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pratiquants",
                table: "pratiquants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_compagnie_utilisateurs",
                table: "compagnie_utilisateurs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_admins",
                table: "admins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_activites",
                table: "activites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_compagnies",
                table: "compagnies");

            migrationBuilder.RenameTable(
                name: "utilisateurs",
                newName: "Utilisateurs");

            migrationBuilder.RenameTable(
                name: "uploadexcels",
                newName: "UploadExcels");

            migrationBuilder.RenameTable(
                name: "sessions",
                newName: "Sessions");

            migrationBuilder.RenameTable(
                name: "presences",
                newName: "Presences");

            migrationBuilder.RenameTable(
                name: "pratiquants",
                newName: "Pratiquants");

            migrationBuilder.RenameTable(
                name: "compagnie_utilisateurs",
                newName: "Compagnie_Utilisateurs");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "admins",
                newName: "Admins");

            migrationBuilder.RenameTable(
                name: "activites",
                newName: "Activites");

            migrationBuilder.RenameTable(
                name: "compagnies",
                newName: "Compagnyes");

            migrationBuilder.RenameIndex(
                name: "IX_presences_id_session",
                table: "Presences",
                newName: "IX_Presences_id_session");

            migrationBuilder.RenameIndex(
                name: "IX_presences_id_pratiquant",
                table: "Presences",
                newName: "IX_Presences_id_pratiquant");

            migrationBuilder.RenameIndex(
                name: "IX_presences_id_categorie",
                table: "Presences",
                newName: "IX_Presences_id_categorie");

            migrationBuilder.RenameIndex(
                name: "IX_presences_id_activite",
                table: "Presences",
                newName: "IX_Presences_id_activite");

            migrationBuilder.RenameIndex(
                name: "IX_pratiquants_id_session",
                table: "Pratiquants",
                newName: "IX_Pratiquants_id_session");

            migrationBuilder.RenameIndex(
                name: "IX_pratiquants_id_categorie",
                table: "Pratiquants",
                newName: "IX_Pratiquants_id_categorie");

            migrationBuilder.RenameIndex(
                name: "IX_pratiquants_id_activite",
                table: "Pratiquants",
                newName: "IX_Pratiquants_id_activite");

            migrationBuilder.RenameIndex(
                name: "IX_compagnie_utilisateurs_id_utilisateur",
                table: "Compagnie_Utilisateurs",
                newName: "IX_Compagnie_Utilisateurs_id_utilisateur");

            migrationBuilder.RenameIndex(
                name: "IX_compagnie_utilisateurs_id_compagnie",
                table: "Compagnie_Utilisateurs",
                newName: "IX_Compagnie_Utilisateurs_id_compagnie");

            migrationBuilder.RenameIndex(
                name: "IX_categories_id_activite",
                table: "Categories",
                newName: "IX_Categories_id_activite");

            migrationBuilder.RenameIndex(
                name: "IX_activites_id_compagnie",
                table: "Activites",
                newName: "IX_Activites_id_compagnie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utilisateurs",
                table: "Utilisateurs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadExcels",
                table: "UploadExcels",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Presences",
                table: "Presences",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pratiquants",
                table: "Pratiquants",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Compagnie_Utilisateurs",
                table: "Compagnie_Utilisateurs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admins",
                table: "Admins",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activites",
                table: "Activites",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Compagnyes",
                table: "Compagnyes",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activites_Compagnyes_id_compagnie",
                table: "Activites",
                column: "id_compagnie",
                principalTable: "Compagnyes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Activites_id_activite",
                table: "Categories",
                column: "id_activite",
                principalTable: "Activites",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Compagnie_Utilisateurs_Compagnyes_id_compagnie",
                table: "Compagnie_Utilisateurs",
                column: "id_compagnie",
                principalTable: "Compagnyes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Compagnie_Utilisateurs_Utilisateurs_id_utilisateur",
                table: "Compagnie_Utilisateurs",
                column: "id_utilisateur",
                principalTable: "Utilisateurs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pratiquants_Activites_id_activite",
                table: "Pratiquants",
                column: "id_activite",
                principalTable: "Activites",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pratiquants_Categories_id_categorie",
                table: "Pratiquants",
                column: "id_categorie",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pratiquants_Sessions_id_session",
                table: "Pratiquants",
                column: "id_session",
                principalTable: "Sessions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Presences_Activites_id_activite",
                table: "Presences",
                column: "id_activite",
                principalTable: "Activites",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Presences_Categories_id_categorie",
                table: "Presences",
                column: "id_categorie",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Presences_Pratiquants_id_pratiquant",
                table: "Presences",
                column: "id_pratiquant",
                principalTable: "Pratiquants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Presences_Sessions_id_session",
                table: "Presences",
                column: "id_session",
                principalTable: "Sessions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
