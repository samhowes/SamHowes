using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SamHowes.Extensions.Configuration.Yaml
{
    public class YamlConfigurationProvider : FileConfigurationProvider
    {
        public YamlConfigurationProvider(YamlConfigurationSource source) : base(source)
        {
        }

        public override void Load(Stream stream)
        {
            try
            {
                Data = YamlConfigurationFileParser.Parse(stream);
            }
            catch (Exception e)
            {
                throw new FormatException("Failed to parse YAML configuration file", e);
            }
        }
    }
}