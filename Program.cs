using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	partial class Program
	{
		static void Main(string[] args)
		{
			// Select challenge
			Action challenge = null;
			if(args.Length == 0)
			{
				Console.WriteLine("No arguments passed, running last challenge");
				challenge = Challenges.Last();
			}
			else if(int.TryParse(args[0], out int challengeIndex) && challengeIndex > 0 && challengeIndex <= Challenges.Count)
			{
				Console.WriteLine($"Running challenge #{challengeIndex}");
				challenge = Challenges[challengeIndex - 1];
			}

			// Run challenge
			Console.WriteLine("----------------------------------------------------");
			challenge();
		}

		// Challenge list
		static List<Action> Challenges = new List<Action>()
		{
			Challenge01
		};
	}
}
