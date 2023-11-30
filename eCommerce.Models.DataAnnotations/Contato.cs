using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class Contato
    {
        public int Id { get; set; }
        public string? Telefone { get; set; }
        public string? Celular { get; set; }

        /*
         * Coluna - MER (Modelo Entidade e Relacionamento)
         * Convensão: {Modelo} + {PK} -> UsuarioId
         */
        //[ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        /*
         * POO (Navegar)
         */
        //[ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }
    }
}
