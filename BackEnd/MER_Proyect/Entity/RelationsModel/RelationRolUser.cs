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
    public class RelationRolUser : IEntityTypeConfiguration<RolUser>
    {
        public void Configure(EntityTypeBuilder<RolUser> builder)
        {
            builder.ToTable("roluser", schema: "segurity");


            builder.HasKey(ru => ru.id);

            builder.Property(ru => ru.active)
                   .IsRequired();

            // Relación RolUser -> Rol (muchos a uno)
            builder.HasOne(ru => ru.Rol)
                   .WithMany(r => r.RolUser)
                   .HasForeignKey(ru => ru.rolid)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_RolUser_Rol");

            // Relación RolUser -> User (muchos a uno)
            builder.HasOne(ru => ru.User)
                   .WithMany(u => u.Roles)
                   .HasForeignKey(ru => ru.userid)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_RolUser_User");
        }
    }
}
