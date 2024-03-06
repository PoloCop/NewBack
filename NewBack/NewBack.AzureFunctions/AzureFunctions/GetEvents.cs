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

public class GetEvents
{
    private readonly IEventService _eventService;
    private readonly ILogger<GetEvents> _logger;

    // Constructeur pour l'injection de dépendances
    public GetEvents(IEventService eventService, ILogger<GetEvents> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }
    
    // Fonction pour obtenir tout les events
    [Function("GetEvents")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "events")] HttpRequestData req)
    {
        try
        {
            var events = await _eventService.FindAll();
            
            if (events == null)
            {
                _logger.LogWarning("Events not found");
                return new NotFoundResult();
            }
            
            return new OkObjectResult(events);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in GetEvents Azure function");
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }
}