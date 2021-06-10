﻿using Application.Helpers;
using Application.Workflow;
using Domain.Abstract;
using Domain.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace Application.Services
{
    public class VirtualUser
    {
        private Plan _plan;
        private Execution _execution;
        private readonly IGenericRepository<Execution> _executionRepository;
        private readonly IGenericRepository<Result> _resultRepository;
        private readonly IWorkflowController _workflowService;

        private RestClient _restClient;
        private List<UserRequest> _requests;
        private List<Result> _results;

        private RequestData _data;

        public VirtualUser(Plan plan, 
            Execution execution,
            IGenericRepository<Execution> executionRepository,
            IGenericRepository<Result> resultRepository,
            IWorkflowController workflowService)
        {
            _plan = plan;
            _execution = execution;
            _executionRepository = executionRepository;
            _resultRepository = resultRepository;
            _workflowService = workflowService;

            _requests = new List<UserRequest>();
            _restClient = new RestClient(_plan.EndpointUrl);
            _results = new List<Result>();
        }

        public void CreateRequests()
        {
            if (_plan.Steps.Count == 0)
                throw new ApplicationException("Unexpected zero steps number");

            foreach (var step in _plan.Steps)
                _requests.Add(CreateRequest(step));
        }

        public async Task Run()
        {
            //if (_requests.Count == 0)
            //    CreateRequests();

            //foreach (var request in _requests)
            //{
            //    var watch = new Stopwatch();

            //    watch.Start();
            //    var response =  await _restClient.ExecuteAsync(request.RestRequest);
            //    watch.Stop();

            //    Console.WriteLine((int)response.StatusCode);

            //    Result r = new Result();
            //    r.RequestResource = request.RequestResource;
            //    r.StatusCode = (int)response.StatusCode;
            //    r.ResponseBody = response.Content;
            //    r.ResponseTime = watch.ElapsedMilliseconds;
            //    r.ExecutionId = _execution.Id;

            //    _results.Add(r);                
            //}

            _data = new RequestData { Plan = _plan, Client = _restClient, Execution = _execution };
            await _workflowService.StartWorkflow("RequestWorkflow", 1, _data);
            Console.WriteLine($"{_data.Results.Count}");
        }

        public async Task SaveResults()
        {
            _results = _data.Results;

            await _resultRepository.Create(_results);

            _execution.Results.AddRange(_results);
            await _executionRepository.Update(_execution);
        }

        private UserRequest CreateRequest(Step step)
        {
            RestRequest request = new RequestBuilder()
                .Resource(step.Resource)
                .Method(step.Method)
                .Headers(step.Headers)
                .Parameters(step.Parameters)
                .Body(step.Body)
                .Build();

            return new UserRequest { RestRequest = request, RequestResource = step.Resource };
        }
    }
}
