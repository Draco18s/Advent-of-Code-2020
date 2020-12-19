using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020 {
	internal class DayNineteen {
		internal static long Part1(string input) {
			string[] lines = input.Split('\n');
			int blank = GetBlankLine(lines);
			Dictionary<int, string> rules = new Dictionary<int, string>();
			for(int i = 0; i < blank; i++) {
				int v = int.Parse(lines[i].Split(':')[0]);
				rules[v] = lines[i];
			}
			string s = "^"+ParseRules(rules, 0)+"$";
			Regex pattern = new Regex(s);
			int count = 0;
			for(int i = blank+1; i < lines.Length; i++) {
				if(pattern.IsMatch(lines[i]))
					count++;
			}
			return count;
		}

		private static int GetBlankLine(string[] lines) {
			for(int i=0;i<lines.Length;i++) {
				if(lines[i].Length < 1) return i;
			}
			return -1;
		}

		internal static long Part2(string input) {
			string[] lines = input.Split('\n');
			int blank = GetBlankLine(lines);
			Dictionary<int,string> rules = new Dictionary<int,string>();
			for(int i = 0; i < blank; i++) {
				int v = int.Parse(lines[i].Split(':')[0]);
				rules[v] = lines[i];
			}
			rules[8] = "8: 42 +"; // 8: 42 | 42 8
			rules[11] = "11: 42 {n} 31 {n}"; // 11: 42 31 | 42 11 31
			// Rule 8 is effectively "some number of rule 42s" which is just +
			// Rule 11 is "some number of rule 42s followed by the same number of rule 31s"
			// And we have to handle n ourselves.
			string s = "^" + ParseRules(rules, 0) + "$";
			int count = 0;
			for(int j = 1; j < 50; j++) {
				string s2 = s.Replace("n", j.ToString());
				Regex pattern = new Regex(s2);
				for(int i = blank + 1; i < lines.Length && j < lines[i].Length; i++) {
					if(pattern.IsMatch(lines[i])) {
						count++;
					}
				}
			}
			return count;
		}

		private static string ParseRules(Dictionary<int,string> rules, int v) {
			string result = "";
			string[] parts = rules[v].Split(':')[1].Split(' ');
			bool parens = false;
			foreach(string part in parts) {
				if(part.Length < 1) continue;
				if(part == "|") {
					result += "|";
					parens = true;
				}
				else if(part == "\"a\"") {
					return "a";
				}
				else if(part == "\"b\"") {
					return "b";
				}
				else if(part == "+") {
					result += "+";
				}
				else if(part == "{n}") {
					result += "{n}";
				}
				else {
					int v2 = int.Parse(part);
					result += ParseRules(rules, v2);
				}
			}
			if(parens) {
				result = "(" + result + ")";
			}
			return result;
		}
	}
}