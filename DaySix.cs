using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2020 {
	internal class DaySix {
		internal static int Part1(string input) {
			input = input.Replace('\n', ',');
			input = input.Replace(",,", "|");
			string[] lines = input.Split('|');
			int total = 0;
			foreach(string line in lines) {
				char[] s = line.ToCharArray();
				char[] q = s.Distinct().ToArray();
				total += q.Length;
				if(q.Contains(',')) {
					total -= 1;
				}
			}
			return total;
		}

		internal static int Part2(string input) {
			input = input.Replace('\n', ',');
			input = input.Replace(",,", "|");
			string[] lines = input.Split('|');
			int total = 0;
			foreach(string line in lines) {
				string[] people = line.Split(',');
				Dictionary<char, int> dict = new Dictionary<char,int>();
				foreach(string pep in people) {
					foreach(char c in pep.ToCharArray()) {
						if(dict.ContainsKey(c))
							dict[c]++;
						else
							dict.Add(c, 1);
					}
				}
				foreach(char c in dict.Keys) {
					if(dict[c] == people.Length) {
						total += 1;
					}
 				}
			}
			return total;
		}
	}
}