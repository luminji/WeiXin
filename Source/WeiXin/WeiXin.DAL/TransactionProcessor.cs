using System;
using System.Transactions;

namespace WeiXin.DAL
{
    public sealed class TransactionProcessor
    {
        public static void Commit(Action action)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 20, 0)))
            {
                action();
                ts.Complete();
            }
        }
    }
}
