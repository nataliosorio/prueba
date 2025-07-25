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
    public class RelationPerson : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            // Nombre de la tabla (opcional)
            builder.ToTable("person", schema: "persons");


            // Clave primaria
            builder.HasKey(p => p.id);

            // Propiedades básicas
            builder.Property(p => p.firstname)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.lastname)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.phonenumber)
                   .HasMaxLength(20);

            builder.Property(p => p.active)
                   .IsRequired();


            // Relación: Person -> Users (uno a muchos)
            builder.HasMany(p => p.User)
                   .WithOne(u => u.Person)
                   .HasForeignKey(u => u.personid)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_Person_User");
        }
    }
}
