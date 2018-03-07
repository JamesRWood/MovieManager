namespace MovieManager.Contracts.Commands
{
    using System.Windows.Input;

    public interface IScanForLocalMovieFilesCommand : ICommand
    {
        ICommand Command { get; }
    }
}
