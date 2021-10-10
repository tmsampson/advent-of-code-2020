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
		private static class Constants
		{
			public static string Input = "puzzles/puzzle-01-input.txt";
			public static int TargetResult = 2020;
		}
		public class Answer : IPuzzleAnswer
		{
			public int Num0 = -1, Num1 = -1, Num2 = -1;
			public int Result { get { return Num0 * Num1 * Num2; } }
			public override string ToString() { return $"{Result} ({Num0} * {Num1} * {Num2})"; }
		}

		public IPuzzleResults Run()
		{
			return new PuzzleResults()
			{
				// NOTE: Unlike previous puzzle, HashSet now comes out on top as way more lookup operations are being performed
				Framework.Run("Array lookup", Solve_Array),
				Framework.Run("HashSet lookup", Solve_HashSet),
			};
		}

		private static Answer Solve_Array()
		{
			string[] lines = File.ReadAllLines(Constants.Input);
			int[] numbers = lines.Select(int.Parse).ToArray();
			foreach(int number in numbers)
			{
				int target = Constants.TargetResult - number;
				if(number != target)
				{
					var result = FindPairSum_Array(numbers, target, number);
					if(result != null)
					{
						return new Answer() { Num0 = number, Num1 = result.Item1, Num2 = result.Item2 };
					}
				}
			}
			return null;
		}

		private static Answer Solve_HashSet()
		{
			string[] lines = File.ReadAllLines(Constants.Input);
			int[] numbers = lines.Select(int.Parse).ToArray();
			HashSet<int> numbersLookup = lines.Select(int.Parse).ToHashSet();
			foreach(int number in numbers)
			{
				int target = Constants.TargetResult - number;
				if(number != target)
				{
					var result = FindPairSum_HashSet(numbers, numbersLookup, target, number);
					if(result != null)
					{
						return new Answer() { Num0 = number, Num1 = result.Item1, Num2 = result.Item2 };
					}
				}
			}
			return null;
		}

		private static Tuple<int, int> FindPairSum_Array(int[] numbers, int target, int ignore)
		{
			foreach(int number in numbers)
			{
				if(number != ignore)
				{
					int other = target - number;
					if(number != other && other != ignore && numbers.Contains(other))
					{
						return Tuple.Create(number, other);
					}
				}
			}
			return null;
		}

		private static Tuple<int, int> FindPairSum_HashSet(int[] numbers, HashSet<int> numbersLookup, int target, int ignore)
		{
			foreach(int number in numbers)
			{
				if(number != ignore)
				{
					int other = target - number;
					if(number != other && other != ignore && numbersLookup.Contains(other))
					{
						return Tuple.Create(number, other);
					}
				}
			}
			return null;
		}
	}
}