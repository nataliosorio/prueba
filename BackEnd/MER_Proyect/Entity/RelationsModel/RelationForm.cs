using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.RelationsModel
{
    public class RelationForm : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.ToTable("form", schema: "segurity");

            builder.HasKey(f => f.id);

            builder.Property(f => f.name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(f => f.description)
                   .HasMaxLength(250);

            builder.Property(f => f.active)
                   .IsRequired();


            builder.HasMany(f => f.FormModules)
                   .WithOne(fm => fm.Form)
                   .HasForeignKey(fm => fm.formid)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_Form_FormModules");

            builder.HasMany(f => f.RolFormPermission)
                   .WithOne(rfp => rfp.Form)
                   .HasForeignKey(rfp => rfp.formid)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_Form_RolFormPermission");
        }
    }
}
