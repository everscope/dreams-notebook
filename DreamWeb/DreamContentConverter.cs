using System.Text;

namespace DreamWeb
{
    public static class DreamContentConverter
    {
        private static readonly string splitContentMarker = "(%%#newpart#%%)";

        public static string Convert(string [] content)
        {
            StringBuilder resultBuilder = new();
            for (int i = 0; i < content.Length - 1; i++)
            {
                resultBuilder.Append(content[i] + splitContentMarker);
            }

            return resultBuilder.ToString();
        }
    }
}
