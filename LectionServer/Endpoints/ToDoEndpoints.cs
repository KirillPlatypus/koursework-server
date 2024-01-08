using LectionServer.Contracts;
using LectionServer.Endpoints.Data;
using LectionServer.Models;
using LectionServer.Services;

namespace LectionServer.Endpoints
{
    public static class ToDoEndpoints
    {
        private const string EndpointsTag = "ToDo";

        public static void MapToDoEndpoints(this WebApplication app)
        {
            app.MapGet("api/todo", GetToDos)
                .RequireAuthorization()
                .Produces<IEnumerable<ToDo>>()
                .WithDescription("Get all ToDo")
                .WithTags(EndpointsTag)
                .WithOpenApi();

            app.MapGet("api/todo/{id:guid}", GetToDo)
                .RequireAuthorization()
                .Produces<ToDo>()
                .Produces(StatusCodes.Status404NotFound)
                .WithDescription("Get a task with an id")
                .WithTags(EndpointsTag)
                .WithOpenApi();

            app.MapPost("api/todo", AddToDo)
                .RequireAuthorization()
                .Accepts<ToDoRequest>("application/json")
                .Produces<ToDo>(StatusCodes.Status201Created)
                .WithDescription("Create a new task")
                .WithTags(EndpointsTag)
                .WithOpenApi();

            app.MapPut("api/todo/{id:guid}", UpdateToDo)
                .RequireAuthorization()
                .Accepts<ToDoRequest>("application/json")
                .Produces<ToDo>()
                .Produces(StatusCodes.Status404NotFound)
                .WithDescription("Update a task with an id")
                .WithTags(EndpointsTag)
                .WithOpenApi();

            app.MapDelete("api/todo/{id:guid}", DeleteToDo)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .WithDescription("Delete a task with an id")
                .WithTags(EndpointsTag)
                .WithOpenApi();

            app.MapDelete("api/todo", ClearToDo)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .WithDescription("Delete all tasks")
                .WithTags(EndpointsTag)
                .WithOpenApi();
        }

        private static IResult GetToDos(ToDoService toDoService, RequestData requestData, CancellationToken cancellationToken)
        {
            var result = toDoService.GetToDos(requestData.UserId);
            return Results.Json(result);
        }

        private static IResult GetToDo(ToDoService toDoService, RequestData requestData, Guid id, CancellationToken cancellationToken)
        {
            var result = toDoService.GetToDo(id, requestData.UserId);
            return result is null ? Results.NotFound() : Results.Json(result);
        }

        private static IResult AddToDo(ToDoService toDoService, RequestData requestData, ToDoRequest request, CancellationToken cancellationToken)
        {
            var result = toDoService.AddToDo(request, requestData.UserId);
            return Results.Created($"api/todo/{result.Id}", result);
        }

        private static IResult UpdateToDo(ToDoService toDoService, RequestData requestData, Guid id, ToDoRequest request, CancellationToken cancellationToken)
        {
            var result = toDoService.UpdateToDo(id, request, requestData.UserId);
            return result is null ? Results.NotFound() : Results.Json(result);
        }

        private static IResult DeleteToDo(ToDoService toDoService, RequestData requestData, Guid id, CancellationToken cancellationToken)
        {
            toDoService.DeleteToDo(id, requestData.UserId);
            return Results.Ok();
        }

        private static IResult ClearToDo(ToDoService toDoService, RequestData requestData, CancellationToken cancellationToken)
        {
            toDoService.ClearToDo(requestData.UserId);
            return Results.Ok();
        }
    }
}
