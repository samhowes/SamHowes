using Microsoft.SqlServer.Dac;
using Microsoft.SqlServer.Dac.Model;
using SamHowes.Extensions.EntityFrameworkCore.Codegen;
using Xunit;

namespace SamHowes.Extensions.Tests.EntityFrameworkCore.Codegen
{
    public class GeneratorTests
    {
        private CodeGenerator _generator;

        public GeneratorTests()
        {
        }

        [Fact]
        public void Works()
        {
            var sqlmodel = new TSqlModel(SqlServerVersion.Sql150, new TSqlModelOptions());
            _generator = new CodeGenerator(sqlmodel, "");
        }
    }
}