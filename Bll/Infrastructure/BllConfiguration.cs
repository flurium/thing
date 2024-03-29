﻿using Thing.Repository;
using Thing.Repository.Interfaces;
using Thing.Services;
using Thing.UnitOfWork;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bll.Infrastructure
{
    public static class BllConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentImageRepository, CommentImageRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPropertyValueRepository, PropertyValueRepository>();
            services.AddScoped<IRequiredPropertyRepository, RequiredPropertyRepository>();
            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddScoped<ICustomPropertyRepository, CustomPropertyRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmailSender, EmailSenderService>();

            // Logic services
            services.AddTransient<CategoryService>();
            services.AddTransient<OrderService>();
            services.AddTransient<BanService>();
            services.AddTransient<RequiredPropertiesService>();
            services.AddTransient<RequiredPropertyValueService>();
            services.AddTransient<ProductService>();
            services.AddTransient<SellerService>();
            services.AddTransient<ProductImageService>();
            services.AddTransient<CatalogService>();
            services.AddTransient<ImageService>();
            services.AddTransient<CustomPropertyService>();
        }
    }
}