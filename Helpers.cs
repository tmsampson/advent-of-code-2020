using System.Diagnostics;
using System;

namespace AdventOfCode2020
{
	public static class Helpers
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

		public class Result<T>
		{
			public Result(string label) => Label = label;
			public string Label = "None";
			public T Answer;
			public ProfileInfo ProfileInfo;
			public override string ToString()
			{
				string result = $"[{Label}] Answer = {Answer}";
				if(ProfileInfo != null)
				{
					result += $" | {ProfileInfo}";
				}
				return result;
			}
		}

		public static Result<T> Run<T>(string label, Func<T> func, bool Profile = true, int ProfileIterationCount = 1000)
		{
			// Capture result
			Result<T> result = new Result<T>(label);
			result.Answer = func();

			// Profile N iterations
			if(Profile)
			{
				ProfileInfo profileInfo = new ProfileInfo();
				profileInfo.IterationCount = ProfileIterationCount;
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

			// Print and return result
			Console.WriteLine(result);
			return result;
		}
	}
}