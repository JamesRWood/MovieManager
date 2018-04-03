namespace MovieManager.Contracts.Commands.RelayCommands
{
    using System.Windows.Input;

    public interface ISetSelectedMovieCommand : ICommand
    {
        ICommand Command { get; }
    }
}
