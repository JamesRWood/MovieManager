namespace MovieManager.Contracts.Commands.RelayCommands
{
    using System.Windows.Input;

    public interface ISearchForMovieWithTextCommand
    {
        ICommand Command { get; }
    }
}
