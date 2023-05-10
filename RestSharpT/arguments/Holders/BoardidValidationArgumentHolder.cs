using System.Net;
using System.Collections.Generic;
using RestSharp;

namespace RestSharpT.arguments.Holders
{

        public class BoardIdValidationArgumentsHolder
        {
            public IEnumerable<Parameter> PathParams { get; set; }

            public string ErrorMessage { get; set; }

            public HttpStatusCode StatusCode { get; set; }
        }

}

