using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace questionnaire.Migrations
{
    public partial class BasicStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Form",
                columns: table => new
                {
                    FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form", x => x.FormId);
                });

            migrationBuilder.CreateTable(
                name: "questionnaire",
                columns: table => new
                {
                    id_questionnaire = table.Column<Guid>(type: "uuid", nullable: false),
                    title_questionnaire = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questionnaire", x => x.id_questionnaire);
                });

            migrationBuilder.CreateTable(
                name: "question",
                columns: table => new
                {
                    id_question = table.Column<Guid>(type: "uuid", nullable: false),
                    type_question = table.Column<string>(type: "text", nullable: true),
                    text_question = table.Column<string>(type: "text", nullable: true),
                    QuestionnaireId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question", x => x.id_question);
                    table.ForeignKey(
                        name: "FK_question_questionnaire_QuestionnaireId",
                        column: x => x.QuestionnaireId,
                        principalTable: "questionnaire",
                        principalColumn: "id_questionnaire",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "walkthrough",
                columns: table => new
                {
                    id_walkthrough = table.Column<Guid>(type: "uuid", nullable: false),
                    start_walkthrough = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_walkthrough = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    QuestionnaireId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_walkthrough", x => x.id_walkthrough);
                    table.ForeignKey(
                        name: "FK_walkthrough_questionnaire_QuestionnaireId",
                        column: x => x.QuestionnaireId,
                        principalTable: "questionnaire",
                        principalColumn: "id_questionnaire",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "variant",
                columns: table => new
                {
                    id_variant = table.Column<Guid>(type: "uuid", nullable: false),
                    is_correct_variant = table.Column<bool>(type: "boolean", nullable: false),
                    text_variant = table.Column<string>(type: "text", nullable: true),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_variant", x => x.id_variant);
                    table.ForeignKey(
                        name: "FK_variant_question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "question",
                        principalColumn: "id_question",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "walkthrough_question",
                columns: table => new
                {
                    id_walkthrough_question = table.Column<Guid>(type: "uuid", nullable: false),
                    id_walkthrough = table.Column<Guid>(type: "uuid", nullable: false),
                    id_question = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_walkthrough_question", x => x.id_walkthrough_question);
                    table.ForeignKey(
                        name: "FK_walkthrough_question_question_id_question",
                        column: x => x.id_question,
                        principalTable: "question",
                        principalColumn: "id_question",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_walkthrough_question_walkthrough_id_walkthrough",
                        column: x => x.id_walkthrough,
                        principalTable: "walkthrough",
                        principalColumn: "id_walkthrough",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "selected_variant",
                columns: table => new
                {
                    id_selected_variant = table.Column<Guid>(type: "uuid", nullable: false),
                    id_variant = table.Column<Guid>(type: "uuid", nullable: false),
                    id_walkthrough_question = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_selected_variant", x => x.id_selected_variant);
                    table.ForeignKey(
                        name: "FK_selected_variant_variant_id_variant",
                        column: x => x.id_variant,
                        principalTable: "variant",
                        principalColumn: "id_variant",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_selected_variant_walkthrough_question_id_walkthrough_questi~",
                        column: x => x.id_walkthrough_question,
                        principalTable: "walkthrough_question",
                        principalColumn: "id_walkthrough_question",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "text_answer",
                columns: table => new
                {
                    id_text_answer = table.Column<Guid>(type: "uuid", nullable: false),
                    text_text_answer = table.Column<string>(type: "text", nullable: true),
                    id_walkthrough_question = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_text_answer", x => x.id_text_answer);
                    table.ForeignKey(
                        name: "FK_text_answer_walkthrough_question_id_walkthrough_question",
                        column: x => x.id_walkthrough_question,
                        principalTable: "walkthrough_question",
                        principalColumn: "id_walkthrough_question",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_question_QuestionnaireId",
                table: "question",
                column: "QuestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_selected_variant_id_variant",
                table: "selected_variant",
                column: "id_variant");

            migrationBuilder.CreateIndex(
                name: "IX_selected_variant_id_walkthrough_question",
                table: "selected_variant",
                column: "id_walkthrough_question");

            migrationBuilder.CreateIndex(
                name: "IX_text_answer_id_walkthrough_question",
                table: "text_answer",
                column: "id_walkthrough_question");

            migrationBuilder.CreateIndex(
                name: "IX_variant_QuestionId",
                table: "variant",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_walkthrough_QuestionnaireId",
                table: "walkthrough",
                column: "QuestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_walkthrough_question_id_question",
                table: "walkthrough_question",
                column: "id_question");

            migrationBuilder.CreateIndex(
                name: "IX_walkthrough_question_id_walkthrough",
                table: "walkthrough_question",
                column: "id_walkthrough");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Form");

            migrationBuilder.DropTable(
                name: "selected_variant");

            migrationBuilder.DropTable(
                name: "text_answer");

            migrationBuilder.DropTable(
                name: "variant");

            migrationBuilder.DropTable(
                name: "walkthrough_question");

            migrationBuilder.DropTable(
                name: "question");

            migrationBuilder.DropTable(
                name: "walkthrough");

            migrationBuilder.DropTable(
                name: "questionnaire");
        }
    }
}
