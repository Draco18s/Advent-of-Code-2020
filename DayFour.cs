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
			/*
			byr (Birth Year)
			iyr (Issue Year)
			eyr (Expiration Year)
			hgt (Height)
			hcl (Hair Color)
			ecl (Eye Color)
			pid (Passport ID)
			cid (Country ID)
			*/
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
				if(fields[0] == "byr") {
					byr = true;
				}
				else if(fields[0] == "iyr") {
					iyr = true;
				}
				else if(fields[0] == "eyr") {
					eyr = true;
				}
				else if(fields[0] == "hgt") {
					hgt = true;
				}
				else if(fields[0] == "hcl") {
					hcl = true;
				}
				else if(fields[0] == "ecl") {
					ecl = true;
				}
				else if(fields[0] == "pid") {
					pid = true;
				}
				else if(fields[0] == "cid") {
					cid = true;
				}
				else {
					//log any unknown fields
					Console.WriteLine(fields[0]);
					return false;
				}
			}
			return byr && iyr && eyr && hgt && hcl && ecl && pid;// && cid;
		}

		private static bool ValidatePassport2(string lin) {
			string[] parts = lin.Split(' ');
			/*
			byr (Birth Year) - four digits; at least 1920 and at most 2002.
			iyr (Issue Year) - four digits; at least 2010 and at most 2020.
			eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
			hgt (Height) - a number followed by either cm or in:
				If cm, the number must be at least 150 and at most 193.
				If in, the number must be at least 59 and at most 76.
			hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
			ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
			pid (Passport ID) - a nine-digit number, including leading zeroes.
			cid (Country ID) - ignored, missing or not.
			*/
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
				if(fields[0] == "byr") {
					byr = true;
					if(int.TryParse(fields[1], out int yr)) {
						if(yr < 1920 || yr > 2020) return false;
					}
					else {
						return false;
					}
				}
				else if(fields[0] == "iyr") {
					iyr = true;
					if(int.TryParse(fields[1], out int yr)) {
						if(yr < 2010 || yr > 2020) return false;
					}
					else {
						return false;
					}
				}
				else if(fields[0] == "eyr") {
					eyr = true;
					if(int.TryParse(fields[1], out int yr)) {
						if(yr < 2020 || yr > 2030) return false;
					}
					else {
						return false;
					}
				}
				else if(fields[0] == "hgt") {
					hgt = true;
					string vv = fields[1].Substring(0, fields[1].Length - 2);
					string unit = fields[1].Substring(fields[1].Length - 2, 2);
					if(int.TryParse(vv, out int hh)) {
						if(unit == "cm") {
							if(hh < 150 || hh > 193) return false;
						}
						else if(unit == "in") {
							if(hh < 59 || hh > 76) return false;
						}
						else {
							return false;
						}
					}
					else {
						return false;
					}
				}
				else if(fields[0] == "hcl") {
					hcl = true;
					if(fields[1][0] != '#' || fields[1].Length != 7) return false;
					foreach(char c in fields[1]) {
						if(c == '#') continue;
						if(c != 'a' && c != 'b' && c != 'c' && c != 'd' && c != 'e' && c != 'f') {
							if(c < '0' || c > '9') {
								return false;
							}
						}
					}
				}
				else if(fields[0] == "ecl") {
					ecl = true;
					string col = fields[1];
					if(!(col == "amb" || col == "blu" || col == "brn" || col == "gry"
						|| col == "grn" || col == "hzl" || col == "oth")) {
						return false;
					}
				}
				else if(fields[0] == "pid") {
					pid = true;
					if(!int.TryParse(fields[1], out int yr) || fields[1].Length != 9) {
						return false;
					}
				}
				else if(fields[0] == "cid") {
					cid = true;
				}
				else {
					//log any unknown fields
					Console.WriteLine(fields[0]);
					return false;
				}
			}
			return byr && iyr && eyr && hgt && hcl && ecl && pid;// && cid;
		}
	}
}
