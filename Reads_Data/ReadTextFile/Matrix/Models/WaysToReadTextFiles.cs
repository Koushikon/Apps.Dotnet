using System.Buffers;
using System.Text;

namespace Matrix.Models;

/***
 * Source: https://code-maze.com/csharp-fastest-way-to-read-text-file/amp/
 */

public class WaysToReadTextFiles
{
	private static readonly string TempFilePath = Path.GetTempFileName();
	private string _filePath;

	public WaysToReadTextFiles(string filePath)
	{
		_filePath = filePath;
	}


	// This method to read all the lines in our text file into the memory
	public string UseFileReadAllLines()
	{
		var lines = File.ReadAllLines(_filePath);
		StringBuilder stringBuilder = new StringBuilder();

		foreach (var line in lines)
		{
			stringBuilder.AppendLine(line);
		}
		stringBuilder.Length -= Environment.NewLine.Length;

		return stringBuilder.ToString();
	}


	// File.ReadAllText() method with our sample file path as the argument.
	// We can directly read all the lines in our file and return them as a single string.
	public string UseFileReadAllText() => File.ReadAllText(_filePath);


	// This method reads the lines from our file and returns an IEnumerable<string> instance that we can use to iterate over our file.
	// Unlike the File.ReadAllLines() method, this method doesn’t load the entire file into memory.
	public string UseFileReadLines()
	{
		var lines = File.ReadLines(_filePath);
		StringBuilder stringBuilder = new StringBuilder();

		foreach (var line in lines)
		{
			stringBuilder.AppendLine(line);
		}
		stringBuilder.Length -= Environment.NewLine.Length;

		return stringBuilder.ToString();
	}


	// With Stream Reader ReadLine reading the line and append it to the stringBuilder
	public string UseStreamReaderReadLine()
	{
		using var reader = new StreamReader(_filePath);
		StringBuilder stringBuilder = new StringBuilder();

		// Matches the pattern and assign it to fileLine
		while (reader.ReadLine() is { } fileLine)
		{
			stringBuilder.AppendLine(fileLine);
		}
		stringBuilder.Length -= Environment.NewLine.Length;

		return stringBuilder.ToString();
	}


	// with StreamReader ReadToEnd read the data and return the string
	public string UseStreamReaderReadToEnd()
	{
		using var reader = new StreamReader(_filePath);

		return reader.ReadToEnd();
	}


	public string UseStreamReaderReadBlock()
	{
		using var reader = new StreamReader(_filePath);
		var buffer = new char[4096];
		int numberRead;
		StringBuilder stringBuilder = new StringBuilder();

		/***
		 * Reads a specified maximum number of characters from the current stream and writes the data to a buffer,
		 * beginning at the specified index.
		 */
		while ((numberRead = reader.ReadBlock(buffer, 0, buffer.Length)) > 0)
		{
			stringBuilder.Append(buffer[..numberRead]);
		}

		return stringBuilder.ToString();
	}


	public string UseStreamReaderReadBlockWithArrayPool()
	{
		using var reader = new StreamReader(_filePath);
		var buffer = ArrayPool<char>.Shared.Rent(4096);
		int numberRead;
		StringBuilder stringBuilder = new StringBuilder();

		/***
		 * Reads a specified maximum number of characters from the current stream and writes the data to a buffer,
		 * beginning at the specified index.
		 */
		while ((numberRead = reader.ReadBlock(buffer, 0, buffer.Length)) > 0)
		{
			stringBuilder.Append(buffer[..numberRead]);
		}

		ArrayPool<char>.Shared.Return(buffer);

		return stringBuilder.ToString();
	}


	public string UseStreamReaderReadBlockWithSpan()
	{
		using var reader = new StreamReader(_filePath);
		var buffer = new char[4096].AsSpan();
		int numberRead;
		StringBuilder stringBuilder = new StringBuilder();

		/***
		 * Reads a specified maximum number of characters from the current stream and writes the data to a buffer,
		 * beginning at the specified index.
		 */
		while ((numberRead = reader.ReadBlock(buffer)) > 0)
		{
			stringBuilder.Append(buffer[..numberRead]);
		}

		return stringBuilder.ToString();
	}


	public string UseBufferedStreamObjectWithNoFileStreamBuffer()
	{
		var stringBuilder = new StringBuilder();

		using var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
		using var bufferedStream = new BufferedStream(fileStream);
		using var streamReader = new StreamReader(bufferedStream);

		while (streamReader.ReadLine() is { } fileLine)
		{
			stringBuilder.AppendLine(fileLine);
		}
		stringBuilder.Length -= Environment.NewLine.Length;

		return stringBuilder.ToString();
	}


	public string UseBufferedStreamObjectWithNoFileStreamBufferIsDisabled()
	{
		var stringBuilder = new StringBuilder();

		using var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 0);
		using var bufferedStream = new BufferedStream(fileStream);
		using var streamReader = new StreamReader(bufferedStream);

		while (streamReader.ReadLine() is { } fileLine)
		{
			stringBuilder.AppendLine(fileLine);
		}
		stringBuilder.Length -= Environment.NewLine.Length;

		return stringBuilder.ToString();
	}
}