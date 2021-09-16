using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestioAPI.Extensions.Logger
{
    public class TLogger
    {
        private static ILogger _loggerInstance;
        private string _message;
        
        public static void Config()
        {
            _loggerInstance = new LoggerConfiguration()
               .WriteTo.Console()
               .WriteTo.File("logs/log.text",
               restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
               rollingInterval: RollingInterval.Day)
               .WriteTo.File("logs/error.text",
               restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
               .CreateLogger();
        }

        public static TLogger Log()
        {
            return new TLogger();
        }

        public TLogger Msc(string message)
        {
            _message = message;
            return this;
        }

        public void Error()
        {
            _loggerInstance.Error(_message);
        }

        public void Information()
        {
            _loggerInstance.Information(_message);
        }

        public void Warning()
        {
            _loggerInstance.Warning(_message);
        }


    }
}
