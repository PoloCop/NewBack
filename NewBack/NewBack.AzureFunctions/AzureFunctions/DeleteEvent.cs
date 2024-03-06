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

public class DeleteEvent
{
    private readonly IEventService _eventService;
    private readonly ILogger<DeleteEvent> _logger;
    
    // Constructeur pour l'injection de dépendances
    public DeleteEvent(IEventService eventService, ILogger<DeleteEvent> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }
    
    // Fonction pour suprimer un event
    [Function("DeleteEvent")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "event/{eventId}/delete")] HttpRequestData req, Guid eventId)
    {
        try
        {
            await _eventService.Delete(eventId);
            return new StatusCodeResult(200);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in DeleteEvent Azure function");
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }
}