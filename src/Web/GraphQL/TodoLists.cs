using Security.Application.TodoLists.Queries.GetTodos;

namespace Security.Web.GraphQL;

public class TodoLists
{
    public async Task<TodosVm> GetTodoLists([Service] ISender sender)
    {
        return await sender.Send(new GetTodosQuery());
    }
}
