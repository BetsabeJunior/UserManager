// <copyright file="UserManagerDbContext.cs" company="DITOS SAS">
// Copyright (c) DITOS SAS. All rights reserved.
// </copyright>

namespace UserManager.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using UserManager.Domain.Entities;

    /// <summary>
    /// This class is context for the UserManager database.
    /// </summary>
    public class UserManagerDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserManagerDbContext"/> class.
        /// </summary>
        /// <param name="options"></param>
        public UserManagerDbContext(DbContextOptions<UserManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<IdentificationType> Identities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeo de la entidad User a la tabla "Usuarios"
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Usuarios");

                entity.Property(e => e.FirstName)
                      .HasColumnName("Nombre")
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.LastName)
                      .HasColumnName("Apellido")
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.IdentificationTypeId)
                      .HasColumnName("TipoIdentificacionId");

                entity.Property(e => e.IdentificationNumber)
                      .HasColumnName("NumeroIdentificacion")
                      .IsRequired()
                      .HasMaxLength(30);

                entity.Property(e => e.Email)
                      .HasColumnName("CorreoElectronico")
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(e => e.Password)
                      .HasColumnName("Contrasena")
                      .IsRequired()
                      .HasMaxLength(256);

                entity.HasOne(e => e.IdentificationType)
                      .WithMany()
                      .HasForeignKey(e => e.IdentificationTypeId);
            });

            // Mapeo para IdentificationType
            modelBuilder.Entity<IdentificationType>(entity =>
            {
                entity.ToTable("TipoIdentificacion");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Code).HasColumnName("Codigo");
                entity.Property(e => e.Name).HasColumnName("Nombre");
            });
        }
    }
}
