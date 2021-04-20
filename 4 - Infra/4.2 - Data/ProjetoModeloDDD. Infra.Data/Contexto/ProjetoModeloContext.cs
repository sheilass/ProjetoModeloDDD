

using ProjetoModeloDDD.Domain.Entities;
using System.Data.Entity;

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
    }
}
