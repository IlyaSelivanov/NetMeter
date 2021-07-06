using Application.Services;
using Domain.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Application.Workflow.Steps
{
    public class SendRequest : StepBodyAsync
    {
        public RestClient Client { get; set; }
        public UserRequest Request { get; set; }
        public Result Result { get; set; }
        public Execution Execution { get; set; }
        public List<Result> Results { get; set; }
        public IRestResponse Response { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Console.WriteLine("Sending request...");

            var watch = new Stopwatch();

            watch.Start();
            var response = await Client.ExecuteAsync(Request.RestRequest);
            Response = response;
            watch.Stop();

            Console.WriteLine((int)response.StatusCode);

            Result = new Result();
            Result.RequestResource = Request.RequestResource;
            Result.StatusCode = (int)response.StatusCode;
            Result.ResponseBody = response.Content;
            Result.ResponseTime = watch.ElapsedMilliseconds;
            Result.ExecutionId = Execution.Id;
            Result.UserId = Execution.UserId;

            return ExecutionResult.Next();
        }
    }
}
