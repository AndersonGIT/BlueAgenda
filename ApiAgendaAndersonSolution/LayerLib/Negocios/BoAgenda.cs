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
    public class BoAgenda
    {
        IDispatcherAgenda _DispatcherAgenda;
        public BoAgenda(IDispatcherAgenda pDispatcherAgenda)
        {
            _DispatcherAgenda = pDispatcherAgenda;
        }

        public Contato Consultar(long pIdContato)
        {
            Contato contato = new Contato { Id = pIdContato, Nome = "Anderson Diego", Telefone = "9.9638-0646", Email = "andersondiego@live.com" };

            return new Contato();
        }

        public List<Contato> ListarTodosContatos()
        {
            List<Contato> contatos = _DispatcherAgenda.ListarTodosContatos();

            return contatos;
        }

        public long InserirContato(Contato pContato)
        {
            return _DispatcherAgenda.InserirContato(pContato);
        }

        public bool AtualizarContato(Contato pContato)
        {
            return _DispatcherAgenda.AtualizarContato(pContato);
        }

        public bool RemoverContato(long pIdContato)
        {
            return _DispatcherAgenda.RemoverContato(pIdContato);
        }
    }
}
