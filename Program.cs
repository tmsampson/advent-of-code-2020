using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	partial class Program
	{
		static void Main(string[] args)
		{
			if(args.Length == 0)
			{
				Console.WriteLine("No arguments passed, running last challenge");
				Challenges.Last();
			}
			else if(int.TryParse(args[0], out int challengeIndex) && challengeIndex > 0 && challengeIndex <= Challenges.Count)
			{
				Console.WriteLine($"Running challenge #{challengeIndex}");
				Challenges[challengeIndex - 1]();
			}
		}

		// Challenge list
		static List<Action> Challenges = new List<Action>()
		{
			Challenge01
		};
	}
}
