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
		public int IterationCount = -1;
		public override string ToString()
		{
			long AverageTicks = (IterationCount > 1)? Ticks / IterationCount : Ticks;
			double AverageMs = (IterationCount > 1)? Ms / IterationCount : Ms;
			return $"{AverageTicks} ticks | {AverageMs:0.######} ms (averaged over {IterationCount} iterations)";
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
		public override string ToString()
		{
			string result = string.Empty;
			if(!String.IsNullOrEmpty(Label))
			{
				result += $"[{Label}] ";
			}
			result += $"Answer = {Answer}";
			if(ProfileInfo != null)
			{
				result += $" | {ProfileInfo}";
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
			var sortedResults = Results.OrderBy(r => (r.ProfileInfo != null)? r.ProfileInfo.Ticks : 0);
			foreach(var result in sortedResults)
			{
				Console.WriteLine(result);
			}
		}
	}

	class Framework
	{
		public static PuzzleResult<T> Run<T>(Func<T> func, bool profile = true, int profileIterationCount = 1000)
		{
			return Run(string.Empty, func, profile, profileIterationCount);
		}

		public static PuzzleResult<T> Run<T>(string label, Func<T> func, bool profile = true, int profileIterationCount = 1000)
		{
			// Setup result and capture answer
			PuzzleResult<T> result = new PuzzleResult<T>(label);
			result.Answer = func();

			// Profile N iterations
			if(profile)
			{
				ProfileInfo profileInfo = new ProfileInfo();
				profileInfo.IterationCount = profileIterationCount;
				Stopwatch sw = new Stopwatch();
				sw.Start();
				for (int i = 0; i < profileInfo.IterationCount; i++)
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