using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace IronJade.Util.Core
{
    public class JsonConvenienceModel
    {
        public T FromJson<T>(byte[] bytes)
        {
            string json = Encoding.UTF8.GetString(bytes);
            return JsonUtility.FromJson<T>(json);
        }

        public T FromJson<T>(string json, System.Type type)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            T responseEntity = (T)JsonConvert.DeserializeObject(json, type, settings);

            return responseEntity;
        }

        public T FromJson<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public string ToJson<T>(T json)
        {
            return JsonUtility.ToJson(json);
        }

        public string NewtonSoftToJson(object json)
        {
            return JsonConvert.SerializeObject(json);
        }

        public List<T> FromJsonList<T>(byte[] bytes)
        {
            string json = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        public List<T> FromJsonList<T>(string json)
        {
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        public T[] FromJsonArray<T>(byte[] bytes)
        {
            string json = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<T[]>(json);
        }

        public T[] FromJsonArray<T>(string json)
        {
            T[] parsing = null;

            try
            {
                parsing = JsonConvert.DeserializeObject<T[]>(json);
            }
            catch (System.Exception e)
            {
                IronJade.Debug.LogError($"Parsing Error!!! => [{typeof(T).Name}]{json}");
                IronJade.Debug.LogError(e);
                throw e;
            }

            return parsing;
        }

        public string ToJsonArray<T>(T json)
        {
            return JsonConvert.SerializeObject(json);
        }
    }
}

