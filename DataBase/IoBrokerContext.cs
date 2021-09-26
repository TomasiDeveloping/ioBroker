using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    public partial class IoBrokerContext : DbContext
    {
        public IoBrokerContext(DbContextOptions<IoBrokerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Datapoint> Datapoints { get; set; } = null!;
        public virtual DbSet<Source> Sources { get; set; } = null!;
        public virtual DbSet<TsBool> TsBools { get; set; } = null!;
        public virtual DbSet<TsCounter> TsCounters { get; set; } = null!;
        public virtual DbSet<TsNumber> TsNumbers { get; set; } = null!;
        public virtual DbSet<TsString> TsStrings { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Tomasi");

            modelBuilder.Entity<Datapoint>(entity =>
            {
                entity.ToTable("datapoints", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.ToTable("sources", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TsBool>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ts_bool", "dbo");

                entity.HasIndex(e => new { e.Id, e.Ts }, "i_id");

                entity.Property(e => e.Ack).HasColumnName("ack");

                entity.Property(e => e.From).HasColumnName("_from");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Q).HasColumnName("q");

                entity.Property(e => e.Ts).HasColumnName("ts");

                entity.Property(e => e.Val).HasColumnName("val");
            });

            modelBuilder.Entity<TsCounter>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ts_counter", "dbo");

                entity.HasIndex(e => new { e.Id, e.Ts }, "i_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ts).HasColumnName("ts");

                entity.Property(e => e.Val).HasColumnName("val");
            });

            modelBuilder.Entity<TsNumber>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ts_number", "dbo");

                entity.HasIndex(e => new { e.Id, e.Ts }, "i_id");

                entity.Property(e => e.Ack).HasColumnName("ack");

                entity.Property(e => e.From).HasColumnName("_from");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Q).HasColumnName("q");

                entity.Property(e => e.Ts).HasColumnName("ts");

                entity.Property(e => e.Val).HasColumnName("val");
            });

            modelBuilder.Entity<TsString>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ts_string", "dbo");

                entity.HasIndex(e => new { e.Id, e.Ts }, "i_id");

                entity.Property(e => e.Ack).HasColumnName("ack");

                entity.Property(e => e.From).HasColumnName("_from");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Q).HasColumnName("q");

                entity.Property(e => e.Ts).HasColumnName("ts");

                entity.Property(e => e.Val)
                    .HasColumnType("text")
                    .HasColumnName("val");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
