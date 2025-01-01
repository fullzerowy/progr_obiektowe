using System;
using System.Collections.Generic;

class Person
{
    private string FirstName { get; set; }
    private string LastName { get; set; }
    private int Age { get; set; }

    public Person(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }

    public virtual void View()
    {
        Console.WriteLine($"Person: {FirstName} {LastName}, Age: {Age}");
    }
}

class Book
{
    public string Title { get; set; }
    protected Person Author { get; set; }
    protected DateTime PublicationDate { get; set; }

    public Book(string title, Person author, DateTime publicationDate)
    {
        Title = title;
        Author = author;
        PublicationDate = publicationDate;
    }

    public virtual void View()
    {
        Console.WriteLine($"Book: {Title}, Author: {Author.FirstName} {Author.LastName}, Published: {PublicationDate.ToShortDateString()}");
    }
}

class AdventureBook : Book
{
    private string AdventureType { get; set; }

    public AdventureBook(string title, Person author, DateTime publicationDate, string adventureType)
        : base(title, author, publicationDate)
    {
        AdventureType = adventureType;
    }

    public override void View()
    {
        base.View();
        Console.WriteLine($"Adventure Type: {AdventureType}");
    }
}

class DocumentaryBook : Book
{
    private string Topic { get; set; }

    public DocumentaryBook(string title, Person author, DateTime publicationDate, string topic)
        : base(title, author, publicationDate)
    {
        Topic = topic;
    }

    public override void View()
    {
        base.View();
        Console.WriteLine($"Topic: {Topic}");
    }
}

class Reader : Person
{
    private List<Book> ReadBooks { get; set; } = new List<Book>();
    public Reader(string firstName, string lastName, int age) : base(firstName, lastName, age) { }
    public void AddBook(Book book)
    {
        ReadBooks.Add(book);
    }

    public void ViewBooks()
    {
        Console.WriteLine("Read books:");
        foreach (var book in ReadBooks)
        {
            book.View();
        }
    }

    public override void View()
    {
        base.View();
        ViewBooks();
    }
}

class Reviewer : Reader
{
    public Reviewer(string firstName, string lastName, int age) : base(firstName, lastName, age) { }

    public void WriteReviews()
    {
        Console.WriteLine("Reviews:");
        Random random = new Random();
        foreach (var book in ReadBooks)
        {
            Console.WriteLine($"{book.Title} - Rating: {random.Next(1, 6)}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Person author1 = new Person("Michael", "Johnson", 50);
        Person author2 = new Person("Sophia", "Taylor", 42);

        Book book1 = new Book("Advanced C# Programming", author1, new DateTime(2021, 3, 25));
        AdventureBook book2 = new AdventureBook("Mountains of Fear", author2, new DateTime(2022, 8, 15), "Climbing");
        DocumentaryBook book3 = new DocumentaryBook("Our Planet", author1, new DateTime(2020, 11, 12), "Nature Conservation");

        Reader reader1 = new Reader("James", "Miller", 29);
        Reader reader2 = new Reader("Olivia", "Davis", 34);

        Reviewer reviewer1 = new Reviewer("Liam", "Martinez", 38);
        Reviewer reviewer2 = new Reviewer("Isabella", "Gonzalez", 46);

        reader1.AddBook(book1);
        reader1.AddBook(book2);
        reader2.AddBook(book3);

        reviewer1.AddBook(book1);
        reviewer1.AddBook(book2);

        reviewer2.AddBook(book3);

        List<Person> people = new List<Person> { reader1, reader2, reviewer1, reviewer2 };
        foreach (var person in people)
        {
            person.View();
            Console.WriteLine();
        }
        reviewer1.WriteReviews();
        reviewer2.WriteReviews();
    }
}

class Car
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string BodyType { get; set; }
    public string Color { get; set; }
    public int ProductionYear { get; set; }
    private int mileage;
    public int Mileage
    {
        get { return mileage; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Mileage cannot be negative!");
            mileage = value;
        }
    }
    public Car()
    {
        Console.Write("Enter brand: ");
        Brand = Console.ReadLine();
        Console.Write("Enter model: ");
        Model = Console.ReadLine();
        Console.Write("Enter body type: ");
        BodyType = Console.ReadLine();
        Console.Write("Enter color: ");
        Color = Console.ReadLine();
        Console.Write("Enter production year: ");
        ProductionYear = int.Parse(Console.ReadLine());
        Console.Write("Enter mileage: ");
        Mileage = int.Parse(Console.ReadLine());
    }
    public Car(string brand, string model, string bodyType, string color, int productionYear, int mileage)
    {
        Brand = brand;
        Model = model;
        BodyType = bodyType;
        Color = color;
        ProductionYear = productionYear;
        Mileage = mileage;
    }
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Car: {Brand} {Model}, Body Type: {BodyType}, Color: {Color}, Production Year: {ProductionYear}, Mileage: {Mileage} km");
    }
}

class Sedan : Car
{
    public double Weight { get; set; }
    public double EngineCapacity { get; set; }
    public int PassengerCount { get; set; }
    public Sedan() : base()
    {
        Console.Write("Enter weight (1.5-3.0 tons): ");
        Weight = double.Parse(Console.ReadLine());
        if (Weight < 1.5 || Weight > 3.0)
            throw new ArgumentException("Weight must be between 1.5 and 3.0 tons!");

        Console.Write("Enter engine capacity (1.0-3.5): ");
        EngineCapacity = double.Parse(Console.ReadLine());
        if (EngineCapacity < 1.0 || EngineCapacity > 3.5)
            throw new ArgumentException("Engine capacity must be between 1.0 and 3.5 liters!");

        Console.Write("Enter passenger count: ");
        PassengerCount = int.Parse(Console.ReadLine());
    }
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Weight: {Weight} tons, Engine Capacity: {EngineCapacity} liters, Passengers: {PassengerCount}");
    }
}

class ProgramCar
{
    static void Main(string[] args)
    {
        Console.WriteLine("Creating a sedan:");
        Sedan sedan = new Sedan();

        Console.WriteLine("\nCreating the first car:");
        Car car1 = new Car();

        Console.WriteLine("\nCreating the second car:");
        Car car2 = new Car("Ford", "Mustang", "Coupe", "Red", 2020, 40000);

        Console.WriteLine("\nCar Information:");
        sedan.DisplayInfo();
        car1.DisplayInfo();
        car2.DisplayInfo();
    }
}
