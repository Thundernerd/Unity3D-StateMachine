namespace TNRD.StateManagement
{
    internal static class Utilities
    {
        public static string LowerCaseFirstChar(this string input)
        {
            if(string.IsNullOrEmpty(input))
                return input;

            return char.ToLower(input[0]) + input.Substring(1);
        }
    }
}
