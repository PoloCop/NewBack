﻿using NewBack.Models;

namespace NewBack.Services.Contracts;

public interface IEventService
{
    public Task Add(Event eEvent);
    public Task<IEnumerable<Event>> FindAll();
}