﻿// <auto-generated />
using System;
using ChatNow_WebAPi.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChatNow_WebAPi.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20241026220213_ArrumadoDataannotationConversation")]
    partial class ArrumadoDataannotationConversation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChatNow_WebAPi.Domains.Conversation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdFriendship")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdFriendship");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("ChatNow_WebAPi.Domains.Friendship", b =>
                {
                    b.Property<Guid>("IdFriendship")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUserOne")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUserTwo")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdFriendship");

                    b.HasIndex("IdUserOne");

                    b.HasIndex("IdUserTwo");

                    b.HasIndex("StatusId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("ChatNow_WebAPi.Domains.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<Guid>("ConversationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SentTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ChatNow_WebAPi.Domains.Status", b =>
                {
                    b.Property<Guid>("IdStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("IdStatus");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("ChatNow_WebAPi.Domains.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("GoogleId")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("Password")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PhotoUrl")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ChatNow_WebAPi.Domains.Conversation", b =>
                {
                    b.HasOne("ChatNow_WebAPi.Domains.Friendship", "Friendship")
                        .WithMany()
                        .HasForeignKey("IdFriendship")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Friendship");
                });

            modelBuilder.Entity("ChatNow_WebAPi.Domains.Friendship", b =>
                {
                    b.HasOne("ChatNow_WebAPi.Domains.User", "UserOne")
                        .WithMany()
                        .HasForeignKey("IdUserOne")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ChatNow_WebAPi.Domains.User", "UserTwo")
                        .WithMany()
                        .HasForeignKey("IdUserTwo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ChatNow_WebAPi.Domains.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");

                    b.Navigation("UserOne");

                    b.Navigation("UserTwo");
                });

            modelBuilder.Entity("ChatNow_WebAPi.Domains.Message", b =>
                {
                    b.HasOne("ChatNow_WebAPi.Domains.Conversation", "Conversation")
                        .WithMany()
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChatNow_WebAPi.Domains.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conversation");

                    b.Navigation("Sender");
                });
#pragma warning restore 612, 618
        }
    }
}
