using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebApplication1.Configuration;
using WebApplication1.Configuration.Constants;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region swaggerConfiguration
            var adminApiConfiguration = Configuration.GetSection(nameof(ApiConfiguration)).Get<ApiConfiguration>();
            services.AddSingleton(adminApiConfiguration);
            services.AddSwaggerGen(options =>
            {
               // options.OperationFilter<XCodeSampleOperationFilter>();
                options.SwaggerDoc(ApiConfigurationConsts.ApiVersionV1, new OpenApiInfo { Title = ApiConfigurationConsts.ApiName, Version = ApiConfigurationConsts.ApiVersionV1 });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{adminApiConfiguration.IdentityServerBaseUrl}/connect/authorize", UriKind.Absolute),
                            Scopes = new Dictionary<string, string>
                            {
                                 { adminApiConfiguration.OidcApiName, ApiConfigurationConsts.ApiName }
                            }
                        },
                        ClientCredentials = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{adminApiConfiguration.IdentityServerBaseUrl}/connect/token", UriKind.Absolute),
                            Scopes = new Dictionary<string, string>
                            {
                                 { adminApiConfiguration.OidcApiName, ApiConfigurationConsts.ApiName }
                            }
                        }
                    }
                });
                options.OperationFilter<AuthorizeCheckOperationFilter>();
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);
            });

            #endregion
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var pathBase = Configuration["PATH_BASE"] ?? string.Empty;
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
#if DEBUG
                string DGS_OAS3JsonUrl = $"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json";
#else
                string DGS_OAS3JsonUrl = $"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }../swagger/v1/swagger.json";
#endif
                c.SwaggerEndpoint(DGS_OAS3JsonUrl, ApiConfigurationConsts.ApiName);
                c.OAuthAppName(ApiConfigurationConsts.ApiName);
            });
            //app.UseReDoc(c =>
            //{
            //    string DGS_OAS3JsonUrl = $"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json";
            //    c.DocumentTitle = $"{ApiConfigurationConsts.ApiName} Docs";
            //    c.SpecUrl(DGS_OAS3JsonUrl);
            //});
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
