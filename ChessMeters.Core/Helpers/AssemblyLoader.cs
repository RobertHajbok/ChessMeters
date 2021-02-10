using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ChessMeters.Core.Helpers
{
    public class AssemblyLoader : IAssemblyLoader
    {
        public IEnumerable<T> GetAllTypesOf<T>()
        {
            var assembly = Assembly.GetEntryAssembly();
            var assemblies = assembly.GetReferencedAssemblies();

            foreach (var assemblyName in assemblies)
            {
                assembly = Assembly.Load(assemblyName);

                foreach (var ti in assembly.DefinedTypes)
                {
                    if (ti.ImplementedInterfaces.Contains(typeof(T)))
                    {
                        yield return (T)assembly.CreateInstance(ti.FullName);
                    }
                }
            }
        }
    }
}
