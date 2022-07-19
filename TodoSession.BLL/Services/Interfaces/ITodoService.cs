using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoSession.DataAccess.Models;
using TodoSession.BLL.DTO.Todo;

namespace TodoSession.BLL.Services.Interfaces
{
    public interface ITodoService
    {
        public Task<IEnumerable<Todo>> GetTodosAsync(int? userId);
        public Task<Todo> CreateTodoAsync(CreateTodoDto dto, int? userId);
        public Task<Todo> UpdateTodoAsync(UpdateTodoDto dto);
        public Task<Todo> DeleteTodoAsync(int id);
    }
}
