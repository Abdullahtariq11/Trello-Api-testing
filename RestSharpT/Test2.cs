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
    public class Test2
    {
        
        private static RestClient client;

        [OneTimeSetUp]
        public static void IntializeRestClient()=>client=new RestClient("http://google.com");

        [Test]
        public void ApiTest ()
        {
            var request = new RestRequest();
            request.Method = Method.GET;
            Console.WriteLine($"{client.BaseUrl} {request.Method}");
            var response = client.Get(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);


        }
    } 
}
