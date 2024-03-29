﻿using Domain.ValueObjects;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace Application.Helpers
{
    public class RequestBuilder
    {
        private RestRequest _request = new RestRequest();

        public RequestBuilder Resource(string url)
        {
            _request.Resource = url;
            return this;
        }

        public RequestBuilder Method(int method)
        {
            _request.Method = (Method)method;
            return this;
        }

        public RequestBuilder Headers(ICollection<KeyValuePair<string, string>> headers)
        {
            if (headers != null && headers.Count > 0)
                _request.AddHeaders(headers);

            return this;
        }

        public RequestBuilder Headers(List<KeyValueParameter> headers)
        {
            if (headers != null && headers.Count > 0)
                _request.AddHeaders(headers.ToDictionary(h => h.Key, h => h.Value));

            return this;
        }

        public RequestBuilder Headers(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                List<KeyValueParameter> headers = JsonConvert.DeserializeObject<List<KeyValueParameter>>(json);
                return Headers(headers);
            }

            return this;
        }

        public RequestBuilder Parameters(ICollection<KeyValuePair<string, string>> parameters)
        {
            return AddOrUpdateParameters(parameters, ParameterType.QueryString);
        }

        public RequestBuilder Parameters(List<KeyValueParameter> parameters)
        {
            if(parameters != null)
                return AddOrUpdateParameters(parameters.ToDictionary(p => p.Key, p => p.Value), ParameterType.QueryString);

            return this;
        }

        public RequestBuilder Parameters(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                List<KeyValueParameter> parametes = JsonConvert.DeserializeObject<List<KeyValueParameter>>(json);
                return Parameters(parametes);
            }

            return this;
        }

        public RequestBuilder UrlSegments(ICollection<KeyValuePair<string, string>> parameters)
        {
            return AddOrUpdateParameters(parameters, ParameterType.UrlSegment);
        }

        public RequestBuilder UrlSegments(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                Dictionary<string, string> parametes = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                return AddOrUpdateParameters(parametes, ParameterType.UrlSegment);
            }

            return this;
        }

        public RequestBuilder Body(object obj)
        {
            if (obj != null)
                _request.AddJsonBody(obj);

            return this;
        }

        public RequestBuilder Body(string json)
        {
            if (!string.IsNullOrEmpty(json))
                _request.AddJsonBody(json);

            return this;
        }

        public RestRequest Build()
        {
            return _request;
        }

        private RequestBuilder AddOrUpdateParameters(ICollection<KeyValuePair<string, string>> parameters, ParameterType paramType)
        {
            if (parameters != null && parameters.Count > 0)
            {
                foreach (var parameter in parameters)
                    _request.AddOrUpdateParameter(parameter.Key, parameter.Value, paramType);
            }

            return this;
        }
    }
}
