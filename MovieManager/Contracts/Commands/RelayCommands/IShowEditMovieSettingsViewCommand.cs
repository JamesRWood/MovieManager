namespace MovieManager.Contracts.Commands.RelayCommands
{
    using System.Windows.Input;
    public interface IShowEditMovieSettingsViewCommand : ICommand
    {
        ICommand Command { get; }
    }
}
