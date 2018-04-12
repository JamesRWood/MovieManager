namespace MovieManager.Contracts.Commands.RelayCommands
{
    using System.Windows.Input;

    public interface ISlideGridCommand : ICommand
    {
        ICommand Command { get; }
    }
}
