namespace ReadSavvy.Services
{
    public class LogService
    {
        private string msg;
        private LogService()
        {
            
        }
        private static readonly LogService Instance = new LogService();
        public static LogService getInstance
        {
            get
            {
                return Instance;
            }
        }
        public void LogMessage(string msg)
        {
            Console.WriteLine(msg);
        }

    }
}
