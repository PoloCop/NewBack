using NewBack.Models;

namespace NewBack.Repository.Contracts;

public interface IEventRepository
{
    public Task Add(Event eEvent);
}