using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lego
{
    internal class AppConf
    {
        public static IConfiguration Configuration { get; private set; }
        static AppConf()
        {
            if (Configuration == null)
            {
                Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("conf.json", optional: false, reloadOnChange: true)
                    .Build();
            }
        }

        public static string? GetConnectionString()
        {
            return Configuration.GetConnectionString("Default");
        }
    }
}
