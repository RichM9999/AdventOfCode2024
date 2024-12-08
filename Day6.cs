﻿//https://adventofcode.com/2024/day/6
namespace AdventOfCode
{
    class Day6
    {
        const int mapSize = 130;

        public void Run()
        {

            var start = DateTime.Now;

            var map = new char[mapSize, mapSize];

            SetupMap(ref map);

            var startX = 0;
            var startY = 0;
            var currentX = 0;
            var currentY = 0;
            var direction = 'U';

            // Find starting point
            for (var y = 0; y < mapSize; y++)
            {
                for (var x = 0; x < mapSize; x++)
                {
                    if (map[y, x] == '^')
                    {
                        startX = x;
                        startY = y;
                        break;
                    }
                }
            }

            currentX = startX;
            currentY = startY;
            direction = 'U';

            while (true)
            {
                // Mark current spot
                map[currentY, currentX] = 'X';

                // Check direction and move to next spot or turn
                // Exit if move would go off the grid
                if (direction == 'U')
                {
                    if (currentY == 0)
                    {
                        break;
                    }

                    if (map[currentY - 1, currentX] == '#')
                    {
                        direction = 'R';
                        continue;
                    }

                    currentY--;
                }

                if (direction == 'R')
                {
                    if (currentX == mapSize - 1)
                    {
                        break;
                    }

                    if (map[currentY, currentX + 1] == '#')
                    {
                        direction = 'D';
                        continue;
                    }

                    currentX++;
                }

                if (direction == 'D')
                {
                    if (currentY == mapSize - 1)
                    {
                        break;
                    }

                    if (map[currentY + 1, currentX] == '#')
                    {
                        direction = 'L';
                        continue;
                    }

                    currentY++;
                }

                if (direction == 'L')
                {
                    if (currentX == 0)
                    {
                        break;
                    }

                    if (map[currentY, currentX - 1] == '#')
                    {
                        direction = 'U';
                        continue;
                    }

                    currentX--;

                }
            }

            // Count marked steps
            var steps = 0;

            for (var y = 0; y < mapSize; y++)
            {
                for (var x = 0; x < mapSize; x++)
                {
                    if (map[y, x] == 'X')
                        steps++;
                }
            }

            Console.WriteLine($"Steps: {steps}");
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            start = DateTime.Now;

            var filledMap = (char[,])map.Clone();

            var obstacles = 0;

            for (var obstacleY = 0; obstacleY < mapSize; obstacleY++)
            {
                for (var obstacleX = 0; obstacleX < mapSize; obstacleX++)
                {
                    // Use grid with marked steps
                    map = (char[,])filledMap.Clone();

                    // Only place an obstacle on walking path, but not starting point
                    if (map[obstacleY, obstacleX] != 'X' || (obstacleX == startX && obstacleY == startY))
                    {
                        continue;
                    }

                    var looped = false;

                    map[obstacleY, obstacleX] = '#';

                    currentX = startX;
                    currentY = startY;
                    direction = 'U';

                    var locations = new Dictionary<string, int>();

                    while (true)
                    {
                        var location = $"{currentX}:{currentY}:{direction}";
                        if (locations.ContainsKey(location))
                        {
                            // if this spot has been walked before in same direction, we've looped
                            looped = true;
                            break;
                        }
                        else
                        {
                            locations.Add(location, 1);
                        }

                        if (direction == 'U')
                        {
                            if (currentY == 0)
                            {
                                break;
                            }

                            if (map[currentY - 1, currentX] == '#')
                            {
                                direction = 'R';
                                continue;
                            }

                            currentY--;
                        }

                        if (direction == 'R')
                        {
                            if (currentX == mapSize - 1)
                            {
                                break;
                            }

                            if (map[currentY, currentX + 1] == '#')
                            {
                                direction = 'D';
                                continue;
                            }

                            currentX++;
                        }

                        if (direction == 'D')
                        {
                            if (currentY == mapSize - 1)
                            {
                                break;
                            }

                            if (map[currentY + 1, currentX] == '#')
                            {
                                direction = 'L';
                                continue;
                            }

                            currentY++;
                        }

                        if (direction == 'L')
                        {
                            if (currentX == 0)
                            {
                                break;
                            }

                            if (map[currentY, currentX - 1] == '#')
                            {
                                direction = 'U';
                                continue;
                            }

                            currentX--;
                        }
                    }

                    if (looped)
                    {
                        obstacles++;
                    }
                }
            }

            Console.WriteLine($"Obstacles: {obstacles}");
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        void AddMapRow(int row, ref char[,] map, string data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                var space = data[i];
                map[row, i] = space;
            }
        }

        void SetupMap(ref char[,] map)
        {
            AddMapRow(0, ref map, "..........##.....................................#............#........................#.....#........#...........#...............");
            AddMapRow(1, ref map, "...........#.............#.....#....................#...........#.#..............#.#......................................#.......");
            AddMapRow(2, ref map, ".#..............#...............................#.......#.#...................#...............#...............#..#...........#....");
            AddMapRow(3, ref map, ".#............................#.............................................................................#.#...................");
            AddMapRow(4, ref map, "...............#..........#............#...........................................#....#...#..........................#..........");
            AddMapRow(5, ref map, "..##.#..#......#.....................#..#......#................#..........................................#................#.....");
            AddMapRow(6, ref map, ".......#.....#...........................................#........#...............................#.#.............................");
            AddMapRow(7, ref map, ".........#...............#......#............#...................#...........#.........#..#...#...................................");
            AddMapRow(8, ref map, "#.....#.#....................................#.......................................................##..#...##..#..#.............");
            AddMapRow(9, ref map, ".............................#...#.......#...............................#.#...............#......................................");
            AddMapRow(10, ref map, ".....#................................#..........................#....................#...........................................");
            AddMapRow(11, ref map, ".......#............#..#................................................................................#..........#...#..........");
            AddMapRow(12, ref map, "...........#.............................................................#...............#................................#.....#.");
            AddMapRow(13, ref map, ".....................................................#.........#.....#..#.....#..................#.......#.....#...........#......");
            AddMapRow(14, ref map, ".#..........#................#..................#........#..............................................#....#..................#.");
            AddMapRow(15, ref map, ".....................#......#..........#...........#................#..#..#............................#...##.........#...........");
            AddMapRow(16, ref map, ".................#.....................................................................................#..........................");
            AddMapRow(17, ref map, "..........................#.....................................#.#..................................................#.#..........");
            AddMapRow(18, ref map, "............#....#........#...................................#...................................................#...............");
            AddMapRow(19, ref map, "............#........................#........##..............#....#.............#............#......#........#...................");
            AddMapRow(20, ref map, "....................#.#........#..........................#...........#.............#...............#.............##.......#......");
            AddMapRow(21, ref map, "...............................................#..................................#...................................#....##.....");
            AddMapRow(22, ref map, "...#.....#.......................................#..........#......................#.............#.............#.##.......#.......");
            AddMapRow(23, ref map, "........#......##...................................................................................#.............................");
            AddMapRow(24, ref map, "...#.................................#.....................................#......................................#.........#.....");
            AddMapRow(25, ref map, "....#.................#......#.........#.........................................................................#........#....#..");
            AddMapRow(26, ref map, "..........................#..#.......#............................##.............................................#.........##..#..");
            AddMapRow(27, ref map, "......................#......................................#......................#................#.........#..#...............");
            AddMapRow(28, ref map, "....#.....................................#...................#..............#..........................#...........#...#.........");
            AddMapRow(29, ref map, "...............#........#...............#..........#.....#..............................#....................................#.#..");
            AddMapRow(30, ref map, "......#......................#.......#.#...................#.............#...#.........#.....#....................................");
            AddMapRow(31, ref map, ".....#..#...............##....................................................#..#...#........................#..................#");
            AddMapRow(32, ref map, "......#.#..........#...#.................#...............#..........................................#...#.#.......................");
            AddMapRow(33, ref map, "..........#.......................................................................................................##..............");
            AddMapRow(34, ref map, "......................#..#...#......#.....................................................#.........#.........#...................");
            AddMapRow(35, ref map, "...................................................#...#.........#.....#......................#...............................#...");
            AddMapRow(36, ref map, "........................#...................................^..............................#.............#..........#...#.........");
            AddMapRow(37, ref map, "........#............................#.....................#......#...........##...........#......................................");
            AddMapRow(38, ref map, "..........#..................#........#...........................................................................................");
            AddMapRow(39, ref map, ".....#......................#.....#.............#....................#...............#...........##.......#..................##...");
            AddMapRow(40, ref map, "........#......................................#................................................#.....................#...........");
            AddMapRow(41, ref map, "......#.........#..............#.................................................................#.......##..#.#..................");
            AddMapRow(42, ref map, "........#......................................#...................#......................#.......................................");
            AddMapRow(43, ref map, "..#...............#....##.....#........................#.#.........#........#....#...................#.............#...#.........#");
            AddMapRow(44, ref map, ".............#..............#....#....#................#.....................................#...............#..................#.");
            AddMapRow(45, ref map, "..............#...........................................................................#..##..#...#.......#..........#.........");
            AddMapRow(46, ref map, "#...........#........................#..#......#..#.........#........#...#..............#............#.....#.................#...#");
            AddMapRow(47, ref map, ".....................#........................................#.................#.#..................#.............#.........#....");
            AddMapRow(48, ref map, ".............#.....................................................#.#....................................#.......................");
            AddMapRow(49, ref map, ".............#....#.............#.........................................................................#...........#...........");
            AddMapRow(50, ref map, "#.................#...#...#...............#..........#.........................##......#.....#..#.............................#...");
            AddMapRow(51, ref map, "#................#........#.......#.....#...............#..................................#.#.............................#......");
            AddMapRow(52, ref map, ".........#.......#.............#...................................................................................#..........#...");
            AddMapRow(53, ref map, "......#..............................................................................##............................#..........#...");
            AddMapRow(54, ref map, "..............#..................................................#...................................#..........................#.");
            AddMapRow(55, ref map, ".................................................................#.#...........#................#..........#..........#.......#.#.");
            AddMapRow(56, ref map, "...................................#.....................#..#...........................................................#.........");
            AddMapRow(57, ref map, "......................................................................................#.............#.............................");
            AddMapRow(58, ref map, ".....#......................................................................#......#....#.........#....#..#.......................");
            AddMapRow(59, ref map, "..................#.............................................................#.........................#.......................");
            AddMapRow(60, ref map, "......................#......#..........#..#............................#....#.........#.........#................................");
            AddMapRow(61, ref map, "..#...........#.......................#....#....................................#.......#.................................#.......");
            AddMapRow(62, ref map, "...................#.......................................................................................#..............#.......");
            AddMapRow(63, ref map, ".....#....................................#......#............#...##........#...............#......................#...........#..");
            AddMapRow(64, ref map, "......................................#.......................#.........#.#.........................................#.............");
            AddMapRow(65, ref map, "...#..............................................#................................................##...........................#.");
            AddMapRow(66, ref map, ".......................#.................................................................#.......................................#");
            AddMapRow(67, ref map, ".#...........#..#...................#.#......#................#......................................................#............");
            AddMapRow(68, ref map, ".......................................................................##..........#......#................#.......#.......#......");
            AddMapRow(69, ref map, "#..#...#...........................................#......#...............#.......................................................");
            AddMapRow(70, ref map, "....#.....#...........................#...........#...............##..............................................................");
            AddMapRow(71, ref map, "...#.......#.......................................................................................#............#.................");
            AddMapRow(72, ref map, ".............................................................................#...#.....................#...#......................");
            AddMapRow(73, ref map, "...................#.................................#.......#...........................#.....#.................................#");
            AddMapRow(74, ref map, ".........................................#........#....#.............#.............#...........................#..................");
            AddMapRow(75, ref map, "....#.....................................................................................#.#.#.......................#.#.........");
            AddMapRow(76, ref map, "..............................................................#..........#............................................#...........");
            AddMapRow(77, ref map, ".........#.#.............................#...#..........................#....................#....................................");
            AddMapRow(78, ref map, "......................................#.#......#..................................................................................");
            AddMapRow(79, ref map, ".................................................................................................................#................");
            AddMapRow(80, ref map, "..............#...................................................................................................................");
            AddMapRow(81, ref map, "...................................................#....................................................................##........");
            AddMapRow(82, ref map, "................#........................................#..............................................#...............#.........");
            AddMapRow(83, ref map, ".........#....#...#......#....................#.#.......................................#.............#.....#..............#....#.");
            AddMapRow(84, ref map, ".............................................................................................................##.....#..#..........");
            AddMapRow(85, ref map, "....#.............................#......#.....................................................#......................#.......#...");
            AddMapRow(86, ref map, "..#.................#........................#....#...##...................................#...............#......................");
            AddMapRow(87, ref map, ".........#..................................#.................#...#..#..........##................................................");
            AddMapRow(88, ref map, "......##.......................................................#...........#......................................................");
            AddMapRow(89, ref map, "......#..............................#...................##....#..................................................................");
            AddMapRow(90, ref map, "..................#.........#.................................................#.......#..................#........................");
            AddMapRow(91, ref map, ".........................#...........................................#.......#...........................................#........");
            AddMapRow(92, ref map, "............#.#...................#.#....#.................................#......##...........................................#..");
            AddMapRow(93, ref map, "..#....................#.....................#.........................#..............#.....#.........#...........................");
            AddMapRow(94, ref map, "..#............................................#.........................#....................#..................................#");
            AddMapRow(95, ref map, "...........................#......#...............................................................#......#..........#..........#..");
            AddMapRow(96, ref map, ".......................#.....#..................#...................................#.............#....#.....................#....");
            AddMapRow(97, ref map, "....................................................................#.....#....#..................................................");
            AddMapRow(98, ref map, "..#................#...#..........................................................................................................");
            AddMapRow(99, ref map, ".........................##.....#.................#.............#....................#...............#..............#......#.#....");
            AddMapRow(100, ref map, ".......................#...................##......................................#....#...............#........#.........#......");
            AddMapRow(101, ref map, "......#..#.........#............................................................#................................#......#.........");
            AddMapRow(102, ref map, "#........................#..................#...............................................#.#........#..........................");
            AddMapRow(103, ref map, "..........#................#...#.....................................................#............#...............................");
            AddMapRow(104, ref map, "............#................................................#.#...............#............#................................#....");
            AddMapRow(105, ref map, "..#............................................#....#....#.#.#................................#....................#..............");
            AddMapRow(106, ref map, ".........#...........................##.............#..#.................#........................#............................#..");
            AddMapRow(107, ref map, "..........................##......................#..............................#..................#.............................");
            AddMapRow(108, ref map, ".........#........#.......#..........#...........#.............................#...........................#.......#..............");
            AddMapRow(109, ref map, "..........................#................#......................................................................................");
            AddMapRow(110, ref map, "..........................#...............#..................#....................................#.........#....#..........#.....");
            AddMapRow(111, ref map, "..................#..#..................................................................#........#................................");
            AddMapRow(112, ref map, "##..#.......#..............#..#..............##.............#...#..#.........##.......................................#......#....");
            AddMapRow(113, ref map, ".............#.........#........#....................................................#................#.....#.....................");
            AddMapRow(114, ref map, "......#...................................#.....#................#..........................#.....................................");
            AddMapRow(115, ref map, "..#.......................#...#.#...#...........#..........#...................................#.............#.....#..............");
            AddMapRow(116, ref map, "..........#...#......................##...........................................................................................");
            AddMapRow(117, ref map, "....#............................#.#...................#....................#................#...........#..................#.....");
            AddMapRow(118, ref map, ".......#......#................#......................#......#........##..#....................#..................................");
            AddMapRow(119, ref map, ".......#..................#....#.....................................................................................#............");
            AddMapRow(120, ref map, "..................#......#................................#.........................#....#.............................#...#......");
            AddMapRow(121, ref map, ".................#.............#..........#....#.........................#..................................#.....##...........##.");
            AddMapRow(122, ref map, "............#..#............................................................................##...#.#....#....#.......##.#.......#.");
            AddMapRow(123, ref map, "......................#............................#.........#.....#.#.......##........................#..........................");
            AddMapRow(124, ref map, "...........#........................#..##........#....#.......#....#.#.....##.....................#.......#......#...............#");
            AddMapRow(125, ref map, ".................#...........#.......#..........#................................................#......................#.........");
            AddMapRow(126, ref map, "................#.......#.#.....................................................................................#.................");
            AddMapRow(127, ref map, "........................................#...#......#.......................................#...........#..........................");
            AddMapRow(128, ref map, "....#........##...............#...........#..................................................................#....................");
            AddMapRow(129, ref map, "#....................#...................#.........#....................................................................#.........");
        }
    }
}
