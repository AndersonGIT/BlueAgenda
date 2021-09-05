using LayerLib.Classes.Constantes;
using LayerLib.Entidades;
using LayerLib.Negocios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiAgendaAnderson.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AgendaController : ControllerBase
    {
        BoContato _BoContato;
        BoContatoEF _BoContatoEF;
        public AgendaController(BoContato pBoContato, BoContatoEF pBoContatoEF)
        {
            _BoContato = pBoContato;
            _BoContatoEF = pBoContatoEF;
        }

        [HttpGet]
        public IActionResult Consultar(long pIdContato)
        {
            if (pIdContato > -1)
            {
                ContatoEF contatoEF = _BoContatoEF.Consultar(pIdContato);

                if (contatoEF != null)
                {
                    return Ok(contatoEF);
                }
                else
                    return NotFound(new { Status = Constantes.JsonStatus_ERRO, Mensagem = $"Contato não encontrado, ID consultado = {pIdContato}." });
            }
            else
            {
                return BadRequest(new { Status = Constantes.JsonStatus_ERRO, Mensagem = $"ID do Contato informado, {pIdContato}, é inválido." });
            }
        }

        [HttpGet]
        public IActionResult Listar()
        {
            List<ContatoEF> contatos = _BoContatoEF.ListarTodosContatos();

            if (contatos?.Count > 0)
            {
                return Ok(contatos);
            }
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Inserir(ContatoEF pContato)
        {
            try
            {
                _BoContatoEF.InserirContato(pContato);

                if (pContato.Id.HasValue && pContato.Id.Value> 0)
                {
                    return Ok(pContato);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Status = Constantes.JsonStatus_ERRO, Mensagem = "Contato não inserido, erro interno." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = Constantes.JsonStatus_ERRO, Mensagem = ex.Message });
            }
        }

        [HttpPut]
        public IActionResult Atualizar(ContatoEF pContato)
        {
            try
            {
                if (pContato.Id > 0)
                {
                    if (_BoContatoEF.AtualizarContato(pContato))
                    {
                        return Ok(pContato);
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, new { Status = Constantes.JsonStatus_ERRO, Mensagem = "Contato não atualizado, erro interno." });
                    }
                }
                else
                {
                    return BadRequest(new { Status = Constantes.JsonStatus_ERRO, Mensagem = $"Contato não atualizado, Id informado está inválido. ID = {pContato.Id}" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = Constantes.JsonStatus_ERRO, Mensagem = ex.Message });
            }
        }

        [HttpDelete]
        public IActionResult Remover(long pIdContato)
        {
            if (pIdContato > 0)
            {
                if (_BoContatoEF.RemoverContato(pIdContato))
                {
                    return Ok(new { Status = Constantes.JsonStatus_OK, Mensagem = "Removido com sucesso." });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Status = Constantes.JsonStatus_ERRO, Mensagem = "Contato não removido, erro interno." });
                }
            }
            else
            {
                return BadRequest(new { Status = Constantes.JsonStatus_ERRO, Mensagem = $"Contato não removido, entrada inválida. pIdContato = {pIdContato}." });
            }
        }
    }
}
