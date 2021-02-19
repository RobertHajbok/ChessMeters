using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine;
using Quartz;
using System.Collections;
using Xunit;

namespace ChessMeters.Core.Helpers.Tests
{
    public class AssemblyLoaderTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void GetAllTypesOf_Should_ReturnAssemblyLoaderType()
        {
            var assemblyLoader = new AssemblyLoader();

            var assemblies = assemblyLoader.GetAllTypesOf<ITestEngine>();

            Assert.Single(assemblies);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetAllTypesOf_Should_ReturnEmptyListIfNoTypeIsFound()
        {
            var assemblyLoader = new AssemblyLoader();

            var assemblies = assemblyLoader.GetAllTypesOf<ICronTrigger>();

            Assert.Empty(assemblies);
        }
    }
}
