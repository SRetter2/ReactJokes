using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace ReactJokes.Data
{
    public static class JokeApi
    {
        public static Joke GetJoke()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
            var client = new HttpClient();
            var json = client.GetStringAsync(
                $"https://official-joke-api.appspot.com/jokes/programming/random").Result;
           var result  = JsonConvert.DeserializeObject<List<Joke>>(json);
            return result[0];
        }
    }
}
