using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020 {
	internal class BagHolder {
		public readonly string name;
		public Dictionary<string,int> canContain;
		private static Regex containMatch = new Regex(@"(\d+) ([a-z ]+) bags*");

		public BagHolder(string n, string contents) {
			name = n;
			canContain = new Dictionary<string,int>();
			if(contents == "no other bags") {

			}
			else {
				MatchCollection matches = containMatch.Matches(contents);
				foreach(Match match in matches) {
					GroupCollection groups = match.Groups;
					string bagName = groups[2].Value;
					int count;
					int.TryParse(groups[1].Value, out count);
					canContain.Add(bagName, count);
				}
			}
		}
	}
}