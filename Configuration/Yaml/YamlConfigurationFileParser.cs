using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using YamlDotNet.Serialization;

namespace SamHowes.Extensions.Configuration.Yaml
{
    public class YamlConfigurationFileParser
    {
        private readonly IDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private readonly Stack<string> _path = new();
        public static IDictionary<string, string> Parse(Stream input)
            => new YamlConfigurationFileParser().ParseStream(input);

        private IDictionary<string, string> ParseStream(Stream input)
        {
            var deserializer = new DeserializerBuilder().Build();

            var obj = deserializer.Deserialize(new StreamReader(input));
            Flatten(obj);
            return _data;
        }

        private void Flatten(object o)
        {
            switch (o)
            {
                case Dictionary<object, object> dict:
                    foreach (var (key, value) in dict)
                    {
                        _path.Push(key as string);
                        Flatten(value);
                        _path.Pop();
                    }
                    break;
                case List<object> list:
                    for (var i = 0; i < list.Count; i++)
                    {
                        _path.Push(i.ToString());
                        Flatten(list[i]);
                        _path.Pop();
                    }
                    break;
                default:
                    var configurationPath = ConfigurationPath.Combine(_path.Reverse());
                    _data[configurationPath] = o.ToString();
                    break;
            }
        }
    }
}