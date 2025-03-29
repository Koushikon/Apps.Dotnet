using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace Matrix.Models;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
//[HideColumns(Column.Gen0, Column.Gen1, Column.Gen2)]
[RankColumn]
public class WaysToReadTextFilesBenchmark
{
	// Debugging and Release mode default file exe location is different
	private static readonly string SampleFilePath
	   = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.Parent!.Parent!.Parent!.FullName, "TextFile.txt");
	public readonly WaysToReadTextFiles WaysToReadTextFiles = new(SampleFilePath);


	[Benchmark(Baseline = true)]
	public string UseFileReadAllLines() =>
		WaysToReadTextFiles.UseFileReadAllLines();


	[Benchmark]
	public string UseFileReadAllText() =>
		WaysToReadTextFiles.UseFileReadAllText();


	[Benchmark]
	public string UseFileReadLines() =>
		WaysToReadTextFiles.UseFileReadLines();


	[Benchmark]
	public string UseStreamReaderReadLine() =>
		WaysToReadTextFiles.UseStreamReaderReadLine();


	[Benchmark]
	public string UseStreamReaderReadToEnd() =>
		WaysToReadTextFiles.UseStreamReaderReadToEnd();


	[Benchmark]
	public string UseStreamReaderReadBlock() =>
		WaysToReadTextFiles.UseStreamReaderReadBlock();


	[Benchmark]
	public string UseStreamReaderReadBlockWithArrayPool() =>
		WaysToReadTextFiles.UseStreamReaderReadBlockWithArrayPool();


	[Benchmark]
	public string UseStreamReaderReadBlockWithSpan() =>
		WaysToReadTextFiles.UseStreamReaderReadBlockWithSpan();


	[Benchmark]
	public string UseBufferedStreamObjectWithNoFileStreamBuffer() =>
		WaysToReadTextFiles.UseBufferedStreamObjectWithNoFileStreamBuffer();


	[Benchmark]
	public string UseBufferedStreamObjectWithNoFileStreamBufferIsDisabled() =>
		WaysToReadTextFiles.UseBufferedStreamObjectWithNoFileStreamBufferIsDisabled();
}
