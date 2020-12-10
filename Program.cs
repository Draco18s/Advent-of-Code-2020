using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2020 {
	class Program {
		static void Main(string[] args) {
			string input = File.ReadAllText(Path.GetFullPath("../../inputs/Day10.txt"));
			input = input.Replace("\r", "");
			//input = "";
			long result = DayTen.Part1(input);
			Console.WriteLine(result);
			result = DayTen.Part2(input);
			Console.WriteLine(result);
			Console.ReadKey();
		}
	}
}
