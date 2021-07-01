using Application.Workflow.Steps;
using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Application.Workflow
{
    public class RequestWorkflow : IWorkflow<RequestData>
    {
        public string Id => "RequestWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<RequestData> builder)
        {
            builder
                .StartWith(context =>
                {
                    Console.WriteLine("Starting workflow...");
                    return ExecutionResult.Next();
                })
                .ForEach(data => data.Plan.Steps, data => false)
                    .Do(x => x
                            .StartWith<CreateRequest>()
                                .Input(step => step.Step, (data, context) => context.Item)
                                .Input(step => step.Variables, (data, context) => data.Variables)
                                .Output(data => data.Request, step => step.Request)
                            .Then<SendRequest>()
                                .Input(step => step.Client, (data, context) => data.Client)
                                .Input(step => step.Request, (data, context) => data.Request)
                                .Input(step => step.Results, (data, context) => data.Results)
                                .Input(step => step.Execution, (data, context) => data.Execution)
                                .Input(step => step.Response, (data, context) => data.Response)
                                .Output(data => data.Result, step => step.Result)
                                .Output(data => data.Results, step => step.Results)
                                .Output(data => data.Response, step => step.Response)
                            .Then<SaveResults>()
                                .Input(step => step.Result, (data, context) => data.Result)
                                .Input(step => step.Execution, (data, context) => data.Execution)
                            .Then<UpdateVariables>()
                                .Input(step => step.Step, (data, context) => context.Item)
                                .Input(step => step.Variables, (data, context) => data.Variables)
                                .Input(step => step.Response, (data, context) => data.Response)
                                .Output(data => data.Variables, step => step.Variables)
                            )
                .Then(context =>
                {
                    Console.WriteLine("Workflow complete.");
                })
                .EndWorkflow();            
        }
    }
}
