using System.Text.Json;

namespace SimpleToDoApp.Wpf.IpcServer.Models
{
    public class IpcMessage
    {
        public string Command { get; set; } = string.Empty;
        public Dictionary<string, string> Parameters { get; set; } = new();

        public static IpcMessage Create(string command, Dictionary<string, string>? parameters = null)
        {
            return new IpcMessage
            {
                Command = command,
                Parameters = parameters ?? new Dictionary<string, string>()
            };
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static IpcMessage? Deserialize(string json)
        {
            return JsonSerializer.Deserialize<IpcMessage>(json);
        }
    }
} 