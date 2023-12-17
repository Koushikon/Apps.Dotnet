# Read Text File if Many ways in C#

This Project implements different ways to Read Text files. The ways we can read the files are:
- With File `ReadAllLines` method.
- With File `ReadAllText` method.
- With FIle `ReadLines` method.
- With `Stream Reader` Object, `ReadLine` method.
- With `Stream Reader` Object, `ReadToEnd` method.
- With `Stream Reader` Object, `ReadBlock` method.
- With `Stream Reader` Object, `ReadBlock` method & `ArrayPool` class.
- With `Stream Reader` Object, `ReadBlock` method & `Span` class.
- With `Buffered Stream`, `StreamBuffer` Object  & `ReadLine` method.
- With `Buffered Stream`, Object `StreamBuffer` with 0 Buffersize & `ReadLine` method.


## To Run the Benchmark
1. Commented in the `RunAll` method code in `Program.cs`.
2. Commented out this line in `Program.cs`.
	- `BenchmarkRunner.Run<PrintArrayModelBenchmark>();`
3. The, Open terminal navigate to the project folder.
4. Run this command `dotnet run --project <project_name>.csproj -c Release`.
5. This will start the Benchmarking process.