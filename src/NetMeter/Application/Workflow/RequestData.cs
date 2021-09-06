using Application.Services;
using Domain.Entities;
using Domain.ValueObjects;
using RestSharp;
using System.Collections.Generic;

namespace Application.Workflow
{
    public class RequestData
    {
        private List<Result> _results;

        public Plan Plan { get; set; }
        public RestClient Client { get; set; }
        public List<KeyValueParameter> Variables { get; set; } = new List<KeyValueParameter>();
        public UserRequest Request { get; set; }
        public Execution Execution { get; set; }
        public Result Result { get; set; }
        public List<Result> Results
        {
            get
            {
                if (_results == null)
                {
                    _results = new List<Result>();
                    return _results; // redundant
                }

                return _results;
                // consider:
                // return _result ??= new List<Result>();
            }
            set
            {
                _results = value;
            }
        }
        public IRestResponse Response { get; set; }
    }
}
