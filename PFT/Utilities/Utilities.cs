namespace PFT.Utilities
{
    public static class Utilities
    {
        public static string ReadFromFile(string fileName)
        {
            try
            {
                using StreamReader reader = new(fileName);
                string text = reader.ReadToEnd();
                Console.WriteLine(text);
                return text;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
    }
}
