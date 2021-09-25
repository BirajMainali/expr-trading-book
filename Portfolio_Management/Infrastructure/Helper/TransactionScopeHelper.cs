using System.Transactions;

namespace Portfolio_Management.Infrastructure.Helper
{
    public static class TransactionScopeHelper
    {
        public static TransactionScope Scope()
            => new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
    }
}