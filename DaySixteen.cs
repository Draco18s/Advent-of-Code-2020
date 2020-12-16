using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020 {
	internal class DaySixteen {
		internal static long Part1(string input) {
			Dictionary<string, string> rules = new Dictionary<string, string>();
			string[] lines = input.Split('\n');
			int yourTicket = GetLineIndex(lines, "your ticket:");
			// All the lines before "your ticket:" are rules
			for(int i=0;i<yourTicket-1;i++) {
				string[] halves = lines[i].Split(':');
				rules.Add(halves[0], halves[1]);
			}

			int nearbyStart = GetLineIndex(lines, "nearby tickets:");
			int invalid = 0;
			// All lines after "nearby tickets:" are other tickets
			for(int i = nearbyStart+1; i < lines.Length; i++) {
				foreach(string val in lines[i].Split(',')) {
					int v = int.Parse(val);
					// If ticket does not match any rules, apply sum to final value
					if(!DoesMatchAnyRule(v, rules)) {
						invalid += v;
					}
				}
			}
			return invalid;
		}

		private static bool DoesMatchAnyRule(int val, Dictionary<string, string> rules) {
			foreach(string rule in rules.Values) {
				if(DoesMatchRule(rule,val)) {
					return true;
				}
			}
			return false;
		}

		private static bool DoesMatchRule(string rule, int val) {
			Regex parse = new Regex(@"([\d]+)-([\d]+) or ([\d]+)-([\d]+)");
			MatchCollection matches = parse.Matches(rule);
			GroupCollection groups = matches[0].Groups;
			// Check if the val matches the ranges specified by the rule
			// eg "4" is not in "1-3 or 5-7"
			// And yes, these ints get parsed *a lot*
			if((val >= int.Parse(groups[1].Value) && val <= int.Parse(groups[2].Value))
				|| (val >= int.Parse(groups[3].Value) && val <= int.Parse(groups[4].Value))) {
				return true;
			}
			return false;
		}

		// Find a line with a specific string
		private static int GetLineIndex(string[] lines, string search) {
			for(int i = 0; i < lines.Length; i++) {
				if(lines[i] == search)
					return i;
			}
			return -1;
		}

		internal static long Part2(string input) {
			Dictionary<string, string> rules = new Dictionary<string, string>();
			Dictionary<string, List<int>> fieldMatching = new Dictionary<string, List<int>>();
			string[] lines = input.Split('\n');
			int yourTicket = GetLineIndex(lines, "your ticket:");
			// All the lines before "your ticket:" are rules
			for(int i = 0; i < yourTicket - 1; i++) {
				string[] halves = lines[i].Split(':');
				rules.Add(halves[0], halves[1]);
				List<int> list = new List<int>();
				for(int j = 0; j < yourTicket - 1; j++) {
					list.Add(j);
				}
				fieldMatching.Add(halves[0], list);
			}

			int nearbyStart = GetLineIndex(lines, "nearby tickets:");
			
			List<string> validTickets = new List<string>();
			for(int i = nearbyStart + 1; i < lines.Length; i++) {
				foreach(string val in lines[i].Split(',')) {
					int v = int.Parse(val);
					if(!DoesMatchAnyRule(v, rules)) {
						goto badticket;
					}
				}
				// Valid tickets go here
				validTickets.Add(lines[i]);
			badticket:
				;
			}
			// For all valid tickets, start eliminating rule-field pairs
			foreach(string ticket in validTickets) {
				string[] values = ticket.Split(',');
				for(int i=0;i<values.Length;i++) {
					string val = values[i];
					int v = int.Parse(val);
					foreach(string r in rules.Keys) {
						if(!DoesMatchRule(rules[r], v)) {
							fieldMatching[r].Remove(i);
							if(fieldMatching[r].Count == 1) {
								// We found a rule that can only be one thing.
								// Cleanup time.
								CleanupCascade(ref fieldMatching, r);
							}
						}
					}
				}
			}
			long finalValue = 1;
			string[] ticketParts = lines[yourTicket + 1].Split(',');
			// For all rules containing "departure", get their field index
			// and multiply onto the final answer
			foreach(string rule in fieldMatching.Keys) {
				if(rule.Contains("departure")) {
					finalValue *= int.Parse(ticketParts[fieldMatching[rule][0]]);
				}
			}
			return finalValue;
		}

		private static void CleanupCascade(ref Dictionary<string, List<int>> fieldMatching, string rule) {
			foreach(string k in fieldMatching.Keys) {
				if(k == rule) continue;
				// Any rule that still might be field N when we already know field N is known to be rule K
				// Needs to have N removed from its list of possibilities
				// If that rule is now identified, clean up recursively
				if(fieldMatching[k].Remove(fieldMatching[rule][0]) && fieldMatching[k].Count == 1) {
					CleanupCascade(ref fieldMatching, k);
				}
			}
		}
	}
}