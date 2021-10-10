using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	partial class Program
	{
		static void Main(string[] args)
		{
			// Puzzle list
			var puzzles = new List<IPuzzle>()
			{
				new Puzzle01(),
				new Puzzle02()
			};

			// Select puzzle
			IPuzzle puzzle = null;
			if(args.Length == 0)
			{
				Console.WriteLine("No arguments passed, running latest puzzle");
				puzzle = puzzles.Last();
			}
			else if(int.TryParse(args[0], out int puzzleIndex) && puzzleIndex > 0 && puzzleIndex <= puzzles.Count)
			{
				Console.WriteLine($"Running puzzle #{puzzleIndex:00}");
				puzzle = puzzles[puzzleIndex - 1];
			}

			// Run puzzle
			if(puzzle != null)
			{
				Console.WriteLine("-------------------------------------------------------------------------------------------------------");
				IPuzzleResults result = puzzle.Run();

				// Print results
				result.Print();
			}
		}
	}
}
