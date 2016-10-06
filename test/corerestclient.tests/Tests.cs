using System;
using Xunit;
using corerestclient;

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
    }
}