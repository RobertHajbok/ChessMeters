using System.Collections.Generic;

namespace ChessMeters.Core.Helpers
{
    public interface IAssemblyLoader
    {
        IEnumerable<T> GetAllTypesOf<T>();
    }
}