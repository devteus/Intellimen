using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Intellimen.Business.DTOs;
using Intellimen.Business.Util;

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
        public static bool IsLogado => Context?.User.Identity?.IsAuthenticated ?? false;

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

        public static async Task SignInAsync(UserDTO userDTO)
        {
            List<Claim> claims = new()
            {
                new Claim("ide", userDTO.Ide.ToString()),
                new Claim(ClaimTypes.NameIdentifier, userDTO.Name)
            };

            if (!string.IsNullOrEmpty(userDTO.Email)) claims.Add(new Claim("email", userDTO.Email.ToString() ?? ""));

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await Context!.SignInAsync(claimsPrincipal,
                new AuthenticationProperties()
                {
                    ExpiresUtc = userDTO.ExpiresIn
                });
        }

        public static async Task LogOutAsync()
        {
            await Context.SignOutAsync();
            Context.Session.Clear();
        }

        public static UserDTO? CurrentUser => IsLogado && Context?.User != null ? new UserDTO()
        {
            Ide = Context.User.TryGetClaim("ide", out string? ide) ? Convert.ToInt32(ide) : 0,
            Name = Context.User.TryGetClaim(ClaimTypes.NameIdentifier, out string? name) ? name ?? "" : "",
            Email = Context.User.TryGetClaim("email", out string? email) ? email ?? "" : "",
            ExpiresIn = Context.User.TryGetClaim("exp", out string? exp)
               ? DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp)) : null
        } : null;

    }
}
