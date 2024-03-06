using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using NewBack.Models.Exception;
using NewBack.Services.Contracts;
using Newtonsoft.Json;

namespace NewBack.AzureFunctions.AzureFunctions;

public class AddEvent
{
    private readonly IEventService _eventService;
    private readonly ILogger<AddEvent> _logger;
    
    // Constructeur pour l'injection de dépendances
    public AddEvent(IEventService eventService, ILogger<AddEvent> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }
    
    // Fonction pour ajouter un event
    [Function("AddEvent")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "event/add")]
        HttpRequestData req)
    {
        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Models.Event eEvent = JsonConvert.DeserializeObject<Models.Event>(requestBody);
            
            var response = req.CreateResponse();
            response.StatusCode = HttpStatusCode.OK;
            await _eventService.Add(eEvent);
            return response;
        }
        catch (AlreadyExistException e)
        {
            var response = req.CreateResponse();
            response.WriteString("Event already Exist");
            response.StatusCode = HttpStatusCode.Unauthorized;
            return response;
        }
        catch (Exception e)
        {
            var response = req.CreateResponse();
            response.WriteString("An error occurred in AddEvent Azure function");
            response.StatusCode = HttpStatusCode.InternalServerError;
            return response;
        }
    }
}