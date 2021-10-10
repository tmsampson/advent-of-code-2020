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
			int puzzleNumber = -1;
			if(args.Length == 0)
			{
				Console.WriteLine("No arguments passed, running latest puzzle");
				puzzleNumber = puzzles.Count;
			}
			else
			{
				int.TryParse(args[0], out puzzleNumber);
			}

			// Run puzzle
			if(puzzleNumber > 0 && puzzleNumber <= puzzles.Count)
			{
				IPuzzle puzzle = puzzles[puzzleNumber - 1];
				Console.WriteLine("-------------------------------------------------------------------------------------------------------");
				Console.WriteLine($"Running puzzle #{puzzleNumber:00}");
				Console.WriteLine("-------------------------------------------------------------------------------------------------------");
				IPuzzleResults result = puzzle.Run();

				// Print results
				result.Print();
			}
		}
	}
}
