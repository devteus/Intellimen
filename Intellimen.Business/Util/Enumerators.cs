using System.ComponentModel;

namespace Intellimen.Business.Util
{
    public static class Enumerators
    {
        public enum StatusDesafio
        {
            [Description("Pendente")]
            Pendente = 1,
            [Description("Em andamento")]
            EmAndamento = 2,
            [Description("Completo")]
            Completo = 3
        }
    }
}
