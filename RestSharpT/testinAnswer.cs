using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Extensions;
using System.Net;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace RestSharpT
{
   
    public class testinAnswer
    {
        private static RestClient client;

        [OneTimeSetUp]
        public void IntializeClient() => client = new RestClient("https://openlibrary.org");

        [Test]
        public void CheckResponseMatch()
        {
            var request = new RestRequest("search.json").AddQueryParameter("title", "Goodnight Moon Base");
            var response = client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var content = JToken.Parse(response.Content);
            var expectedJson = File.ReadAllText("C:/Users/abdul/Documents/Study/Software tessting/API Testing/New folder/RestSharpT/RestSharpT/resources/expectedResponse.json");
            var expected = JToken.Parse(expectedJson);

            bool isEqual = JToken.DeepEquals(content, expectedJson);
            if (isEqual)
            {
                Console.WriteLine($"The response for Goodnight Moon Base matches the expected response.");
            }
            else
            {
                Console.WriteLine($"The response for Goodnight Moon Base does not match the expected response:");
                content = content.SelectToken("docs");
                expected = expected.SelectToken("docs");
            }

            string[] propertiesName = new string[] { "key", "type", "seed" , "title", "title_suggest","title_sort",
        "edition_count", "edition_key","publish_date", "publish_year", "first_publish_year", "number_of_pages_median",
        "isbn","last_modified_i","ebook_count_i","ebook_access","has_fulltext","public_scan_b","readinglog_count",
        "want_to_read_count","currently_reading_count","already_read_count","cover_edition_key","cover_i","publisher",
        "language","author_key","author_name","subject","publisher_facet","subject_facet","_version_","subject_key"};

            foreach (var propertyName in propertiesName)
            {
                if (content.SelectTokens(propertyName).Count() != 0 && expected.SelectTokens(propertyName).Count() != 0)
                {
                    var contentValues = content.SelectTokens(propertyName).Select(x => x.Value<string>());
                    var expectedValues = expected.SelectTokens(propertyName).Select(x => x.Value<string>());
                    if (!contentValues.SequenceEqual(expectedValues))
                    {
                        Console.WriteLine($"Difference found in property: {propertyName}");
                        Console.WriteLine($"Expected value: {string.Join(", ", expectedValues)}");
                        Console.WriteLine($"Actual value: {string.Join(", ", contentValues)}");
                    }
                }
                else
                {
                    if (content.SelectTokens(propertyName).Count() == 0 && expected.SelectTokens(propertyName).Count() != 0)
                    {
                        Console.WriteLine($"Missing property in response: {propertyName}");
                        Console.WriteLine($"Expected value: {string.Join(", ", expected.SelectTokens(propertyName).Select(x => x.Value<string>()))}");
                    }
                    else if (content.SelectTokens(propertyName).Count() != 0 && expected.SelectTokens(propertyName).Count() == 0)
                    {
                        Console.WriteLine($"Unexpected property in response: {propertyName}");
                        Console.WriteLine($"Actual value: {string.Join(", ", content.SelectTokens(propertyName).Select(x => x.Value<string>()))}");
                    }
                }
            }
        }

    }
}
