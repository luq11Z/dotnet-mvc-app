using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace LStudies.App.Configuration
{
    public static class MvcConfig
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            //services.AddControllersWithViews(options =>
            //{
            //    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "Message that you want to place in your culture");
            //                                     ...Set...
            //})
            //    .AddRazorRuntimeCompilation();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddRazorRuntimeCompilation();

            return services;
        }
    }
}
