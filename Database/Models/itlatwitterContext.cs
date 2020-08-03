using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
namespace Database.Models
{
    public partial class itlatwitterContext : IdentityDbContext
    {
        public itlatwitterContext()
        {
        }

        public itlatwitterContext(DbContextOptions<itlatwitterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Amigos> Amigos { get; set; }
        public virtual DbSet<Comentario> Comentario { get; set; }
        public virtual DbSet<Publicaciones> Publicaciones { get; set; }
        public virtual DbSet<RespuestasCom> RespuestasCom { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<PublicacionesCustom> Publicaciones2 { get; set; }
        public virtual DbSet<Popularpub> Popularpub { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-5VA8VTG\\MSSQLSERVER01; Database = itlatwitter; persist security info = True; Integrated Security = SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Amigos>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Idenvia)
                    .HasColumnName("idenvia")
                    .HasMaxLength(250);

                entity.Property(e => e.Idrecibe)
                    .HasColumnName("idrecibe")
                    .HasMaxLength(250);

                entity.HasOne(d => d.IdenviaNavigation)
                    .WithMany(p => p.AmigosIdenviaNavigation)
                    .HasForeignKey(d => d.Idenvia)
                    .HasConstraintName("FK_Amigos_envia");

                entity.HasOne(d => d.IdrecibeNavigation)
                    .WithMany(p => p.AmigosIdrecibeNavigation)
                    .HasForeignKey(d => d.Idrecibe)
                    .HasConstraintName("FK_Amigos_recibe");
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contenido)
                    .HasColumnName("contenido")
                    .HasMaxLength(1000)
                    .IsFixedLength();

                entity.Property(e => e.Idpublicacion).HasColumnName("idpublicacion");

                entity.Property(e => e.Idusuario)
                    .HasColumnName("idusuario")
                    .HasMaxLength(250);

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.Comentario)
                    .HasForeignKey(d => d.Idusuario)
                    .HasConstraintName("FK_Comentario_Usuarios");
            });

            modelBuilder.Entity<Publicaciones>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Idusuario)
                    .HasColumnName("idusuario")
                    .HasMaxLength(250);

                entity.Property(e => e.Imagen)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.Publicaciones)
                    .HasForeignKey(d => d.Idusuario)
                    .HasConstraintName("FK_Publicaciones_Usuarios");
            });

            modelBuilder.Entity<RespuestasCom>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contenido).HasColumnName("contenido");

                entity.Property(e => e.Idcomentario).HasColumnName("idcomentario");

                entity.Property(e => e.Idusuario)
                    .HasColumnName("idusuario")
                    .HasMaxLength(250);

                entity.HasOne(d => d.IdcomentarioNavigation)
                    .WithMany(p => p.RespuestasCom)
                    .HasForeignKey(d => d.Idcomentario)
                    .HasConstraintName("FK_RespuestasCom_Comentario");

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.RespuestasCom)
                    .HasForeignKey(d => d.Idusuario)
                    .HasConstraintName("FK_RespuestasCom_Usuarios");
            });
            modelBuilder.Entity<Popularpub>().HasNoKey();

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(250);

                entity.Property(e => e.Apellido).HasMaxLength(50);

                entity.Property(e => e.Contraseña).HasMaxLength(100);

                entity.Property(e => e.Correo).HasMaxLength(50);

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.Foto).HasMaxLength(100);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Telefono).HasMaxLength(50);

                entity.Property(e => e.Usuario).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
