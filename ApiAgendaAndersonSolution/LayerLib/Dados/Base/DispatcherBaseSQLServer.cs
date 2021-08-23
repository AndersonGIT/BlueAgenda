using LayerLib.Classes.Constantes;
using LayerLib.Classes.Erros;
using LayerLib.Infraestrutura.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LayerLib.Dados.Base
{
    public class DispatcherBase : IDispatcher
    {
        #region Propriedades
        SqlConnection _sqlConnection = null;
        SqlTransaction _sqlTransaction = null;
        string _transactionName = string.Empty;

        public bool EstaConectado
        {
            get
            {
                return _sqlConnection?.State == ConnectionState.Open;
            }
        }

        #endregion Propriedades

        public DispatcherBase()
        {
            Inicializador();
        }

        public void Inicializador()
        {
            string connString = "Server=tcp:soupdataserver.database.windows.net,1433;Initial Catalog=mySoupDataBase;Persist Security Info=False;User ID=ssistersentry;Password=ssisterpass12*-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            
            _sqlConnection = new SqlConnection(connString);
        }

        public void AbrirConexao()
        {
            if (_sqlConnection?.State == ConnectionState.Closed)
                _sqlConnection.Open();
        }

        public void FecharConexao()
        {
            if (_sqlConnection?.State == ConnectionState.Open)
                _sqlConnection.Close();
        }

        public DataSet Consultar(SqlCommand pSqlCommand)
        {
            try
            {
                AbrirConexao();

                DataSet dataSet = null;

                pSqlCommand.Connection = _sqlConnection;

                using (SqlDataAdapter sqlDatAdapter = new SqlDataAdapter(pSqlCommand))
                {
                    dataSet = new DataSet();
                    sqlDatAdapter.Fill(dataSet);
                }

                return dataSet;
            }
            catch
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public int Executar(SqlCommand pSqlCommand)
        {
            int valorRetorno = default(int);

            try
            {
                AbrirConexao();

                if (!EstaConectado)
                    Erro.GerarErro(ConstantesErros.ERRO_NAO_EXISTE_CONEXAO_BANCO_ABERTA, string.Empty);
                else
                {
                    _transactionName = $"{pSqlCommand.CommandText}";
                    _sqlTransaction = _sqlConnection.BeginTransaction(_transactionName);
                    
                    pSqlCommand.Transaction = _sqlTransaction;
                    pSqlCommand.Connection = _sqlConnection;
                    valorRetorno = pSqlCommand.ExecuteNonQuery();
                    _sqlTransaction.Commit();
                }

                return valorRetorno;
            }
            catch
            {
                if (EstaConectado && !string.IsNullOrWhiteSpace(_transactionName))
                    if(_sqlTransaction != null)
                        _sqlTransaction.Rollback(_transactionName);

                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public void ExecutarEmLote(List<SqlCommand> pSqlCommands)
        {
            try
            {
                AbrirConexao();

                if (!EstaConectado)
                {
                    Erro.GerarErro(ConstantesErros.ERRO_NAO_EXISTE_CONEXAO_BANCO_ABERTA, string.Empty);
                }
                else
                {
                    if(pSqlCommands?.Count > 0)
                    {
                        _transactionName = $"{pSqlCommands[0].CommandText}";

                        _sqlTransaction = _sqlConnection.BeginTransaction(_transactionName);
                        
                        foreach (SqlCommand command in pSqlCommands)
                        {
                            command.Connection = _sqlConnection;
                            command.Transaction = _sqlTransaction;
                            command.ExecuteNonQuery();
                        }

                        _sqlTransaction.Commit();
                    }
                }
            }
            catch
            {
                if (EstaConectado && !string.IsNullOrWhiteSpace(_transactionName))
                    if (_sqlTransaction != null)
                        _sqlTransaction.Rollback(_transactionName);

                throw;
            }
            finally
            {
                FecharConexao();
            }
        }
    }
}