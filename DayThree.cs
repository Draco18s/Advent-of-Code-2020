using System;

namespace Advent_of_Code_2020 {
	internal class DayThree {

		internal static int Part1(string input) {
			string[] lines = input.Split('\n');
			return FindTrees(3, 1, lines);
		}

		private static int FindTrees(int dx, int dy, string[] lines) {
			int x = 0, y = 0;
			int trees = 0;
			while(y + dy < lines.Length) {
				x += dx;
				y += dy;
				x %= lines[0].Length;
				if(lines[y][x] == '#') {
					trees++;
				}
			}
			return trees;
		}

		internal static long Part2(string input) {
			string[] lines = input.Split('\n');
			long a = FindTrees(1, 1, lines);
			int b = FindTrees(3, 1, lines);
			int c = FindTrees(5, 1, lines);
			int d = FindTrees(7, 1, lines);
			int e = FindTrees(1, 2, lines);
			return a*b*c*d*e;
		}
	}
}