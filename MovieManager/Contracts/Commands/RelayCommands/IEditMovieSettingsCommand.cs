namespace MovieManager.Contracts.Commands.RelayCommands
{
    using System.Windows.Input;
    public interface IEditMovieSettingsCommand : ICommand
    {
        ICommand Command { get; }
    }
}
