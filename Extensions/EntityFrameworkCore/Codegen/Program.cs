using System;
using Microsoft.SqlServer.Dac.Model;

namespace SamHowes.Extensions.EntityFrameworkCore.Codegen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class CodeGenerator
    {
        private readonly TSqlModel _model;
        private readonly string _outputDirectory;

        public CodeGenerator(TSqlModel model, string outputDirectory)
        {
            _model = model;
            _outputDirectory = outputDirectory;
        }

        public void Generate()
        {
            
        }
    }
}
