using LayerLib.Classes.Constantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerLib.Classes.Erros
{
    public static class Erro
    {
        public static void GerarErro(string pCodigoErro, string pMensagemAuxiliar)
        {
            throw new Exception(ConstantesErros.ObterDescricaoErro(pCodigoErro, pMensagemAuxiliar));
        }
    }
}
