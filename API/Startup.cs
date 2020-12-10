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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Application services
            services.AddTransient<IGalleryService, GalleryService>();
            services.AddTransient<IPictureService, PictureService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IImageService, ImageService>();
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

            // Write streamlined request completion events, instead of the more verbose ones from the framework.
            // To use the default framework request logging instead, remove this line and set the "Microsoft"
            // level in appsettings.json to "Information".
            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
