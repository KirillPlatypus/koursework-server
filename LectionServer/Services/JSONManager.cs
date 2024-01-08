using System.Text.Json;

namespace LectionServer.Services
{
    public class JSONManager<T>
    {
        private string name { get; }
        public JSONManager(string name) 
        {
            this.name ="JSON/" + name;
        }
        public T Get() 
        {
            if (!File.Exists(name)) File.Create(name).Close();
            using (var reader = new StreamReader(name))
            {
                var result = reader.ReadToEnd();
                if (result != "")
                    return JsonSerializer.Deserialize<T>(result);
                else
                    return default(T);
            }
        }
        public void Set(T value) 
        {
            if (!File.Exists(name)) File.Create(name).Close();

            using (var writer = new StreamWriter(name))
            {
                string jsonString = JsonSerializer.Serialize(value);
                writer.WriteLine(jsonString);
            }
        }
    }
}
