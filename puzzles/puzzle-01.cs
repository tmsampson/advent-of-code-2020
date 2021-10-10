// Find the two entries that sum to 2020 and then multiply those two numbers together
// https://adventofcode.com/2020/day/1

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
	class Puzzle01 : IPuzzle
	{
		private static string Input = "puzzles/puzzle-01-input.txt";

		public IPuzzleResults Run()
		{
			return new PuzzleResults<int>()
			{
				// NOTE: Array lookup is currently fastest. I suspect HashSet implementation
				//       would come out on top if dataset was larger than 200
				Framework.Run("Naive", Solve_Naive),
				Framework.Run("Array lookup", Solve_Array),
				Framework.Run("HashSet lookup", Solve_HashSet)
			};
		}

		private static int Solve_Naive()
		{
			string[] lines = File.ReadAllLines(Input);
			int[] numbers = lines.Select(int.Parse).ToArray();
			for(int i = 0; i < numbers.Length; ++i)
			{
				int number = numbers[i];
				for(int j = 0; j < numbers.Length; ++j)
				{
					if(i != j)
					{
						int other = numbers[j];
						if((number + other) == 2020)
						{
							return number * other;
						}
					}
				}

			}
			return -1;
		}

		private static int Solve_Array()
		{
			string[] lines = File.ReadAllLines(Input);
			int[] numbers = lines.Select(int.Parse).ToArray();
			foreach(int number in numbers)
			{
				int other = 2020 - number;
				if(number != other && numbers.Contains(other))
				{
					return number * other;
				}
			}
			return -1;
		}

		private static int Solve_HashSet()
		{
			string[] lines = File.ReadAllLines(Input);
			int[] numbers = lines.Select(int.Parse).ToArray();
			HashSet<int> numbersLookup = lines.Select(int.Parse).ToHashSet();
			foreach(int number in numbers)
			{
				int other = 2020 - number;
				if(numbersLookup.Contains(other))
				{
					return number * other;
				}
			}
			return -1;
		}
	}
}