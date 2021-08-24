using LayerLib.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerLib.Infraestrutura.Interfaces
{
    public interface IDispatcherContato
    {
        public Contato ConsultarContato(long pIdContato);
        public List<Contato> ListarTodosContatos();
        public long InserirContato(Contato pContato);
        public bool AtualizarContato(Contato pContato);
        public bool RemoverContato(long pIdContato);
    }
}
