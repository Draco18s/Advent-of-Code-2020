﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2020 {
	class Program {
		static void Main(string[] args) {
			string input = ".##.#.........#.....#....#...#.\n.#.#.#...#.......#.............\n......#..#....#.#...###.......#\n.......###......#.....#..##..#.\n..#...##.......#.......###.....\n....###.#....###......#....#..#\n......#..#....#...##...........\n..#..#....#...#.....####.......\n...#........#.#.......#..#...#.\n......#...#........#...#..##...\n#..#........#............#...##\n..#..#.#....#...........#...###\n#.#..#...........#.##.#.#....#.\n.#.#....#...##.....#...........\n.....##....#...#..............#\n...#....#...#.#.#.#...#........\n#....#....#.#.#..#....#..#..#..\n.................#..#.....#....\n#..###...#.#..#.#......#.......\n...#..........#......#....#....\n.#.#.........##..#.......#...#.\n.#..........#...#..#...........\n....##.#.......................\n.......#...........#...#.......\n...#...#..##...#....###..#....#\n....#.#.....##...##.#.#........\n...........#.#..#.#......#..#..\n.....#.....#....#...#........#.\n..#......#..#.........#.....#..\n.........................#...#.\n#...#...#....#........##....#..\n#..#.#.............#..........#\n.#.........#.....#..#.#.#..#.#.\n#...#..#.......####.#....##....\n##...##..#.#.#...#.#.....#..#.#\n.#..#....#.##........#...#....#\n#...#..##.#....##..#..#.#......\n.#........#.....#.#....##.##.#.\n...#...#........#..#.##.##.....\n....................#.#.#.#...#\n..####.#..##...#....#.....##...\n#......#.....#.#......#.#..#.##\n..#.....#..#...........##.#....\n#....#........#............#...\n..##....#..............#......#\n..#......#.#.......####......#.\n..............##....#....##.#..\n.#...............#....#....#.#.\n..#.#.#..#.......##.#..........\n.#...#.......#.#....#.##.......\n.....#.##...#...........#.#....\n..#.#..#...#..##...#.#.......##\n.#.....#....#.#......#.#.......\n....##.........#.#.............\n.......##.......#..............\n..........#......#......#....##\n..##.....#..#.#..........#.....\n...#....#.......#....##........\n.......#...........#...........\n...#.#......#.#........#....#..\n.....#...........#.#.#...#.#..#\n.#.#...#.#.#..........#.....###\n#........#...#.................\n...##.....#.....#..#..#.......#\n......##...........#..#....##..\n.........#............##...#...\n.....#.....##...##.............\n.#....#..#.#.#.#...#..#..#.....\n.....#..#.#..#....#..#.........\n....#.....#......#...#.........\n#..#..#.................#......\n.###.....#...#.#........##.#...\n..#...#....#.##..#.....#.#....#\n..#...##.................#.#...\n....##..........#..#..#..#....#\n....#..##....##.....#.#....#...\n.#.#.#.....##........#.##..##.#\n....#..#......#..#........#....\n.......#.....###.#....#.......#\n#....#.......#......##.#.......\n.##.#.........#.#..##..#....##.\n......#........#.#....#...#....\n.####.....#.........#.#......##\n##....#......#....#..#.#....##.\n...........###.#.....#..#......\n.......#...........#...........\n........###....#..#.#..........\n....#........#......#..........\n.........#......#..............\n...#...............#......#...#\n....#..##...#.........#...#....\n##........#.#....#......###....\n....#.......................#..\n#................#.#..#......##\n...#.#.....#...#...........#.##\n.#....#.##......#...##.#....#..\n#...#....#..............#..#..#\n.......#....#.##............#.#\n.....#.#.......#.#...#.........\n...#.....#..##...##...#........\n..#.......#..####..#..#...#....\n#.#................##...##.#..#\n.....#.....##.#.....#......#..#\n....#.#...#.........#.........#\n..#......#............#.....#..\n.....#..........#.#..#..##...##\n........#................#.#...\n#...#.#....##...###...#.#......\n.............##.#..##..........\n#..#......#...........#......#.\n#.#....#..........#.##....###..\n.............#.........#....#..\n#........#..#.#..#...#....#....\n..............#..............##\n.....#...#..............#.##...\n#...##..#...........#..........\n..#....#...#.#........#..#.#..#\n..##......#...............#....\n....#...#..###..#......###.#...\n.......##..#.#........#....#...\n..##...#.......#...#...........\n.#.......#.....#.#...##..#....#\n.............#.......#.#.#....#\n#.......#..#..#...#.#......##..\n#.##..#..#..#....##.#...###.#.#\n...##...#..#..#........#.#..#..\n#....##........................\n##...#...#......#.#.....#..#...\n......#............#....#......\n#......#.......#.......##.#....\n..................#..#..#.#....\n..#..................##.#......\n..##........#.#.....##..#..#.#.\n#....#..............#....####..\n#..#..........................#\n..#.#.#.#....#.......#....#.#..\n.....#.#........#..........#.#.\n........#.....#.......#........\n#.....#....#.###.....#.......#.\n.....##.#...#.#..#...#.#.#.....\n......##...#.#...##..........#.\n.#............#.....#..#....#..\n.#................#.#..#.......\n....................##...##....\n#.......##...#.....#..#........\n.##....#.#.#.#...........#...#.\n..#.#..#.#.........#...........\n...#......#.....#...##.........\n..........#.#.....###.#........\n.............#.....##..........\n.........#...####........#.####\n...................#....#......\n.....#.........#.#....#..#...#.\n.##...#.......##.#...#.#.#..#..\n.....##........#....#...#.##.#.\n#...#...#.#....#..............#\n#..#.##.............#..........\n..#...#..#.#.##..............##\n#......#.#...##..........#.##..\n.##.#...#...#.........#.#......\n......#........##.#..#.........\n#..#.......#......#.#..#.#.....\n.#..#...........#.#.##.....#...\n.....................#..#.#....\n........#...##......#.....##...\n#.............#...##....##....#\n#.#...........#....##.#......##\n.....#.....#.#..........###..#.\n....#...#....##....#..##.......\n.#....#....#.......#.#.....#...\n.#...#.......##...##........#..\n......##.......#.##.#.###......\n....##.......#......#..........\n...................#..##.......\n......................#...##...\n...##....#.#..#..#.............\n.#......##..........#...#......\n....##..#....#..#...#...####.#.\n...#.......#.......#........#.#\n#.........#..#...#...##...#.#.#\n....#...#.......#...#....#.....\n...#.....#.##..##.#.......##.##\n.......#....#........#.........\n.....#...#....#..#....#....#...\n.##....#...#........#...#.#...#\n.......##............#..#...#..\n#.#...#....#......#.#..........\n.#.##...........#........#.....\n.#....#.............#.#.##.....\n#.......###..#...###.........#.\n#..#.#.......#.........#...#..#\n..........#......#........#...#\n.#.#...#.##.......##...........\n.....#.........#.....#.........\n.........#.........#....##.#..#\n.#.......##..##..#.....#...#...\n.#.....##...#..#..............#\n..##...#..#..#.#...#..........#\n.#.......####......#......####.\n##..##........#.....#........#.\n..##.#..#.#....................\n...........#..#...##....##.....\n..#.#........#.........#....##.\n..#...#..##..###.#..###........\n......#..#.............#..##...\n.##.........#.#..#...#.##.###..\n.#...............#...........#.\n.#....#........#....#........##\n..#####.#.#..#.#........##...#.\n###....#....#...#..............\n.....#...##............#...#...\n##...........##.#.##.....#.....\n..............#..#.....#...#...\n...................#...........\n#..........##.........#........\n...#.........#..#.....#..#..#..\n....###.#......#......##....#..\n#......#..........#...#........\n...#.#...#..#..........##......\n.....##.....#.#............##..\n..#..#.###....#.#.#...##....#..\n...#........#....##.......#....\n.#.............#..##.......#...\n..#.#..###..#.....#...##.......\n.........#......##...#.#..#....\n.............#....##....#.#....\n#..#...#....#.#...#......##....\n.............#.#......#.....###\n#.##....#........#.............\n.....#...#.####...#.....#......\n....#....###....##.......#.....\n..#....##..#....#.#.......#....\n...#.....#....#.........#......\n.#......#.#....#.#........#....\n.......#......#.....#.#..#.....\n#......#.........##.##.#...#...\n..#.###...................#....\n....#..#....##.#........#....#.\n...........#..........#......#.\n.#..#.#...###..........#..#...#\n...#...##..#....#...#..........\n.#........#.................##.\n....#.......##....#...#........\n#.#...##.##...#.#.......#...#..\n.....#.#.##.#......#..#..##....\n.....##...#.#.....#...#........\n#.#.......#..#..........##.....\n................#......#..#.#.#\n#......#...#...................\n...#.....##.#.........#.#..#..#\n...#..##..##.......#....#......\n....##...#....#..#...........#.\n..#..#......#...#..#...........\n...#.##....#...##.......#......\n.......#....#..#..##..#..#....#\n.#.................#.#...#.##..\n.....#..................#..#.#.\n...#......##...#...........#...\n..#.........#....#..#...#.....#\n..#...#.....#.........##.#.....\n.....#.#....##...............#.\n....#...#............#.........\n.....#.....###............#....\n..#.#.#.......#....#...........\n...........##...##...#.......#.\n.........###.#......#..........\n.#.......#....#.....#.##..#...#\n..#..................#..###....\n..#....#...#......##.........#.\n........#..#........#.........#\n.#..#......#.........#.........\n...#..##.....#....#....#.....#.\n......#.#............###.....##\n.......#........#.......#.#....\n..#.............#..............\n.............##..#.#.#....#....\n.................#....#.#......\n##..#.#.......#....#.....#.....\n.##............##.#.......#.#..\n#..#...........##......#.......\n.##......#####..##.#....#.#....\n.......##.....#...#........#...\n.#.#.....##....#..#....#..#...#\n............##.#.....##.#......\n........##...###.#......#......\n......#..#.#...#..#............\n.........#...........#......#..\n.#.........#............##.....\n.#..#..#...#.#.............#...\n......#.#..##...#.#...........#\n#.##.......#...#.........#.....\n.....#..#............#....##...\n.#......#........#.............\n..#...#....#..#.......###......\n....#.......###.#.#...........#\n.............#...##............\n.##.#.#.#...........#...#....#.\n............##.........#......#\n...............#......#...#....\n...#.....#..###..#...........#.\n.#........#.....##........#.#..\n....#.#.......#..#..#...##.#.#.\n.......##...........#...#......\n....#.#..##......#.......#.....\n..#........#.#......#.#........\n........#....#..#....#..##.....\n.#.........##..........#.#.....\n..##...##.....##......##..#....\n.###.....##...........##.#...##\n...#................#.......#..\n#.......#.#.#..#.#.##..#...#...\n.#.#.......#..#................\n..#.#.#......#............#....\n#.....#.###..#.#...#...........\n#...........#..........#.#.#.##\n..#.#...#......##.....#........\n........#.......#.#...#...#....\n..#..........#......###......#.\n..........##.#....#.....#.##...\n..#.....#......#.........#..##.\n.#...#........#..#.#..#...##..#\n..###........#......#.#........\n..#.##.#....#.#....#.#...#.....";
			int result = DayThree.Part1(input);
			Console.WriteLine(result);
			Console.WriteLine(DayThree.Part2(input));
			Console.ReadKey();
		}
	}
}
