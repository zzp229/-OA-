using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MyToDo.Api.Context.Repository
{

    /// <summary>
    /// 有增删查改方法
    /// </summary>
    public class ToDoRepository : Repository<ToDo>, IRepository<ToDo>
    {
        public ToDoRepository(MyToDoContext dbContext) : base(dbContext)
        {
        }
    }
}
