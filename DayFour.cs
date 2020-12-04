using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Advent_of_Code_2020 {
	internal class DayFour {
		internal static int Part1(string input) {
			string[] lines = input.Split('|');
			int validPass = 0;
			foreach(string lin in lines) {
				validPass += ValidatePassport1(lin) ? 1 :0;
			}
			return validPass;
		}
		
		internal static int Part2(string input) {
			string[] lines = input.Split('|');
			int validPass = 0;
			foreach(string lin in lines) {
				validPass += ValidatePassport2(lin) ? 1 : 0;
			}
			return validPass;
		}

		private static bool ValidatePassport1(string lin) {
			string[] parts = lin.Split(' ');
			bool byr = false;
			bool iyr = false;
			bool eyr = false;
			bool hgt = false;
			bool hcl = false;
			bool ecl = false;
			bool pid = false;
			bool cid = false;
			if(parts.Length < 7 || parts.Length > 8) return false;
			foreach(string prt in parts) {
				string[] fields = prt.Split(':');
				if(fields[0].Equals("byr")) {
					byr = true;
				}
				else if(fields[0].Equals("iyr")) {
					iyr = true;
				}
				else if(fields[0].Equals("eyr")) {
					eyr = true;
				}
				else if(fields[0].Equals("hgt")) {
					hgt = true;
				}
				else if(fields[0].Equals("hcl")) {
					hcl = true;
				}
				else if(fields[0].Equals("ecl")) {
					ecl = true;
				}
				else if(fields[0].Equals("pid")) {
					pid = true;
				}
				else if(fields[0].Equals("cid")) {
					cid = true;
				}
				else {
					Console.WriteLine(fields[0]);
					return false;
				}
			}
			return byr && iyr && eyr && hgt && hcl && ecl && pid;// && cid;
		}

		private static bool ValidatePassport2(string lin) {
			string[] parts = lin.Split(' ');
			/*
			byr (Birth Year)
			iyr (Issue Year)
			eyr (Expiration Year)
			hgt (Height)
			hcl (Hair Color)
			ecl (Eye Color)
			pid (Passport ID)
			cid (Country ID)*/
			bool byr = false;
			bool iyr = false;
			bool eyr = false;
			bool hgt = false;
			bool hcl = false;
			bool ecl = false;
			bool pid = false;
			bool cid = false;
			if(parts.Length < 7 || parts.Length > 8) return false;
			foreach(string prt in parts) {
				string[] fields = prt.Split(':');
				if(fields[0].Equals("byr")) {
					byr = true;
					int yr;
					if(int.TryParse(fields[1], out yr)) {
						if(yr < 1920 || yr > 2020) return false;
					}
					else {
						return false;
					}
				}
				else if(fields[0].Equals("iyr")) {
					iyr = true;
					int yr;
					if(int.TryParse(fields[1], out yr)) {
						if(yr < 2010 || yr > 2020) return false;
					}
					else {
						return false;
					}
				}
				else if(fields[0].Equals("eyr")) {
					eyr = true;
					int yr;
					if(int.TryParse(fields[1], out yr)) {
						if(yr < 2020 || yr > 2030) return false;
					}
					else {
						return false;
					}
				}
				else if(fields[0].Equals("hgt")) {
					hgt = true;
					string vv = fields[1].Substring(0, fields[1].Length - 2);
					string unit = fields[1].Substring(fields[1].Length - 2, 2);
					int yr;
					if(int.TryParse(vv, out yr)) {
						if(unit.Equals("cm")) {
							if(yr < 150 || yr > 193) return false;
						}
						else if(unit.Equals("in")) {
							if(yr < 59 || yr > 76) return false;
						}
						else {
							return false;
						}
					}
					else {
						return false;
					}
				}
				else if(fields[0].Equals("hcl")) {
					hcl = true;
					if(fields[1][0] != '#' || fields[1].Length > 7) return false;
					foreach(char c in fields[1]) {
						if(c == '#') continue;
						if(c != 'a' && c != 'b' && c != 'c' && c != 'd' && c != 'e' && c != 'f') {
							if(c < '0' || c > '9') {
								return false;
							}
						}
					}
				}
				else if(fields[0].Equals("ecl")) {
					ecl = true;
					string col = fields[1];
					if(col.Equals("amb") || col.Equals("blu") || col.Equals("brn") || col.Equals("gry")
						|| col.Equals("grn") || col.Equals("hzl") || col.Equals("oth")) {

					}
					else {
						return false;
					}
				}
				else if(fields[0].Equals("pid")) {
					pid = true;
					int yr;
					if(!int.TryParse(fields[1], out yr) || fields[1].Length != 9) {
						return false;
					}
				}
				else if(fields[0].Equals("cid")) {
					cid = true;
				}
				else {
					Console.WriteLine(fields[0]);
					return false;
				}
			}
			return byr && iyr && eyr && hgt && hcl && ecl && pid;// && cid;
		}
	}
}
