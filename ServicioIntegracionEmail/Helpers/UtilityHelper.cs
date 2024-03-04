using System.Globalization;
using System.Text.RegularExpressions;

namespace ServicioIntegracionEmail.Helpers
{
    public static class UtilityHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static DateTime ExpireToken(string p_expire)
        {
            DateTime Now = DateTime.Now;
            DateTime expire_token = DateTime.MinValue;
            string expires_token_loc = p_expire ?? "1h";
            short expire = Int16.Parse(expires_token_loc[0..^1]);
            char type_expire = expires_token_loc[^1];

            expire_token = type_expire switch
            {
                'm' => DateTime.UtcNow.AddMinutes(expire),
                'h' => DateTime.UtcNow.AddHours(expire),
                'd' => DateTime.UtcNow.AddDays(expire),
                'y' => DateTime.UtcNow.AddYears(expire),
                _ => DateTime.UtcNow.AddHours(1),
            };
            return expire_token;
        }
    }
}
