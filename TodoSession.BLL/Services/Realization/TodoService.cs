using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoSession.DataAccess;
using TodoSession.DataAccess.Models;
using TodoSession.BLL.Services.Interfaces;
using TodoSession.BLL.DTO.Todo;

namespace TodoSession.BLL.Services.Realization
{
    public class TodoService : ITodoService
    {
        private readonly ApplicationDbContext _db;
        public TodoService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync(int? userId)
        {
            var todos = await _db.Todos.Where(t => t.UserId == userId).ToListAsync();
            return todos;
        }

        public async Task<Todo> CreateTodoAsync(CreateTodoDto dto, int? userId)
        {
            var newTodo = new Todo
            {
                Text = dto.Text,
                UserId = userId
            };
            await _db.Todos.AddAsync(newTodo);
            await _db.SaveChangesAsync();
            return newTodo;
        }

        public async Task<Todo> UpdateTodoAsync(UpdateTodoDto dto)
        {
            var todoToUpdate = await _db.Todos.FindAsync(dto.Id);
            if (todoToUpdate == null) return null;
            _db.Entry(todoToUpdate).CurrentValues.SetValues(dto);
            await _db.SaveChangesAsync();
            return todoToUpdate;
        }

        public async Task<Todo> DeleteTodoAsync(int id)
        {
            var todoToDelete = await _db.Todos.FindAsync(id);
            if (todoToDelete == null) return null;
            _db.Todos.Remove(todoToDelete);
            await _db.SaveChangesAsync();
            return todoToDelete;
        }
    }
}
