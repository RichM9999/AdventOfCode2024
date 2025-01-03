﻿//https://adventofcode.com/2024/day/20
namespace AdventOfCode.Year2024
{
    using Coordinate = (int x, int y);

    class Day20
    {
        Coordinate[] orthogonalNeighbors;
        int mapSizeY = 141;
        int mapSizeX = 141;

        Dictionary<Coordinate, char> map;

        public Day20()
        {
            map = [];
            orthogonalNeighbors = [new Coordinate(0, 1), new Coordinate(1, 0), new Coordinate(0, -1), new Coordinate(-1, 0)];
        }


        public void Run()
        {
            var start = DateTime.Now;

            map = [];
            SetupMap();

            start = DateTime.Now;

            var route = FindRoute();
            Console.WriteLine($"Path length: {route.Count}");
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            start = DateTime.Now;

            // Part 1
            int minimumSavings = 100;
            int cheatLength = 2;

            Console.WriteLine($"Part 1: {NumberOfCheats(route, minimumSavings, cheatLength)} cheats of max length 2 save at least 100 picoseconds");
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            start = DateTime.Now;

            // Part 2
            cheatLength = 20;

            Console.WriteLine($"Part 1: {NumberOfCheats(route, minimumSavings, cheatLength)} cheats of max length 20 save at least 100 picoseconds");
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        Dictionary<Coordinate, int> FindRoute()
        {
            // Find start and end
            var start = map.First(m => m.Value == 'S').Key;
            var end = map.First(m => m.Value == 'E').Key;

            var currentPosition = start;
            map[end] = '.';
            int steps = 0;
            
            Dictionary<Coordinate, int> route = new() { { currentPosition, 0 } };

            do
            {
                steps++;
                foreach (Coordinate neighbor in orthogonalNeighbors)
                {
                    Coordinate newPosition = (currentPosition.x + neighbor.x, currentPosition.y + neighbor.y);
                    if (map[newPosition] == '.' && !route.ContainsKey(newPosition))
                    {
                        currentPosition = newPosition;
                        route[currentPosition] = steps;
                        break;
                    }
                }
            } while (currentPosition != end);

            return route;
        }

        long NumberOfCheats(Dictionary<Coordinate, int> route, int minimumSavings, int cheatLength)
        {
            long numberOfCheats = 0;

            // Used to multiply by cheat difference X and Y (0..cheat Length) to get amount 
            // to add to cheat start position to get cheat end position
            // Left+Down, Right+Down, Left+Up, Right+Up
            // When x or y are zero, also becomes just Left, Right, Up and Down
            //  Eight possible offsets when diff X and diff Y are both Positive or either diff X or diff Y are zero:
            //  (-1,-1)( 0,-1)( 1,-1)
            //  (-1, 0) START ( 1, 0)
            //  (-1, 1)( 0, 1)( 1, 1)
            List<(int, int)> cheatOffsets = [(-1, 1), (1, 1), (-1, -1), (1, -1)];

            foreach ((int x, int y) in route.Keys)
            {
                HashSet<(int x, int y)> cheatsUsed = [];

                // For any given cheat start S, check all possible cheat end locations E up to cheat Length away
                //
                // For cheat Length 2:
                //
                //               diffs   offset     diffs   offset     diffs   offset    diffs   offset    diffs   offset     diffs   offset     diffs   offset    diffs   offset
                //     E         (0,2) * (-1, -1) > (0,2) * (1, -1)
                //    EEE        (1,1) * (-1, -1),  (0,1) * (-1, -1),  (1,1) * (1, -1) > (0,1) * (1, -1)
                //   EESEE       (2,0) * (-1, -1),  (1,0) * (-1, -1),  (1,0) * (1,  1),  (2,0) * (1,  1) > (2,0) * (-1,  1),  (1,0) * (-1,  1),  (1,0) * (1, -1),  (2,0) * (1, -1)
                //    EEE        (1,1) * (-1,  1),  (0,1) * ( 1,  1),  (1,1) * (1,  1) > (0,1) * (-1, 1)
                //     E         (0,2) * ( 1,  1) > (0,2) * (-1, 1)
                //
                // NOTE: When a 0 diff X or 0 diff Y is involved, multiple offsets give the same end location
                //       These are denoted after the > in the chart above
                // 
                // Valid E points are X and Y away from S where X + Y <= Cheat Length


                // Check -length to +length in X and Y directions where diff X + diff Y <= max cheat length
                for (int cheatDiffX = 0; cheatDiffX <= cheatLength; cheatDiffX++)
                {
                    for (int cheatDiffY = 0; cheatDiffY <= cheatLength; cheatDiffY++)
                    {
                        // Combination of X and Y movement is not greater than allowed cheat length
                        if (cheatDiffX + cheatDiffY > cheatLength)
                        { 
                            continue; 
                        }

                        // Check diff in all offset directions
                        foreach ((int offsetFactorX, int offsetFactorY) in cheatOffsets)
                        {
                            // Calculate new position based on starting value plus diff spaces in direction offset direction
                            Coordinate cheatEnd = (x + (cheatDiffX * offsetFactorX), y + (cheatDiffY * offsetFactorY));

                            // Ignore cheat end positions off the map
                            // 1 to mapSize - 2 since map has a one unit border
                            if (cheatEnd.x < 1 || cheatEnd.y < 1
                                || cheatEnd.x > mapSizeX - 2 || cheatEnd.y > mapSizeY - 2)
                            {
                                continue;
                            }

                            // Get standard distance to the potential cheat end spot
                            if (route.TryGetValue(cheatEnd, out int standardDistanceToNewPosition))
                            {
                                // Cheat savings is standard distance to the cheat end
                                // minus the standard distance to the cheat start
                                // minus the distance X traveled during cheat
                                // minus the distance Y traveled during cheat
                                int cheatSavings = standardDistanceToNewPosition - route[(x, y)] - cheatDiffX - cheatDiffY;
                                
                                // If cheat saves at least minimum amount
                                // and haven't used a cheat that lands in the same spot yet
                                // then we found a new cheat
                                if (cheatSavings >= minimumSavings && !cheatsUsed.Contains(cheatEnd))
                                {
                                    numberOfCheats++;
                                }
                                cheatsUsed.Add(cheatEnd);
                            }
                        }
                    }
                }
            }
            return numberOfCheats;
        }

        void AddMapRow(int row, string data)
        {
            for (var x = 0; x < data.Length; x++)
            {
                var space = data[x];
                map.Add((x, row), space);
            }
        }

        void SetupMap()
        {
            //AddMapRow(00, "###############");
            //AddMapRow(01, "#...#...#.....#");
            //AddMapRow(02, "#.#.#.#.#.###.#");
            //AddMapRow(03, "#S#...#.#.#...#");
            //AddMapRow(04, "#######.#.#.###");
            //AddMapRow(05, "#######.#.#...#");
            //AddMapRow(06, "#######.#.###.#");
            //AddMapRow(07, "###..E#...#...#");
            //AddMapRow(08, "###.#######.###");
            //AddMapRow(09, "#...###...#...#");
            //AddMapRow(10, "#.#####.#.###.#");
            //AddMapRow(11, "#.#...#.#.#...#");
            //AddMapRow(12, "#.#.#.#.#.#.###");
            //AddMapRow(13, "#...#...#...###");
            //AddMapRow(14, "###############");
            //return;

            AddMapRow(000, "#############################################################################################################################################");
            AddMapRow(001, "#...###...#.......#.....#...###...#...........#...#...#.......#...###...#...#.............###...#...#...........#...###...###.....#...#...###");
            AddMapRow(002, "#.#.###.#.#.#####.#.###.#.#.###.#.#.#########.#.#.#.#.#.#####.#.#.###.#.#.#.#.###########.###.#.#.#.#.#########.#.#.###.#.###.###.#.#.#.#.###");
            AddMapRow(003, "#.#.#...#.#.....#.#...#.#.#.#...#.#.....#.....#.#.#.#...#.....#.#.....#.#.#.#.....#.......#...#...#.#...#.......#.#.#...#...#.#...#.#.#.#...#");
            AddMapRow(004, "#.#.#.###.#####.#.###.#.#.#.#.###.#####.#.#####.#.#.#####.#####.#######.#.#.#####.#.#######.#######.###.#.#######.#.#.#####.#.#.###.#.#.###.#");
            AddMapRow(005, "#.#.#...#.#...#.#...#.#.#.#.#...#.#.....#.....#.#...#.....#...#...#.....#.#.#...#.#.......#.......#.#...#...#...#.#.#.....#...#...#.#.#.#...#");
            AddMapRow(006, "#.#.###.#.#.#.#.###.#.#.#.#.###.#.#.#########.#.#####.#####.#.###.#.#####.#.#.#.#.#######.#######.#.#.#####.#.#.#.#.#####.#######.#.#.#.#.###");
            AddMapRow(007, "#.#.#...#...#.#...#...#.#.#.....#.#.....#.....#.....#.#...#.#.#...#.....#.#.#.#.#.#.......###...#.#.#...#...#.#.#.#.#...#...#.....#.#.#.#...#");
            AddMapRow(008, "#.#.#.#######.###.#####.#.#######.#####.#.#########.#.#.#.#.#.#.#######.#.#.#.#.#.#.#########.#.#.#.###.#.###.#.#.#.#.#.###.#.#####.#.#.###.#");
            AddMapRow(009, "#.#.#.......#.#...#.....#.......#.###...#.#...#.....#.#.#.#.#.#...#.....#.#.#.#.#.#...#...#...#...#.###.#.....#.#.#...#.#...#.#...#.#...#...#");
            AddMapRow(010, "#.#.#######.#.#.###.###########.#.###.###.#.#.#.#####.#.#.#.#.###.#.#####.#.#.#.#.###.#.#.#.#######.###.#######.#.#####.#.###.#.#.#.#####.###");
            AddMapRow(011, "#.#...#.....#.#...#.....#...#...#...#...#.#.#.#...#...#.#.#.#.#...#.#...#.#...#.#.#...#.#.#...#.....#...#.......#...#...#...#.#.#...#...#...#");
            AddMapRow(012, "#.###.#.#####.###.#####.#.#.#.#####.###.#.#.#.###.#.###.#.#.#.#.###.#.#.#.#####.#.#.###.#.###.#.#####.###.#########.#.#####.#.#.#####.#.###.#");
            AddMapRow(013, "#...#.#.....#.#...#.....#.#.#.....#...#.#...#.#...#...#.#.#.#.#.#...#.#.#.#...#...#...#.#.#...#.....#...#.......###.#.#...#.#.#.#.....#.....#");
            AddMapRow(014, "###.#.#####.#.#.###.#####.#.#####.###.#.#####.#.#####.#.#.#.#.#.#.###.#.#.#.#.#######.#.#.#.#######.###.#######.###.#.#.#.#.#.#.#.###########");
            AddMapRow(015, "#...#.......#...#...#...#.#...###...#.#.#...#...#.....#.#...#...#...#.#.#...#.#.......#.#.#.....#...#...#...#...#...#.#.#.#.#.#.#.###.......#");
            AddMapRow(016, "#.###############.###.#.#.###.#####.#.#.#.#.#####.#####.###########.#.#.#####.#.#######.#.#####.#.###.###.#.#.###.###.#.#.#.#.#.#.###.#####.#");
            AddMapRow(017, "#...#...#.......#...#.#.#...#.#.....#.#...#...#...#...#...#.........#.#...###.#...###...#...#...#...#.#...#...###...#.#.#.#.#...#...#.#.....#");
            AddMapRow(018, "###.#.#.#.#####.###.#.#.###.#.#.#####.#######.#.###.#.###.#.#########.###.###.###.###.#####.#.#####.#.#.###########.#.#.#.#.#######.#.#.#####");
            AddMapRow(019, "#...#.#.#.#.....#...#.#...#.#.#.....#.#...###.#.....#.....#...#...###...#.#...#...#...#.....#...#...#.#.......#.....#.#.#...#.....#...#.....#");
            AddMapRow(020, "#.###.#.#.#.#####.###.###.#.#.#####.#.#.#.###.###############.#.#.#####.#.#.###.###.###.#######.#.###.#######.#.#####.#.#####.###.#########.#");
            AddMapRow(021, "#...#.#...#.....#...#...#.#.#.#...#.#.#.#.#...#.......#.....#...#...#...#.#...#...#...#.#...#...#.....#.......#.....#...#...#...#.#.........#");
            AddMapRow(022, "###.#.#########.###.###.#.#.#.#.#.#.#.#.#.#.###.#####.#.###.#######.#.###.###.###.###.#.#.#.#.#########.###########.#####.#.###.#.#.#########");
            AddMapRow(023, "###...###...###...#.....#...#.#.#...#.#.#.#.#...#...#...#...#.......#...#...#.#...#...#.#.#...#.........#...#...###.....#.#.....#.#.........#");
            AddMapRow(024, "#########.#.#####.###########.#.#####.#.#.#.#.###.#.#####.###.#########.###.#.#.###.###.#.#####.#########.#.#.#.#######.#.#######.#########.#");
            AddMapRow(025, "###...#...#.......#.....#...#...#...#...#...#.###.#.....#.....#...#.....#...#.#.#...#...#.....#.#...###...#.#.#.#...#...#.......#...........#");
            AddMapRow(026, "###.#.#.###########.###.#.#.#####.#.#########.###.#####.#######.#.#.#####.###.#.#.###.#######.#.#.#.###.###.#.#.#.#.#.#########.#############");
            AddMapRow(027, "#...#.#.........#...###...#...#...#.....#...#...#.#...#...#...#.#.#.....#.#...#.#.#...###.....#.#.#...#...#.#.#...#.#...#.......###...#.....#");
            AddMapRow(028, "#.###.#########.#.###########.#.#######.#.#.###.#.#.#.###.#.#.#.#.#####.#.#.###.#.#.#####.#####.#.###.###.#.#.#####.###.#.#########.#.#.###.#");
            AddMapRow(029, "#...#.#...#...#...#...#.....#...#...###...#...#.#.#.#.....#.#.#.#.#...#.#.#...#.#.#.#...#.....#.#.#...#...#...#.....#...#.#...#...#.#.#.#...#");
            AddMapRow(030, "###.#.#.#.#.#.#####.#.#.###.#####.#.#########.#.#.#.#######.#.#.#.#.#.#.#.###.#.#.#.#.#.#####.#.#.#.###.#######.#####.###.#.#.#.#.#.#.#.#.###");
            AddMapRow(031, "###.#.#.#.#.#.#...#.#.#...#.......#.#...#...#.#.#.#...#...#.#.#.#.#.#.#.#...#.#.#.#.#.#.#...#.#.#.#...#.#.......#...#...#.#.#...#.#.#.#.#.###");
            AddMapRow(032, "###.#.#.#.#.#.#.#.#.#.###.#########.#.#.#.#.#.#.#.###.#.#.#.#.#.#.#.#.#.###.#.#.#.#.#.#.#.#.#.#.#.###.#.#.#######.#.###.#.#.#####.#.#.#.#.###");
            AddMapRow(033, "#...#...#...#...#.#.#.###...#.....#.#.#.#.#.#.#.#.#...#.#.#.#.#.#.#.#.#...#.#.#.#.#.#.#.#.#.#.#.#.#...#.#.###...#.#.###.#.#.#.....#.#...#...#");
            AddMapRow(034, "#.###############.#.#.#####.#.###.#.#.#.#.#.#.#.#.#.###.#.#.#.#.#.#.#.###.#.#.#.#.#.#.#.#.#.#.#.#.#.###.#.###.#.#.#.###.#.#.#.#####.#######.#");
            AddMapRow(035, "#...#.....#.....#...#.#...#...#...#...#.#.#...#.#.#.#...#.#.#...#.#.#...#.#.#.#...#.#.#...#.#.#.#.#.#...#...#.#.#.#.....#...#.....#.#.......#");
            AddMapRow(036, "###.#.###.#.###.#####.#.#.#####.#######.#.#####.#.#.#.###.#.#####.#.###.#.#.#.#####.#.#####.#.#.#.#.#.#####.#.#.#.###############.#.#.#######");
            AddMapRow(037, "###...###...###.....#...#.#...#.#...###.#.#.....#.#.#...#.#...#...#...#.#.#.#...#...#...#...#.#.#.#.#.#.....#.#...#...............#.#.......#");
            AddMapRow(038, "###################.#####.#.#.#.#.#.###.#.#.#####.#.###.#.###.#.#####.#.#.#.###.#.#####.#.###.#.#.#.#.#.#####.#####.###############.#######.#");
            AddMapRow(039, "#.......###...#...#.....#...#...#.#...#...#.....#.#...#.#.....#.#...#.#.#.#.#...#...#...#.#...#.#.#...#.......#...#.................#...#...#");
            AddMapRow(040, "#.#####.###.#.#.#.#####.#########.###.#########.#.###.#.#######.#.#.#.#.#.#.#.#####.#.###.#.###.#.#############.#.###################.#.#.###");
            AddMapRow(041, "#.....#.....#...#.......#...#.....#...#...#.....#.#...#.......#.#.#.#.#...#.#...#...#...#.#.###...###.......#...#.....#...#...#...#...#...###");
            AddMapRow(042, "#####.###################.#.#.#####.###.#.#.#####.#.#########.#.#.#.#.#####.###.#.#####.#.#.#########.#####.#.#######.#.#.#.#.#.#.#.#########");
            AddMapRow(043, "#...#.....................#...#...#.....#.#...#...#.#...#...#.#.#.#.#...#...#...#.#...#.#.#...#.......#...#.#.......#...#...#.#.#...###.....#");
            AddMapRow(044, "#.#.###########################.#.#######.###.#.###.#.#.#.#.#.#.#.#.###.#.###.###.#.#.#.#.###.#.#######.#.#.#######.#########.#.#######.###.#");
            AddMapRow(045, "#.#.#.........#...#.........###.#.#.......###...#...#.#...#...#.#.#.#...#...#...#.#.#.#.#...#.#.#.......#.#.#.....#.........#.#...#...#.#...#");
            AddMapRow(046, "#.#.#.#######.#.#.#.#######.###.#.#.#############.###.#########.#.#.#.#####.###.#.#.#.#.###.#.#.#.#######.#.#.###.#########.#.###.#.#.#.#.###");
            AddMapRow(047, "#.#.#.......#...#.#.......#.....#...###...#...###...#.......###.#.#.#...###...#.#.#.#.#...#.#.#...#.......#.#...#.#.....#...#...#...#...#...#");
            AddMapRow(048, "#.#.#######.#####.#######.#############.#.#.#.#####.#######.###.#.#.###.#####.#.#.#.#.###.#.#.#####.#######.###.#.#.###.#.#####.###########.#");
            AddMapRow(049, "#.#...#...#.....#.#...#...#...........#.#...#...###.#.....#...#.#.#.#...#.....#.#.#.#.#...#.#.#.....#.....#.....#.#...#.#.....#...#...#...#.#");
            AddMapRow(050, "#.###.#.#.#####.#.#.#.#.###.#########.#.#######.###.#.###.###.#.#.#.#.###.#####.#.#.#.#.###.#.#.#####.###.#######.###.#.#####.###.#.#.#.#.#.#");
            AddMapRow(051, "#.#...#.#.#...#.#.#.#.#.....###.......#.#.....#...#.#...#.#...#.#.#.#.###...#...#.#.#...###...#.....#.#...#...###...#.#...#...###...#.#.#.#.#");
            AddMapRow(052, "#.#.###.#.#.#.#.#.#.#.#########.#######.#.###.###.#.###.#.#.###.#.#.#.#####.#.###.#.###############.#.#.###.#.#####.#.###.#.#########.#.#.#.#");
            AddMapRow(053, "#.#.....#...#...#...#.#...#...#.#.......#...#...#.#.#...#.#.###...#.#...#...#.###.#.....###...#...#...#.....#...#...#...#...#...#...#...#...#");
            AddMapRow(054, "#.###################.#.#.#.#.#.#.#########.###.#.#.#.###.#.#######.###.#.###.###.#####.###.#.#.#.#############.#.#####.#####.#.#.#.#########");
            AddMapRow(055, "#.#.........#.......#.#.#.#.#.#...#...#...#.###.#.#...#...#.......#.....#.#...#...#.....#...#...#...............#.......#...#.#.#.#.#...#...#");
            AddMapRow(056, "#.#.#######.#.#####.#.#.#.#.#.#####.#.#.#.#.###.#.#####.#########.#######.#.###.###.#####.###############################.#.#.#.#.#.#.#.#.#.#");
            AddMapRow(057, "#.#.#.......#.#...#.#...#...#.......#...#.#...#.#...###.....#.....###.....#...#.....#...#.#.......#...........###...#...#.#.#.#.#.#.#.#.#.#.#");
            AddMapRow(058, "#.#.#.#######.#.#.#.#####################.###.#.###.#######.#.#######.#######.#######.#.#.#.#####.#.#########.###.#.#.#.#.#.#.#.#.#.#.#.#.#.#");
            AddMapRow(059, "#.#.#.......#.#.#...#...#.......#...#...#.....#.....#...###.#.......#.#...#...###...#.#.#...#...#...#...#.....#...#.#.#.#.#...#...#...#.#.#.#");
            AddMapRow(060, "#.#.#######.#.#.#####.#.#.#####.#.#.#.#.#############.#.###.#######.#.#.#.#.#####.#.#.#.#####.#.#####.#.#.#####.###.#.#.#.#############.#.#.#");
            AddMapRow(061, "#.#.....###...#.......#...#.....#.#...#.....#...#...#.#...#.#.......#...#...###...#...#...###.#.#.....#.#...###...#...#...#.....#.......#.#.#");
            AddMapRow(062, "#.#####.###################.#####.#########.#.#.#.#.#.###.#.#.#################.#########.###.#.#.#####.###.#####.#########.###.#.#######.#.#");
            AddMapRow(063, "#.#...#.#...................#.....#.........#.#...#.#.#...#.#.#.....#.....#...#.....#...#.....#.#.....#.#...#.....#...#...#...#.#.........#.#");
            AddMapRow(064, "#.#.#.#.#.###################.#####.#########.#####.#.#.###.#.#.###.#.###.#.#.#####.#.#.#######.#####.#.#.###.#####.#.#.#.###.#.###########.#");
            AddMapRow(065, "#.#.#...#.#...#.....#...#...#.#...#...........#...#...#...#.#...#...#...#...#...###...#.#.....#.......#...#...#####.#.#.#.....#...#...#.....#");
            AddMapRow(066, "#.#.#####.#.#.#.###.#.#.#.#.#.#.#.#############.#.#######.#.#####.#####.#######.#######.#.###.#############.#######.#.#.#########.#.#.#.#####");
            AddMapRow(067, "#.#...###...#...###...#.#.#...#.#.#...#.......#.#.#.......#.#.....#...#.......#.#.......#...#...#...#.......#######.#.#.........#...#.#.....#");
            AddMapRow(068, "#.###.#################.#.#####.#.#.#.#.#####.#.#.#.#######.#.#####.#.#######.#.#.#########.###.#.#.#.#############.#.#########.#####.#####.#");
            AddMapRow(069, "#.....#.......###.......#.#.....#...#.#.....#...#.#.......#.#.......#.#.......#.#.....#...#.#...#.#.#.#############S#.#...#...#.....#.#.....#");
            AddMapRow(070, "#######.#####.###.#######.#.#########.#####.#####.#######.#.#########.#.#######.#####.#.#.#.#.###.#.#.###############.#.#.#.#.#####.#.#.#####");
            AddMapRow(071, "#.....#.....#...#.......#.#.........#.#...#...###.#.......#...........#.......#.......#.#...#.....#...###############.#.#.#.#...#...#...#...#");
            AddMapRow(072, "#.###.#####.###.#######.#.#########.#.#.#.###.###.#.#########################.#########.#############################.#.#.#.###.#.#######.#.#");
            AddMapRow(073, "#...#.#...#...#...#...#...#.........#...#...#...#.#...#...###.....#...#...###...#...#...#############################...#.#...#.#.#.......#.#");
            AddMapRow(074, "###.#.#.#.###.###.#.#.#####.###############.###.#.###.#.#.###.###.#.#.#.#.#####.#.#.#.###################################.###.#.#.#.#######.#");
            AddMapRow(075, "#...#...#.#...#...#.#...###...............#.....#.....#.#.#...#...#.#.#.#.#...#.#.#...###################################.....#.#...#...#...#");
            AddMapRow(076, "#.#######.#.###.###.###.#################.#############.#.#.###.###.#.#.#.#.#.#.#.#############################################.#####.#.#.###");
            AddMapRow(077, "#.......#...#...###...#.#...#...........#.....#...###...#...#...###.#.#.#...#.#...#####################################...#...#.#...#.#.#.###");
            AddMapRow(078, "#######.#####.#######.#.#.#.#.#########.#####.#.#.###.#######.#####.#.#.#####.#########################################.#.#.#.#.#.#.#.#.#.###");
            AddMapRow(079, "#.......#...#.###...#.#.#.#.#.........#.#.....#.#.....#.......###...#.#.#...#...###############..E#####################.#...#.#...#...#.#...#");
            AddMapRow(080, "#.#######.#.#.###.#.#.#.#.#.#########.#.#.#####.#######.#########.###.#.#.#.###.###############.#######################.#####.#########.###.#");
            AddMapRow(081, "#.#.......#.#.....#...#.#.#.#...###...#...#.....#.......#...#...#...#.#...#...#.###############...#.......###...#...###.#.....#.....#...#...#");
            AddMapRow(082, "#.#.#######.###########.#.#.#.#.###.#######.#####.#######.#.#.#.###.#.#######.#.#################.#.#####.###.#.#.#.###.#.#####.###.#.###.###");
            AddMapRow(083, "#.#.#.......#...#.....#...#...#.....#...###.#.....###.....#...#.#...#...#...#.#...###########...#...#...#.....#...#.....#.....#.#...#.....###");
            AddMapRow(084, "#.#.#.#######.#.#.###.###############.#.###.#.#######.#########.#.#####.#.#.#.###.###########.#.#####.#.#####################.#.#.###########");
            AddMapRow(085, "#.#.#...#...#.#.#.#...#...#...........#.....#.......#.#.........#.....#.#.#.#...#.###########.#.#...#.#.......................#.#.#...#...###");
            AddMapRow(086, "#.#.###.#.#.#.#.#.#.###.#.#.#######################.#.#.#############.#.#.#.###.#.###########.#.#.#.#.#########################.#.#.#.#.#.###");
            AddMapRow(087, "#.#.#...#.#...#...#...#.#.#.#.........#...#...#...#...#.......#...#...#.#.#.#...#...#...#...#.#.#.#...#.........................#.#.#...#...#");
            AddMapRow(088, "#.#.#.###.###########.#.#.#.#.#######.#.#.#.#.#.#.###########.#.#.#.###.#.#.#.#####.#.#.#.#.#.#.#.#####.#########################.#.#######.#");
            AddMapRow(089, "#.#.#...#.###.......#...#...#...#...#...#.#.#...#.........#...#.#.#...#...#.#.#...#...#...#...#.#...#...#.........#...#...#.....#...#...#...#");
            AddMapRow(090, "#.#.###.#.###.#####.###########.#.#.#####.#.#############.#.###.#.###.#####.#.#.#.#############.###.#.###.#######.#.#.#.#.#.###.#####.#.#.###");
            AddMapRow(091, "#.#.###...#...#...#...........#.#.#.....#.#...#.....#...#...#...#.###.....#.#...#.............#...#.#.###.....###...#...#...###.#.....#...###");
            AddMapRow(092, "#.#.#######.###.#.###########.#.#.#####.#.###.#.###.#.#.#####.###.#######.#.#################.###.#.#.#######.#################.#.###########");
            AddMapRow(093, "#.#.#.....#.....#.#.......#...#...#.....#.....#...#...#.#...#.#...#.....#.#...#...###...#.....#...#...#.....#.............#...#.#...........#");
            AddMapRow(094, "#.#.#.###.#######.#.#####.#.#######.#############.#####.#.#.#.#.###.###.#.###.#.#.###.#.#.#####.#######.###.#############.#.#.#.###########.#");
            AddMapRow(095, "#...#...#.#...#...#.#...#.#.#...#...#...###...###.#.....#.#.#.#...#...#.#.#...#.#...#.#.#.....#...#...#...#...............#.#.#...#...#...#.#");
            AddMapRow(096, "#######.#.#.#.#.###.#.#.#.#.#.#.#.###.#.###.#.###.#.#####.#.#.###.###.#.#.#.###.###.#.#.#####.###.#.#.###.#################.#.###.#.#.#.#.#.#");
            AddMapRow(097, "#.......#...#.#.....#.#...#.#.#.#.....#.....#.....#...###.#.#.#...#...#.#.#.###...#...#.#...#.#...#.#...#.#.............#...#...#...#...#...#");
            AddMapRow(098, "#.###########.#######.#####.#.#.#####################.###.#.#.#.###.###.#.#.#####.#####.#.#.#.#.###.###.#.#.###########.#.#####.#############");
            AddMapRow(099, "#.....#.....#.........#...#...#.......................#...#...#.#...#...#.#.....#...#...#.#...#.###...#.#...#...........#.#.....#...#...#...#");
            AddMapRow(100, "#####.#.###.###########.#.#############################.#######.#.###.###.#####.###.#.###.#####.#####.#.#####.###########.#.#####.#.#.#.#.#.#");
            AddMapRow(101, "#...#...###...#.......#.#...#...#.......#...#...#.....#.......#.#...#.#...#.....#...#...#.....#...#...#.#.....#.......#...#.......#...#...#.#");
            AddMapRow(102, "#.#.#########.#.#####.#.###.#.#.#.#####.#.#.#.#.#.###.#######.#.###.#.#.###.#####.#####.#####.###.#.###.#.#####.#####.#.###################.#");
            AddMapRow(103, "#.#...........#.#.....#...#...#.#.#.....#.#.#.#.#.#...#...###.#.###.#.#.#...#...#...#...#...#...#.#...#.#.#...#...#...#.#.................#.#");
            AddMapRow(104, "#.#############.#.#######.#####.#.#.#####.#.#.#.#.#.###.#.###.#.###.#.#.#.###.#.###.#.###.#.###.#.###.#.#.#.#.###.#.###.#.###############.#.#");
            AddMapRow(105, "#.#.....#.....#.#.#...#...#.....#.#...#...#.#.#.#.#...#.#...#.#.#...#.#.#...#.#.#...#...#.#...#.#...#.#.#...#.....#...#.#.#.........#...#...#");
            AddMapRow(106, "#.#.###.#.###.#.#.#.#.#.###.#####.###.#.###.#.#.#.###.#.###.#.#.#.###.#.###.#.#.#.#####.#.###.#.###.#.#.#############.#.#.#.#######.#.#.#####");
            AddMapRow(107, "#...#...#.###...#.#.#.#...#.#...#.#...#...#...#.#...#...#...#.#.#...#.#.#...#.#.#...#...#...#...#...#.#.#...#.....#...#.#.#.#.....#...#.....#");
            AddMapRow(108, "#####.###.#######.#.#.###.#.#.#.#.#.#####.#####.###.#####.###.#.###.#.#.#.###.#.###.#.#####.#####.###.#.#.#.#.###.#.###.#.#.#.###.#########.#");
            AddMapRow(109, "#.....#...#.....#.#.#...#.#.#.#.#.#.#...#.....#.#...#...#...#.#.#...#...#...#.#.....#.....#...###.#...#.#.#.#...#.#.....#...#...#...#.......#");
            AddMapRow(110, "#.#####.###.###.#.#.###.#.#.#.#.#.#.#.#.#####.#.#.###.#.###.#.#.#.#########.#.###########.###.###.#.###.#.#.###.#.#############.###.#.#######");
            AddMapRow(111, "#.#...#...#...#...#...#...#.#.#.#.#.#.#...#...#...#...#.....#.#.#.....#.....#...........#.#...#...#...#...#.#...#...#.......#...###...#.....#");
            AddMapRow(112, "#.#.#.###.###.#######.#####.#.#.#.#.#.###.#.#######.#########.#.#####.#.###############.#.#.###.#####.#####.#.#####.#.#####.#.#########.###.#");
            AddMapRow(113, "#...#.#...#...#...#...#.....#.#.#.#.#.#...#.#...###.....#.....#.#...#.#.#.....#.......#.#.#...#.#.....###...#...###.#.....#...#...#...#.#...#");
            AddMapRow(114, "#####.#.###.###.#.#.###.#####.#.#.#.#.#.###.#.#.#######.#.#####.#.#.#.#.#.###.#.#####.#.#.###.#.#.#######.#####.###.#####.#####.#.#.#.#.#.###");
            AddMapRow(115, "#.....#.#...#...#...#...#...#.#.#.#...#...#...#...#...#...#.....#.#.#.#.#.#...#.....#.#.#...#.#.#.#.......#...#.#...#...#.......#...#...#...#");
            AddMapRow(116, "#.#####.#.###.#######.###.#.#.#.#.#######.#######.#.#.#####.#####.#.#.#.#.#.#######.#.#.###.#.#.#.#.#######.#.#.#.###.#.###################.#");
            AddMapRow(117, "#.....#.#...#.....#...#...#...#...#.......#...#...#.#.#...#...#...#.#.#...#...#.....#.#...#.#.#.#.#...#...#.#...#...#.#...#.................#");
            AddMapRow(118, "#####.#.###.#####.#.###.###########.#######.#.#.###.#.#.#.###.#.###.#.#######.#.#####.###.#.#.#.#.###.#.#.#.#######.#.###.#.#################");
            AddMapRow(119, "#.....#.#...#...#.#...#.#.........#...#...#.#.#.#...#.#.#.....#...#...#.......#.....#...#.#...#...###.#.#.#.....#...#.#...#...............###");
            AddMapRow(120, "#.#####.#.###.#.#.###.#.#.#######.###.#.#.#.#.#.#.###.#.#########.#####.###########.###.#.###########.#.#.#####.#.###.#.#################.###");
            AddMapRow(121, "#.......#.#...#...###.#.#.......#.....#.#...#.#.#...#...#.......#.....#.#...#...#...###.#...#.........#.#...#...#.#...#.#...#.....#.....#...#");
            AddMapRow(122, "#########.#.#########.#.#######.#######.#####.#.###.#####.#####.#####.#.#.#.#.#.#.#####.###.#.#########.###.#.###.#.###.#.#.#.###.#.###.###.#");
            AddMapRow(123, "#.........#.........#...###...#.....#...#.....#.###.....#.#...#.....#.#.#.#...#.#.....#...#.#...#...#...#...#...#.#.#...#.#.#.###...###...#.#");
            AddMapRow(124, "#.#################.#######.#.#####.#.###.#####.#######.#.#.#.#####.#.#.#.#####.#####.###.#.###.#.#.#.###.#####.#.#.#.###.#.#.###########.#.#");
            AddMapRow(125, "#.....#.....#...#...#...#...#.......#.#...#.....#...###.#...#.###...#.#.#.#.....#...#.###...#...#.#.#.#...#...#.#.#.#.....#.#.......#...#.#.#");
            AddMapRow(126, "#####.#.###.#.#.#.###.#.#.###########.#.###.#####.#.###.#####.###.###.#.#.#.#####.#.#.#######.###.#.#.#.###.#.#.#.#.#######.#######.#.#.#.#.#");
            AddMapRow(127, "#.....#...#.#.#...#...#...#...#...#...#.#...#.....#.....#...#.#...#...#.#.#...#...#.#.......#...#.#...#...#.#.#.#...#.......#.....#...#.#.#.#");
            AddMapRow(128, "#.#######.#.#.#####.#######.#.#.#.#.###.#.###.###########.#.#.#.###.###.#.###.#.###.#######.###.#.#######.#.#.#.#####.#######.###.#####.#.#.#");
            AddMapRow(129, "#...#.....#...#.....#.....#.#.#.#.#.#...#.#...#.....#...#.#.#.#.#...#...#.###.#.###...#.....###.#.....###.#.#.#.#...#.#...#...###.....#.#.#.#");
            AddMapRow(130, "###.#.#########.#####.###.#.#.#.#.#.#.###.#.###.###.#.#.#.#.#.#.#.###.###.###.#.#####.#.#######.#####.###.#.#.#.#.#.#.#.#.#.#########.#.#.#.#");
            AddMapRow(131, "###...###...###.......#...#.#.#.#.#.#.#...#...#.#...#.#.#.#...#.#...#.....#...#.....#.#.#.....#...#...#...#.#...#.#...#.#.#.........#.#.#...#");
            AddMapRow(132, "#########.#.###########.###.#.#.#.#.#.#.#####.#.#.###.#.#.#####.###.#######.#######.#.#.#.###.###.#.###.###.#####.#####.#.#########.#.#.#####");
            AddMapRow(133, "###.......#.........#...#...#.#.#...#.#.#.....#.#...#.#.#...###.#...###...#...#.....#.#...#...#...#...#...#.....#.#...#.#.#.........#.#.....#");
            AddMapRow(134, "###.###############.#.###.###.#.#####.#.#.#####.###.#.#.###.###.#.#####.#.###.#.#####.#####.###.#####.###.#####.#.#.#.#.#.#.#########.#####.#");
            AddMapRow(135, "#...#...#...#.....#...#...###...#.....#.#.#.....###.#.#.#...#...#...#...#.###.#.#...#.......###.#...#.#...#.....#...#...#.#.....#...#.......#");
            AddMapRow(136, "#.###.#.#.#.#.###.#####.#########.#####.#.#.#######.#.#.#.###.#####.#.###.###.#.#.#.###########.#.#.#.#.###.#############.#####.#.#.#########");
            AddMapRow(137, "#...#.#.#.#.#.#...#...#...#.......#...#.#.#.#.......#.#.#...#.#...#.#...#.....#...#.....#.......#.#.#.#...#.....#.........#...#...#.........#");
            AddMapRow(138, "###.#.#.#.#.#.#.###.#.###.#.#######.#.#.#.#.#.#######.#.###.#.#.#.#.###.###############.#.#######.#.#.###.#####.#.#########.#.#############.#");
            AddMapRow(139, "###...#...#...#.....#.....#.........#...#...#.........#.....#...#...###.................#.........#...###.......#...........#...............#");
            AddMapRow(140, "#############################################################################################################################################");
        }

        void DumpMap()
        {
            for (var x = 0; x < mapSizeX; x++)
            {
                for (var y = 0; y < mapSizeY; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(map[(x, y)]);
                }
            }
        }
    }
}
