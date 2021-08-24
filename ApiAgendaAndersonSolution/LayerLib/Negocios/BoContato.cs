using LayerLib.Entidades;
using LayerLib.Infraestrutura.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerLib.Negocios
{
    public class BoContato
    {
        IDispatcherContato _DispatcherContato;
        public BoContato(IDispatcherContato pDispatcherContato)
        {
            _DispatcherContato = pDispatcherContato;
        }

        public Contato Consultar(long pIdContato)
        {
            Contato contato = _DispatcherContato.ConsultarContato(pIdContato);
            return contato;
        }

        public List<Contato> ListarTodosContatos()
        {
            List<Contato> contatos = _DispatcherContato.ListarTodosContatos();

            return contatos;
        }

        public long InserirContato(Contato pContato)
        {
            return _DispatcherContato.InserirContato(pContato);
        }

        public bool AtualizarContato(Contato pContato)
        {
            return _DispatcherContato.AtualizarContato(pContato);
        }

        public bool RemoverContato(long pIdContato)
        {
            return _DispatcherContato.RemoverContato(pIdContato);
        }
    }
}
