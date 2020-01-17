using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RoadStatus.Configuration;
using RoadStatus.Repository;

namespace RoadStatus
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        private static TflApiSettings _tflSettings;

        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Environment.ExitCode = (int) ApplicationStatus.Failed;
                throw new ArgumentNullException(nameof(args), "Road name is required");
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            _tflSettings = TflApiSettingsFactory.GetTflApiSettings(configuration);

            RegisterServiceDependencies();

            var roadStatusService = _serviceProvider.GetService<IRoadStatusRepository>();

            var roadStatus = await roadStatusService.GetRoadStatus(args[0]);

            Console.WriteLine(roadStatus.GetDisplayMessage());
            Environment.ExitCode = (int) roadStatus.GetApplicationStatus();

            Console.ReadKey();
        }

        private static void RegisterServiceDependencies()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<TflApiSettings>(_tflSettings);

            serviceCollection.AddHttpClient(HttpClientNames.TflHttpClientName,client =>
            {
                client.BaseAddress = new Uri(_tflSettings.BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            serviceCollection.AddScoped<IRoadStatusRepository,TflRoadStatusRepository>();
            
            _serviceProvider = serviceCollection.BuildServiceProvider();

        }
    }
}
