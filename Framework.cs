using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace AdventOfCode2020
{
	public class ProfileInfo
	{
		public long Ticks = -1;
		public double Ms = -1.0;
		public static int IterationCount = 1000;
		public string ToString(int tickPadding = 0)
		{
			long averageTicks = (IterationCount > 1)? Ticks / IterationCount : Ticks;
			double averageMs = (IterationCount > 1)? Ms / IterationCount : Ms;
			string averageTicksString = averageTicks.ToString();
			string averageTicksStringPadded = tickPadding > 0? averageTicksString.PadLeft(tickPadding) : averageTicksString;
			return $"{averageTicksStringPadded} ticks | {averageMs:0.######} ms (averaged over {IterationCount} iterations)";
		}
	}

	public interface IPuzzle
	{
		IPuzzleResults Run();
	}

	public class PuzzleResult<T>
	{
		public PuzzleResult(string label) => Label = label;
		public string Label = string.Empty;
		public T Answer;
		public ProfileInfo ProfileInfo;
		public string ToString(int labelPadding = 0, int tickPadding = 0)
		{
			string result = string.Empty;
			if(!String.IsNullOrEmpty(Label))
			{
				string labelPadded = labelPadding > 0? Label.PadLeft(labelPadding) : Label;
				result += $"[{labelPadded}] ";
			}
			result += $"Answer = {Answer}";
			if(ProfileInfo != null)
			{
				result += ProfileInfo.ToString(tickPadding);
			}
			return result;
		}
	}

	public interface IPuzzleResults
	{
		void Print();
	}

	public class PuzzleResults<T> : IPuzzleResults, IEnumerable<PuzzleResult<T>>
	{
		List<PuzzleResult<T>> Results = new List<PuzzleResult<T>>();
		
		public void Add(PuzzleResult<T> Result)
		{
			Results.Add(Result);
		}
		
		public IEnumerator<PuzzleResult<T>> GetEnumerator()
		{
			return Results.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Print()
		{
			var labelPadding = Results.Select(r => r.Label.Length).Max();
			var tickPadding = Results.Select(r => (r.ProfileInfo != null)? r.ProfileInfo.Ticks.ToString().Length : 0).Max();
			var sortedResults = Results.OrderBy(r => (r.ProfileInfo != null)? r.ProfileInfo.Ticks : 0);
			foreach(var result in sortedResults)
			{
				Console.WriteLine(result.ToString(labelPadding, tickPadding));
			}
		}
	}

	class Framework
	{
		public static PuzzleResult<T> Run<T>(Func<T> func, bool profile = true)
		{
			return Run(string.Empty, func, profile);
		}

		public static PuzzleResult<T> Run<T>(string label, Func<T> func, bool profile = true)
		{
			// Setup result and capture answer
			PuzzleResult<T> result = new PuzzleResult<T>(label);
			result.Answer = func();

			// Profile N iterations
			if(profile)
			{
				ProfileInfo profileInfo = new ProfileInfo();
				Stopwatch sw = new Stopwatch();
				sw.Start();
				for (int i = 0; i < ProfileInfo.IterationCount; i++)
				{
					func();
				}
				sw.Stop();
				profileInfo.Ticks = sw.ElapsedTicks;
				profileInfo.Ms = sw.Elapsed.TotalMilliseconds;
				result.ProfileInfo = profileInfo;
			}

			// Return result
			return result;
		}
	}
}