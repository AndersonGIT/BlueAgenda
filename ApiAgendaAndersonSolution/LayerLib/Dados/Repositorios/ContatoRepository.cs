using LayerLib.Classes.Constantes;
using LayerLib.Dados.Base;
using LayerLib.Entidades;
using LayerLib.Infraestrutura.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerLib.Dados.Providers
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly AgendaContext _agendaContext;

        public ContatoRepository(AgendaContext pAgendaContext)
        {
            _agendaContext = pAgendaContext;
        }

        public ContatoEF ConsultarContato(long pId)
        {
            return _agendaContext.Agenda_Contato.Where(c => c.Id == pId && c.Ativo == Constantes.CondicaoSIM).FirstOrDefault();
        }

        public bool AtualizarContato(ContatoEF pContato)
        {
            try
            {
                _agendaContext.Update(pContato);
                _agendaContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                //TODO: Implementar tratamento de erro.
            }

            return false;
        }

        public long? InserirContato(ContatoEF pContato)
        {
            try
            {
                pContato.Id = null;
                pContato.Ativo = Constantes.CondicaoSIM;
                _agendaContext.Add(pContato);
                _agendaContext.SaveChanges();

                return pContato.Id;
            }
            catch (Exception ex)
            {
                //TODO: Implementar tratamento de erro.
            }

            return -1;
        }

        public List<ContatoEF> ListarTodosContatos()
        {
            return _agendaContext.Agenda_Contato.Where(c => c.Ativo == Constantes.CondicaoSIM).ToList<ContatoEF>();
        }

        public bool RemoverContato(long pIdContato)
        {
            try
            {
                ContatoEF contatoEF = this.ConsultarContato(pIdContato);

                if (contatoEF != null)
                {
                    contatoEF.Ativo = Constantes.CondicaoNAO;
                    _agendaContext.Update(contatoEF);
                    _agendaContext.SaveChanges();

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                //TODO: Implementar tratamento de erro.
            }

            return false;
        }
    }
}
