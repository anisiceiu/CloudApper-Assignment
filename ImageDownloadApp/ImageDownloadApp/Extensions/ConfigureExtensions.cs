using ImageDownloadApp.Services;

namespace ImageDownloadApp.Extensions
{
    public static class ConfigureExtensions
    {
        public static void AppConfiguration(this IServiceCollection services)
        {
            // Add your application specific services here 
            services.AddScoped<IImageService,ImageService>();
        }

        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

        }
    }
}
