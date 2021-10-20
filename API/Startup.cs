using API.Utilities;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using DomainModel.Aggregates.Gallery.Interfaces;
using DomainModel.Aggregates.Picture.Interfaces;
using DomainModel.Aggregates.Tag.Interfaces;
using DomainModel.Factories;
using Infrastructure.Common;
using Infrastructure.Galleries;
using Infrastructure.Pictures;
using Infrastructure.Tags;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nest;
using Serilog;
using System;

namespace API
{
    public class Startup
    {
        public IWebHostEnvironment HostingEnvironment { get; }
        private bool IsDevelopmentEnv => HostingEnvironment?.EnvironmentName?.ToUpper() == "DEVELOPMENT";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            // Application services
            services.AddTransient<IGalleryService, GalleryService>();
            services.AddTransient<IPictureService, PictureService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IMetadataService, MetadataService>();

            // Domain services
            services.AddTransient<IGalleryGeneratorFactory, GalleryGeneratorFactory>();

            // Infrastructure
            // repos
            services.AddTransient<IGalleryRepository, GalleryRepository>();
            services.AddTransient<IPictureRepository, PictureRepositoryES>();
            services.AddTransient<ITagRepository, TagRepository>();

            services.AddTransient<Infrastructure.Services.IMetadataService, Infrastructure.Services.MetadataService>();
            //services.AddTransient<Infrastructure.Services.IMetadataService, Infrastructure.Services.MetadataServiceMock>();

            // External dependencies
            services.AddTransient<IWebGalleryDb, WebGalleryDb>((db) =>
            {
                return new WebGalleryDb(connectionString: Configuration.GetValue($"ConnectionStrings:WebGalleryContext", ""));
            });

            services.AddTransient<IElasticClient, ElasticClient>((client) =>
            {
                var connectionSettings = new ConnectionSettings(
                    new Uri(Configuration.GetValue($"ConnectionStrings:ElasticSearchEndpoint", ""))
                );

                return new ElasticClient(connectionSettings);
            });

            // Mappings
            var mapperConfig = new MapperConfiguration(mc => 
            { 
                mc.AddProfile(new Application.Mappings.AutoMapperGalleryProfile());
                mc.AddProfile(new Application.Mappings.AutoMapperPictureProfile());
                mc.AddProfile(new Application.Mappings.AutoMapperTagProfile());
                mc.AddProfile(new Application.Mappings.AutoMapperMetadataProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddControllers();

            services.AddSwaggerGen(c => 
            {
                c.OperationFilter<Utilities.AddRequiredHeaderParameter>();
            });

            ConfigureHealthChecks(services);

            if (!IsDevelopmentEnv)
                services.AddApplicationInsightsTelemetry();     // Should automatically get the key from configuration
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebGallery.API");
            });

            // Write streamlined request completion events, instead of the more verbose ones from the framework.
            // To use the default framework request logging instead, remove this line and set the "Microsoft"
            // level in appsettings.json to "Information".
            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    ResponseWriter = HealthCheckResponseWriter.WriteResponse
                });
            });
        }

        private void ConfigureHealthChecks(IServiceCollection services)
        {
            services.AddHealthChecks().AddElasticsearch(Configuration.GetValue($"ConnectionStrings:ElasticSearchEndpoint", ""));
        }
    }
}
