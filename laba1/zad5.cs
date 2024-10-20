class zad5
{
    static void Main()
    {
        int[] Liczby = { 2, 6, 9, 15, 19 };

        for (int liczba = 20; liczba >= 0; liczba--)
        {
            if (Array.Exists(Liczby, element => element == liczba))
            {
                continue;
            }
            Console.WriteLine(liczba);
        }
    }
}
