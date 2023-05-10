using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Net;
using NUnit.Framework;

namespace RestSharpT
{
    public class TrelloTest
    {
        private static RestClient _client;

        [OneTimeSetUp]
        public static void IntializeRestClient() => _client = new RestClient("http://api.trello.com");
    

        [Test]
        public void checkTrelloApi()
        {
            var request = new RestRequest();
            Console.WriteLine($"{_client.BaseUrl} {request.Method}");

            var response = _client.Get(request);
            Console.WriteLine(response.Content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
 
    }
}
