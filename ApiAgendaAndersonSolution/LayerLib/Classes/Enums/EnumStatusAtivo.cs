using System.ComponentModel;

namespace LayerLib.Classes.Enums
{
    class EnumStatusAtivo
    {
        public enum Ativo
        {
            [Description("Sim")]
            [DefaultValue("S")]
            SIM,

            [Description("Não")]
            [DefaultValue("N")]
            NAO
        }
    }
}
