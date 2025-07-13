
namespace PFT.Utilities
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public List<string > Messages { get; set; } = new List<string>();

        public string GetMessages()
        {
            string messageLog = string.Empty;
            foreach (var message in Messages)
            {
                messageLog += $"{message}\n";
            }

            return messageLog;
        }
    }
}