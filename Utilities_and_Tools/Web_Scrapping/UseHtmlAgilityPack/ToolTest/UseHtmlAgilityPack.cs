using HtmlAgilityPack;
using Xunit.Abstractions;

namespace ToolTest;

public class UseHtmlAgilityPack
{
	private readonly ITestOutputHelper _output;

    public UseHtmlAgilityPack(ITestOutputHelper output)
    {
		_output = output;

	}

    [Fact]
	public void WhenParsingHtmlFromString_TheHtmlDocumentIsCreated()
	{
		// arrenge
		var html = @"<!DOCTYPE html>
				<html>
				<body>
					<h1>Learn To Code in C#</h1>
					<p>Programming is really <i>easy</i>.</p>
				</body>
				</html>";

		var dom = new HtmlDocument();
		dom.LoadHtml(html);

		// act
		var documentHeader = dom.DocumentNode.SelectSingleNode("//h1");

		// assert
		Assert.Equal("Learn To Code in C#", documentHeader.InnerHtml);
	}


	[Fact]
	public void WhenParsingHtmlFromFile_TheHtmlDocumentIsCreated()
	{
		// arrenge
		var file = "Test.html";
		var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + @"\TestFile\", file);

		var dom = new HtmlDocument();
		dom.Load(filePath);

		// act
		var documentHeader = dom.DocumentNode.SelectSingleNode("//h2");

		// assert
		Assert.Equal("HTML Agility Pack", documentHeader.InnerHtml);
	}


	[Fact]
	public void WhenParsingHtmlFromUrl_TheHtmlDocumentIsCreated()
	{
		// arrenge
		var url = "https://code-maze.com/";

		var web = new HtmlWeb();
		var htmlDoc = web.Load(url);

		// act
		var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");

		// assert
		Assert.Equal("Code Maze - C#, .NET and Web Development Tutorials", node.InnerHtml);
	}


	[Fact]
	public async void WhenParsingHtmlFromUrlAsync_TheHtmlDocumentIsCreated()
	{
		// arrenge
		var url = "https://code-maze.com/";

		var web = new HtmlWeb();
		var htmlDoc = await web.LoadFromWebAsync(url);

		// act
		var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");

		// assert
		Assert.Equal("Code Maze - C#, .NET and Web Development Tutorials", node.InnerHtml);		
	}


	[Fact]
	public void WhenUsingDoubleSlash_ThenNodeSelectedRegardlessPosition()
	{
		// arrenge
		var file = "Test.html";
		var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + @"\TestFile\", file);

		var htmlDoc = new HtmlDocument();
		htmlDoc.Load(filePath);

		// act
		// Double Slash for selects all nodes of a specific name regardless of their position in the document tree
		var nodes = htmlDoc.DocumentNode.SelectNodes("//li");

		// assert
		Assert.Equal("Parser", nodes[0].InnerHtml);
		Assert.Equal("Selectors", nodes[1].InnerHtml);
		Assert.Equal("DOM management", nodes[2].InnerHtml);
	}


	[Fact]
	public void WhenUsingSingleSlash_ThenNodeSelected()
	{
		// arrenge
		var file = "Test.html";
		var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + @"\TestFile\", file);

		var htmlDoc = new HtmlDocument();
		htmlDoc.Load(filePath);

		// act
		// Single slash for selecting the elements as the hierarchy
		var node = htmlDoc.DocumentNode.SelectSingleNode("/html/body/h2");

		// assert
		Assert.Equal("HTML Agility Pack", node.InnerHtml);
	}


	[Fact]
	public void WhenUsingSelectNodes_ThenSelectDescendantNodes()
	{
		// arrenge
		var file = "Test.html";
		var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + @"\TestFile\", file);

		var htmlDoc = new HtmlDocument();
		htmlDoc.Load(filePath);

		// act
		var body = htmlDoc.DocumentNode.SelectSingleNode("/html/body");

		// Dot Slash for selecting nodes relative to the current node (with matching hierarchy)
		var listItems = body.SelectNodes("./ul/li");

		// assert
		Assert.Equal(3, listItems.Count);
	}


	[Fact]
	public void WhenSelectByAttribute_ThenElementIsReturned()
	{
		// arrenge
		var file = "Test.html";
		var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + @"\TestFile\", file);

		var htmlDoc = new HtmlDocument();
		htmlDoc.Load(filePath);

		// act
		// Attribute Selectors: select nodes based on their attributes like class or even id.
		var node = htmlDoc.DocumentNode.SelectSingleNode("//p[@id='second']");

		// assert
		Assert.Equal("HAP is a popular web scraping tool.", node.InnerHtml);
	}


	[Fact]
	public void WhenSelectInCollection_ThenUseIndexesAndFunctions()
	{
		// arrenge
		var file = "Test.html";
		var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + @"\TestFile\", file);

		// act
		var htmlDoc = new HtmlDocument();
		htmlDoc.Load(filePath);

		// XPath expressions can select specific items in a collection by its one-based index
		// or using functions like first() or last():
		var secondParagraph = htmlDoc.DocumentNode.SelectSingleNode("//p[1]");
		var lastParagraph = htmlDoc.DocumentNode.SelectSingleNode("//p[last()]");

		// assert
		Assert.Equal("Programming is really <i>easy</i>.", secondParagraph.InnerHtml);
		Assert.Equal("Features:", lastParagraph.InnerHtml);
	}


	[Fact]
	public void WhenAddingNode_ThenDomGetsUpdated()
	{
		// arrenge
		var file = "Test.html";
		var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + @"\TestFile\", file);

		var htmlDoc = new HtmlDocument();
		htmlDoc.Load(filePath);

		var listNode = htmlDoc.DocumentNode.SelectSingleNode("//ul");

		// act
		// Creating a new Node on the Document
		listNode.ChildNodes.Add(HtmlNode.CreateNode("<li>Programmatically Added Node</li>"));

		// assert
		Assert.Equal(@"<ul>
        <li>Parser</li>
        <li>Selectors</li>
        <li>DOM management</li>
    <li>Programmatically Added Node</li></ul>", listNode.OuterHtml);
	}


	[Fact]
	public void WhenRemovingNode_ThenDomGetsUpdated()
	{
		// arrenge
		var file = "Test.html";
		var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + @"\TestFile\", file);

		var htmlDoc = new HtmlDocument();
		htmlDoc.Load(filePath);

		var listNode = htmlDoc.DocumentNode.SelectSingleNode("//ul");

		// act
		// Removing a Node from the Document
		listNode.RemoveChild(listNode.SelectNodes("//li").FirstOrDefault());

		// assert
		Assert.Equal(@"<ul>
        
        <li>Selectors</li>
        <li>DOM management</li>
    </ul>", listNode.OuterHtml);
	}


	[Fact]
	public void WhenUpdatingNode_ThenDomisUpdated()
	{
		// arrenge
		var file = "Test.html";
		var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + @"\TestFile\", file);

		var htmlDoc = new HtmlDocument();
		htmlDoc.Load(filePath);

		var listNode = htmlDoc.DocumentNode.SelectSingleNode("//ul");

		// act
		// Updating the nodes InnerHtml & Properties
		foreach (var node in listNode.ChildNodes.Where(x => x.Name == "li"))
		{
			node.FirstChild.InnerHtml = "List Item Text";
			node.Attributes.Append("class", "list-item");
		}

		// assert
		Assert.Equal(@"<ul>
        <li class=""list-item"">List Item Text</li>
        <li class=""list-item"">List Item Text</li>
        <li class=""list-item"">List Item Text</li>
    </ul>", listNode.OuterHtml);
	}


	[Fact]
	public void WhenWrittingOutHtml_ThenWritesToDiskFile()
	{
		// arrenge
		var file = "Test.html";
		var path = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + @"\TestFile\";
		var filePath = Path.Combine(path, file);

		var htmlDoc = new HtmlDocument();
		htmlDoc.Load(filePath);

		// act
		using var textWriter = File.CreateText($"{path}test-out.html");
		htmlDoc.Save(textWriter);

		// assert
		Assert.True(File.Exists($"{path}test-out.html"));
	}


	[Fact]
	public void WhenTraversingDocument_ThenAllNodesVisited()
	{
		// arrenge
		var file = "Test.html";
		var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + @"\TestFile\", file);

		var htmlDoc = new HtmlDocument();
		htmlDoc.Load(filePath);

		// act
		var toc = new List<HtmlNode>();
		var headerTags = new string[] { "h1", "h2", "h3", "h4", "h5", "h6" };

		// Traversing through the document and add the founded h1 to h6 element nodes to HtmlNode list.
		void VisitNodesRecursively(HtmlNode node)
		{
			if (headerTags.Contains(node.Name))
			{
				toc.Add(node);
			}

			foreach (var child in node.ChildNodes)
			{
				VisitNodesRecursively(child);
			}
		}

		VisitNodesRecursively(htmlDoc.DocumentNode);

		// assert
		Assert.Equal(2, toc.Count);
		Assert.Contains(toc, x => x.Name == "h1" && x.InnerText == "Learn To Code in C#");
		Assert.Contains(toc, x => x.Name == "h2" && x.InnerText == "HTML Agility Pack");
	}


	[Fact]
	public void WhenWritingOutNodeContent_ThenWritesToDiskFile()
	{
		// arrenge
		var file = "Test.html";
		var path = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + @"\TestFile\";
		var filePath = Path.Combine(path, file);

		var htmlDoc = new HtmlDocument();
		htmlDoc.Load(filePath);

		var listNode = htmlDoc.DocumentNode.SelectSingleNode("//ul");

		// act
		// Saves the li with ul included content to the file
		using (var textWriter = File.CreateText($"{path}test-list.html"))
		{
			listNode.WriteTo(textWriter);
		}

		// Saves the li content only to the file
		using (var textWriter = File.CreateText($"{path}test-list-only.html"))
		{
			listNode.WriteContentTo(textWriter);
		}

		// assert
		Assert.Equal(@"<ul>
        <li>Parser</li>
        <li>Selectors</li>
        <li>DOM management</li>
    </ul>", File.ReadAllText($"{path}test-list.html"));
		Assert.Equal(@"
        <li>Parser</li>
        <li>Selectors</li>
        <li>DOM management</li>
    ", File.ReadAllText($"{path}test-list-only.html"));
	}


	[Fact]
	public void WhenDescendantNodes_TheFlatListOfNodesReturned()
	{
		// arrenge
		var file = "Test.html";
		var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName + @"\TestFile\", file);

		var htmlDoc = new HtmlDocument();
		htmlDoc.Load(filePath);

		// act
		// With these extension methods we're grouping similar tags
		var groups = htmlDoc.DocumentNode.DescendantsAndSelf()
			.Where(x => !x.Name.StartsWith("#"))
			.GroupBy(x => x.Name);

		// Then, with the Tag name as the key get the infomation about the tag like
		// Same tag found how many times in the document with count
		foreach (var group in groups)
		{
			this._output.WriteLine($"Tag {group.Key} found {group.Count()} times.");
		}

		// assert
		Assert.Contains(groups, x => x.Key == "html" && x.Count() == 1);
		Assert.Contains(groups, x => x.Key == "body" && x.Count() == 1);
		Assert.Contains(groups, x => x.Key == "h1" && x.Count() == 1);
		Assert.Contains(groups, x => x.Key == "p" && x.Count() == 3);
		Assert.Contains(groups, x => x.Key == "i" && x.Count() == 1);
		Assert.Contains(groups, x => x.Key == "h2" && x.Count() == 1);
		Assert.Contains(groups, x => x.Key == "ul" && x.Count() == 1);
		Assert.Contains(groups, x => x.Key == "li" && x.Count() == 3);
	}
}