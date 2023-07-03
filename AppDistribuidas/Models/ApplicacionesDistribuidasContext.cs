using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppDistribuidas.Models;

public partial class ApplicacionesDistribuidasContext : DbContext
{
    public ApplicacionesDistribuidasContext()
    {
    }

    public ApplicacionesDistribuidasContext(DbContextOptions<ApplicacionesDistribuidasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calificacione> Calificaciones { get; set; }

    public virtual DbSet<Conversione> Conversiones { get; set; }

    public virtual DbSet<Foto> Fotos { get; set; }

    public virtual DbSet<Ingrediente> Ingredientes { get; set; }

    public virtual DbSet<Multimedium> Multimedia { get; set; }

    public virtual DbSet<Paso> Pasos { get; set; }

    public virtual DbSet<Receta> Recetas { get; set; }

    public virtual DbSet<Recetasfavorita> Recetasfavoritas { get; set; }

    public virtual DbSet<Tipo> Tipos { get; set; }

    public virtual DbSet<Unidade> Unidades { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Utilizado> Utilizados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sqldbdistribuidas.database.windows.net;Database=ApplicacionesDistribuidas;User Id=distribsa;Password=databasePa$$word01;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calificacione>(entity =>
        {
            entity.HasKey(e => e.IdCalificacion).HasName("pk_calificaciones");

            entity.ToTable("calificaciones");

            entity.Property(e => e.IdCalificacion).HasColumnName("idCalificacion");
            entity.Property(e => e.Calificacion).HasColumnName("calificacion");
            entity.Property(e => e.Comentarios)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("comentarios");
            entity.Property(e => e.IdReceta).HasColumnName("idReceta");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");

            entity.HasOne(d => d.IdRecetaNavigation).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.IdReceta)
                .HasConstraintName("fk_calificaciones_recetas");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.Idusuario)
                .HasConstraintName("fk_calificaciones_usuarios");
        });

        modelBuilder.Entity<Conversione>(entity =>
        {
            entity.HasKey(e => e.IdConversion).HasName("pk_conversiones");

            entity.ToTable("conversiones");

            entity.Property(e => e.IdConversion).HasColumnName("idConversion");
            entity.Property(e => e.FactorConversiones).HasColumnName("factorConversiones");
            entity.Property(e => e.IdUnidadDestino).HasColumnName("idUnidadDestino");
            entity.Property(e => e.IdUnidadOrigen).HasColumnName("idUnidadOrigen");

            entity.HasOne(d => d.IdUnidadDestinoNavigation).WithMany(p => p.ConversioneIdUnidadDestinoNavigations)
                .HasForeignKey(d => d.IdUnidadDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_unidad_destino");

            entity.HasOne(d => d.IdUnidadOrigenNavigation).WithMany(p => p.ConversioneIdUnidadOrigenNavigations)
                .HasForeignKey(d => d.IdUnidadOrigen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_unidad_origen");
        });

        modelBuilder.Entity<Foto>(entity =>
        {
            entity.HasKey(e => e.Idfoto).HasName("pk_fotos");

            entity.ToTable("fotos");

            entity.Property(e => e.Idfoto).HasColumnName("idfoto");
            entity.Property(e => e.Extension)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("extension");
            entity.Property(e => e.IdReceta).HasColumnName("idReceta");
            entity.Property(e => e.UrlFoto)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("urlFoto");

            entity.HasOne(d => d.IdRecetaNavigation).WithMany(p => p.Fotos)
                .HasForeignKey(d => d.IdReceta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_fotos_recetas");
        });

        modelBuilder.Entity<Ingrediente>(entity =>
        {
            entity.HasKey(e => e.IdIngrediente).HasName("pk_ingredientes");

            entity.ToTable("ingredientes");

            entity.Property(e => e.IdIngrediente).HasColumnName("idIngrediente");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Multimedium>(entity =>
        {
            entity.HasKey(e => e.IdContenido).HasName("pk_multimedia");

            entity.ToTable("multimedia");

            entity.Property(e => e.IdContenido).HasColumnName("idContenido");
            entity.Property(e => e.Extension)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("extension");
            entity.Property(e => e.IdPaso).HasColumnName("idPaso");
            entity.Property(e => e.TipoContenido)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tipo_contenido");
            entity.Property(e => e.UrlContenido)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("urlContenido");

            entity.HasOne(d => d.IdPasoNavigation).WithMany(p => p.Multimedia)
                .HasForeignKey(d => d.IdPaso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_multimedia_pasos");
        });

        modelBuilder.Entity<Paso>(entity =>
        {
            entity.HasKey(e => e.IdPaso).HasName("pk_pasos");

            entity.ToTable("pasos");

            entity.Property(e => e.IdPaso).HasColumnName("idPaso");
            entity.Property(e => e.IdReceta).HasColumnName("idReceta");
            entity.Property(e => e.NroPaso).HasColumnName("nroPaso");
            entity.Property(e => e.Texto)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("texto");

            entity.HasOne(d => d.IdRecetaNavigation).WithMany(p => p.Pasos)
                .HasForeignKey(d => d.IdReceta)
                .HasConstraintName("fk_pasos_recetas");
        });

        modelBuilder.Entity<Receta>(entity =>
        {
            entity.HasKey(e => e.IdReceta).HasName("pk_recetas");

            entity.ToTable("recetas");

            entity.Property(e => e.IdReceta).HasColumnName("idReceta");
            entity.Property(e => e.CantidadPersonas).HasColumnName("cantidadPersonas");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Foto)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("foto");
            entity.Property(e => e.Habilitado).HasColumnName("habilitado");
            entity.Property(e => e.IdTipo).HasColumnName("idTipo");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Personalizada).HasColumnName("personalizada");
            entity.Property(e => e.Porciones).HasColumnName("porciones");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Receta)
                .HasForeignKey(d => d.IdTipo)
                .HasConstraintName("fk_recetas_tipos");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Receta)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_recetas_usuarios");
        });

        modelBuilder.Entity<Recetasfavorita>(entity =>
        {
            entity.HasKey(e => e.IdRecetaFavorita).HasName("PK__recetasf__364204873F97335C");

            entity.ToTable("recetasfavoritas");

            entity.Property(e => e.IdRecetaFavorita).HasColumnName("idRecetaFavorita");
            entity.Property(e => e.Idreceta).HasColumnName("idreceta");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
        });

        modelBuilder.Entity<Tipo>(entity =>
        {
            entity.HasKey(e => e.IdTipo).HasName("pk_tipos");

            entity.ToTable("tipos");

            entity.Property(e => e.IdTipo).HasColumnName("idTipo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Unidade>(entity =>
        {
            entity.HasKey(e => e.IdUnidad).HasName("pk_unidades");

            entity.ToTable("unidades");

            entity.Property(e => e.IdUnidad).HasColumnName("idUnidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("pk_usuarios");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Mail, "UQ__usuarios__7A212904E08257E2").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Avatar)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.Habilitado)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("habilitado");
            entity.Property(e => e.Mail)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("mail");
            entity.Property(e => e.Nickname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nickname");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tipo_usuario");
            entity.Property(e => e.Token)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("token");
        });

        modelBuilder.Entity<Utilizado>(entity =>
        {
            entity.HasKey(e => e.IdUtilizado).HasName("pk_utilizados");

            entity.ToTable("utilizados");

            entity.Property(e => e.IdUtilizado).HasColumnName("idUtilizado");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdIngrediente).HasColumnName("idIngrediente");
            entity.Property(e => e.IdReceta).HasColumnName("idReceta");
            entity.Property(e => e.IdUnidad).HasColumnName("idUnidad");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("observaciones");

            entity.HasOne(d => d.IdIngredienteNavigation).WithMany(p => p.Utilizados)
                .HasForeignKey(d => d.IdIngrediente)
                .HasConstraintName("fk_utilizados_ingredientes");

            entity.HasOne(d => d.IdRecetaNavigation).WithMany(p => p.Utilizados)
                .HasForeignKey(d => d.IdReceta)
                .HasConstraintName("fk_utilizados_recetas");

            entity.HasOne(d => d.IdUnidadNavigation).WithMany(p => p.Utilizados)
                .HasForeignKey(d => d.IdUnidad)
                .HasConstraintName("fk_utilizados_unidades");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
