using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LayerLib.Infraestrutura.Interfaces
{
    interface IDispatcher
    {
        void Inicializador();

        void AbrirConexao();

        void FecharConexao();

        DataSet Consultar(SqlCommand pSqlCommand);

        int Executar(SqlCommand pSqlCommand);

        void ExecutarEmLote(List<SqlCommand> pSqlCommands);
    }
}
