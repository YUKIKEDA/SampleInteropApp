using System.Runtime.InteropServices;
using SimpleToDoApp.Core.Models;

namespace VbaInterop
{
    /// <summary>
    /// VBAからの利用例：
    /// 
    /// 1. ビルド後、以下のファイルを同じフォルダに配置します：
    ///    - VbaInterop.dll
    ///    - VbaInterop.deps.json
    ///    - VbaInterop.manifest
    ///    - SimpleToDoApp.Core.dll
    /// 
    /// 2. VBAプロジェクトで以下のコードを使用：
    /// ```vba
    /// ' 新しいインスタンスを作成
    /// Dim Manager As Object
    /// Set Manager = CreateObject("VbaInterop.TodoManager")
    /// 
    /// ' Todoを追加
    /// Manager.AddTodo "買い物に行く"
    /// Manager.AddTodo "報告書を作成"
    /// 
    /// ' Todoを完了にする
    /// Manager.CompleteTodo 1
    /// 
    /// ' 全てのTodoを取得して表示
    /// Dim items() As String
    /// items = Manager.GetAllTodos
    /// 
    /// ' 結果を表示（Immediate Windowに出力）
    /// Dim item As Variant
    /// For Each item In items
    ///     Debug.Print item
    /// Next item
    /// 
    /// ' Excelのセルに表示する場合
    /// Dim i As Long
    /// For i = LBound(items) To UBound(items)
    ///     Cells(i + 1, 1).Value = items(i)
    /// Next i
    /// ```
    /// </summary>
    [ComVisible(true)]
    [Guid("E043AD74-F936-4817-B2F5-37F41CAFA3C3")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
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
