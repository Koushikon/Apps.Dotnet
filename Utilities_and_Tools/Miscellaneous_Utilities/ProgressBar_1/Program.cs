﻿Console.WriteLine("Source [Alt + Click]: https://gist.github.com/DanielSWolf/0ab6a96899cc5377bf54");

Console.WriteLine("= = = == = = = == = = = ==");
Console.Write("Performing some task... ");
using (var progress = new ProgressBar())
{
    for (int i = 0; i <= 100; i++)
    {
        progress.Report((double)i / 100);
        Thread.Sleep(20);
    }
}
Console.WriteLine("Done.");
Console.WriteLine("= = = == = = = == = = = ==");