using Microsoft.EntityFrameworkCore;

namespace FilmsWebApp.Models;

public partial class MoviesContext : DbContext
{
    public MoviesContext()
    {
    }

    public MoviesContext(DbContextOptions<MoviesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<FilmsAndActor> FilmsAndActors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=MOVIES;Trusted_Connection=True; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK__Actors__E57717451E06EAD3");

            entity.Property(e => e.ActorId).HasColumnName("Actor_id");
            entity.Property(e => e.ActorName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Actor_name");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.DirectorTableId).HasName("PK__Director__3F0A139A4DE1605F");

            entity.Property(e => e.DirectorTableId).HasColumnName("Director_table_id");
            entity.Property(e => e.DirectorName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Director_name");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.FilmId).HasName("PK__Films__CE6796E44963C2AD");

            entity.Property(e => e.FilmId).HasColumnName("Film_id");
            entity.Property(e => e.DirectorId).HasColumnName("Director_id");
            entity.Property(e => e.Genre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ReleaseYear).HasColumnName("Release_year");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Director).WithMany(p => p.Films)
                .HasForeignKey(d => d.DirectorId)
                .HasConstraintName("FK__Films__Director___267ABA7A")
                .OnDelete(DeleteBehavior.ClientCascade); //delete on cascade//
        });

        modelBuilder.Entity<FilmsAndActor>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("PK__FilmsAnd__B5731FEEEA9E81E2");

            entity.Property(e => e.TableId).HasColumnName("Table_id");
            entity.Property(e => e.ActorIdFk).HasColumnName("Actor_id_fk");
            entity.Property(e => e.FilmIdFk).HasColumnName("Film_id_fk");

            entity.HasOne(d => d.ActorIdFkNavigation).WithMany(p => p.FilmsAndActors)
                .HasForeignKey(d => d.ActorIdFk)
                .HasConstraintName("FK__FilmsAndA__Actor__2C3393D0")
                .OnDelete(DeleteBehavior.ClientCascade); //delete on cascade//

            entity.HasOne(d => d.FilmIdFkNavigation).WithMany(p => p.FilmsAndActors)
                .HasForeignKey(d => d.FilmIdFk)
                .HasConstraintName("FK__FilmsAndA__Film___2B3F6F97")
                .OnDelete(DeleteBehavior.ClientCascade); //delete on cascade//
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
