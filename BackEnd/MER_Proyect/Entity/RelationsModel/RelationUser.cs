using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Entity.RelationsModel
{
    public class RelationUser : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Tabla opcionalmente puedes nombrarla explícitamente
            builder.ToTable("User", schema: "segurity");


            // Clave primaria
            builder.HasKey(r => r.id);

            // Reglas adicionales sobre las columnas si quieres
            builder.Property(u => u.username)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.email)
                   .HasMaxLength(200);

            builder.Property(u => u.password)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(rfp => rfp.active)
                  .IsRequired();

            // Relación: User -> Person (muchos a uno)
            builder.HasOne(u => u.Person)
                   .WithMany(p => p.User)
                   .HasForeignKey(u => u.personid)
                   .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada

          
        }
    }
}
