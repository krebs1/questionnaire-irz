﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace questionnaire.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20231219195303_BasicStructure")]
    partial class BasicStructure
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entities.Models.Form", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("FormId");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Form");
                });

            modelBuilder.Entity("Entities.Models.Question", b =>
                {
                    b.Property<Guid>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id_question");

                    b.Property<string>("QuestionText")
                        .HasColumnType("text")
                        .HasColumnName("text_question");

                    b.Property<string>("QuestionType")
                        .HasColumnType("text")
                        .HasColumnName("type_question");

                    b.Property<Guid>("QuestionnaireId")
                        .HasColumnType("uuid");

                    b.HasKey("QuestionId");

                    b.HasIndex("QuestionnaireId");

                    b.ToTable("question");
                });

            modelBuilder.Entity("Entities.Models.Questionnaire", b =>
                {
                    b.Property<Guid>("QuestionnaireId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id_questionnaire");

                    b.Property<string>("QuestionnaireTitle")
                        .HasColumnType("text")
                        .HasColumnName("title_questionnaire");

                    b.HasKey("QuestionnaireId");

                    b.ToTable("questionnaire");
                });

            modelBuilder.Entity("Entities.Models.SelectedVariant", b =>
                {
                    b.Property<Guid>("SelectedVariantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id_selected_variant");

                    b.Property<Guid>("VariantId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_variant");

                    b.Property<Guid>("WalkthroughQuestionId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_walkthrough_question");

                    b.HasKey("SelectedVariantId");

                    b.HasIndex("VariantId");

                    b.HasIndex("WalkthroughQuestionId");

                    b.ToTable("selected_variant");
                });

            modelBuilder.Entity("Entities.Models.TextAnswer", b =>
                {
                    b.Property<Guid>("TextAnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id_text_answer");

                    b.Property<string>("TextAnswerText")
                        .HasColumnType("text")
                        .HasColumnName("text_text_answer");

                    b.Property<Guid>("WalkthroughQuestionId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_walkthrough_question");

                    b.HasKey("TextAnswerId");

                    b.HasIndex("WalkthroughQuestionId");

                    b.ToTable("text_answer");
                });

            modelBuilder.Entity("Entities.Models.Variant", b =>
                {
                    b.Property<Guid>("VariantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id_variant");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid");

                    b.Property<bool>("VariantIsCorrect")
                        .HasColumnType("boolean")
                        .HasColumnName("is_correct_variant");

                    b.Property<string>("VariantText")
                        .HasColumnType("text")
                        .HasColumnName("text_variant");

                    b.HasKey("VariantId");

                    b.HasIndex("QuestionId");

                    b.ToTable("variant");
                });

            modelBuilder.Entity("Entities.Models.Walkthrough", b =>
                {
                    b.Property<Guid>("WalkthroughId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id_walkthrough");

                    b.Property<Guid>("QuestionnaireId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("WalkthroughEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_walkthrough");

                    b.Property<DateTime>("WalkthroughStart")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_walkthrough");

                    b.HasKey("WalkthroughId");

                    b.HasIndex("QuestionnaireId");

                    b.ToTable("walkthrough");
                });

            modelBuilder.Entity("Entities.Models.WalkthroughQuestion", b =>
                {
                    b.Property<Guid>("WalkthroughQuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id_walkthrough_question");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_question");

                    b.Property<Guid>("WalkthroughId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_walkthrough");

                    b.HasKey("WalkthroughQuestionId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("WalkthroughId");

                    b.ToTable("walkthrough_question");
                });

            modelBuilder.Entity("Entities.Models.Question", b =>
                {
                    b.HasOne("Entities.Models.Questionnaire", "Questionnaire")
                        .WithMany()
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questionnaire");
                });

            modelBuilder.Entity("Entities.Models.SelectedVariant", b =>
                {
                    b.HasOne("Entities.Models.Variant", "Variant")
                        .WithMany()
                        .HasForeignKey("VariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.WalkthroughQuestion", "WalkthroughQuestion")
                        .WithMany()
                        .HasForeignKey("WalkthroughQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Variant");

                    b.Navigation("WalkthroughQuestion");
                });

            modelBuilder.Entity("Entities.Models.TextAnswer", b =>
                {
                    b.HasOne("Entities.Models.WalkthroughQuestion", "WalkthroughQuestion")
                        .WithMany()
                        .HasForeignKey("WalkthroughQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WalkthroughQuestion");
                });

            modelBuilder.Entity("Entities.Models.Variant", b =>
                {
                    b.HasOne("Entities.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Entities.Models.Walkthrough", b =>
                {
                    b.HasOne("Entities.Models.Questionnaire", "Questionnaire")
                        .WithMany()
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questionnaire");
                });

            modelBuilder.Entity("Entities.Models.WalkthroughQuestion", b =>
                {
                    b.HasOne("Entities.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Walkthrough", "Walkthrough")
                        .WithMany()
                        .HasForeignKey("WalkthroughId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Walkthrough");
                });
#pragma warning restore 612, 618
        }
    }
}