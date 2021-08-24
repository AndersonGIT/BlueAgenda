using LayerLib.Dados.Base;
using LayerLib.Entidades;
using LayerLib.Infraestrutura.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerLib.Dados.DispatcherContato
{
    public class DispatcherContato : DispatcherBase, IDispatcherContato
    {
        SqlCommand _sqlCommand = null;

        #region Procedures
        private const string Proc_ListarTodosContatos = "SP_AGD_ListarTodosContatos_V1";
        private const string Proc_InserirContato = "SP_AGD_InserirContato_V1";
        private const string Proc_AtualizarContato = "SP_AGD_AtualizarContato_V1";
        private const string Proc_ConsultarContato = "SP_AGD_ConsultarContatoPorID_V1";
        private const string Proc_RemoverContato = "SP_AGD_RemoverContato_V1";
        #endregion Procedures

        private const string Col_ID = "ID";
        private const string Col_NOME = "NOME";
        private const string Col_TELEFONE = "TELEFONE";
        private const string Col_EMAIL = "EMAIL";
        private const string Col_ATIVO = "ATIVO";

        public long InserirContato(Contato pContato)
        {
            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.CommandText = Proc_InserirContato;

            _sqlCommand.Parameters.Add("@pNome", SqlDbType.VarChar, 200);
            _sqlCommand.Parameters.Add("@pTelefone", SqlDbType.VarChar, 14);
            _sqlCommand.Parameters.Add("@pEmail", SqlDbType.VarChar, 250);
            _sqlCommand.Parameters.Add("@pIdContato", SqlDbType.BigInt).Direction = ParameterDirection.Output;

            _sqlCommand.Parameters["@pNome"].Value = pContato.Nome;
            _sqlCommand.Parameters["@pTelefone"].Value = pContato.Telefone;
            _sqlCommand.Parameters["@pEmail"].Value = pContato.Email;
            _sqlCommand.Parameters["@pIdContato"].Value = pContato.Id;

            Executar(_sqlCommand);

            string idAux = _sqlCommand.Parameters["@pIdContato"].Value.ToString();

            if (!string.IsNullOrWhiteSpace(idAux))
                pContato.Id = Convert.ToInt64(idAux);

            return pContato.Id;
        }

        public Contato ConsultarContato(long pIdContato)
        {
            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.CommandText = Proc_ConsultarContato;
            _sqlCommand.Parameters.AddWithValue("@pIdContato", pIdContato);

            DataSet dataSetTodosContatos = Consultar(_sqlCommand);

            List<Contato> listaContatos = ConverterDataSetParaListContato(dataSetTodosContatos);

            if (listaContatos?.Count > 0)
                return listaContatos.Where(c => c.Id == pIdContato).FirstOrDefault();
            else
                return null;
        }

        public List<Contato> ListarTodosContatos()
        {
            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.CommandText = Proc_ListarTodosContatos;

            DataSet dataSetTodosContatos = Consultar(_sqlCommand);

            List<Contato> listaContatos = ConverterDataSetParaListContato(dataSetTodosContatos);

            return listaContatos;
        }

        public bool AtualizarContato(Contato pContato)
        {
            try
            {
                _sqlCommand = new SqlCommand();
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.CommandText = Proc_AtualizarContato;

                _sqlCommand.Parameters.Add("@pNome", SqlDbType.VarChar, 200);
                _sqlCommand.Parameters.Add("@pTelefone", SqlDbType.VarChar, 14);
                _sqlCommand.Parameters.Add("@pEmail", SqlDbType.VarChar, 250);
                _sqlCommand.Parameters.Add("@pIdContato", SqlDbType.BigInt);

                _sqlCommand.Parameters["@pNome"].Value = pContato.Nome;
                _sqlCommand.Parameters["@pTelefone"].Value = pContato.Telefone;
                _sqlCommand.Parameters["@pEmail"].Value = pContato.Email;
                _sqlCommand.Parameters["@pIdContato"].Value = pContato.Id;

                Executar(_sqlCommand);

                return true;
            }
            catch (Exception ex)
            {
                //TODO: Implementar tratamento de erro.
            }

            return false;
        }

        public bool RemoverContato(long pIdContato)
        {
            try
            {
                _sqlCommand = new SqlCommand();
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.CommandText = Proc_RemoverContato;

                _sqlCommand.Parameters.Add("@pIdContato", SqlDbType.BigInt);

                _sqlCommand.Parameters["@pIdContato"].Value = pIdContato;

                Executar(_sqlCommand);

                return true;
            }
            catch (Exception ex)
            {
                //TODO: Implementar tratamento de erro.
            }

            return false;
        }

        public List<Contato> ConverterDataSetParaListContato(DataSet pDataSet)
        {
            List<Contato> listaContatos = null;

            if(pDataSet?.Tables?.Count > 0)
            {
                if (pDataSet.Tables[0].Rows?.Count > 0)
                {
                    listaContatos = new List<Contato>();

                    foreach (DataRow itemLinha in pDataSet.Tables[0].Rows)
                    {
                        Contato contato = new Contato();

                        if (pDataSet.Tables[0].Columns.Contains(Col_ID) && itemLinha[Col_ID] != DBNull.Value)
                            contato.Id = Convert.ToInt64(itemLinha[Col_ID].ToString());

                        if (pDataSet.Tables[0].Columns.Contains(Col_NOME) && itemLinha[Col_NOME] != DBNull.Value)
                            contato.Nome = itemLinha[Col_NOME].ToString();

                        if (pDataSet.Tables[0].Columns.Contains(Col_TELEFONE) && itemLinha[Col_TELEFONE] != DBNull.Value)
                            contato.Telefone = itemLinha[Col_TELEFONE].ToString();

                        if (pDataSet.Tables[0].Columns.Contains(Col_EMAIL) && itemLinha[Col_EMAIL] != DBNull.Value)
                            contato.Email = itemLinha[Col_EMAIL].ToString();

                        if (pDataSet.Tables[0].Columns.Contains(Col_ATIVO) && itemLinha[Col_ATIVO] != DBNull.Value)
                            contato.Ativo = itemLinha[Col_ATIVO].ToString();

                        listaContatos.Add(contato);
                    }
                }
            }

            return listaContatos;
        }
    }
}
