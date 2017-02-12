using System.Threading.Tasks;

namespace AccountManagement.Framework
{
    public interface IEvent
    {
    }

    public interface ICommand
    {
    }

    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }

    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}
