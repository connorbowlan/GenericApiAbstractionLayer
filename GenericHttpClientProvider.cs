using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Api
{
    public class GenericHttpClientProvider<TObj, TKey>
    {
        private readonly string _entity;

        public HttpClient Client { get; }

        public GenericHttpClientProvider(string entity)
        {
            _entity = entity;

            Client = new HttpClient
            {
                BaseAddress = string.Empty,
            };
        }

        public GenericHttpClientProvider()
        {
            _entity = typeof(TObj).Name.Pluralize().ToLower();
            Client = new HttpClient
            {
                BaseAddress = string.Empty,
            };
        }

        public HttpClient GetClient()
        {
            return Client;
        }

        public string Get(bool includeRelationships = false)
        {
            try
            {
                var includeRelationShipsParam = includeRelationships ? "?includeRelationships=true" : string.Empty;

                var response = Client.GetAsync($"{_entity}/{includeRelationShipsParam}").Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var json = response.Content.ReadAsStringAsync().Result;

                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string Get(TKey id, bool includeRelationships = false)
        {
            try
            {
                var includeRelationShipsParam = includeRelationships ? "?includeRelationships=true" : string.Empty;

                var response = Client.GetAsync($"{_entity}/{id}{includeRelationShipsParam}").Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var json = response.Content.ReadAsStringAsync().Result;

                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string Put(TKey id, TObj obj)
        {
            try
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                var response = Client.PutAsync($"{_entity}/{id}", httpContent).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var json = response.Content.ReadAsStringAsync().Result;

                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string Post(TObj obj)
        {
            try
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                var response = Client.PostAsync($"{_entity}", httpContent).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var json = response.Content.ReadAsStringAsync().Result;

                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string Delete(TKey id)
        {
            try
            {
                var response = Client.DeleteAsync($"{_entity}/{id}").Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var json = response.Content.ReadAsStringAsync().Result;

                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
