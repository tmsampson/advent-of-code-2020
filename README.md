# Advent of Code 2020

### Overview
 Solution for Advent of Code 2020 Challenges https://adventofcode.com/2020

### Framework
* Each puzzle solution is a class implementing **IPuzzle**
* As such, each puzzle class implements **IPuzzle::Run()** which returns an **IPuzzleResults** collection:
	* This allows you to try out different solutions/algorithms for each puzzle, each of which is profiled and the results automatically sorted, revealing which solution came out on top!
* **IPuzzleResults** contains one or more **PuzzleRunResult** objects:
	* Each **PuzzleRunResult** contains an **IPuzzleAnswer**, allowing each puzzle to store arbitrary answer data, summarised via **IPuzzleAnswer::ToString()**
* Don't forget to register your  puzzle classes inside **Program::Main()**

### Running in 'Release' mode
To run in release mode, use command:

`dotnet run --configuration Release -- $args`

For example, to run puzzle 1:

`dotnet run --configuration Release -- 1`