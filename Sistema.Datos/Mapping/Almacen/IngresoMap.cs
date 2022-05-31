using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Almacen
{
    public class IngresoMap : IEntityTypeConfiguration<Ingreso>
    {
        public void Configure(EntityTypeBuilder<Ingreso> builder)
        {
            builder.ToTable("ingreso")
                .HasKey(i => i.idingreso);
            //Entidades Persona e Ingreso
            builder.HasOne(i => i.persona)//relaciono entidad persona con
                .WithMany(p => p.ingresos)//relacion de la entidad ingresos 
                .HasForeignKey(i => i.idproveedor);
        }
    }
}
