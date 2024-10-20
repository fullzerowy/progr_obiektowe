class zad4
{
    static void Main()
    {

        double[] liczby = new double[10];

        for (int i = 0; i < liczby.Length; i++)
        {
            Console.WriteLine($"Podaj liczbę nr {i + 1}: ");
            liczby[i] = Convert.ToDouble(Console.ReadLine());
        }

        double suma = 0;
        for (int i = 0; i < liczby.Length; i++)
        {
            suma += liczby[i];
        }
        Console.WriteLine($"\nSuma: {suma}");

        double iloczyn = 1;
        for (int i = 0; i < liczby.Length; i++)
        {
            iloczyn *= liczby[i];
        }
        Console.WriteLine($"Iloczyn: {iloczyn}");

        double srednia = suma / liczby.Length;
        Console.WriteLine($"Średnia: {srednia}");

        double min = liczby[0];
        for (int i = 1; i < liczby.Length; i++)
        {
            if (liczby[i] < min)
            {
                min = liczby[i];
            }
        }
        Console.WriteLine($"Minimalna: {min}");

        double max = liczby[0];
        for (int i = 1; i < liczby.Length; i++)
        {
            if (liczby[i] > max)
            {
                max = liczby[i];
            }
        }
        Console.WriteLine($"Maksymalna: {max}");
    }
}
