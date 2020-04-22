using Demo11_ConfigurationExtension;
using Microsoft.Extensions.Configuration;

static internal class ConfigurationBuilderExtensions
{
    public static void AddMyConfigurationSource(this IConfigurationBuilder builder)
    {
        builder.Add(new MyConfigurationSource());
    }
}