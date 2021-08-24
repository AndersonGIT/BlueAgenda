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
        public AgendaController(BoContato pBoContato)
        {
            _BoContato = pBoContato;
        }

        [HttpGet]
        public IActionResult Consultar(long pIdContato)
        {
            if(pIdContato > -1)
            {
                Contato contato = _BoContato.Consultar(pIdContato);

                if (contato != null)
                {
                    return Ok(contato);
                }
                else
                    return NotFound(new { Status = Constantes.JsonStatus_ERRO, Mensagem = $"Contato não encontrado, ID consultado = {pIdContato}." });
            }
            else
            {
                return BadRequest(new { Status = Constantes.JsonStatus_ERRO, Mensagem = $"ID do Contato informado, {pIdContato}, é inválido."});
            }
        }

        [HttpGet]
        public IActionResult Listar()
        {
            List<Contato> contatos = _BoContato.ListarTodosContatos();

            if (contatos?.Count > 0)
            {
                return Ok(contatos);
            }
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Inserir(Contato pContato)
        {
            _BoContato.InserirContato(pContato);

            if (pContato.Id > 0)
            {
                return Ok(pContato);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Status = Constantes.JsonStatus_ERRO, Mensagem = "Contato não inserido, erro interno." });
            }
        }

        [HttpPut]
        public IActionResult Atualizar(Contato pContato)
        {
            if (pContato.Id > 0)
            {
                if (_BoContato.AtualizarContato(pContato))
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

        [HttpDelete]
        public IActionResult Remover(long pIdContato)
        {
            if(pIdContato > 0)
            {
                if (_BoContato.RemoverContato(pIdContato))
                {
                    return Ok(new { Status = Constantes.JsonStatus_OK, Mensagem = "Removido com sucesso. (MOC)" });
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
