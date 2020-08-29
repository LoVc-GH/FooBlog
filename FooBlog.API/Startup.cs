using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.Extensions.Logging;
using FooBlog.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FooBlog.Core.Entities;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.HttpOverrides;

namespace FooBlog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static readonly ILoggerFactory MyLoggerFactory
                = LoggerFactory.Create(builder => { builder.AddConsole(); });

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FooBlogContext>(options => options.UseLoggerFactory(MyLoggerFactory));

            services.AddControllers(mvcOptions => mvcOptions.EnableEndpointRouting = false);

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = "https://fooblogidentity.azurewebsites.net/";
                        options.Audience = "FooBlog_api";
                    });

            services.AddOData();
            services.AddODataQueryFilter();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();
            app.UseAuthorization();

            // Add the OData Batch middleware to support OData $Batch
            app.UseODataBatching();

            app.Use((context, next) =>
            {
                context.Response.Headers["OData-Version"] = "4.0";
                return next.Invoke();
            });

            app.UseCors();

            app.UseMvc(routes =>
            {
                routes.Select().Expand().Filter().OrderBy().MaxTop(null).Count().EnableContinueOnErrorHeader();
                routes.MapODataServiceRoute("odata", "odata", GetEdmModel(), new DefaultODataBatchHandler());
                routes.EnableDependencyInjection();
            });
        }

        IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<Blog>("Blogs").EntityType.Filter().Count().Expand().OrderBy().Page().Select();
            odataBuilder.EntitySet<Comment>("Comments").EntityType.Filter().Count().Expand().OrderBy().Page().Select();

            return odataBuilder.GetEdmModel();
        }
    }
}
