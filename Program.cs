using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	partial class Program
	{
		static void Main(string[] args)
		{
			// Select puzzle
			Action puzzle = null;
			if(args.Length == 0)
			{
				Console.WriteLine("No arguments passed, running last puzzle");
				puzzle = Puzzles.Last();
			}
			else if(int.TryParse(args[0], out int puzzleIndex) && puzzleIndex > 0 && puzzleIndex <= Puzzles.Count)
			{
				Console.WriteLine($"Running puzzle #{puzzleIndex}");
				puzzle = Puzzles[puzzleIndex - 1];
			}

			// Run puzzle
			Console.WriteLine("----------------------------------------------------");
			puzzle();
		}

		// puzzle list
		static List<Action> Puzzles = new List<Action>()
		{
			Puzzle01.Run
		};
	}
}
