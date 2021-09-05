using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LayerLib.Entidades
{
    [Table("AGENDA_CONTATO")]
    public class ContatoEF
    {
        [Column("ID")]
        public long? Id { get; set; }

        [Column("NOME")]
        public string Nome { get; set; }

        [Column("TELEFONE")]
        public string Telefone { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("ATIVO")]
        public string Ativo { get; set; }
    }
}
