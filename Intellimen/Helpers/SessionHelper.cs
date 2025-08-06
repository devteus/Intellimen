using System.Diagnostics.CodeAnalysis;

namespace Intellimen.Helpers
{
    public static class SessionHelper
    {
        private static IHttpContextAccessor? _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext? Context => _httpContextAccessor?.HttpContext;

        #region TempValue

        public static bool TryGetTempValue<T>(string? chave,
            [MaybeNullWhen(false)] out T? valor)
        {
            valor = GetTempValue<T>(chave);
            return valor != null;
        }

        public static T? GetTempValue<T>(string? chave)
        {
            if (string.IsNullOrWhiteSpace(chave)) return default;

            Dictionary<string, object>? tempValores =
                Context?.Session.GetTempValues();
            if (!(tempValores?.Any() ?? false)) return default;

            bool temValor = tempValores.TryGetValue(
                chave, out object? valor);

            if (temValor) SetTempValue(chave, null);

            return temValor && valor != null
                ? (T)valor : default;
        }

        public static void SetTempValue(string? chave, object? valor)
        {
            if (string.IsNullOrWhiteSpace(chave)) return;

            Dictionary<string, object>? tempValores =
                Context?.Session.GetTempValues() ?? new();

            tempValores.Remove(chave);
            if (valor != null) tempValores.Add(chave, valor);

            Context?.Session.SetTempValues(tempValores);
        }

        #endregion
    }
}
