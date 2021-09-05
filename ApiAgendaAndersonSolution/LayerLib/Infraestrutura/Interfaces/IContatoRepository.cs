using LayerLib.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerLib.Infraestrutura.Interfaces
{
    public interface IContatoRepository
    {
        public ContatoEF ConsultarContato(long pId);
        public List<ContatoEF> ListarTodosContatos();
        public long? InserirContato(ContatoEF pContato);
        public bool AtualizarContato(ContatoEF pContato);
        public bool RemoverContato(long pIdContato);
    }
}
