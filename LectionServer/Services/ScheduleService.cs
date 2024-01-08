using System.Collections.Immutable;
using LectionServer.Contracts;
using LectionServer.Models;

namespace LectionServer.Services
{
    public class ScheduleService
    {
        private string name = "Schelule.json";

        private readonly List<Schedule> _schedule = new();
        private JSONManager<List<Schedule>> jsonManager;
        public ScheduleService() 
        {
            jsonManager = new(name);
        }

        public IImmutableList<Schedule> GetSchedules(Guid userId)
        {
            _schedule.Clear();
            _schedule.AddRange(jsonManager.Get()==null? new():jsonManager.Get());

            return _schedule.Where(x => x.UserId == userId).ToImmutableList();
        }

        public Schedule? GetSchedule(Guid id, Guid userId)
        {
            _schedule.Clear();
            _schedule.AddRange(jsonManager.Get() == null ? new() : jsonManager.Get());

            return _schedule.SingleOrDefault(x => x.Id == id && x.UserId == userId);
        }

        public Schedule AddSchedule(ScheduleRequest request, Guid userId)
        {
            var schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = request.Name,
                Type = request.Type,
                Time = request.Time,
                Place = request.Place,
                Teacher = request.Teacher,
                Week = request.Week,
                Data = request.Data,
            };
            _schedule.Add(schedule);
            jsonManager.Set(_schedule);

            return schedule;
        }

        public Schedule? UpdateSchedule(Guid id, ScheduleRequest request, Guid userId)
        {
            var schedule = GetSchedule(id, userId);
            if (schedule is null) return null;

            schedule.Name = request.Name;
            schedule.Type = request.Type;
            schedule.Time = request.Time;
            schedule.Place = request.Place;
            schedule.Teacher = request.Teacher;
            schedule.Week = request.Week;
            schedule.Data = request.Data;

            _schedule[_schedule.FindIndex(_schedule => _schedule.Id == schedule.Id)]= schedule;
            jsonManager.Set(_schedule);

            return schedule;
        }

        public void DeleteSchedule(Guid id, Guid userId)
        {
            var schedule = GetSchedule(id, userId);
            if (schedule is null) return;
            _schedule.Remove(schedule);
            jsonManager.Set(_schedule); 
        }

        public void ClearSchedule(Guid userId)
        {
            _schedule.RemoveAll(x => x.UserId == userId);

           jsonManager.Set(_schedule);
        }
    }
}
