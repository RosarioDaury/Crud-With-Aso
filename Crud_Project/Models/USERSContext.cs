using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Crud_Project.Models
{
    public partial class USERSContext : DbContext
    {
        public USERSContext()
        {
        }

        public USERSContext(DbContextOptions<USERSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-MP2A3IP\\SQLEXPRESS; Database=USERS; Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("PERSON");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Img).HasColumnName("IMG");

                entity.Property(e => e.PersonLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PERSON_LAST_NAME");

                entity.Property(e => e.PersonName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PERSON_NAME");

                entity.Property(e => e.Profesion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PROFESION");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
