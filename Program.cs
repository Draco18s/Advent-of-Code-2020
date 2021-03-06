﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;
using Advent_of_Code_2020.StatsBuilder;

namespace Advent_of_Code_2020 {
	class Program {
		static void Main(string[] args) {
			string input = File.ReadAllText(Path.GetFullPath("../../inputs/Day25.txt"));
			input = input.Replace("\r", "");
			//string input = @"";
			DateTime s = DateTime.Now;
			long result = DayTwentyFive.Part1(input);
			DateTime e = DateTime.Now;
			Console.WriteLine(result);
			Console.WriteLine("Time: " + (e - s).TotalMilliseconds);
			s = DateTime.Now;
			result = DayTwentyFive.Part2(input);
			e = DateTime.Now;
			Console.WriteLine(result);
			Console.WriteLine("Time: " + (e - s).TotalMilliseconds);
			Console.ReadKey();
			//BuildLeaderboard();
		}

		static void BuildLeaderboard() {
			string input = File.ReadAllText(Path.GetFullPath("../../inputs/leaderboard.txt"));
			AoCLeaderboard obj = JsonSerializer.Deserialize<AoCLeaderboard>(input);
			List<AoCUser> users = new List<AoCUser>();
			foreach(AoCUser u in obj.members.Values) {
				if(u.name == null) {
					u.name = "(anonymous user #" + u.id + ")";
				}
				users.Add(u);
			}
			string mainTable = "<table> <tbody> <tr> <td class=\"typeheader\" colspan=\"3\">(25 items)<span class=\"fixedextenser\">4</span></td></tr><tr> <th title=\"System.String\">day</th> <th title=\"System.String,System.DateTime,System.Int32[]\">silver_order</th> <th title=\"System.String,System.DateTime,System.Int32[]\">gold_order</th> </tr>{0}</tbody></table>";
			StringBuilder builder = new StringBuilder();
			for(int d = 1; d <= 25; d++) {
				builder.Append(GetTableRow(users, d));
			}
			builder.Append(GetTableRowScores(users));
			input = File.ReadAllText(Path.GetFullPath("../../inputs/leaderboard_html.txt"));
			input = input.Replace("{", "{{").Replace("}", "}}").Replace("{{0}}", "{0}");
			if(File.Exists(Path.GetFullPath("../../leaderboard.html"))) {
				File.Delete(Path.GetFullPath("../../leaderboard.html"));
			}
			File.WriteAllText(Path.GetFullPath("../../leaderboard.html"),string.Format(input, string.Format(mainTable, builder.ToString())));
		}

		private static string GetTableRow(List<AoCUser> users, int d) {
			string tablerowtable = "<tr> <td>{4}</td><td> <table> <tbody> <tr> <td class=\"typeheader\" colspan=\"3\">ø[{2}]&nbsp;<span class=\"fixedextenser\">4</span></td></tr><tr> <th title=\"System.String\">name</th> <th title=\"System.DateTime\">get_star_ts</th> <th title=\"System.Int32\">value</th> </tr>{0}</tbody> </table> </td><td> <table> <tbody> <tr> <td class=\"typeheader\" colspan=\"3\">ø[{3}]&nbsp;<span class=\"fixedextenser\">4</span></td></tr><tr> <th title=\"System.String\">name</th> <th title=\"System.DateTime\">get_star_ts</th> <th title=\"System.Int32\">value</th> </tr>{1}</tbody> </table> </td></tr>";
			string day = d.ToString();
			string[] parts = new string[2];
			parts[0] = "";
			parts[1] = "";
			for(int p = 1; p <= 2; p++) {
				string part = p.ToString();
				SortUsers(ref users, day, part);
				foreach(AoCUser user in users) {
					int pts = GetPointsForUser(ref users, user.id, day, part);
					if(d > 1)
						user.locPoints += pts;
					parts[p - 1] += GetUserLineScore(user, day, part, pts);
				}
			}
			return string.Format(tablerowtable, parts[0], parts[1], Count(users, day, "1"), Count(users, day, "2"), day);
		}

		private static string GetTableRowScores(List<AoCUser> users) {
			users.Sort((x, y) => y.locPoints.CompareTo(x.locPoints));
			string tablerowtable = "<tr> <td>{2}</td><td> <table> <tbody> <tr> <td class=\"typeheader\" colspan=\"3\">ø[{1}]&nbsp;<span class=\"fixedextenser\">4</span></td></tr><tr> <th title=\"System.String\">name</th> <th title=\"System.Int32\">total</th> </tr>{0}</tbody> </table> </td> </table> </td></tr>";
			string row = "";
			foreach(AoCUser user in users) {
				int pts = user.locPoints;
				//user.locPoints += pts;
				row += GetUserTotalScore(user, pts);
			}
			return string.Format(tablerowtable, row, users.Count, "Total");
		}

		private static int Count(List<AoCUser> users, string day, string part) {
			return users.Count(x => {
				return x.completion_day_level.ContainsKey(day) && x.completion_day_level[day].ContainsKey(part);
			});
		}

		private static void SortUsers(ref List<AoCUser> users, string day, string part) {
			users.Sort((x, y) => {
				bool xhas = true;
				bool yhas = true;
				if(!y.completion_day_level.ContainsKey(day) || !y.completion_day_level[day].ContainsKey(part)) {
					yhas = false;
				}
				if(!x.completion_day_level.ContainsKey(day) || !x.completion_day_level[day].ContainsKey(part)) {
					xhas = false;
				}
				if(xhas && yhas)
					return x.completion_day_level[day][part].dateTime.CompareTo(y.completion_day_level[day][part].dateTime);
				else
					return yhas.CompareTo(xhas);
			});
		}

		private static int GetPointsForUser(ref List<AoCUser> users, string user, string day, string part) {
			int i = users.FindIndex(x => x.id == user);
			if(users[i].completion_day_level.ContainsKey(day) && users[i].completion_day_level[day].ContainsKey(part))
				return users.Count - i;
			return 0;
		}

		private static string GetUserLineScore(AoCUser user, string day, string part, int pts) {
			string p = "<tr><td>{0}</td><td>{1}</td><td class=\"n\">{2}</td></tr>";
			if(user.completion_day_level.ContainsKey(day) && user.completion_day_level[day].ContainsKey(part))
				return string.Format(p, user.name, user.completion_day_level[day][part].dateTime.ToString("M/d/yyyy h:mm:ss tt"), pts);
			return string.Format(p, user.name, "&nbsp;", "&nbsp;");
		}

		private static string GetUserTotalScore(AoCUser user, int pts) {
			string p = "<tr><td>{0}</td><td class=\"n\">{1}</td></tr>";
			return string.Format(p, user.name, pts.ToString());
		}
	}
}
