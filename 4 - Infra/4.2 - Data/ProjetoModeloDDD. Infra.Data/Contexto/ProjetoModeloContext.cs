

using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Infra.Data.EntityConfig;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace ProjetoModeloDDD.Infra.Data.Contexto
{
    //adiciona o dbcontext, instala o migrations e depois colocar a string de conexão
    public class ProjetoModeloContext : DbContext        
    {
        //construtor para linkar com a string de conexão
        public ProjetoModeloContext()
            : base("ProjetoModeloDDD")
        {
        }

        //tabela a ser criada
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // tira a convenção que coloca os nomes no plural
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            //para não deletar em cascata quando tiver a relação de um para muitos
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //para não deletar em cascata quando tiver a relação de muitos para muitos
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //toda vez que um dado for criado e tiver uma palavra com final Id, vai ser uma chave primária
            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            // todo dado criado como string será definido como varchar no banco e não nvarchar
            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            // define o tamanho da string para um tamanho máximo de 100
            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            //o contexto vai entender que ele deve obedecer o que está definido na classe
            modelBuilder.Configurations.Add(new ClienteConfiguration());
        }


        //para não precisar ficar continuamente adicionando o valor do DATETIME
        public override int SaveChanges()
        {

            foreach (var entry in ChangeTracker.Entries().Where( entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            { 
                
                if(entry.State == EntityState.Added) 
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }

            }

            return base.SaveChanges();
        }
    }
}
