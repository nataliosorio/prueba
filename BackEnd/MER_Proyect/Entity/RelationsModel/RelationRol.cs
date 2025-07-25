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
    public class RelationRol : IEntityTypeConfiguration<rol>
    {
        public void Configure(EntityTypeBuilder<rol> builder)
        {
            // Nombre de tabla (opcional)
            builder.ToTable("rol", schema: "segurity");


            // Clave primaria
            builder.HasKey(r => r.id);

            // Propiedades básicas
            builder.Property(r => r.name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(r => r.description)
                   .HasMaxLength(200);

            builder.Property(rfp => rfp.active)
                  .IsRequired();

            // Relación: Rol -> RolUser (uno a muchos)
            builder.HasMany(r => r.RolUser)
                   .WithOne(ru => ru.Rol)
                   .HasForeignKey(ru => ru.rolid)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_Rol_RolUser");

            builder.HasMany(f => f.RolFormPermission)
                  .WithOne(rfp => rfp.Rol)
                  .HasForeignKey(rfp => rfp.rolid)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Form_RolFormPermission");
        }
    }
}
