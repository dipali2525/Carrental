namespace Carrental.Factories
{
    public interface IBulkDataServiceFactory
    {
        IBulkOperation GetBulkOperator(string typeName);
    }
}