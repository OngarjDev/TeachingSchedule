using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TeachingSchedule.Models;

public partial class TeachingScheduleDbContext : DbContext
{
    public TeachingScheduleDbContext()
    {
    }

    public TeachingScheduleDbContext(DbContextOptions<TeachingScheduleDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<TableClassRoom> TableClassRooms { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ONGARJDEVLAPTOP\\MSEDUCATIONDB;Initial Catalog=TeachingScheduleDB;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.IdClass).HasName("PK_class");

            entity.ToTable("Class");

            entity.Property(e => e.IdClass).HasColumnName("id_class");
            entity.Property(e => e.ContentClass)
                .IsUnicode(false)
                .HasColumnName("content_class");
            entity.Property(e => e.NameClass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name_class");
            entity.Property(e => e.NumberClass).HasColumnName("number_class");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.IdRoom);

            entity.ToTable("Room");

            entity.Property(e => e.IdRoom).HasColumnName("id_room");
            entity.Property(e => e.IdClass).HasColumnName("id_class");
            entity.Property(e => e.NameRoom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name_room");

            entity.HasOne(d => d.IdClassNavigation).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.IdClass)
                .HasConstraintName("FK_Room_Class");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.IdSubject);

            entity.ToTable("Subject");

            entity.Property(e => e.IdSubject).HasColumnName("id_subject");
            entity.Property(e => e.AmountSubject).HasColumnName("amount_subject");
            entity.Property(e => e.IdClass).HasColumnName("id_class");
            entity.Property(e => e.NameSubject)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name_subject");
            entity.Property(e => e.PassSubject)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pass_subject");
        });

        modelBuilder.Entity<TableClassRoom>(entity =>
        {
            entity.HasKey(e => e.IdTableclass);

            entity.ToTable("TableClassRoom");

            entity.Property(e => e.IdTableclass).HasColumnName("id_tableclass");
            entity.Property(e => e.BreaktimeTableclass).HasColumnName("breaktime_tableclass");
            entity.Property(e => e.DayTableclass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("day_tableclass");
            entity.Property(e => e.IdClass).HasColumnName("id_class");
            entity.Property(e => e.IdRoom).HasColumnName("id_room");
            entity.Property(e => e.IdSubject).HasColumnName("id_subject");
            entity.Property(e => e.IdTeacher).HasColumnName("id_teacher");
            entity.Property(e => e.IdtimePeriod).HasColumnName("idtime_period");
            entity.Property(e => e.NumberroomTableclass).HasColumnName("numberroom_tableclass");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.IdTeacher).HasName("PK_Teacher_1");

            entity.ToTable("Teacher");

            entity.Property(e => e.IdTeacher).HasColumnName("id_teacher");
            entity.Property(e => e.IdClass).HasColumnName("id_class");
            entity.Property(e => e.IdSubject).HasColumnName("id_subject");
            entity.Property(e => e.NameTeacher)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name_teacher");

            entity.HasOne(d => d.IdClassNavigation).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.IdClass)
                .HasConstraintName("FK_Teacher_Class");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
