using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace corerestclient
{
    public class RestClient
    {
        private string authToken;
        private string contentType;
        private bool hasAuth;
        private HttpClient client = new HttpClient();
        
        private HttpClient Client
        {
            get 
            {
                if(this.hasAuth)
                {
                    this.client.DefaultRequestHeaders.Add("Authorization", this.authToken);
                }
                this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(this.contentType));

                return this.client;
            }
        }

        public RestClient()
        {
            this.hasAuth = false;
            this.contentType = "application/json";
        }

        public RestClient(string basicAuthToken)
        {
            this.hasAuth = true;
            this.authToken = basicAuthToken;
            this.contentType = "application/json";
        }

        public RestClient(string basicAuthToken, string contentType)
        {
            this.hasAuth = true;
            this.authToken = basicAuthToken;
            this.contentType = contentType;
        }

        public string Post(string uri, string body)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(body);
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue(this.contentType);

                var response = Client.PostAsync(uri, content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public string Patch(string uri, string body)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, uri) { Content = new StringContent(body, Encoding.UTF8, this.contentType) };
                
            var response = Client.SendAsync(request).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public string Get(string uri)
        {
            var response = Client.GetAsync(uri).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public string Put(string uri, string resource, string content)
        {
            Client.BaseAddress = new Uri(uri);
            var requestContent = new StringContent(content, Encoding.UTF8, this.contentType);
            return Client.PutAsync(resource, requestContent).Result.Content.ReadAsStringAsync().Result;
        }
    }
}
