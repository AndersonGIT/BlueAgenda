using LayerLib.Classes.Constantes;
using LayerLib.Entidades;
using LayerLib.Negocios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAgendaAnderson.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AgendaController : ControllerBase
    {
        BoAgenda _BoAgenda;
        public AgendaController(BoAgenda pBoAgenda)
        {
            _BoAgenda = pBoAgenda;
        }

        [HttpGet]
        public IActionResult Consultar(long pIdContato)
        {
            if(pIdContato > -1)
            {
                Contato contato = _BoAgenda.Consultar(pIdContato);

                return Ok(contato);
            }
            else
            {
                return BadRequest(new { Status = Constantes.JsonStatus_ERRO, Mensagem = $"Contato informado, {pIdContato}, é inválido."});
            }
        }

        [HttpGet]
        public IActionResult Listar()
        {
            List<Contato> contatos = _BoAgenda.ListarTodosContatos();

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
            _BoAgenda.InserirContato(pContato);

            if (pContato.Id > 0)
            {
                return Ok(pContato);
            }
            else
            {
                return BadRequest(new { Status = Constantes.JsonStatus_ERRO, Mensagem = "Contato não inserido." });
            }
        }

        [HttpPut]
        public IActionResult Atualizar(Contato pEntrada)
        {
            return Ok(new { Status = Constantes.JsonStatus_OK, Mensagem = "Atualizado com sucesso. (MOC)" });
        }

        [HttpDelete]
        public IActionResult Remover(long pIdContato)
        {
            return Ok(new { Status = Constantes.JsonStatus_OK, Mensagem = "Removido com sucesso. (MOC)" });
        }
    }
}
