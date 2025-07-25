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
    public class RelationRolFormPermission : IEntityTypeConfiguration<RolFormPermission>
    {
        public void Configure(EntityTypeBuilder<RolFormPermission> builder)
        {
            builder.ToTable("rolformpermission", schema: "segurity");


            builder.HasKey(rfp => rfp.id);

            builder.Property(rfp => rfp.active)
                   .IsRequired();

            // Relación RolFormPermission -> Rol (muchos a uno)
            builder.HasOne(rfp => rfp.Rol)
                   .WithMany(r => r.RolFormPermission)
                   .HasForeignKey(rfp => rfp.rolid)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_RolFormPermission_Rol");

            // Relación RolFormPermission -> Form (muchos a uno)
            builder.HasOne(rfp => rfp.Form)
                   .WithMany(f => f.RolFormPermission)
                   .HasForeignKey(rfp => rfp.formid)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_RolFormPermission_Form");

            // Relación RolFormPermission -> Permission (muchos a uno)
            builder.HasOne(rfp => rfp.Permission)
                   .WithMany(p => p.RolFormPermission)
                   .HasForeignKey(rfp => rfp.permissionid)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_RolFormPermission_Permission");
        }
    }
}
