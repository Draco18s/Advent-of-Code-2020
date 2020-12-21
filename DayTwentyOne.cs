using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020 {
	internal class DayTwentyOne {
		public class FoodItem {
			public string[] ingredients;
			public string[] allergens;

			public FoodItem(string[] ingred, string[] aller) {
				List<string> a = new List<string>(aller);
				a.Remove("contains");
				a.Remove("");
				allergens = a.ToArray();

				a = new List<string>(ingred);
				a.Remove("");
				ingredients = a.ToArray();
			}
		}

		internal static long Part1(string input) {
			string[] lines = input.Split('\n');
			int i = 0;
			List<FoodItem> foods = new List<FoodItem>();
			List<string> knownAllergens = new List<string>();
			List<string> knownIngredients = new List<string>();
			foreach(string lin in lines) {
				Regex pattern = new Regex(@"([\w ,]+)");
				MatchCollection matches = pattern.Matches(lin);
				GroupCollection groups = matches[0].Groups;
				string ingreds = groups[1].Value;
				groups = matches[1].Groups;
				string allergens = groups[1].Value.Replace(",", "");
				FoodItem fi = new FoodItem(ingreds.Split(' '), allergens.Split(' '));
				knownAllergens.AddRange(fi.allergens);
				knownIngredients.AddRange(fi.ingredients);
				foods.Add(fi);
				i++;
			}
			knownAllergens = knownAllergens.Distinct().ToList();
			knownIngredients = knownIngredients.Distinct().ToList();
			// ingredient -> allergen
			Dictionary<string, string> allergenDict = new Dictionary<string, string>();
			IdentifyAllergens(foods, ref allergenDict);
			int nonCount = 0;
			foreach(FoodItem food in foods) {
				foreach(string ingred in food.ingredients) {
					if(allergenDict[ingred] == "none") nonCount++;
				}
			}
			var p = allergenDict.Where(kvp => kvp.Value != "none").ToList();
			int vv = allergenDict.Values.Count(x => x == "none");
			return nonCount; //2017 - 2351
		}

		private static void IdentifyAllergens(List<FoodItem> foods, ref Dictionary<string, string> allergenDict) {
			while(true) {
				string unknown = "";
				foreach(FoodItem food in foods) {
					foreach(string ingred in food.ingredients) {
						if(!allergenDict.ContainsKey(ingred)) {
							unknown = ingred;
							goto haveIngred;
						}
					}
				}
			haveIngred:
				bool dobreak = true;
				int v = allergenDict.Values.Count(x => x == "none");
				if(unknown == "") {
					List<string> removeThese = new List<string>();
					foreach(string allergen in allergenDict.Keys) {
						if(allergenDict[allergen] == "??") {
							removeThese.Add(allergen);
							dobreak = false;
						}
					}
					foreach(string rem in removeThese) {
						allergenDict.Remove(rem);
					}
					if(dobreak)
						break;
					continue;
				}
				string possible = FindPattern(foods, foods.Where(x => x.ingredients.Contains(unknown)).ToList(), unknown, allergenDict);
				allergenDict[unknown] = possible;
			}
		}

		private static string FindPattern(List<FoodItem> allFoods, List<FoodItem> foods, string ingredient, Dictionary<string, string> allergenDict) {
			Dictionary<string, int> allergenCount = new Dictionary<string, int>();
			List<FoodItem> nonFoods = allFoods.Where(x => !foods.Contains(x)).ToList();
			foreach(FoodItem food in foods) {
				foreach(string allergen in food.allergens) {
					if(!allergenCount.ContainsKey(allergen)) {
						allergenCount[allergen] = 0;
					}
					allergenCount[allergen]++;
				}
			}
			List<string> removeThese = new List<string>();
			foreach(string allergen in allergenCount.Keys) {
				if(nonFoods.Any(f => f.allergens.Contains(allergen))) {
					removeThese.Add(allergen);
				}
			}
			foreach(string ingred in allergenDict.Keys) {
				if(allergenDict[ingred] != "??" && allergenDict[ingred] != "none") {
					removeThese.Add(allergenDict[ingred]);
				}
			}
			foreach(string rem in removeThese) {
				allergenCount.Remove(rem);
			}
			if(allergenCount.Count == 0) {
				return "none";
			}
			if(allergenCount.Count == 1) {
				return allergenCount.Keys.First();
			}

			int maxValue = allergenCount.Values.Max();
			return "??";
		}

		internal static long Part2(string input) {
			string[] lines = input.Split('\n');
			int i = 0;
			List<FoodItem> foods = new List<FoodItem>();
			List<string> knownAllergens = new List<string>();
			List<string> knownIngredients = new List<string>();
			foreach(string lin in lines) {
				Regex pattern = new Regex(@"([\w ,]+)");
				MatchCollection matches = pattern.Matches(lin);
				GroupCollection groups = matches[0].Groups;
				string ingreds = groups[1].Value;
				groups = matches[1].Groups;
				string allergens = groups[1].Value.Replace(",", "");
				FoodItem fi = new FoodItem(ingreds.Split(' '), allergens.Split(' '));
				knownAllergens.AddRange(fi.allergens);
				knownIngredients.AddRange(fi.ingredients);
				foods.Add(fi);
				i++;
			}
			knownAllergens = knownAllergens.Distinct().ToList();
			knownIngredients = knownIngredients.Distinct().ToList();
			// ingredient -> allergen
			Dictionary<string, string> allergenDict = new Dictionary<string, string>();
			IdentifyAllergens(foods, ref allergenDict);
			List<KeyValuePair<string, string>> allergenList = allergenDict.Where(x => x.Value != "none").ToList();

			allergenList.Sort((a, b) => a.Value.CompareTo(b.Value));
			StringBuilder sb = new StringBuilder();
			foreach(KeyValuePair<string, string> kvp in allergenList) {
				sb.Append(kvp.Key).Append(',');
			}
			sb.Remove(sb.Length - 1, 1);
			Console.WriteLine(sb.ToString());
			return 1;
		}
	}
} 