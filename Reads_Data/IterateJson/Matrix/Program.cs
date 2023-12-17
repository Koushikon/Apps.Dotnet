
namespace Matrix;

public class Program
{
    public static void Main()
    {
        Console.WriteLine();

        JsonIteration ji = new JsonIteration();

        ji.IterateOverJsonDynamically();

        ji.IterateUsingJArray();

        ji.IterateUsingStaticallyTypeObject();

        ji.IterateUsingSystemJson();
    }
}