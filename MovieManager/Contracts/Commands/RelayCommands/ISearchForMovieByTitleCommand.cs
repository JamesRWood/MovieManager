namespace MovieManager.Contracts.Commands.RelayCommands
{
    using System.Windows.Input;

    public interface ISearchForMovieByTitleCommand : ICommand
    {
        ICommand Command { get; }
    }
}
