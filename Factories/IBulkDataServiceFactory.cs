namespace Carrental.Factories
{
    public interface IBulkDataServiceFactory
    {
        IBulkOperation Add(string typeName);
    }
}