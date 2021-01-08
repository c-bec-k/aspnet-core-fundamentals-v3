using System;
using Microsoft.Extensions.Configuration;

namespace SimpleCrm.Web
{
    public class ConfigurationGreeter : IGreeter
    {
        public ConfigurationGreeter(IConfiguration configuration)
        {
           config = configuration;
        }

        public IConfiguration config { get; }

        public string GetGreeting()
        {
            return config["greeting"];
        }
    }
}
