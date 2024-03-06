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
}