using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using EventManagement.Models.Models;
using EventManagement.DAL.Data;

public class AddEventFunction
{
    private readonly AppDbContext _context;

    public AddEventFunction(AppDbContext context)
    {
        _context = context;
    }

    [Function("AddEvent")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "events")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("AddEventFunction");
        logger.LogInformation("C# HTTP trigger function processed a request to add an event.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var inputEvent = JsonConvert.DeserializeObject<Event>(requestBody);

        if (inputEvent != null)
        {
            _context.Events.Add(inputEvent);
            await _context.SaveChangesAsync();

            var response = req.CreateResponse(System.Net.HttpStatusCode.Created);
            await response.WriteAsJsonAsync(inputEvent);
            return response;
        }
        else
        {
            return req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
        }
    }
}