namespace Matrix;

internal class Program
{
	static void Main(string[] args)
	{
		// Conditional Compilation

#if NET7_0
		Console.WriteLine("Hello from .Net 7 Framework");
#elif NET6_0
		Console.WriteLine("Hello from .Net 6 Framework");
#else
		Console.WriteLine("Hello from lower than .Net 6 Framework);
#endif

		Console.WriteLine("Done processing...");
	}
}