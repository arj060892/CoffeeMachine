namespace CoffeeMachine.Application.Contracts.ApplicationHelper
{
    /// <summary>
    /// Helps in defining custom logics for System.Console sealed class
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// Will override System.Console.ReadLine() method for custom implimentation
        /// </summary>
        /// <returns>either user input or mocked value from implimentation</returns>
        string ReadLine();
    }
}