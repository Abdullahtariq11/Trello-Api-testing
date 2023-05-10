using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using NUnit.Framework;
using RestSharp;

namespace RestSharpT
{
    public class BaseTest
    {
        protected static IRestClient _client;

        [OneTimeSetUp]
        public static void IntializeRestClient() => _client = new RestClient("https://api.trello.com");
        protected IRestRequest requestWithAuthorization(string url) => requestWithoutAuthorization(url)
           .AddQueryParameter("key", "064e60c684237667c6b47f98ec2e77c0")
           .AddQueryParameter("token", "ATTAe939df95a776e1c4c92f72d7698356c1871bd9e2f0823160b3ecdcae54b77a7cFA6EEE9A");

        protected RestRequest requestWithoutAuthorization(string url) => new RestRequest(url);
    }
}
