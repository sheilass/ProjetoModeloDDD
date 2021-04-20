

using System;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Entities
{
    public class Cliente
    {
        //chave primária
        public int ClienteId { get; set; }
        
        public string Nome { get; set; }
       
        public string Sobrenome { get; set; }
        
        public int Email { get; set; }
        
        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        public virtual IEnumerable<Produto> Produtos { get; set; }

        //regra de negócio: valida cliente especial
        public bool ClienteEspecial (Cliente cliente)
        {
            //verifica se cliente está ativo e tempo de serviço é maior ou igual a 5
            return cliente.Ativo && DateTime.Now.Year - cliente.DataCadastro.Year >= 5;
        }
    }
}
