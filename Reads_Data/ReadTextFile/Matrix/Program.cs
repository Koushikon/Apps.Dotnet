
using BenchmarkDotNet.Running;
using Matrix.Models;

namespace Matrix;

internal class Program
{
	static void Main()
	{
		BenchmarkRunner.Run<WaysToReadTextFilesBenchmark>();

		/*
		
		string SampleFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName, "TextFile.txt");
		WaysToReadTextFiles fileRead = new WaysToReadTextFiles(SampleFilePath);

		Console.WriteLine($"Text total length: {fileRead.UseFileReadAllLines().Length}");

		Console.WriteLine($"Text total length: {fileRead.UseFileReadAllText().Length}");

		Console.WriteLine($"Text total length: {fileRead.UseFileReadLines().Length}");

		Console.WriteLine($"Text total length: {fileRead.UseStreamReaderReadLine().Length}");

		Console.WriteLine($"Text total length: {fileRead.UseStreamReaderReadToEnd().Length}");

		Console.WriteLine($"Text total length: {fileRead.UseStreamReaderReadBlock().Length}");

		Console.WriteLine($"Text total length: {fileRead.UseStreamReaderReadBlockWithArrayPool().Length}");

		Console.WriteLine($"Text total length: {fileRead.UseStreamReaderReadBlockWithSpan().Length}");

		Console.WriteLine($"Text total length: {fileRead.UseBufferedStreamObjectWithNoFileStreamBuffer().Length}");

		Console.WriteLine($"Text total length: {fileRead.UseBufferedStreamObjectWithNoFileStreamBufferIsDisabled().Length}");

		*/
	}
}