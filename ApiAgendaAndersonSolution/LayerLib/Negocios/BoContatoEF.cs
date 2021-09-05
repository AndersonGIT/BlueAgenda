using LayerLib.Entidades;
using LayerLib.Infraestrutura.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LayerLib.Negocios
{
    public class BoContatoEF
    {
        IContatoRepository _contatoRepository;

        public BoContatoEF(IContatoRepository pContatoProvider)
        {
            _contatoRepository = pContatoProvider;
        }

        public ContatoEF Consultar(long pIdContato)
        {
            ContatoEF contato = _contatoRepository.ConsultarContato(pIdContato);
            return contato;
        }

        public List<ContatoEF> ListarTodosContatos()
        {
            List<ContatoEF> contatos = _contatoRepository.ListarTodosContatos();

            return contatos;
        }

        public long? InserirContato(ContatoEF pContato)
        {
            long? idContatoInserido = default(long);

            try
            {
                string validacaoContato = ValidarContato(pContato);

                if (string.IsNullOrWhiteSpace(validacaoContato))
                {
                    idContatoInserido = _contatoRepository.InserirContato(pContato);
                }
                else
                {
                    throw new Exception(validacaoContato);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return idContatoInserido;
        }

        public bool AtualizarContato(ContatoEF pContato)
        {
            try
            {
                string validacaoContato = this.ValidarContato(pContato);

                if (string.IsNullOrWhiteSpace(validacaoContato))
                {
                    _contatoRepository.AtualizarContato(pContato);
                }
                else
                {
                    throw new Exception(validacaoContato);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
        }

        public bool RemoverContato(long pIdContato)
        {
            return _contatoRepository.RemoverContato(pIdContato);
        }

        private string ValidarEmail(string pEmail)
        {
            string retorno = string.Empty;

            try
            {
                bool emailValido = false;

                if (!string.IsNullOrWhiteSpace(pEmail))
                {
                    emailValido = Regex.IsMatch(pEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                }

                if (!emailValido)
                    throw new Exception($"'{pEmail}' não está em um formato correto.");
            }
            catch (Exception ex)
            {
                retorno = ("E-Mail inválido. " + Environment.NewLine + ex.Message);
            }

            return retorno;
        }

        private string ValidarContato(ContatoEF pContato)
        {
            string validacaoContato = string.Empty;

            try
            {
                if (pContato != null)
                {

                    if (string.IsNullOrWhiteSpace(pContato.Nome))
                    {
                        validacaoContato += "Nome do contato é obrigatório." + Environment.NewLine;
                    }

                    if (string.IsNullOrWhiteSpace(pContato.Telefone))
                    {
                        validacaoContato += "Telefone do contato é obrigatório." + Environment.NewLine;
                    }

                    if (string.IsNullOrWhiteSpace(pContato.Email))
                    {
                        validacaoContato += "E-Mail do contato é obrigatório." + Environment.NewLine;
                    }
                    else
                    {
                        string validacaoEmail = this.ValidarEmail(pContato.Email);

                        if(!string.IsNullOrWhiteSpace(validacaoEmail))
                            validacaoContato += validacaoEmail + Environment.NewLine;
                    }
                }
                else
                {
                    validacaoContato = "Contato não informado.";
                }
            }
            catch (Exception ex)
            {
                validacaoContato += ex.Message;
            }

            return validacaoContato;
        }
    }
}
