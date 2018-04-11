namespace MovieManager.Contracts.Commands.RelayCommands
{
    using System.Windows.Input;

    public interface IAnimateToGridCommand : ICommand
    {
        ICommand Command { get; }
    }
}
