using SimpleToDoApp.Core.Services;
using System.Text.Json;

namespace SimpleToDoApp.Wpf.IpcServer.Models
{
    public class IpcMessageHandler
    {
        private readonly TodoService _todoService;

        public IpcMessageHandler(TodoService todoService)
        {
            _todoService = todoService;
        }

        public async Task HandleMessageAsync(string message)
        {
            var ipcMessage = IpcMessage.Deserialize(message);
            if (ipcMessage == null) return;

            switch (ipcMessage.Command.ToLower())
            {
                case "addtodo":
                    if (ipcMessage.Parameters.TryGetValue("title", out var title))
                    {
                        await _todoService.AddTodoAsync(title);
                    }
                    break;

                case "deletetodo":
                    if (ipcMessage.Parameters.TryGetValue("id", out var idStr) &&
                        int.TryParse(idStr, out var id))
                    {
                        await _todoService.DeleteTodoAsync(id);
                    }
                    break;
            }
        }
    }
} 