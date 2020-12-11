using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2020 {
	class Program {
		static void Main(string[] args) {
			string input = File.ReadAllText(Path.GetFullPath("../../inputs/Day11.txt"));
			input = input.Replace("\r", "");
			//input = "";
			long result = DayEleven.Part1(input);
			Console.WriteLine(result);
			DateTime s = DateTime.Now;
			result = DayEleven.Part2(input);
			DateTime e = DateTime.Now;
			Console.WriteLine(result);
			Console.WriteLine((e-s).TotalMilliseconds);
			Console.ReadKey();
		}
	}
}
