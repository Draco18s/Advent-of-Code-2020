using Koopakiller.Numerics;
using System;
using System.Collections.Generic;

namespace Advent_of_Code_2020 {
	internal class DayEightteen {
		internal static long Part1(string input) {
			string[] lines = input.Split('\n');
			long total = 0;
			foreach(string lin in lines) {
				Stack<Tuple<char, long>> stack = new Stack<Tuple<char, long>>();
				stack.Push(new Tuple<char, long>(' ', 0));
				foreach(char exp in lin) {
					if(exp == ' ') continue;
					Tuple<char, long> t = stack.Pop();
					switch(exp) {
						case '(':
							stack.Push(t);
							stack.Push(new Tuple<char, long>(' ', 0));
							break;
						case ')':
							Tuple<char, long> t2 = stack.Pop();
							switch(t2.Item1) {
								case '+':
									stack.Push(new Tuple<char, long>(t.Item1, t.Item2 + t2.Item2));
									break;
								case '*':
									stack.Push(new Tuple<char, long>(t.Item1, t.Item2 * t2.Item2));
									break;
								case ' ':
									stack.Push(new Tuple<char, long>(' ', t.Item2));
									if(t2.Item2 != 0) {
										Console.Write(lin);
										return -1;
									}
									break;
							}
							break;
						case '+':
						case '*':
							stack.Push(new Tuple<char, long>(exp, t.Item2));
							break;
						default: // numbers
							if(int.TryParse("" + exp, out int v)) {
								switch(t.Item1) {
									case '+':
										stack.Push(new Tuple<char, long>(t.Item1, t.Item2 + v));
										break;
									case '*':
										stack.Push(new Tuple<char, long>(t.Item1, t.Item2 * v));
										break;
									case ' ':
										stack.Push(new Tuple<char, long>(t.Item1, v));
										break;
								}
								if(stack.Peek().Item2 < 0) {
									Console.WriteLine(lin);
								}
							}
							break;
					}
				}
				if(stack.Count > 1) {
					Console.WriteLine("AAA?");
					return -1;
				}
				Tuple<char, long> f = stack.Pop();
				if(total + f.Item2 < 0) {
					Console.Write(total + "+");
					total = 0;
				}
				total += f.Item2;
			}
			return total;
		}

		internal static long Part2(string input) {
			string[] lines = input.Split('\n');
			long total = 0;
			foreach(string lin in lines) {
				Queue<char> output = new Queue<char>();
				Stack<char> trainyard = new Stack<char>();
				foreach(char exp in lin) {
					if(exp == ' ') continue;
					switch(exp) {
						case '+':
						case '*':
						case '(':
							while(trainyard.Count > 0 && trainyard.Peek() != '(' && GetPrecedence(exp) < GetPrecedence(trainyard.Peek())) {
								output.Enqueue(trainyard.Pop());
							}
							trainyard.Push(exp);
							break;
						case ')':
							do {
								char c = trainyard.Pop();
								if(c == '(') break;
								output.Enqueue(c);
							} while(true);
							break;
						default:
							output.Enqueue(exp);
							break;
					}
				}
				while(trainyard.Count > 0) {
					output.Enqueue(trainyard.Pop());
				}
				Stack<object> ops = new Stack<object>();
				long v1, v2;
				while(output.Count > 0) {
					object a = output.Dequeue();
					switch(a) {
						case '+':
							v1 = (long)ops.Pop();
							v2 = (long)ops.Pop();
							ops.Push(v1 + v2);
							break;
						case '*':
							v1 = (long)ops.Pop();
							v2 = (long)ops.Pop();
							ops.Push(v1 * v2);
							break;
						default:
							ops.Push(long.Parse(""+a));
							break;
					}
				}
				total += (long)ops.Pop();
			}
			return total;
		}

		private static int GetPrecedence(char op) {
			if(op == '(') return 999;
			if(op == '+') return 2;
			return 1;
		}
	}
}