using System.Collections.Generic;

namespace Api
{
    public class CrudProvider<TObj, TKey> : IAbstractProvider<TObj, TKey>
    {
        public GenericHttpClientProvider<TObj, TKey> Client { get; }

        public CrudProvider(string entity)
        {
            Client = new GenericHttpClientProvider<TObj, TKey>(entity);
        }

        public CrudProvider()
        {
            Client = new GenericHttpClientProvider<TObj, TKey>();
        }

        public IEnumerable<TObj> Get(bool includeRelationships = false)
        {
            var response = Client.Get(includeRelationships);

            if (response == null) return default;

            var objs = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TObj>>(response);

            return objs;
        }

        public TObj Get(TKey id, bool includeRelationships = false)
        {
            var response = Client.Get(id, includeRelationships);

            if (response == null) return default;

            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<TObj>(response);

            return obj;
        }

        public TObj Put(TKey id, TObj obj)
        {
            var response = Client.Put(id, obj);

            if (response == null) return default;

            var updatedObj = Newtonsoft.Json.JsonConvert.DeserializeObject<TObj>(response);

            return updatedObj;
        }

        public TObj Post(TObj obj)
        {
            var response = Client.Post(obj);

            if (response == null) return default;

            var createdObj = Newtonsoft.Json.JsonConvert.DeserializeObject<TObj>(response);

            return createdObj;
        }

        public TObj Delete(TKey id)
        {
            var response = Client.Delete(id);

            if (response == null) return default;

            var deletedObj = Newtonsoft.Json.JsonConvert.DeserializeObject<TObj>(response);

            return deletedObj;
        }
    }
}
