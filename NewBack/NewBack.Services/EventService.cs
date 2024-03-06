﻿using NewBack.Models;
using NewBack.Repository.Contracts;
using NewBack.Services.Contracts;

namespace NewBack.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    public async Task Add(Event eEvent)
    {
        await _eventRepository.Add(eEvent);
    }
}