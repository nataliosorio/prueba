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
    public class RelationPermission : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission", schema: "segurity");


            builder.HasKey(p => p.id);

            builder.Property(p => p.name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.description)
                   .HasMaxLength(250);

            builder.Property(p => p.active)
                   .IsRequired();

           

            // Relación Permission -> RolFormPermission (uno a muchos)
            builder.HasMany(p => p.RolFormPermission)
                   .WithOne(rfp => rfp.Permission)  // suponiendo que RolFormPermission tiene propiedad Permission
                   .HasForeignKey(rfp => rfp.permissionid)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_Permission_RolFormPermission");
        }
    }
}
