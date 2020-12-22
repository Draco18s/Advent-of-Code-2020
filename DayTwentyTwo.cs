using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020 {
	internal class DayTwentyTwo {
		public class PlayerDeck {
			public Queue<int> cards;

			public PlayerDeck() {
				cards = new Queue<int>();
			}

			public PlayerDeck(PlayerDeck deck, int max) {
				cards = new Queue<int>();

				for(int x=0;x<max&&x<deck.cards.Count;x++) {
					int xx = deck.cards.ElementAt(x);
					cards.Enqueue(xx);
				}
			}

			public void GainCards(int yours, int theirs) {
				cards.Enqueue(yours);
				cards.Enqueue(theirs);
			}

			public string GetCards() {
				StringBuilder sb = new StringBuilder();
				var e = cards.GetEnumerator();
				while(e.MoveNext()) {
					sb.Append(e.Current).Append(",");
				}
				return sb.ToString();
			}
		}

		internal static long Part1(string input) {
			string[] lines = input.Split('\n');
			PlayerDeck p1 = new PlayerDeck();
			PlayerDeck p2 = new PlayerDeck();

			bool playerone = true;
			foreach(string ln in lines) {
				if(int.TryParse(ln, out int v)) {
					if(playerone) {
						p1.cards.Enqueue(v);
					}
					else
						p2.cards.Enqueue(v);
				}
				else if(ln == "") {
					playerone = false;
				}
			}
			do {
				int card1 = p1.cards.Dequeue();
				int card2 = p2.cards.Dequeue();
				if(card1 > card2) {
					p1.GainCards(card1, card2);
				}
				else {
					p2.GainCards(card2, card1);
				}
			} while(!GameOver(p1, p2));
			int score = 0;
			if(p1.cards.Count == 0) {
				while(p2.cards.Count > 0) {
					score += p2.cards.Count * p2.cards.Dequeue();
				}
			}
			else {
				while(p1.cards.Count > 0) {
					score += p1.cards.Count * p1.cards.Dequeue();
				}
			}
			return score;
		}

		private static bool GameOver(PlayerDeck p1, PlayerDeck p2) {
			return p1.cards.Count == 0 || p2.cards.Count == 0;
		}

		internal static long Part2(string input) {
			string[] lines = input.Split('\n');
			PlayerDeck p1 = new PlayerDeck();
			PlayerDeck p2 = new PlayerDeck();

			bool playerone = true;
			foreach(string ln in lines) {
				if(int.TryParse(ln, out int v)) {
					if(playerone) {
						p1.cards.Enqueue(v);
					}
					else
						p2.cards.Enqueue(v);
				}
				else if(ln == "") {
					playerone = false;
				}
			}
			GameClass gc = new GameClass(p1, int.MaxValue, p2, int.MaxValue);
			int score = 0;
			if(gc.GetWinner()) {
				//p1
				while(gc.p1.cards.Count > 0) {
					score += gc.p1.cards.Count * gc.p1.cards.Dequeue();
				}
			}
			else {
				//p2
				while(gc.p2.cards.Count > 0) {
					score += gc.p2.cards.Count * gc.p2.cards.Dequeue();
				}
			}
			return score;
		}

		public class GameClass {
			public PlayerDeck p1;
			public PlayerDeck p2;

			public GameClass(PlayerDeck _p1, int a, PlayerDeck _p2, int b) {
				p1 = new PlayerDeck(_p1, a);
				p2 = new PlayerDeck(_p2, b);
			}

			public bool GetWinner() {
				while(!GameOver(p1, p2)) {
					if(CheckInfinite(p1, p2)) {
						return true;
					}
					int card1 = p1.cards.Dequeue();
					int card2 = p2.cards.Dequeue();

					if(card1 <= p1.cards.Count && card2 <= p2.cards.Count) {
						GameClass gc = new GameClass(p1, card1, p2, card2);
						if(gc.GetWinner()) {
							p1.GainCards(card1, card2);
						}
						else {
							p2.GainCards(card2, card1);
						}
						continue;
					}

					if(card1 > card2) {
						p1.GainCards(card1, card2);
					}
					else {
						p2.GainCards(card2, card1);
					}
				} 

				return p2.cards.Count == 0; //true => player 1 wins
			}

			HashSet<string> playerDecks = new HashSet<string>();

			private bool CheckInfinite(PlayerDeck p1, PlayerDeck p2) {
				string a = p1.GetCards();
				string b = p2.GetCards();
				string s = a + "|" + b;
				if(playerDecks.Contains(s)) {
					return true;
				}
				playerDecks.Add(s);
				return false;
			}
		}
	}
}