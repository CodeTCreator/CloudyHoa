using CalculateService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    internal class Program
    {
        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8733/WcfServiceLibrary/Service1/");

            using (var host = new ServiceHost(typeof(WcfServiceLibrary.ServiceHoaAccount)))
            {
                host.Open();
                Console.WriteLine("Хост стартовал!");
                Console.ReadLine();
            }
        }
    }
}
