using System.Runtime.InteropServices;
using SimpleToDoApp.Core.Models;

namespace VbaInterop
{
    /// <summary>
    /// VBAからTodoリストを管理するためのクラスです。
    /// 
    /// セットアップ手順:
    /// 1. ビルドされた以下のファイルを任意のフォルダに配置します：
    ///    - SimpleToDoApp.VbaInterop.dll
    ///    - SimpleToDoApp.Core.dll
    ///    - TodoApp.manifest
    /// 
    /// 2. Excelマクロの設定:
    ///    a. VBEエディタを開きます（Alt + F11）
    ///    b. 新しいモジュールを追加します（挿入 > モジュール）
    ///    c. 以下のようなコードを記述します：
    /// 
    /// Option Explicit
    /// 
    /// Private todoManager As Object
    /// 
    /// ' 初期化処理
    /// Sub InitializeTodoManager()
    ///     Set todoManager = CreateObject("VbaInterop.TodoManager")
    /// End Sub
    /// 
    /// ' タスクを追加する例
    /// Sub AddNewTask()
    ///     If todoManager Is Nothing Then InitializeTodoManager
    ///     
    ///     ' セルA1からタスク名を取得して追加
    ///     Dim taskTitle As String
    ///     taskTitle = Range("A1").Value
    ///     todoManager.AddTodo taskTitle
    ///     
    ///     ' 全タスクを取得して表示
    ///     DisplayAllTasks
    /// End Sub
    /// 
    /// ' タスクを完了にする例
    /// Sub CompleteTask()
    ///     If todoManager Is Nothing Then InitializeTodoManager
    ///     
    ///     ' セルB1からタスクIDを取得して完了にする
    ///     Dim taskId As Integer
    ///     taskId = Range("B1").Value
    ///     todoManager.CompleteTodo taskId
    ///     
    ///     ' 全タスクを取得して表示
    ///     DisplayAllTasks
    /// End Sub
    /// 
    /// ' 全タスクを表示する例
    /// Sub DisplayAllTasks()
    ///     If todoManager Is Nothing Then InitializeTodoManager
    ///     
    ///     ' 全タスクを取得
    ///     Dim items() As String
    ///     items = todoManager.GetAllTodos
    ///     
    ///     ' C1セルから下に向かって表示
    ///     Dim i As Integer
    ///     For i = 0 To UBound(items)
    ///         Range("C" & (i + 1)).Value = items(i)
    ///     Next i
    /// End Sub
    /// 
    /// エラー処理:
    /// - COMオブジェクトの作成に失敗する場合：
    ///   1. マニフェストファイルが正しい場所にあることを確認
    ///   2. DLLファイルが正しい場所にあることを確認
    ///   3. .NET 8.0ランタイムがインストールされていることを確認
    /// 
    /// 注意事項:
    /// - todoManagerオブジェクトはモジュールレベルで保持することを推奨
    /// - 必要に応じてエラーハンドリングを追加することを推奨
    /// - Excelを閉じる際にオブジェクトは自動的に解放されます
    /// </summary>
    [ComVisible(true)]
    [Guid("E043AD74-F936-4817-B2F5-37F41CAFA3C3")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("VbaInterop.TodoManager")]
    public class TodoManager
    {
        private List<TodoItem> _todos;

        public TodoManager()
        {
            _todos = new List<TodoItem>();
        }

        public void AddTodo(string title)
        {
            var todo = new TodoItem(title);
            todo.Id = _todos.Count + 1;
            _todos.Add(todo);
        }

        public void CompleteTodo(int id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                todo.IsCompleted = true;
            }
        }

        public string[] GetAllTodos()
        {
            return _todos.Select(t => $"{t.Id}: {t.Title} - {(t.IsCompleted ? "完了" : "未完了")}").ToArray();
        }
    }
}
