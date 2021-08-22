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
        [HttpGet]
        public IActionResult Consultar(long pId)
        {
            return Ok(new { Status = "OK", Mensagem = "Consultado com sucesso." });
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(new { Status = "OK", Mensagem = "Listado com sucesso." });
        }

        [HttpPost]
        public IActionResult Inserir(string pEntrada)
        {
            return Ok(new { Status = "OK", Mensagem = "Inserido com sucesso." });
        }

        [HttpPut]
        public IActionResult Atualizar(string pEntrada)
        {
            return Ok(new { Status = "OK", Mensagem = "Atualizado com sucesso." });
        }

        [HttpDelete]
        public IActionResult Remover(long pId)
        {
            return Ok(new { Status = "OK", Mensagem = "Removido com sucesso." });
        }
    }
}
