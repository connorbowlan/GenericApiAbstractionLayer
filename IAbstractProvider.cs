using System.Collections.Generic;

namespace Api
{
    public interface IAbstractProvider<TObj, TKey>
    {
        GenericHttpClientProvider<TObj, TKey> Client { get; }
        IEnumerable<TObj> Get(bool includeRelationships = false);
        TObj Get(TKey id, bool includeRelationships = false);
        TObj Put(TKey id, TObj obj);
        TObj Post(TObj obj);
        TObj Delete(TKey id);
    }
}
