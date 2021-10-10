// Find the three entries that sum to 2020 and then multiply those three numbers together
// https://adventofcode.com/2020/day/1#part2

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
	class Puzzle02 : IPuzzle
	{
		private static string Input = "puzzles/puzzle-01-input.txt";
		private static class Constants
		{
			public static int TargetResult = 2020;
			public static int InvalidResult = -1;
		}

		public IPuzzleResults Run()
		{
			return new PuzzleResults<int>()
			{
				// NOTE: Array lookup is currently fastest. I suspect HashSet implementation
				//       would come out on top if dataset was larger than 200
				Framework.Run("Array lookup", Solve_Array),
			};
		}

		private static int Solve_Array()
		{
			string[] lines = File.ReadAllLines(Input);
			int[] numbers = lines.Select(int.Parse).ToArray();
			foreach(int number in numbers)
			{
				int target = Constants.TargetResult - number;
				if(number != target)
				{
					int other = FindPairSum(numbers, target, number);
					if(other != Constants.InvalidResult)
					{
						return number * other;
					}
				}
			}
			return Constants.InvalidResult;
		}

		private static int FindPairSum(int[] numbers, int target, int ignore)
		{
			foreach(int number in numbers)
			{
				if(number != ignore)
				{
					int other = target - number;
					if(number != other && other != ignore && numbers.Contains(other))
					{
						return number * other;
					}
				}
			}
			return Constants.InvalidResult;
		}
	}
}