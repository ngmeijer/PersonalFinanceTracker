namespace PFT.Utilities
{
    public static class Utilities
    {
        public static string ReadFromFile(string fileName)
        {
            try
            {
                string filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                using StreamReader reader = new(fileName);
                string text = reader.ReadToEnd();
                return text;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return string.Empty;
            }
        }
    }
}
