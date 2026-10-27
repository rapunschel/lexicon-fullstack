
Program1 p1 = new Program1();

Program2 p2 = new Program2();
p1.Start();
p2.Start();

((IBye)p1).Bye();
p1.Bye("Ciao ciao");

public interface IStart
{
    public void Start();
}

public interface IBye
{
    public void Bye()
    {
        System.Console.WriteLine("Bye");
    }

    public void Bye(string msg);
}
// p1.Start();
public class Program1 : IStart, IBye
{
    public void Bye(string msg)
    {
        System.Console.WriteLine(msg);
    }


    // public void Run()
    // {
    //     System.Console.WriteLine("Nu kör vi program 1");
    // }
    public void Start()
    {
        System.Console.WriteLine("Program 1");
    }
}

public class Program2 : IStart
{
    // public void Run()
    // {
    //     System.Console.WriteLine("Nu kör vi program 2");
    // }
    public void Start()
    {
        System.Console.WriteLine("Program 2");
    }
}




/*
    Fördel: Vi vet att varje klass som implementerar interfacet har samma metoder.
    Kan ha flera interfaces.
*/