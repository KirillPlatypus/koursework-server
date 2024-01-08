using LectionServer.Contracts;
using LectionServer.Endpoints.Data;
using LectionServer.Models;
using LectionServer.Services;

namespace LectionServer.Endpoints
{
    public static class SessionEndpoints
    {
        private const string EndpointsTag = "Session";

        public static void MapSessionEndpoints(this WebApplication app)
        {
            app.MapGet("api/session", GetSessions)
               .RequireAuthorization()
               .Produces<IEnumerable<Session>>()
               .WithDescription("Get all session")
               .WithTags(EndpointsTag)
               .WithOpenApi();

            app.MapGet("api/session/{id:guid}", GetSession)
                .RequireAuthorization()
                .Produces<Session>()
                .Produces(StatusCodes.Status404NotFound)
                .WithDescription("Get a session with an id")
                .WithTags(EndpointsTag)
                .WithOpenApi();

            app.MapPost("api/session", AddSession)
                .RequireAuthorization()
                .Accepts<SessionRequest>("application/json")
                .Produces<Session>(StatusCodes.Status201Created)
                .WithDescription("Create a new session")
                .WithTags(EndpointsTag)
                .WithOpenApi();

            app.MapPut("api/session/{id:guid}", UpdateSession)
                .RequireAuthorization()
                .Accepts<SessionRequest>("application/json")
                .Produces<Session>()
                .Produces(StatusCodes.Status404NotFound)
                .WithDescription("Update a session with an id")
                .WithTags(EndpointsTag)
                .WithOpenApi();
            app.MapDelete("api/session/{id:guid}", DeleteSession)
               .RequireAuthorization()
               .Produces(StatusCodes.Status200OK)
               .WithDescription("Delete a session with an id")
               .WithTags(EndpointsTag)
               .WithOpenApi();

        }
        private static IResult GetSessions(SessionService sessionService, RequestData requestData, CancellationToken cancellationToken)
        {
            var result = sessionService.GetSessions(requestData.UserId);
            return Results.Json(result);
        }

        private static IResult DeleteSession(SessionService sessionService, RequestData requestData, Guid id, CancellationToken cancellationToken)
        {
            sessionService.DeleteSession(id, requestData.UserId);
            return Results.Ok();
        }

        private static IResult GetSession(SessionService sessionService, RequestData requestData, Guid id, CancellationToken cancellationToken)
        {
            var result = sessionService.GetSession(id, requestData.UserId);
            return result is null ? Results.NotFound() : Results.Json(result);
        }

        private static IResult AddSession(SessionService sessionService, RequestData requestData, SessionRequest request, CancellationToken cancellationToken)
        {
            var result = sessionService.AddSession(request, requestData.UserId);
            return Results.Created($"api/session/{result.Id}", result);
        }

        private static IResult UpdateSession(SessionService sessionService, RequestData requestData, Guid id, SessionRequest request, CancellationToken cancellationToken)
        {
            var result = sessionService.UpdateSession(id, request, requestData.UserId);
            return result is null ? Results.NotFound() : Results.Json(result);
        }
    }
}
