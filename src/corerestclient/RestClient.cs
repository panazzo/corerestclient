using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace corerestclient
{
    public class RestClient
    {
        private string authToken;
		private string authType;
        private string contentType;
        private bool hasAuth;
        private HttpClient client = new HttpClient();
        
        private HttpClient Client
        {
            get 
            {
                if(this.hasAuth)
                {
                    if (String.IsNullOrWhiteSpace(this.authType))
					{
						// Old school Basic Auth
						client.DefaultRequestHeaders.Add("Authorization", this.authToken);
					}
					else
					{
						client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authType, authToken);
					}
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

        /* Adds generic constructor for multiple auth types like Bearer Basic etc. 
         * OAuth/JWT example : Authorization : Bearer <TOKEN>
         * Basic example : Authorization : Basic <TOKEN> 
         */
        public RestClient(string authType, string authToken, string contentType)
        {
            this.hasAuth = true;
            this.authType = authType;
            this.authToken = authToken;
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
            client.BaseAddress = new Uri(uri);
            var requestContent = new StringContent(content, Encoding.UTF8, this.contentType);
            return Client.PutAsync(resource, requestContent).Result.Content.ReadAsStringAsync().Result;
        }

        public string Delete(string uri)
        {
            var response = Client.DeleteAsync(uri).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
