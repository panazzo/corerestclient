using System;
using Xunit;
using corerestclient;

namespace Tests
{
    public class Tests
    {
        [Fact]
        public void Test1() 
        {
            var client = new corerestclient.RestClient();
            Console.WriteLine("Hello from test");
            Assert.NotNull(client);
        }
    }
}
