using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerLib.Classes.Constantes
{
    public static class ConstantesErros
    {
        #region Codigos
        public const string ERRO_GENERICO = "0";
        public const string ERRO_OBTER_TOKEN = "1";
        public const string ERRO_NAO_EXISTE_CONEXAO_BANCO_ABERTA = "2";

        #endregion Codigos

        #region Definicoes
        private const string ERRO_GENERICO_DEF = "ErroGenerico";
        private const string ERRO_NAO_EXISTE_CONEXAO_BANCO_ABERTA_DEF = "ErroNaoExisteConexaoBancoAberta";
        #endregion Definicoes

        #region Descricoes
        private const string ERRO_GENERICO_DESC = "Ocorreu um erro ao realizar o procedimento.";
        private const string ERRO_OBTER_TOKEN_DESC = "Ocorreu um erro ao Obter Token.";
        private const string ERRO_NAO_EXISTE_CONEXAO_BANCO_ABERTA_DESC = "Não existe uma conexão aberta com o banco de dados.";
        #endregion Descricoes

        #region Metodos

        public static string ObterDescricaoErro(string pCodigoErro, string pMsgAuxiliar)
        {
            string msgRetorno = string.Empty;
            switch (pCodigoErro)
            {
                case ERRO_NAO_EXISTE_CONEXAO_BANCO_ABERTA:
                    msgRetorno = ERRO_NAO_EXISTE_CONEXAO_BANCO_ABERTA_DESC;
                    break;

                default:
                    msgRetorno = ERRO_GENERICO_DESC;
                    break;                    
            }

            if (!string.IsNullOrEmpty(pMsgAuxiliar))
                msgRetorno = string.Format("{0} {1}", msgRetorno, pMsgAuxiliar);

            return msgRetorno;
        }

        #endregion Metodos
    }
}
