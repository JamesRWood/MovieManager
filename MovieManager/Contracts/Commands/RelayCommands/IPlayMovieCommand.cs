namespace MovieManager.Contracts.Commands.RelayCommands
{
    using System.Windows.Input;

    public interface IPlayMovieCommand : ICommand
    {
        ICommand Command { get; }
    }
}
