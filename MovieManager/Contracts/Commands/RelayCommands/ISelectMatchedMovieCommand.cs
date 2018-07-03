namespace MovieManager.Contracts.Commands.RelayCommands
{
    using System.Windows.Input;

    public interface ISelectMatchedMovieCommand : ICommand
    {
        ICommand Command { get; }
    }
}
