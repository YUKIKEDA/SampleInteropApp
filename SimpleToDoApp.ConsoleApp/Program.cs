using SimpleToDoApp.ConsoleApp.Services;

namespace SimpleToDoApp.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var ipcClient = new IpcClient();

            while (true)
            {
                Console.WriteLine("Enter a command to send to WPF app (or 'exit' to quit):");
                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || input.ToLower() == "exit")
                    break;

                try
                {
                    await ipcClient.SendMessageAsync(input);
                    Console.WriteLine("Message sent successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send message: {ex.Message}");
                }
            }
        }
    }
}
