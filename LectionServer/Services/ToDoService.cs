using System.Collections.Immutable;
using LectionServer.Contracts;
using LectionServer.Models;


namespace LectionServer.Services
{
    public class ToDoService
    {
        private string name = "ToDo.json";
        private JSONManager<List<ToDo>> jsonManager;

        public ToDoService()
        {
            jsonManager = new(name);
        }

        private readonly List<ToDo> _todo = new();

        public IImmutableList<ToDo> GetToDos(Guid userId)
        {
            _todo.Clear();
            _todo.AddRange(jsonManager.Get() == null ? new() : jsonManager.Get());

            return _todo.Where(x => x.UserId == userId).ToImmutableList();
        }

        public ToDo? GetToDo(Guid id, Guid userId)
        {
            _todo.Clear();
            _todo.AddRange(jsonManager.Get() == null ? new() : jsonManager.Get());

            return _todo.SingleOrDefault(x => x.Id == id && x.UserId == userId);
        }

        public ToDo AddToDo(ToDoRequest request, Guid userId)
        {
            var todo = new ToDo
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Data = request.Data,
                Name = request.Name,
                IsCompleted = request.IsCompleted,
                Lesson = request.Lesson,
            };
            _todo.Add(todo);
            jsonManager.Set(_todo);

            return todo;
        }

        public ToDo? UpdateToDo(Guid id, ToDoRequest request, Guid userId)
        {
            var toDo = GetToDo(id, userId);
            if (toDo is null) return null;
            toDo.Name = request.Name;
            toDo.Data = request.Data;
            toDo.IsCompleted = request.IsCompleted;
            toDo.Lesson = request.Lesson;

            _todo[_todo.FindIndex(_todo => _todo.Id == toDo.Id)] = toDo;
            jsonManager.Set(_todo);

            return toDo;
        }

        public void DeleteToDo(Guid id, Guid userId)
        {
            var toDo = GetToDo(id, userId);
            if (toDo is null) return;
            _todo.Remove(toDo);
            jsonManager.Set(_todo);

        }

        public void ClearToDo(Guid userId)
        {
            _todo.RemoveAll(x => x.UserId == userId);
            jsonManager.Set(_todo);

        }
    }
}
