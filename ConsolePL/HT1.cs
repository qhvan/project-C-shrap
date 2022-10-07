namespace codep.ConsolePL
{
    public class HT1
    {
        public static void Line(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write("═");
            }
            Console.WriteLine();
        }
        public static void Line1(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write("_");
            }
            Console.WriteLine();
        }
    }
}