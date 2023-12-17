# Benchmark_Test

The main purpose of this project is to Benchmark different methods and see which one is faster and takes less memory. These methods are getting the Year from a string type date.

## To Run the Benchmark
1. Commented in the `RunAll` method code in `Program.cs`.
2. Commented out this line in `Program.cs`.
	- `BenchmarkRunner.Run<DateParserBenchmark>();`
3. The, Open terminal navigate to the project folder.
4. Run this command `dotnet run --project <project_name>.csproj -c Release`.
5. This will start the Benchmarking process.