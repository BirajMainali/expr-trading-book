using System.Threading.Tasks;

namespace Portfolio_Management.Valuator.Interface
{
    public interface IStockValuator
    {
        Task EnsureUniqueStock(string stockName, long? id = null);
        Task EnsureUniquePrefix(string prefix, long? id = null);

    }
}