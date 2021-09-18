using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using SamHowes.Extensions.Configuration.Yaml;
using Xunit;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace SamHowes.Extensions.Tests.Configuration.Yaml
{
    public class RootConfig
    {
        public FooConfig Foo { get; set; }
    }
    public class FooConfig
    {
        public string Bar { get; set; }
        public List<string> What { get; set; }
    }
    
    public class YamlConfigurationParserTests
    {
        private const string BasicYaml = @"
foo:
    bar: bam
    what:
    - first
    - second
";

        [Fact]
        public void Parse_Works()
        {
            
            var result = Parse(BasicYaml);
            result.Should().Equal(
                new KeyValuePair<string, string>("foo:bar", "bam"),
                new KeyValuePair<string, string>("foo:what:0", "first"),
                new KeyValuePair<string, string>("foo:what:1", "second")
            );
        }

        [Fact]
        public void AddToConfigurationWorks()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddYamlStream(MakeStream(BasicYaml))
                .Build();

            var config = configuration.Get<RootConfig>();

            config.Foo.Should().NotBeNull();
            config.Foo.Bar.Should().Be("bam");
            config.Foo.What.Should().Equal("first", "second");
        }

        private IDictionary<string,string> Parse(string yaml)
        {
            using var stream = MakeStream(yaml);
            var result = YamlConfigurationFileParser.Parse(stream);
            return result;
        }

        private Stream MakeStream(string yaml)
        {
            var stream =new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(yaml);
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}
