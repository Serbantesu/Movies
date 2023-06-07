using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class JcervantesCinesContext : DbContext
{
    public JcervantesCinesContext()
    {
    }

    public JcervantesCinesContext(DbContextOptions<JcervantesCinesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cine> Cines { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Zona> Zonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database\n= JCervantesCines; Trusted_Connection=True; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cine>(entity =>
        {
            entity.HasKey(e => e.IdCine).HasName("PK__Cine__394B724BBE4E55DE");

            entity.ToTable("Cine");

            entity.Property(e => e.Direccion)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdZonaNavigation).WithMany(p => p.Cines)
                .HasForeignKey(d => d.IdZona)
                .HasConstraintName("FK__Cine__Ventas__1273C1CD");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97CEB2CCA2");

            entity.ToTable("Usuario");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Zona>(entity =>
        {
            entity.HasKey(e => e.IdZona).HasName("PK__Zona__F631C12DCF68D4E4");

            entity.ToTable("Zona");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
