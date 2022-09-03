namespace _Scripts.Core
{
    public static class Extensions
    {
        public static int Wrap(this int input, int min, int max)
        {
            if (input < min) {
                return max - (min - input) % (max - min);
            } else {
                return min + (input - min) % (max - min);
            }
        }
    }
}