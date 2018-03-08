namespace MovieManager.Contracts
{
    using System.Collections.Generic;

    public interface ICommonDataViewModel
    {
        List<string> MovieFileTypes { get; }
    }
}
