using System;
using Xunit;
using corerestclient;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using RichardSzalay.MockHttp;

namespace corerestclient.Tests
{
    public class Tests
    {
        [Fact]
        public void GetWithoutAuth()
        {
            var client = new corerestclient.RestClient();
            var result = client.Get("https://api.stackexchange.com/2.2/questions?page=1&pagesize=1&order=desc&sort=activity&site=stackoverflow");
            Assert.False(string.IsNullOrEmpty(result));
        }

        [Fact]
        public void TestMockedGet() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Get, "http://127.0.0.1")
                .WithHeaders("Accept", "application/json")
                .Respond("text/plain", "GET OK");            
            var client = new corerestclient.RestClient(mockHandler);
            var result = client.Get("http://127.0.0.1");
            Assert.True(result.Contains("GET OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedPost() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Post, "http://127.0.0.1")
                .WithHeaders("Accept", "application/json")
                .WithContent("POST CONTENT")
                .Respond("text/plain", "POST OK");            
            var client = new corerestclient.RestClient(mockHandler);
            var result = client.Post("http://127.0.0.1", "POST CONTENT");
            Assert.True(result.Contains("POST OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedPut() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Put, "http://127.0.0.1/testResource")
                .WithHeaders("Accept", "application/json")
                .WithContent("PUT CONTENT")
                .Respond("text/plain", "PUT OK");            
            var client = new corerestclient.RestClient(mockHandler);
            var result = client.Put("http://127.0.0.1", "testResource", "PUT CONTENT");
            Assert.True(result.Contains("PUT OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedDelete() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Delete, "http://127.0.0.1")   
                .WithHeaders("Accept", "application/json")             
                .Respond("text/plain", "DELETE OK");            
            var client = new corerestclient.RestClient(mockHandler);
            var result = client.Delete("http://127.0.0.1");
            Assert.True(result.Contains("DELETE OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedPatch() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect("http://127.0.0.1")
                .WithHeaders("Accept", "application/json")
                .WithContent("PATCH CONTENT")
                .Respond("text/plain", "PATCH OK");            
            var client = new corerestclient.RestClient(mockHandler);
            var result = client.Patch("http://127.0.0.1", "PATCH CONTENT");
            Assert.True(result.Contains("PATCH OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedGetWithAuth() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Get, "http://127.0.0.1")
                .WithHeaders("Accept", "application/json")
                .WithHeaders("Authorization", "basicAuthToken")              
                .Respond("text/plain", "GET OK");            
            var client = new corerestclient.RestClient("basicAuthToken", mockHandler);
            var result = client.Get("http://127.0.0.1");
            Assert.True(result.Contains("GET OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedPostWithAuth() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Post, "http://127.0.0.1")
                .WithHeaders("Accept", "application/json")
                .WithHeaders("Authorization", "basicAuthToken")
                .WithContent("POST CONTENT")
                .Respond("text/plain", "POST OK");            
            var client = new corerestclient.RestClient("basicAuthToken", mockHandler);
            var result = client.Post("http://127.0.0.1", "POST CONTENT");
            Assert.True(result.Contains("POST OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedPutWithAuth() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Put, "http://127.0.0.1/testResource")
                .WithHeaders("Accept", "application/json")
                .WithHeaders("Authorization", "basicAuthToken")
                .WithContent("PUT CONTENT")
                .Respond("text/plain", "PUT OK");            
            var client = new corerestclient.RestClient("basicAuthToken", mockHandler);
            var result = client.Put("http://127.0.0.1", "testResource", "PUT CONTENT");
            Assert.True(result.Contains("PUT OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedDeleteWithAuth() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Delete, "http://127.0.0.1") 
                .WithHeaders("Accept", "application/json")
                .WithHeaders("Authorization", "basicAuthToken")               
                .Respond("text/plain", "DELETE OK");            
            var client = new corerestclient.RestClient("basicAuthToken", mockHandler);
            var result = client.Delete("http://127.0.0.1");
            Assert.True(result.Contains("DELETE OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedPatchWithAuth() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect("http://127.0.0.1")
                .WithHeaders("Accept", "application/json")
                .WithHeaders("Authorization", "basicAuthToken")
                .WithContent("PATCH CONTENT")
                .Respond("text/plain", "PATCH OK");            
            var client = new corerestclient.RestClient("basicAuthToken", mockHandler);
            var result = client.Patch("http://127.0.0.1", "PATCH CONTENT");
            Assert.True(result.Contains("PATCH OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedGetWithAuthAndContentType() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Get, "http://127.0.0.1")
                .WithHeaders("Accept", "text/plain")
                .WithHeaders("Authorization", "basicAuthToken")              
                .Respond("text/plain", "GET OK");            
            var client = new corerestclient.RestClient("basicAuthToken", "text/plain", mockHandler);
            var result = client.Get("http://127.0.0.1");
            Assert.True(result.Contains("GET OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedPostWithAuthAndContentType() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Post, "http://127.0.0.1")
                .WithHeaders("Accept", "text/plain")
                .WithHeaders("Authorization", "basicAuthToken")
                .WithContent("POST CONTENT")
                .Respond("text/plain", "POST OK");            
            var client = new corerestclient.RestClient("basicAuthToken", "text/plain", mockHandler);
            var result = client.Post("http://127.0.0.1", "POST CONTENT");
            Assert.True(result.Contains("POST OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedPutWithAuthAndContentType() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Put, "http://127.0.0.1/testResource")
                .WithHeaders("Accept", "text/plain")
                .WithHeaders("Authorization", "basicAuthToken")
                .WithContent("PUT CONTENT")
                .Respond("text/plain", "PUT OK");            
            var client = new corerestclient.RestClient("basicAuthToken", "text/plain", mockHandler);
            var result = client.Put("http://127.0.0.1", "testResource", "PUT CONTENT");
            Assert.True(result.Contains("PUT OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedDeleteWithAuthAndContentType() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Delete, "http://127.0.0.1") 
                .WithHeaders("Accept", "text/plain")
                .WithHeaders("Authorization", "basicAuthToken")               
                .Respond("text/plain", "DELETE OK");            
            var client = new corerestclient.RestClient("basicAuthToken", "text/plain", mockHandler);
            var result = client.Delete("http://127.0.0.1");
            Assert.True(result.Contains("DELETE OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedPatchWithAuthAndContentType() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect("http://127.0.0.1")
                .WithHeaders("Accept", "text/plain")
                .WithHeaders("Authorization", "basicAuthToken")
                .WithContent("PATCH CONTENT")
                .Respond("text/plain", "PATCH OK");            
            var client = new corerestclient.RestClient("basicAuthToken", "text/plain", mockHandler);
            var result = client.Patch("http://127.0.0.1", "PATCH CONTENT");
            Assert.True(result.Contains("PATCH OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedGetWithOtherAuthAndContentType() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Get, "http://127.0.0.1")
                .WithHeaders("Accept", "text/plain")
                .WithHeaders("Authorization", "Basic basicAuthUser:basicAuthPassword")              
                .Respond("text/plain", "GET OK");            
            var client = new corerestclient.RestClient("Basic", "basicAuthUser:basicAuthPassword", "text/plain", mockHandler);
            var result = client.Get("http://127.0.0.1");
            Assert.True(result.Contains("GET OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedPostWithOtherAuthAndContentType() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Post, "http://127.0.0.1")
                .WithHeaders("Accept", "text/plain")
                .WithHeaders("Authorization", "Basic basicAuthUser:basicAuthPassword")
                .WithContent("POST CONTENT")
                .Respond("text/plain", "POST OK");            
            var client = new corerestclient.RestClient("Basic", "basicAuthUser:basicAuthPassword", "text/plain", mockHandler);
            var result = client.Post("http://127.0.0.1", "POST CONTENT");
            Assert.True(result.Contains("POST OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedPutWithOtherAuthAndContentType() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Put, "http://127.0.0.1/testResource")
                .WithHeaders("Accept", "text/plain")
                .WithHeaders("Authorization", "Basic basicAuthUser:basicAuthPassword")
                .WithContent("PUT CONTENT")
                .Respond("text/plain", "PUT OK");            
            var client = new corerestclient.RestClient("Basic", "basicAuthUser:basicAuthPassword", "text/plain", mockHandler);
            var result = client.Put("http://127.0.0.1", "testResource", "PUT CONTENT");
            Assert.True(result.Contains("PUT OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedDeleteWithOtherAuthAndContentType() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Delete, "http://127.0.0.1") 
                .WithHeaders("Accept", "text/plain")
                .WithHeaders("Authorization", "Basic basicAuthUser:basicAuthPassword")               
                .Respond("text/plain", "DELETE OK");            
            var client = new corerestclient.RestClient("Basic", "basicAuthUser:basicAuthPassword", "text/plain", mockHandler);
            var result = client.Delete("http://127.0.0.1");
            Assert.True(result.Contains("DELETE OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedPatchWithOtherAuthAndContentType() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect("http://127.0.0.1")
                .WithHeaders("Accept", "text/plain")
                .WithHeaders("Authorization", "Basic basicAuthUser:basicAuthPassword")
                .WithContent("PATCH CONTENT")
                .Respond("text/plain", "PATCH OK");            
            var client = new corerestclient.RestClient("Basic", "basicAuthUser:basicAuthPassword", "text/plain", mockHandler);
            var result = client.Patch("http://127.0.0.1", "PATCH CONTENT");
            Assert.True(result.Contains("PATCH OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedGetContentType() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Get, "http://127.0.0.1")
                .WithHeaders("Accept", "text/plain")
                .Respond("text/plain", "GET OK");            
            var client = new corerestclient.RestClient(mockHandler);
            client.contentType = "text/plain";
            var result = client.Get("http://127.0.0.1");
            Assert.True(result.Contains("GET OK"));            
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedPostContentType() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Post, "http://127.0.0.1")
                .WithHeaders("Accept", "text/plain")
                .WithContent("POST CONTENT")
                .Respond("text/plain", "POST OK");            
            var client = new corerestclient.RestClient(mockHandler);
            client.contentType = "text/plain";
            var result = client.Post("http://127.0.0.1", "POST CONTENT");
            Assert.True(result.Contains("POST OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedPutContentType() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Put, "http://127.0.0.1/testResource")
                .WithHeaders("Accept", "text/plain")
                .WithContent("PUT CONTENT")
                .Respond("text/plain", "PUT OK");            
            var client = new corerestclient.RestClient(mockHandler);
            client.contentType = "text/plain";
            var result = client.Put("http://127.0.0.1", "testResource", "PUT CONTENT");
            Assert.True(result.Contains("PUT OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedDeleteContentType() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect(HttpMethod.Delete, "http://127.0.0.1")   
                .WithHeaders("Accept", "text/plain")           
                .Respond("text/plain", "DELETE OK");            
            var client = new corerestclient.RestClient(mockHandler);
            client.contentType = "text/plain";
            var result = client.Delete("http://127.0.0.1");
            Assert.True(result.Contains("DELETE OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void TestMockedPatchContentType() 
        {
            var mockHandler = new MockHttpMessageHandler();     
            mockHandler
                .Expect("http://127.0.0.1")
                .WithHeaders("Accept", "text/plain")
                .WithContent("PATCH CONTENT")
                .Respond("text/plain", "PATCH OK");            
            var client = new corerestclient.RestClient(mockHandler);
            client.contentType = "text/plain";
            var result = client.Patch("http://127.0.0.1", "PATCH CONTENT");
            Assert.True(result.Contains("PATCH OK"));
            mockHandler.VerifyNoOutstandingExpectation();
        }
    }    
}