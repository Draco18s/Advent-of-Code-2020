using System;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020 {
	internal class DayTwo {
		internal static int Part1(string input) {
			int numValid = 0;
			Regex test = new Regex(@"(\d+)-(\d+) ([a-z]*): ([a-z]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

			MatchCollection matches = test.Matches(input);

			foreach(Match match in matches) {
				GroupCollection groups = match.Groups;
				string passwordMin = groups[1].Value;
				string passwordMax = groups[2].Value;
				string passwordRule = groups[3].Value;
				string password = groups[4].Value;

				int pMin = int.Parse(passwordMin);
				int pMax = int.Parse(passwordMax);
				int pCount = 0;
				char pRule = passwordRule[0];
				foreach(char c in password) {
					if(c == pRule) {
						pCount++;
					}
				}
				if(pMin <= pCount && pCount <= pMax) {
					numValid++;
				}
			}
			return numValid;
		}

		internal static int Part2(string input) {
			int numValid = 0;
			Regex test = new Regex(@"(\d+)-(\d+) ([a-z]*): ([a-z]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

			MatchCollection matches = test.Matches(input);

			foreach(Match match in matches) {
				GroupCollection groups = match.Groups;
				string passwordMin = groups[1].Value;
				string passwordMax = groups[2].Value;
				string passwordRule = groups[3].Value;
				string password = groups[4].Value;

				int pMin = int.Parse(passwordMin);
				int pMax = int.Parse(passwordMax);
				int pCount = 0;
				char pRule = passwordRule[0];
				for(int i = 0; i < password.Length; i++) {
					if(i+1 == pMin || i+1 == pMax) {
						if(password[i] == pRule) {
							pCount++;
						}
					}
				}
				if(pCount == 1)
					numValid++;
			}

			return numValid;
		}
	}
}