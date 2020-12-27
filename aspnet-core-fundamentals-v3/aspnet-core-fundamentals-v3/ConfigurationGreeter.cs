using System;
using Microsoft.Extensions.Configuration;

namespace aspnet_core_fundamentals_v3
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
