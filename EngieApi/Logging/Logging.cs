namespace EngieApi.Logging
{
    public class Logging : ILogging
    {
        public void Log(string type, string message)
        {
            if (type == "error")
            {
                Console.Error.WriteLine(message);
            }
            else
            {
                Console.WriteLine(message);
            }

        }
    }
}
