using LectionServer.Contracts;
using LectionServer.Models;
using System.Collections.Immutable;
using System.Xml.Linq;

namespace LectionServer.Services
{
    public class SessionService
    {
        //Имя файла любое, но с обязательной припиской расширения .json
        private string name = "Session.json";
        private JSONManager<List<Session>> jsonManager;

        public SessionService()
        {
            //Инициализация менеджера и передача ему имени файла
            jsonManager = new(name);
        }

        //Инициалихация временнного массива данных, который мы будем записывать в JSON файл 
        private readonly List<Session> _session = new();

        public IImmutableList<Session> GetSessions(Guid userId)
        {
            _session.Clear();
            _session.AddRange(jsonManager.Get() == null ? new() : jsonManager.Get());

            return _session.Where(x => x.UserId == userId).ToImmutableList();
        }

        public Session? GetSession(Guid id, Guid userId)
        {
            _session.Clear();
            _session.AddRange(jsonManager.Get() == null ? new() : jsonManager.Get());

            return _session.SingleOrDefault(x => x.Id == id && x.UserId == userId); 
        }

        public Session AddSession(SessionRequest request, Guid userId)
        {
            var session = new Session
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                DataStartSemester = request.DataStartSemester,
                DataEndSemester = request.DataEndSemester,
                DataStartSession = request.DataStartSession
        };
            _session.Add(session);
            jsonManager.Set(_session);

            return session;
        }

        public Session? UpdateSession(Guid id, SessionRequest request, Guid userId)
        {
            var session = GetSession(id, userId);
            if (session is null) return null;
            session.DataStartSemester = request.DataStartSemester;
            session.DataEndSemester = request.DataEndSemester;
            session.DataStartSession = request.DataStartSession;

            _session[_session.FindIndex(_session => _session.Id == session.Id)] = session;
            jsonManager.Set(_session);

            return session;
        }
        public void DeleteSession(Guid id, Guid userid) 
        {
            var session = GetSession(id,userid);
            if (session is null) return;
            _session.Remove(session);
            jsonManager.Set(_session);

        }
    }
}
