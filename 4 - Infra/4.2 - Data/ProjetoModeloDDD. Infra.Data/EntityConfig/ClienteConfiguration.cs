using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;


namespace ProjetoModeloDDD.Infra.Data.EntityConfig
{
    public class ClienteConfiguration : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguration()
        {
            //define como chave primária
            HasKey(c => c.ClienteId);

            //coloca como required e com tamanhpo máximo de 150
            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(150);

            Property(c => c.Sobrenome)
                .IsRequired()
                .HasMaxLength(150);

            Property(c => c.Email)
                .IsRequired();


        }
    }
}
