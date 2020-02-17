using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuppyApi.Initialization
{
    public class ServiceNameInitializer : ITelemetryInitializer
    {
        private readonly IConfiguration _configuration;

        public ServiceNameInitializer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.InstrumentationKey = _configuration["ApplicationInsights:InstrumentationKey"];
            telemetry.Context.Cloud.RoleName = "PuppyApi";
            telemetry.Context.Cloud.RoleInstance = "PuppyApi_DebugInstance";
        }
    }
}
