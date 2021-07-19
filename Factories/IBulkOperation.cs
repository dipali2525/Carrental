using System.IO;

namespace Carrental.Factories
{
    public interface IBulkOperation
    {
        bool Add(Stream stream);

    }

}
