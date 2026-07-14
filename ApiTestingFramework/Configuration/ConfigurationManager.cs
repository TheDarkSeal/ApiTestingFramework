using Microsoft.Extensions.Configuration;

namespace ApiTestingFramework.Configuration;
public static class ConfigurationManager 
{ 
    public static IConfiguration Configuration { get; } static ConfigurationManager() { Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build(); } }