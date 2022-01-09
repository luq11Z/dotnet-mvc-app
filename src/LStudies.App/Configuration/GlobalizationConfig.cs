using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace LStudies.App.Configuration
{
    public static class GlobalizationConfig
    {
        public static IApplicationBuilder UseGlobalizationConfig(this IApplicationBuilder app)
        {
            var defaultCulture = new CultureInfo("pt-PT");
            var defaultCultureBR = new CultureInfo("pt-BR");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture, defaultCultureBR },
                SupportedUICultures = new List<CultureInfo> { defaultCulture, defaultCultureBR }
            };
            app.UseRequestLocalization(localizationOptions);

            return app;
        }
    }
}
