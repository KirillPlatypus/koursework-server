using LectionServer.Contracts;
using LectionServer.Endpoints.Data;
using LectionServer.Models;
using LectionServer.Services;

namespace LectionServer.Endpoints
{
    public static class ScheduleEndpoints
    {
        private const string EndpointsTag = "Schedule";

        public static void MapScheduleEndpoints(this WebApplication app)
        {
            app.MapGet("api/schedule", GetSchedules)
                .RequireAuthorization()
                .Produces<IEnumerable<Schedule>>()
                .WithDescription("Get all Schedule")
                .WithTags(EndpointsTag)
                .WithOpenApi();

            app.MapGet("api/schedule/{id:guid}", GetSchedule)
                .RequireAuthorization()
                .Produces<Schedule>()
                .Produces(StatusCodes.Status404NotFound)
                .WithDescription("Get a schedule with an id")
                .WithTags(EndpointsTag)
                .WithOpenApi();

            app.MapPost("api/schedule", AddSchedule)
                .RequireAuthorization()
                .Accepts<ScheduleRequest>("application/json")
                .Produces<Schedule>(StatusCodes.Status201Created)
                .WithDescription("Create a new schedule")
                .WithTags(EndpointsTag)
                .WithOpenApi();

            app.MapPut("api/schedule/{id:guid}", UpdateSchedule)
                .RequireAuthorization()
                .Accepts<ScheduleRequest>("application/json")
                .Produces<Schedule>()
                .Produces(StatusCodes.Status404NotFound)
                .WithDescription("Update a schedule with an id")
                .WithTags(EndpointsTag)
                .WithOpenApi();

            app.MapDelete("api/schedule/{id:guid}", DeleteSchedule)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .WithDescription("Delete a schedule with an id")
                .WithTags(EndpointsTag)
                .WithOpenApi();

            app.MapDelete("api/schedule", ClearSchedule)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .WithDescription("Delete all Schedule")
                .WithTags(EndpointsTag)
                .WithOpenApi();
        }

        private static IResult GetSchedules(ScheduleService scheduleService, RequestData requestData, CancellationToken cancellationToken)
        {
            var result = scheduleService.GetSchedules(requestData.UserId);
            return Results.Json(result);
        }

        private static IResult GetSchedule(ScheduleService scheduleService, RequestData requestData, Guid id, CancellationToken cancellationToken)
        {
            var result = scheduleService.GetSchedule(id, requestData.UserId);
            return result is null ? Results.NotFound() : Results.Json(result);
        }

        private static IResult AddSchedule(ScheduleService scheduleService, RequestData requestData, ScheduleRequest request, CancellationToken cancellationToken)
        {
            var result = scheduleService.AddSchedule(request, requestData.UserId);
            return Results.Created($"api/schedule/{result.Id}", result);
        }

        private static IResult UpdateSchedule(ScheduleService scheduleService, RequestData requestData, Guid id, ScheduleRequest request, CancellationToken cancellationToken)
        {
            var result = scheduleService.UpdateSchedule(id, request, requestData.UserId);
            return result is null ? Results.NotFound() : Results.Json(result);
        }

        private static IResult DeleteSchedule(ScheduleService scheduleService, RequestData requestData, Guid id, CancellationToken cancellationToken)
        {
            scheduleService.DeleteSchedule(id, requestData.UserId);
            return Results.Ok();
        }

        private static IResult ClearSchedule(ScheduleService scheduleService, RequestData requestData, CancellationToken cancellationToken)
        {
            scheduleService.ClearSchedule(requestData.UserId);
            return Results.Ok();
        }
    }
}
