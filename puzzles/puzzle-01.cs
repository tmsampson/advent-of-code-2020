// ------------------------------------------------------------------------------------
// Day 1: Report Repair - Part 1
// ------------------------------------------------------------------------------------
// https://adventofcode.com/2020/day/1
// ------------------------------------------------------------------------------------
// Find the two entries in the input list that sum to 2020 and then multiply those
// two numbers together.
// ------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
	class Puzzle01 : IPuzzle
	{
		private static class Constants
		{
			public static readonly string Input = "puzzles/puzzle-01-input.txt";
			public static readonly int TargetResult = 2020;
		}

		public class Answer : IPuzzleAnswer
		{
			public int Num0 = -1, Num1 = -1;
			public int Result { get { return Num0 * Num1; } }
			public override string ToString() { return $"{Result} ({Num0} * {Num1})"; }
		}

		public IPuzzleResults Run()
		{
			return new PuzzleResults()
			{
				// NOTE: Array lookup is currently fastest. I suspect HashSet implementation
				//       would come out on top if dataset was larger than 200
				Framework.Run("Naive", Solve_Naive),
				Framework.Run("Array lookup", Solve_Array),
				Framework.Run("HashSet lookup", Solve_HashSet)
			};
		}

		private static Answer Solve_Naive()
		{
			string[] lines = File.ReadAllLines(Constants.Input);
			int[] numbers = lines.Select(int.Parse).ToArray();
			for(int i = 0; i < numbers.Length; ++i)
			{
				int number = numbers[i];
				for(int j = 0; j < numbers.Length; ++j)
				{
					if(i != j)
					{
						int other = numbers[j];
						if((number + other) == Constants.TargetResult)
						{
							return new Answer() { Num0 = number, Num1 = other };
						}
					}
				}

			}
			return null;
		}

		private static Answer Solve_Array()
		{
			string[] lines = File.ReadAllLines(Constants.Input);
			int[] numbers = lines.Select(int.Parse).ToArray();
			foreach(int number in numbers)
			{
				int other = Constants.TargetResult - number;
				if(number != other && numbers.Contains(other))
				{
					return new Answer() { Num0 = number, Num1 = other };
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
				int other = Constants.TargetResult - number;
				if(numbersLookup.Contains(other))
				{
					return new Answer() { Num0 = number, Num1 = other };
				}
			}
			return null;
		}
	}
}