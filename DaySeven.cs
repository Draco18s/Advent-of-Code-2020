using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020 {
	internal class DaySeven {
		internal static int Part1(string input) {
			string[] lines = input.Split('\n');
			List<BagHolder> bags = BagParser(lines);
			List<string> validContainers = new List<string>();
			List<string> searchContainers = new List<string>();
			searchContainers.Add("shiny gold");
			while(searchContainers.Count > 0)
			{
				string startBag = searchContainers[0];
				searchContainers.RemoveAt(0);
				foreach(BagHolder bag in bags) {
					if(bag.canContain.ContainsKey(startBag)) {
						if(!validContainers.Contains(bag.name)) {
							validContainers.Add(bag.name);
							searchContainers.Add(bag.name);
						}
					}
				}
				
			} 
			return validContainers.Count;
		}

		private static List<BagHolder> BagParser(string[] lines) {
			Regex bagMatch = new Regex(@"([a-z ]+) bags contain (.*)");
			List<BagHolder> bags = new List<BagHolder>();
			foreach(string line in lines) {
				MatchCollection matches = bagMatch.Matches(line);
				foreach(Match match in matches) {
					GroupCollection groups = match.Groups;
					BagHolder holder = new BagHolder(groups[1].Value, groups[2].Value);
					bags.Add(holder);
				}
			}
			return bags;
		}

		internal static int Part2(string input) {
			string[] lines = input.Split('\n');
			List<BagHolder> bags = BagParser(lines);

			List<string> searchContainers = new List<string>();
			searchContainers.Add("shiny gold");
			int bagCount = 0;
			while(searchContainers.Count > 0) {
				string currentSearch = searchContainers[0];
				searchContainers.RemoveAt(0);
				BagHolder bag = bags.Find(x => x.name == currentSearch);
				foreach(string b in bag.canContain.Keys) {
					int count = bag.canContain[b];
					for(int i = 0; i < count; i++)
						searchContainers.Add(b);
					bagCount += count;
				}
			}
			return bagCount;
		}
	}
}
 