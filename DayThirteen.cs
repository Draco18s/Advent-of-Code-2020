using System;
using System.Linq;

namespace Advent_of_Code_2020 {
	internal class DayThirteen {
		internal static int Part1(string input) {
			string[] lines = input.Split('\n');
			int current = int.Parse(lines[0]);
			lines = lines[1].Split(',');
			int[] busIDs = new int[lines.Length];
			for(int i=0; i<lines.Length;i++) {
				string l = lines[i];
				int v;
				if(int.TryParse(l, out v)) {
					busIDs[i] = v;
				}
			}
			int minTime = int.MaxValue;
			int minBus = -1;
			foreach(int bus in busIDs) {
				if(bus == 0) continue;
				int nextLeave = (current / bus)+1;
				if(nextLeave * bus < minTime) {
					minTime = nextLeave * bus;
					minBus = bus;
				}
			}
			return minBus * (minTime - current);
		}

		internal static long Part2(string input) {
			string[] lines = input.Split('\n');
			//int current = int.Parse(lines[0]);
			lines = lines[1].Split(',');
			int[] busIDs = new int[lines.Length];
			for(int i = 0; i < lines.Length; i++) {
				string l = lines[i];
				int v;
				if(int.TryParse(l, out v)) {
					busIDs[i] = v;
				}
			}
			int numPlus = 0;
			Console.WriteLine("Plug this into wolframalpha:");
			foreach(int x in busIDs) {
				if(x == 0) {
					numPlus++;
					continue;
				}
				Console.Write("(x+" + numPlus + ")%" + x + "=0,");
				numPlus++;
			}
			Console.Write("\n");
			return -1;
		}
	}
}