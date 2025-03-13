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
                Console.WriteLine("\n利用可能なコマンド:");
                Console.WriteLine("1. タスクを追加 (add <タイトル>)");
                Console.WriteLine("2. タスクを削除 (delete <ID>)");
                Console.WriteLine("3. 終了 (exit)");
                Console.Write("\nコマンドを入力してください: ");

                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) || input.ToLower() == "exit")
                    break;

                try
                {
                    var parts = input.Split(' ', 2);
                    var command = parts[0].ToLower();

                    switch (command)
                    {
                        case "add" when parts.Length > 1:
                            await ipcClient.AddTodoAsync(parts[1]);
                            Console.WriteLine("タスクが追加されました");
                            break;

                        case "delete" when parts.Length > 1 && int.TryParse(parts[1], out int id):
                            await ipcClient.DeleteTodoAsync(id);
                            Console.WriteLine("タスクが削除されました");
                            break;

                        default:
                            Console.WriteLine("無効なコマンドです。正しい形式で入力してください。");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"エラー: {ex.Message}");
                }
            }
        }
    }
}
