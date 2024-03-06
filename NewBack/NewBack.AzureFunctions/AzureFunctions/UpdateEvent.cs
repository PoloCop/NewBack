using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using NewBack.Models.Exception;
using NewBack.Services.Contracts;
using Newtonsoft.Json;

namespace NewBack.AzureFunctions.AzureFunctions;

public class UpdateEvent
{
    private readonly IEventService _eventService;
    private readonly ILogger<UpdateEvent> _logger;
    
    // Constructeur pour l'injection de dépendances
    public UpdateEvent(IEventService eventService, ILogger<UpdateEvent> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }
    
    // Fonction pour modifier un event
    [Function("UpdateEvent")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "event")]
        HttpRequestData req)
    {
        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Models.Event eEvent = JsonConvert.DeserializeObject<Models.Event>(requestBody);
            
            await _eventService.Update(eEvent);
            return new StatusCodeResult(200);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in UpdateEvent Azure function");
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }
}