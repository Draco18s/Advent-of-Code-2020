using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2020 {
	class Program {
		static void Main(string[] args) {
			string input = File.ReadAllText(Path.GetFullPath("../../inputs/Day18.txt"));
			input = input.Replace("\r", "");
			//input = @"";
			DateTime s = DateTime.Now;
			long result = DayEightteen.Part1(input);
			DateTime e = DateTime.Now;
			Console.WriteLine(result);
			Console.WriteLine("Time: " + (e - s).TotalMilliseconds);
			s = DateTime.Now;
			result = DayEightteen.Part2(input);
			e = DateTime.Now;
			Console.WriteLine(result);
			Console.WriteLine("Time: " + (e-s).TotalMilliseconds);
			Console.ReadKey();
		}
	}
}
