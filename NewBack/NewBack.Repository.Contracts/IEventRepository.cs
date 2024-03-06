using NewBack.Models;

namespace NewBack.Repository.Contracts;

public interface IEventRepository
{
    public Task Add(Event eEvent);
    public Task<IEnumerable<Event>> FindAll();
    public Task Update(Event eEvent);
}