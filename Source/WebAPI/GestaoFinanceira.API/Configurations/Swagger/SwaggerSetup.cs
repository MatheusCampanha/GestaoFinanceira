using GestaoFinanceira.API.Configurations.Filters;
using GestaoFinanceira.API.Configurations.HeaderParameters;
using Microsoft.OpenApi.Models;

namespace GestaoFinanceira.API.Configurations.Swagger
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services, string applicationName)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = applicationName });
                c.OperationFilter<AuthenticationHeaderParameter>();
                c.OperationFilter<NotifiablePropertyFilter>();
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(sw =>
            {
                sw.SwaggerEndpoint("../swagger/v1/swagger.json", "v1");
            });
        }
    }
}
