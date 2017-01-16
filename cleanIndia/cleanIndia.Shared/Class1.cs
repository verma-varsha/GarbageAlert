using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.Net.Http.Headers;
//using Newtonsoft.Json;
using Windows.Data.Json;
using System.Runtime.Serialization;
namespace cleanIndia
{
    class Class1
    {
        public Class1()
        {
            //PostRequestaa("http://technexuser.herokuapp.com/api/register/");
            //GetRequest("http://google.co.in");
        }
        /*
        public async static void PostRequest(string url)
        {
            IEnumerable<KeyValuePair<string, string>> emails = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string,string>("email","bikram.bharti99@gmail.com"),
                new KeyValuePair<string,string>("name","bikrambharti"),
                new KeyValuePair<string,string>("password","brinolek"),
                new KeyValuePair<string,string>("year","1"),
                new KeyValuePair<string,string>("mobileNumber","7270976614")
                
            };
            HttpContent q = new FormUrlEncodedContent(emails);

            using (HttpClient client = new HttpClient())
            {
                //client.GetAsync()
                // HttpResponseMessage response = client.GetAsync("http://www.google.co.in");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                using (HttpResponseMessage response = await client.PostAsync(url, q))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        HttpContentHeaders heaPOSTders = content.Headers;
                        dynamic data = JsonConvert.DeserializeObject(mycontent);
                        Debug.WriteLine(data.Count);
                    }


                }
            }
        }
        */
        /*
        public async static void register(string url,RegisterData data)
        {
            IEnumerable<KeyValuePair<string, string>> emails = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string,string>("email",data.email),
                new KeyValuePair<string,string>("name",data.name),
                new KeyValuePair<string,string>("password",data.password),
                new KeyValuePair<string,string>("mobileNumber",data.phoneNumber)
                
            };
            HttpContent q = new FormUrlEncodedContent(emails);

            using (HttpClient client = new HttpClient())
            {
                //client.GetAsync()
                // HttpResponseMessage response = client.GetAsync("http://www.google.co.in");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "x-www-form-urlencoded");
                using (HttpResponseMessage response = await client.PostAsync(url, q))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        HttpContentHeaders heaPOSTders = content.Headers;
                        //JsonValue data = JsonValue.Parse(mycontent);
                        //JsonObject result = data as JsonObject;
                        //JsonObject result = data as JsonObject;
                        JsonObject dataJson = JsonObject.Parse(mycontent);
                        Debug.WriteLine(dataJson);
                        double g = dataJson.GetNamedNumber("status");
                        string name = dataJson.GetNamedString("name");
                        string email = dataJson.GetNamedString("email");
                        string mobileNumber = dataJson.GetNamedString("mobileNumber");
                        
                        //Debug.WriteLine(g);
                        //Debug.WriteLine(mycontent);
                    }


                }
            }
        }
         * */

    }
}
