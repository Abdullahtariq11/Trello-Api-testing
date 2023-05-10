using System;
using RestSharp;
using System.Net;
using NUnit.Framework;
using RestSharpT.arguments.Holders;
using RestSharpT.arguments.providers;

namespace RestSharpT
{
    public class GetBoardsValidationTest : BaseTest
    {
 

        [Test]
        [TestCaseSource(typeof(BoardIdValidationArgumentsProvider))]
            public void CHeckGetBoardWithInvalidId(BoardIdValidationArgumentsHolder validationArguments)
        {
            var request = requestWithAuthorization("/1/boards/{id}?")
                .AddOrUpdateParameters(validationArguments.PathParams);// can accept any tupe of parameter
            var response = _client.ExecuteGetAsync(request);
            Assert.AreEqual(validationArguments.StatusCode, response.Result.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Result.Content);
        }

        [Test]
        public void CHeckGetBoardWithInvalidAuth()
        {
            var request = requestWithoutAuthorization("/1/boards/{id}?").AddUrlSegment("id", "6456d0a0c524c3ba6a8d322c");
            var response = _client.ExecuteGetAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.Result.StatusCode);
            Assert.AreEqual("unauthorized permission requested", response.Result.Content);
        }

        [Test]

        public void CheckGetBoardWithAnotherUserCredentials()
        {
            var request = requestWithoutAuthorization("/1/boards/{id}?").AddUrlSegment("id", "6456d0a0c524c3ba6a8d322c")
                .AddQueryParameter("key", "8b32218e6887516d17c84253faf967b6")
                .AddQueryParameter("token", "492343b8106e7df3ebb7f01e219cbf32827c852a5f9e2b8f9ca296b1cc604955");
            var response = _client.ExecuteGetAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.Result.StatusCode);
            Assert.AreEqual("invalid token", response.Result.Content);

        }

    }
}
