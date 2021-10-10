using System.Diagnostics;
using System;

namespace AdventOfCode2020
{
	public static class Helpers
	{
		public static void Profile(string Label, Action action)
		{
			int IterationCount = 1000;
			Stopwatch sw = new Stopwatch();
			sw.Start();
			for (int i = 0; i < IterationCount; i++)
			{
				action();
			}
			sw.Stop();
			long AverageTicks = sw.ElapsedTicks / IterationCount;
			double AverageMs = sw.Elapsed.TotalMilliseconds / IterationCount;
			Console.WriteLine($"{Label} profile result = {AverageTicks} ticks | {AverageMs:0.######} ms (averaged over {IterationCount} iterations)");
		}
	}
}