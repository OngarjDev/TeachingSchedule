using Microsoft.EntityFrameworkCore;

namespace TeachingSchedule.Models
{
    public partial class TeachingScheduleDBContext : DbContext
    {
        public TeachingScheduleDBContext()
        {
        }

        public TeachingScheduleDBContext(DbContextOptions<TeachingScheduleDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ONGARJDEVLAPTOP\\MSEDUCATIONDB;Initial Catalog=TeachingScheduleDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.IdClass);

                entity.ToTable("class");

                entity.Property(e => e.IdClass)
                    .HasMaxLength(10)
                    .HasColumnName("id_class")
                    .IsFixedLength();

                entity.Property(e => e.IdSubject).HasColumnName("id_subject");

                entity.Property(e => e.IdTeacher).HasColumnName("id_teacher");

                entity.Property(e => e.NameClass)
                    .HasMaxLength(10)
                    .HasColumnName("name_class")
                    .IsFixedLength();

                entity.HasOne(d => d.IdSubjectNavigation)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.IdSubject)
                    .HasConstraintName("FK_subject_class");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Room");

                entity.Property(e => e.ActiveTimeRoom).HasColumnName("activeTime_room");

                entity.Property(e => e.IdRoom)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_room");

                entity.Property(e => e.NameRoom)
                    .HasMaxLength(50)
                    .HasColumnName("name_room");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.IdSubject);

                entity.ToTable("Subject");

                entity.Property(e => e.IdSubject).HasColumnName("id_subject");

                entity.Property(e => e.NameSubject)
                    .HasMaxLength(50)
                    .HasColumnName("name_subject");

                entity.Property(e => e.PassSubject)
                    .HasMaxLength(50)
                    .HasColumnName("pass_subject");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.IdTeacher);

                entity.ToTable("Teacher");

                entity.Property(e => e.IdTeacher)
                    .ValueGeneratedNever()
                    .HasColumnName("id_teacher");

                entity.Property(e => e.IdSubject)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_subject");

                entity.Property(e => e.NameTeacher)
                    .HasMaxLength(50)
                    .HasColumnName("name_teacher");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
