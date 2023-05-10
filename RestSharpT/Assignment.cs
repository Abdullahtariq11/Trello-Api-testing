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
    public class Assignment
    {
        private static RestClient client;

        [OneTimeSetUp]
        public void IntializeClient() => client = new RestClient("https://openlibrary.org");


        [Test]
        public void CheckgetBooks()
        {
            var request = new RestRequest("search.json").AddQueryParameter("title", "Goodnight Moon");
            var response = client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var content = JToken.Parse(response.Content);
            var TotalnumberOfBooks = content.SelectToken("numFound").ToString();


            Console.WriteLine($"Total number of books with the title matching exactly [Goodnight Moon]: " +
                $"{TotalnumberOfBooks}");

            var Books = content.SelectToken("docs");
            foreach (var Book in Books)
            {
                if (Book.SelectToken("publish_year") != null && Book.SelectToken("publish_year").Count() > 0 && (int)Book.SelectToken("publish_year")[0] >= 2000)
                {
                    Console.WriteLine(Book.SelectToken("key"));
                }
            }

        }

        [Test]

        public void CheckResponseMatch()
        {
            var request = new RestRequest("search.json").AddQueryParameter("title", "Goodnight Moon Base");
            var response = client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var contents = JToken.Parse(response.Content);
            var expectedJson = File.ReadAllText("C:/Users/abdul/Documents/Study/Software tessting/API Testing/New folder/RestSharpT/RestSharpT/resources/expectedResponse.json");
            var expected = JToken.Parse(expectedJson);
            //Assert.IsTrue(JToken.DeepEquals(expected, content));

            bool isEqual = JToken.DeepEquals(contents, expectedJson);
            if (isEqual)
            {
                Console.WriteLine($"The response for Goodnight Moon Base matches the expected response.");
            }
            else
            {
                Console.WriteLine($"The response for Goodnight Moon Base does not match the expected response:");
                //Console.WriteLine(content.ToString(Formatting.Indented));
                contents = contents.SelectToken("docs");
                expected = expected.SelectToken("docs");

            }
            string[] properstiesName = new string[] { "key", "type", "seed" , "title", "title_suggest","title_sort",
            "edition_count", "edition_key","publish_date", "publish_year", "first_publish_year", "number_of_pages_median",
            "isbn","last_modified_i","ebook_count_i","ebook_access","has_fulltext","public_scan_b","readinglog_count",
            "want_to_read_count","currently_reading_count","already_read_count","cover_edition_key","cover_i","publisher",
            "language","author_key","author_name","subject","publisher_facet","subject_facet","_version_","subject_key"};


            foreach (var proper in properstiesName)
            {
                foreach (var expect in expected)
                {

                    foreach (var content in contents)
                    {
                        expected = JToken.Parse(expectedJson);
                        expected = expected.SelectToken("docs");

                        if (content.SelectTokens(proper) != null && expect.SelectTokens(proper) != null)
                        {
                            if (!JToken.DeepEquals(content.SelectToken(proper), expect.SelectToken(proper)))
                            {

                                Console.WriteLine($"Difference found in property: {proper}");
                                Console.WriteLine("actual");
                                Console.WriteLine(content.SelectToken(proper));
                                Console.WriteLine("expected");
                                Console.WriteLine(expect.SelectToken(proper));


                            }
                        }
                        else
                        {
                            if (content.SelectTokens(proper) == null && expect.SelectTokens(proper) != null)
                            {
                                Console.WriteLine($"Missing property in response: {proper}");
                                Console.WriteLine(expect.SelectToken(proper));
                            }
                            else if (content.SelectTokens(proper) != null && expect.SelectTokens(proper) == null)
                            {
                                Console.WriteLine(expect.SelectToken(proper));
                                Console.WriteLine(content.SelectToken(proper)); ;
                            }
                        }

                    }
                }



            }

        }
    }
}

    

