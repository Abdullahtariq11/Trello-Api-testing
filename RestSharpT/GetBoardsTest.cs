using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Extensions;
using System.Net;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace RestSharpT
{
    public class GetBoardsTest: BaseTest
    {
       
        [Test]
        public void checkGetBoards()
        {
            // parameters added under braces
            var request = requestWithAuthorization("/1/members/abdullahtariq41/boards?")
                .AddQueryParameter("field","id,name")
                .AddUrlSegment("member", "abdullahtariq41");
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
      
        [Test]
        public void CheckGetBoard()
        {
            // parameters added under braces
            var request = requestWithAuthorization("/1/boards/{id}?")
                .AddUrlSegment("id", "6456d0a0c524c3ba6a8d322c");

            var response = _client.Get(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            //jtoken used to represent json object in the key value format, to accss available methods
            
        }

        [Test]
        public void CheckGetCards()
        {
            var request = requestWithAuthorization("/1/lists/{id}/cards?")
               .AddUrlSegment("id", "6456d0a0c524c3ba6a8d3233");
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }
        [Test]
        public void CheckGetCard()
        {
            var request = requestWithAuthorization("/1/cards/{id}")
                .AddUrlSegment("id", "6456ddace9ac115e18f7273a");
            var response = _client.Get(request);
            var card = JToken.Parse(response.Content);
            Assert.AreEqual("Test card", card.SelectToken("name").ToString());
        }

    }
}
