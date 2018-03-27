namespace MovieManager.Contracts.Commands.RelayCommands
{
    using System.Windows.Input;

    public interface IScanForLocalMovieFilesCommand : ICommand
    {
        ICommand Command { get; }
    }
}
