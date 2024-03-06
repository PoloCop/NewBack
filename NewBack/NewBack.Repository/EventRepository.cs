using Microsoft.EntityFrameworkCore;
using NewBack.DbContext;
using NewBack.Models;
using NewBack.Models.Exception;
using NewBack.Repository.Contracts;

namespace NewBack.Repository;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;

    public EventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(Event eEvent)
    {

        if (UniqueEvent(eEvent.Id))
        {
            _context.Events.Add(eEvent);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new AlreadyExistException("Event already exist!");
        }
    }
    private bool UniqueEvent(Guid idEvent)
    {

        return !_context.Events.Any(e => e.Id == idEvent);
    }
    public async Task<IEnumerable<Event>> FindAll()
    {
        return await _context.Events.ToListAsync();
    }
    public async Task Update(Event eEvent)
    {
        _context.Events.Update(eEvent);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(Guid eventId)
    {
        var eEvent = await Find(eventId);
        _context.Events.Remove(eEvent);
        await _context.SaveChangesAsync();
    }
    public async Task<Event> Find(Guid eventId)
    {
        return await _context.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
    }
}