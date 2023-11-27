namespace AzureSearchEmulator;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            if (hostingContext.HostingEnvironment.IsEnvironment("LocalDocker"))
            {
                config.AddUserSecrets<Program>();
            }
        });
}