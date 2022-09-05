namespace Utilities
{
    public class StringUtils
    {
        public static string FormatTime(float timeInSecond)
        {
            float minutes = timeInSecond / 60;
            float seconds = timeInSecond % 60;

            return $"{minutes:00}:{seconds:00}";
        }
    }
}