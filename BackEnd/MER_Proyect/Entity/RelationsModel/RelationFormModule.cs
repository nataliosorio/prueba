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
    public class RelationFormModule : IEntityTypeConfiguration<FormModule>
    {
        public void Configure(EntityTypeBuilder<FormModule> builder)
        {
            builder.ToTable("formmodule", schema: "segurity");


            builder.HasKey(fm => fm.id);

            builder.Property(fm => fm.active)
                   .IsRequired();

            // Relación FormModule -> Form (muchos a uno)
            builder.HasOne(fm => fm.Form)
                   .WithMany(f => f.FormModules)
                   .HasForeignKey(fm => fm.formid)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_FormModule_Form");

            // Relación FormModule -> Module (muchos a uno)
            builder.HasOne(fm => fm.Module)
                   .WithMany(m => m.FormModules)
                   .HasForeignKey(fm => fm.moduleid)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_FormModule_Module");
        }
    }
}
