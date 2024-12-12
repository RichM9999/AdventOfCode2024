﻿//https://adventofcode.com/2024/day/12
namespace AdventOfCode
{
    class Day12
    {
        int mapSize = 140;

        public void Run()
        {

            var map = new char[mapSize, mapSize];

            var regions = new List<Region>();

            SetupMap(ref map);

            var start = DateTime.Now;

            for (var y = 0; y < mapSize; y++)
            {
                for (var x = 0; x < mapSize; x++)
                {
                    if (map[y, x] != '.')
                    {
                        var region = new Region
                        {
                            plant = map[y, x],
                            originX = x,
                            originY = y,
                        };

                        MapRegion(ref region, ref map, false);

                        regions.Add(region);
                    }
                }
            }

            long price = 0;

            foreach (var region in regions)
            {
                price += region.area * region.perimiter;
            }

            Console.WriteLine($"Price: {price}");
            //1446042
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            SetupMap(ref map);
            
            start = DateTime.Now;
            regions = new List<Region>();

            for (var y = 0; y < mapSize; y++)
            {
                for (var x = 0; x < mapSize; x++)
                {
                    if (map[y, x] != '.')
                    {
                        var region = new Region
                        {
                            plant = map[y, x],
                            originX = x,
                            originY = y,
                        };

                        MapRegion(ref region, ref map, true);

                        regions.Add(region);
                    }
                }
            }

            price = 0;

            foreach (var region in regions)
            {
                price += region.area * region.perimiter;
            }

            Console.WriteLine($"Price: {price}");
            //902742
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        void MapRegion(ref Region region, ref char[,] map, bool sidesOnly)
        {
            var ox = region.originX;
            var oy = region.originY;
            var plant = map[oy, ox];

            var adjacentPlants = new List<(int x, int y)>();
            var lastAdjacentPlants = new List<(int x, int y)> { (ox, oy) };

            while (true)
            {
                var newAdjacentPlants = new List<(int x, int y)>();

                foreach (var adjacent in lastAdjacentPlants)
                {
                    newAdjacentPlants.AddRange(AdjacentPlants(plant, adjacent.x, adjacent.y, ref map));
                }

                newAdjacentPlants = newAdjacentPlants.Except(adjacentPlants).ToList();

                if (newAdjacentPlants.Count == 0)
                    break;

                lastAdjacentPlants = new List<(int x, int y)>(newAdjacentPlants);
                adjacentPlants.AddRange(newAdjacentPlants);
            }

            if (!adjacentPlants.Any(a => a.x == ox && a.y == oy))
            {
                adjacentPlants.Add((ox, oy));
            }

            if (sidesOnly)
            {
                region.perimiter = GetCorners(adjacentPlants, ref map);
                if (GetPerimiter(adjacentPlants, ref map) < region.perimiter)
                    Console.WriteLine();
            }
            else 
            {
                region.perimiter = GetPerimiter(adjacentPlants, ref map);
            }

            foreach (var adjacent in adjacentPlants)
            {
                map[adjacent.y, adjacent.x] = '.';
            }

            region.area = adjacentPlants.Count;
        }

        List<(int x, int y)> AdjacentPlants(char plant, int x, int y, ref char[,] map)
        {
            var adjacent = new List<(int x, int y)>();
            if (x > 0)
            {
                if (map[y, x - 1] == plant)
                    adjacent.Add((x: x - 1, y));
            }
            if (y > 0)
            {
                if (map[y - 1, x] == plant)
                    adjacent.Add((x, y: y - 1));
            }

            if (x < mapSize - 1)
            {
                if (map[y, x + 1] == plant)
                    adjacent.Add((x: x + 1, y));
            }

            if (y < mapSize - 1)
            {
                if (map[y + 1, x] == plant)
                    adjacent.Add((x, y: y + 1));
            }

            return adjacent;
        }

        int GetPerimiter(List<(int x, int y)> adjacent, ref char[,] map)
        {
            var totalPermiter = 0;

            foreach (var adj in adjacent)
            {
                var x = adj.x;
                var y = adj.y;
                var plant = map[y, x];

                var perimiter = 4;

                if (x > 0)
                {
                    if (map[y, x - 1] == plant)
                        perimiter--;
                }

                if (y > 0)
                {
                    if (map[y - 1, x] == plant)
                        perimiter--;
                }

                if (x < mapSize - 1)
                {
                    if (map[y, x + 1] == plant)
                        perimiter--;
                }

                if (y < mapSize - 1)
                {
                    if (map[y + 1, x] == plant)
                        perimiter--;
                }

                totalPermiter += perimiter;
            }

            return totalPermiter;
        }

        int GetCorners(List<(int x, int y)> adjacent, ref char[,] map)
        {
            var totalCorners = 0;
            var corners = new List<(int x, int y)>();   

            foreach (var adj in adjacent)
            {
                var x = adj.x;
                var y = adj.y;
                var plant = map[y, x];

                if (x == 0 && y == 0)
                {
                    // top-left extreme external corner
                    // --
                    // |A
                    totalCorners++;
                    corners.Add((x, y));
                }

                if (x == 0 && y > 0)
                {
                    // top-left edge external corner
                    // |.
                    // |A
                    if (map[y - 1, x] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }

                if (x == mapSize - 1 && y == 0)
                {
                    // top-right extreme external corner
                    // --
                    // A|
                    totalCorners++;
                    corners.Add((x, y));
                }

                if (x == mapSize - 1 && y > 0)
                {
                    // top-right edge external corner
                    // .|
                    // A|
                    if (map[y - 1, x] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }

                if (x == 0 && y == mapSize - 1)
                {
                    // bottom-left extreme external corner
                    // |A
                    // --
                    totalCorners++;
                    corners.Add((x, y));
                }

                if (x == 0 && y < mapSize - 1)
                {
                    // bottom-left edge external corner
                    // |A
                    // |.
                    if (map[y + 1, x] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }

                if (x == mapSize - 1 && y == mapSize - 1)
                {
                    // A|
                    // --
                    // bottom-right extreme external corner
                    totalCorners++;
                    corners.Add((x, y));
                }

                if (x == mapSize - 1 && y < mapSize - 1)
                {
                    // A|
                    // .|
                    // bottom-right edge external corner
                    if (map[y+1, x] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }

                if (x > 0 && y == 0)
                {
                    // top-left top-edge external corner
                    // --
                    // .A
                    if (map[y, x - 1] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }

                if (x < mapSize - 1 && y == 0)
                {
                    // top-right top-edge external corner
                    // --
                    // A.
                    if (map[y, x + 1] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }


                if (x > 0 && y == mapSize - 1)
                {
                    // bottom-left bottom-edge external corner
                    // .A
                    // __
                    if (map[y, x - 1] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }

                if (x < mapSize - 1 && y == mapSize - 1)
                {
                    // bottom-right bottom-edge external corner
                    // A.
                    // --
                    if (map[y, x + 1] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }

                if (x > 0 && y > 0)
                {
                    // top-left external corner
                    //..
                    //.A
                    if (map[y, x - 1] != plant && map[y - 1, x] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }

                if (x < mapSize - 1 && y < mapSize - 1)
                {
                    // bottom-right external corner
                    //A.
                    //..
                    if (map[y, x + 1] != plant && map[y + 1, x] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }

                if (x > 0 && y < mapSize - 1)
                {
                    // bottom-left external corner
                    //.A
                    //..
                    if (map[y, x - 1] != plant && map[y + 1, x] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }

                if (x < mapSize - 1 && y > 0)
                {
                    // top-right external corner
                    //..
                    //A.
                    if (map[y - 1, x] != plant && map[y, x + 1] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }

                if (x < mapSize -1 && y < mapSize - 1)
                {
                    // bottom-right internal corner
                    //AA
                    //A.
                    if (map[y, x + 1] == plant && map[y + 1, x] == plant && map[y + 1, x + 1] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }

                if (x < mapSize - 1 && y > 0)
                {
                    // top-right internal corner
                    //A.
                    //AA
                    if (map[y - 1, x] == plant && map[y, x + 1] == plant && map[y - 1, x + 1] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }

                if (x > 0 && y > 0)
                {
                    // top-left internal corner
                    //.A
                    //AA
                    if (map[y - 1, x] == plant && map[y, x - 1] == plant && map[y - 1, x - 1] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }

                if (x > 0 && y < mapSize - 1)
                {
                    // bottom-left internal corner
                    //AA
                    //.A
                    if (map[y, x - 1] == plant && map[y + 1, x] == plant && map[y + 1, x - 1] != plant)
                    {
                        totalCorners++;
                        corners.Add((x, y));
                    }
                }
            }

            return totalCorners;
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
            AddMapRow(0, ref map, "CCCCPPPPPPPPPPIIIIAAABBBBBBBBLLLLLLFFFFFFFFFFFFFFFFFSSSSSSSSSSSSSWOWWWWWWWZZZZZZZZZDDDDDDDDDIWIIIIIIIJJJJJJJJJJJJJJEEEEEEEKKKKCCCOOOOOOOOOOO");
            AddMapRow(1, ref map, "CCCCCCCXPPPPPPIIIIAAABBBBBBBLLLLLLLLFFFFFFFFFFFFFFFFSSSSSSSSSSSSWWWWWWWWWWZZZZZZZZDDDDDDDDDDIIJIIIIIJJJJJJJJJJJJJJJEEEEEEEECCCCCCOOCCCOOOOOO");
            AddMapRow(2, ref map, "CCCCCCPPPPPPPPPIIAAAAABBBBBLLLLLLLLLFFFGGGGFFFFFFFFFFFFLLSSSSWSWWWWWWWWWWWZZZZZZDDDDDDDDDDDDIIIIIIIIJJJJJJJJJJJWJEEEEEEEEEECCDCICCOCCCCCCCOO");
            AddMapRow(3, ref map, "CCCCCCCPPPPPPPPUIAAAAAABBBBBBLLLLFLLFFFGYGGGFFFFFFFFFFFLLLBSWWWWWWWWWWWDDDZOZZZZDDDDDDDWDDDWQIIIIIIIJJJJJJJJJJWWJEEEEEEEEEKCCCCCCCCCCCCCCOOO");
            AddMapRow(4, ref map, "CCCCCCCPPPPPPQUUAAAAAAAAAAAAAAALLFFFFFGGGGGGFFFFFFFFFFLLBBBWWWWWWWWWWWWDDDDOZZDDDDDDDWWWWWWWWIIIIIIIJJJJJJJJWJWWWWEEEEEEECCCCCCCCCCCCCCCCOOO");
            AddMapRow(5, ref map, "CCCCCCCPPPPPUUUUWAAAAAAAAAAAFFFFFFFFFFFFFGGGGFFFFFFFFFFLBBBBBBWWWWWWWWWWDDDDZZZDDDDDWWWWWWWWWWWIIIIJJJJJJJJJWWWWWEEEEEEECCCCCCCCCCCCCNNNNOON");
            AddMapRow(6, ref map, "CCCCCCCEPPPPPSUUAAAAAAAAAAAAAFFFFFFFFFFGGGGLGFLLFFFFLLLLBBBBBBWWWWWWWWWGDDDDDZDDDDDDWWWWWWWWWQQIIIQQJJJJJJJWWWWWEEEEEEEEECSCCCCCCCCCCNNNNONN");
            AddMapRow(7, ref map, "CRCCCCEEPPUPPPUUUAAAAAAAAAAAAFFFUFFFGFGGGGGGGLLLFLFFLLLLBLBBBBBWWWWWWGGGDDDDDDDDDDDDDWWWWWWWWQQIIQQJJJJJJJJJJWWWEEEEEEEECCCCCCCCCCCCCCCNNONN");
            AddMapRow(8, ref map, "CCCCCEEEEPUPUUUUAAAAAAAAAAAAAFUUUFFFGGGGGGGLLLLLLLFFLLLLLLBBBBBWWWWGGGGGGGGDDDDDDMDDDWWWWWWWWWWIQQQJJJJJJJJJJJWWWJEEEEEEECCCCCCCCCCCCCCNNNNN");
            AddMapRow(9, ref map, "CCCCCEEUUUUUUUUUUAAAAAAAAAAFFFUUUUUFUGGGGGGLLLLLLLLLLLLLLBBBBBBBWWWGGGGGGGDDDDDDMMDWWWWWWWWWWWWQQMQQJJJJJJJJJJWJWJEEEEKEVCCCCCCCCCCCCNNNNNNN");
            AddMapRow(10, ref map, "COOCCEEUUUUUCCUUUBBAAAAAAAAFFFUUUPUUUGGGGGGLLLLLLLLLLLLLLBBBBBBNWWGGGGGGDDDDDDDMMMMMWWWWWWWWWWWMMMQQQJJJJJJJJJJJJJEEKKKEECPCCCCNCCCNNNNNNNNN");
            AddMapRow(11, ref map, "SGGEEEGGUUUCCCUUUBBAAAAAAAFFUUUUUUUUUUUUGGGGLLLLLLLLLLLLLLBBNNNNWWGGGGGDDDDDDDDMMMMMMWWWWWWWMMMMMMQQQQQJJJJJJJJJEEEEKKKNEPPPPPCNNCCNNNCNNNNN");
            AddMapRow(12, ref map, "GGGGGEGGGUUCCCCCBBBBBABAAAUUUUUUUUUUUUUUMMGLLLLLLLLLLLLLLLLLNNNNNWNGGGGDDDDDDDMMMMMMMWWWWWWWMMMMMMMQQQQJJJVVVLJJJJJEKKKKPPPPPPPNNNCNNCCCCCNN");
            AddMapRow(13, ref map, "GGGGGGGGGUCCCCKKBBBBBBBBBAUQQUUUUUUUUUUUULLLLLLLLLLLLLLLSLLLLNNNNNNNNGGDDDDDMMMMMMMMMWWMWWWWMMMMMMRRRQQJJJPVLLLJWJJKKKKKKPPPPPPNNNNNCCCCCCCC");
            AddMapRow(14, ref map, "GGGGGGGGGUKKKKKKBBBBBBBBBBUUUUUUUUUUUUUUULLLLLLLLLSSSSSSSSLLNNNNNNNNNNNDDDDDDMMMMMMMMMMMMMWWWMMMMMRRRQQQJPPPPLWWWWWWPPKKKPPNNNNNNNNCCCCCCCCC");
            AddMapRow(15, ref map, "GGGGGGGGKKKKKKKBBBBBBBBBBBUUUUUUUUUUUUUUULLLLLLLLLSSSSSSSSNNNNNNNNNNNNNNNDDMMMMMMMMMMMMMMMMMMMMMMHRRRQQQQPPPLLLWWWWWPPPPPPNNNNNNNNNNCCCCCCCC");
            AddMapRow(16, ref map, "GGGGGGGGGKKGGKBBBBBBBBBBBBBBUUUUUUUUUUUUXXLLLLLLLSSSSSSSSNNNNNNNNNNNNNNNNDMMMMMMMMMMMMMMMMMMMMMMMMRRRPQQPPPPPLWWWWWWPPPPPPNNNNNNNNNNNSSCCCCC");
            AddMapRow(17, ref map, "GGGGGGGGGGKGDDDDBBBBBBBBBBBBBUUUUUGUUUTUXXXXLLLSSSSSSSSSSSSNNNNNNMMMMNNNNNNMMMMMMMMMMMMMMMMMMMMMMMRRRPQQPPPPVWWWWWWWWPPPPPPNPPPNNNNNNSSCYYYC");
            AddMapRow(18, ref map, "GGGGGGGGGGGGDDDADDBBBBBBBBBBBBUUUUGGUUTTXXXXLLXXSSSSSSSESNNNNNNNMMMMMMNNNNMMMMMMMMMMMMMMMMMMMMMMMMRRZPQPPPPPPPPPPWWWPPPPPPPPPPPNNVNNNSSSQQQC");
            AddMapRow(19, ref map, "GGGGGGGGGGGDDDDDDDDBBBBBBBBBBBUUUGGGNTTTTXXXXLXXNJSSSSSSSNNNNNNMMMMMMKMMNNMMMMMMMMMMMMMMMMMMMMMMMMMMMPPPPPPPPPWWWWWWWPPPPPPPPPPNZZZZSSSSSQCC");
            AddMapRow(20, ref map, "GGGGGGGGGGDDDDDDDDDDDDBBBBBBBBPUUGNNNTTXXXXXXNXNNNNNNSSNNNNNNNNMMMMMMKMMMNMMMMMMMMMMMMMMMMMMMMMMMMMMPPPPPPPPPPWWWWWWWPPPPPPPPPPZZZZZZKKKKQQQ");
            AddMapRow(21, ref map, "GGGGGGGGDGDDDDDDDDDDDBBBBBPPPPPUUGNNTTTTXXXXXNNNNNNNNNNNNNNNNNMMMMMMMMMMTNMMMMMMBGMGMMMMMMMMMMMMMMVMPPPPPPPPPPPWWWWWWPPPPPPPPZZZZZZZZKKRKQQQ");
            AddMapRow(22, ref map, "LGGGGGGDDDDDDDDDDDPPDBBBBBPPPPPPNNNNNTTTXXXXXXXNNNDDNDNNNNNNNMMMMMMMMMMMTTMMMMMGGGGGGMMMMMSMSMMMMVVPPPPPPPPPPPPWWWWWWWPPPFPPZZZZZZZZKKKKKQQQ");
            AddMapRow(23, ref map, "LLGGGGGGDDDDDDDDDDPPPBBBBBBPPPPPPNNNNWWTTXTTXXXXNNMDNDNNDDNNMMMMMMMMMWMWWTMMMMMMMGGGGMMMLSSSSMSMMMVPPPPPPPPPPPPPWWWWWWPPFFPPPZZZZZZZKKKKQQKV");
            AddMapRow(24, ref map, "LLLLLGGGDDDDDDODDDPPPPPPBPPPPPPPNNNWWWWWTTTXXLXLDDDDDDDNLDDNNMMMMMMMMWWWWWMMMMMMMGGGGMMLLSSSSSSKKMVPPPPPPPPPPBBWWWWWFFFFFFPPZZZZZZFFFFFKKKKK");
            AddMapRow(25, ref map, "LLLLLLLDDDPDDDDDPPPPPPPPPPPPEPEPPPPPWWWWOOOOOLLLLDDDDDDDDDDDDMMMMMMMMMMWWWMMMMMMGGGGGMMLLSSSSSSSSAZSPPPPPPPPPPWWWWWWWFFFFFFFZFFFZFFFFREEKQKQ");
            AddMapRow(26, ref map, "LLLLLLLLDDPDDDDPPPPPPPPPPPPEEEEPPPPEWWWWWOOOOOLLLDDDDDDDDDDDDRMMMMMMMWWWWWWWWMWWGGGGGSSSSSSSSSSSAAZSPPPPPPPPPPPWBBWWWFFFFFFFFFFFFFFFRRRERQQQ");
            AddMapRow(27, ref map, "LLLLLLLDDPPPDDPPPPPHPPKKKKPEEEEEPPPEWWWWOOXXXLLLLLDDDDDDDDDDDDDGMMMMMWWWWWWWWWWWGGGGGRSSSSSSSSSPSZZZPPPPPPPPPBBBBBBBFFFFFFFFFFFFFFFFFRRRRRRR");
            AddMapRow(28, ref map, "LLLLLLLDDPFPPPPPPPPHPKKKKPPKEEEEEEEEWWWXXXXXXLLLHDDDDQQDZDDDDDDMMMMMWWWWWWWWWWWGGKGGRRRRSSSSSSSSSZZZZPPPPPPPBBBBBBBBBFFFFFFFFFFFFFFFFRRRRRRR");
            AddMapRow(29, ref map, "LLLLLLLFPPFFFPFFPPPHHKKKKKKKEEEEEEUEWWWXXXXXWWHHHWDDQQDDDDDDDDDMMMMWWWWWWWWWWWWWWKGRRRRSSSSSSSSSSZZZZZZZPPPPBBBBBBBBBFFFFFFFFFFFFFBFRRRRRRRR");
            AddMapRow(30, ref map, "LLLLLLLFPPPFFFFPPSSHHKKKKKKEEEEEEUUWWWWXXXXXWWWHHDDDQQQQDDDDDDMMMMMMWWWWWWWWWWWWKKRRRCSSSSSSSSSSSZZZZZZZPPPPPPBBBBUUUUUUFFFFFFFFFFBBRRRRRRRR");
            AddMapRow(31, ref map, "LLLLLLLFFFFFFFPPSSSSHHHKKKKEEEEEEEEWWWWWWXXWWWHHHHQQQQQQDDDDDMMMMMMMHWWWWWWWWWWWKKRRRSSSSSSSSSSSZZZZZZZZZPLLLLBLBBUUUUUUBFFFFFLLFFBBRRRRRRRR");
            AddMapRow(32, ref map, "LLLLLLFFFFFFFFPSSSSSSUUKUKKEEEEEEEEWWWWWWWWWWWWHHHHQQQQQQDDDDMMMMMMMHWWWWWWWWWWWKKKKSSSSSSSSSSSSZZZZZZZZZLLLLLLUUUUUUUUUFFFUFLLLFBBBRRRRRRRR");
            AddMapRow(33, ref map, "LLLLLFFFFFFFFPPSSSSSSSUUUEEEEEEEOOOWWWWWWWWLWWWGGHHQQQQQQDDMMMMMMMMMMMWWWWWWWWWKKKKKSSSSSSSSSZZZZZZZZZZZZLLLLLLUUUUUUUUUUUUUUUUUUUUBRRRRRRRG");
            AddMapRow(34, ref map, "LLNLFFFFFFFFFFFFSSSSSSSUUEEEEEEEOOOWWWWWWWWMMOMGGHHHQQQAAPMMMMMMMMMMWWWWWWWWWWWKKRKRRSVSSSSSZZZZZZZZZZZZZZZZLLLUUUUUUUUUUUUUUUUUUUUBRRRRRRRR");
            AddMapRow(35, ref map, "NLNLFFFFFFFFFFFFSSSSSSSSSRERREEEOOWWWWWWMMMMMMMMMHHQQAAAAPPMMMMMMMMMJJWWWWWWWWKKKRRRRSSSSSZZZZZZZZZZZZZZZZZZLLLUUUUUUUUUUUUUUUUUUUUBRRRRRRRR");
            AddMapRow(36, ref map, "NNNFFFFFFFFFFFFFFSSSSSSSSRRRREBOOOOWOWWWMWMWMMMMMZHAAAAAAMMMMMMMMMMMMJJWWWAWWVVKKRRLLLLSGLOOZFFZZZZZZZZZZQQZLLQUUUUUUUUUUUUUUUUUUUUBBRBBRREE");
            AddMapRow(37, ref map, "NNNNFFFFFFFFFFFZFSSSSSSSSRRRRROOOOOOOWWWWWWWMMMMMZZAAAAAAFFMMMMMMMMMMWWWAAAWVVVVKRRRLLLLLLOOZFFZZZZZZZZZZQQQQQQUUUUUUUUUUUUUUUUUUUUBBBBBEEEE");
            AddMapRow(38, ref map, "NNNNFFFFFFFFFFFSSSSSSSSSSSSRRROOOWOWWWWWWWWWMMMMMMMMMHAASFFMEETMMGMMMAAWAAAEVVVVLLLLLLLLLLLLFFFZZZZZZZZQQQQQQQQUUUUUUUHDDDUUUUUUUUUBBBBBBEEE");
            AddMapRow(39, ref map, "FNFNFFFFFFFFFFFSSSSSSSSSSSSSRROOOWWWWWWWWWWYMMMMMMMSSSSSSFSEEEMMFFFNNAAAAAEEEEVVBLLLLLLLLLLRRFFFFFZZZZZZQQQQQQQUUUUUUUDDDDUUUUUUUUUQBBBBBEEE");
            AddMapRow(40, ref map, "FFFFFFFFFFFFFFFFSSSSSSSSSSYSRRRROOWWWWWWWWYYYMMMMMKSSSSSSSSSEFFFFFFNFFAAAAEVEEEBBLBWBLLLLLLRRRRFFFFZZZZNQQQQQQQUUUUUUUJJDDUUUUUUUUUQQBBBBEEE");
            AddMapRow(41, ref map, "FFFFFFFFFFFOSSSSSSSSSSSSSSSSRRRMMMMWMWWWWWWYYMMMSSSSSSSUUSSSEFFFFFFFFFFFAAEEEEEEBBBBBLLLLLLRRRIRRFFFFZZQQQQQQQQUUUUUUUJJDDUUUUUUUUUQQQQQEEEE");
            AddMapRow(42, ref map, "FKFFFFFFFFFOOSOSSSSSSSSSSSSSRRMMMMMMMMWWTWYYYTYMPSSSSSSSSFFSEFFFFFFFFFFAAAEEEEEEEEEEELLLLRRRRRRRFFFFFQQQQQQQQQQQQQOOXJJXDDUUUUUUUUUQQQQEEEEE");
            AddMapRow(43, ref map, "KKKFFFKFFFFOOOOSSSSHHGSSSSHIHHHMMMMMMMMTTTTTYYYSSSSSSSSSSSSSEXFIVFFFFFAAAEEEEEEEEEEEEEEEERRRRRRRRRRFFFQQQQQQQQQXQQXXXXXXVDPXQQQQQQQQQQQQEEEE");
            AddMapRow(44, ref map, "KKKFKFKKFFFFOOOSSSSHHHSSHZHIIHHMMMMMMMMTTTTTYYYIISSSSSSSSSSSXXIIFFFLLLLLLEEEEEEEEEEEEEEEERRRRRRRRRVFFVQQQQQQQQQXXXXXXXXXXXXXYQQQQQQQQQQQEEEE");
            AddMapRow(45, ref map, "KKKKKKKKLLLLOOOSSSSHHHHHHHHHHHHMMMMMTTTTTTTTYYYYIISSSSSSSSSSSXIIIIIILLLLLLEEEEEEEEEEEEEEERRRRRRRRRVFVVNQQQQQQXXXXXXXXRXXXXUNYQQQQQQQQQQQEEII");
            AddMapRow(46, ref map, "KKKKKKKKLULLLLOOSSSHHHHHHHHPHHHMMMMMTTTTTTTTTYYYIISSSSSSSSSSSXXIIIVLLLLLLLUEEEEEEEEEEEEEERRRRRHHHVVFVVNNQQQQQXXXXXXXXXXXXXUNQQQQQQQQQQQQIIII");
            AddMapRow(47, ref map, "KKKKKKKKLLLLZLOOOSSHHHHHHHHHHHHMMMMMTMTTTHTIIIYYISSSSSSSSSSMUUUBIUVLLLLVLLLEEEEEEEEEEEEEERRRRRRVVVVVVVNNQQQQQXXXXXXXXXXXXQNNNQQQQQQQQQIIIIII");
            AddMapRow(48, ref map, "KKKKKKKWLLLLLOOOOSBHHHHHHHHHHMMMMMMMMMTTMHHHHIIIISGSSSSSSMMUUUUUUUUULLLLLPEEEEEEEEEEEEEERRRRRRRRVVVVVVVNNNQXXEXXXXXXXXXXXQNNNNQQQQQQQIIIIIII");
            AddMapRow(49, ref map, "KKKKKKKLLLLLLLOOOHHHHHHHHHHHHHMMMMMMMMMMMHHHHHSSISSSSSSSSMMUUUUUVUUULLLLLLLEEEEEEEEEERRRRRRRRRRRVVVVVVVNNNNXXXXXXXXXXXXXXXNNNNNQTQQQIIIIIIII");
            AddMapRow(50, ref map, "KBBBKKLLLLLLLLLLLHHHHHHHHHHHHHHMMMMMMMMMHHHHHHSSSSSSSSSSSUUUUUUUUUUUWLLLLLLEEEEEEEEEERRRRRRRGGGGVVVVVVVNNNXXXXXXXXXXXXXXNNNNNNNNTIIQIIIIIIII");
            AddMapRow(51, ref map, "KBBBBKBQQLLLLLAAAHHHHHHHHHHHHMMMMMMMMMMMHHHHHHSSSSSSSSSYSYUUUUUUUUUUWWLLLLEEEEEEEEEEERRRRRRRGGGGVVVVVVVVVNXIXXXXXXXXXXXXXNNNNNNNNIIIIIIIIIII");
            AddMapRow(52, ref map, "ZBBBBBBBLLLLLLLAAOHHHHHHHHHHHMQMMMMMMMMMHHHHHHSSSSSSSYYYYYUUUUUUUUUUWWLLLLECCEEEECRRRRRRPPRRRGGGGVVVVVVVQQQQXXXXXXXXXXXXNNNNNNNNIIIIIIIIIIII");
            AddMapRow(53, ref map, "BBBBBBBBBLLALAAAAAHHHHHHHHHHHPMMMMMMMMMMRHHHHSSSSSSSSSYYYUUUUUUUUUUWWWLWLCCCCCCCCCCRRRRRRRGGRGGGGVVVVUQVQQQQXXNQXXXXXXXNNNNNNNNNNIIIIIIIIIIA");
            AddMapRow(54, ref map, "BBBBBBBBBLLAAAAAAPPHHPHHPPPPPPPIMMMMMMMMMXHHHHSSSSSSSSSYUUUUUUUUUUUWWWWWTCCCCCCCCCCCRRRRRGGRRGGGGVGVVQQQQQQQXQQQQQXXXXXXNNNNNNNNIIIIIIIIIIIA");
            AddMapRow(55, ref map, "BBBBBBBBBLLAAAAKAPRPPPHHPPPPPPPMMMMMMMMMXXSSSSSSSSSSSSEEBBUUUUUUUUUKWWWTTTCCCCCCCCCBBBBRRRGGGGGGGGGGGQQQQQQQQQQQQQQVVXTXNNNNNNNNIIIIIIIIIIII");
            AddMapRow(56, ref map, "BBBBBBBBBAAAAAAAOPPPPPHPPPPPPPPPMMMMMMXXXXSSSSSSSSSSSSEEBBUUUUUUUUUWWWTTTTCCCCCCCCCBBBGGGGGGGGGGGGGRRRRQQQQQQQQGQQQGVXNNNNNNNNNNNIIIIIIIIIXI");
            AddMapRow(57, ref map, "BBBBBBBBBAAAAAAAOOPPPPPPPPPPPPPPMMMMMMXXXXXSSSSSSSSSSSEKSBUUUUUUUUUHHTTTTTTCCCCCCCCBBBBGGGGGGGGGGRGRNNQQQQQQQGGGGGGGGGGNNUUUUUUUUUUJJIIIIIIR");
            AddMapRow(58, ref map, "BBBBBBBBAAAAAAAAOOOPPPPPPPPPPPPPMMMMMXXXXXSSSSSSSSSSMSSSSUUUUWWUUUUWWDDTTTOTCCCCCCCBBBBGGGGGGGRRRRRRRNRQQQQQQQGGGGGXXGGNNUUUUUUUUUUJJIIIIRRR");
            AddMapRow(59, ref map, "BBBBBBBEAAAAAAAOOOOORRRPPPPPPPPPPXXMXXXXXXXSSSSSSSSSSSQQQQQDWWWWWUUWWDDTTTTTCCCCCCCXBBBXGGGGRRRRRRRRRRRQQQQQQGGGGGGGGGGGNUUUUUUUUUUJJJJJJRRR");
            AddMapRow(60, ref map, "BBBBBBBEPAAAAOOOOOORFRRRRRPPPPPPXXXXXXXXXXXSSSSXSSSSSSSQQQQDWWWWWWWWWTTTTTTTTCCCCCCXGXXXXXGRRRRRRRRRRRRQQQQQQGGGGGGGGGEENUUUUUUUUUUJJJJJJRRR");
            AddMapRow(61, ref map, "BBBBBEEEEUUAAOOOOOORFRRRRRPPPPOOBXXXXXXXXXXXSSSXXXXXXQQQQQQQQWWWWWWWWTTTTTTTTCCCCCCXXXXXXXGRRRRRRRRWRRRQQQQQQGGGGGGGGGGNNUUUUUUUUUUJJJJJJRRR");
            AddMapRow(62, ref map, "XBBBBEEEUUAAOOOOOORRRRRRRRREPZOOBXXXXXXXXXXXXXXXXXXXQQQQQQQQQQQWVVVWMMTTTTTTTCCCCCCXXXXXXXGRRRRRRRRRRQQQQQQQQGGGGUUUUUUUUUUUUUUUJJJJJJJJJRRX");
            AddMapRow(63, ref map, "XXBEBEEEEEEEEOOORORRRRRRRRRRZZOOBBXDXXXXXXXXXXXXXXXXXCCQQQWWWWWWVVVVVVTTTTTTCCCCXXXXXXXXXXXRRRRRRRRRVQQVVVQGGGGGGUUUUUUUUUUUUUUUJJJJJJJJJRXX");
            AddMapRow(64, ref map, "XXBEEEEEEEEEEOORRRRRRRRRRRRROOOOOOODDDDXXXXXXXXXXXXCCCCQWCWWWWWWWWVVVVTTTTTTCCCQXXXXXXXNNNNNNNNRRRVVVVVVVVQQGGGGGUUUUUUUUUUUUUUUXJJJJJJJJJXX");
            AddMapRow(65, ref map, "XBBEEEEEEEEEFFOFFRRRRRRRRRRROOOOOODDDDDDDDXXXXXXWXXCCCCCCCWWWWWWWVVVVVTTMTTTCCQQXXXXXXXXNNNNNNVVRRVVVVVVVVQQLGGGGUUUUUUUUUUUUUUUJJJJJJJJJJJX");
            AddMapRow(66, ref map, "XEEEEEEEEEEEFFFFFFRRRRRRRRRROOOOODDDDDDDDDDXXXXXWWXCCCCCCCCCWWWWWVVVVVMMMMMMMCCQXXXXXXXXNNNNNVVVVVVVVVVVVVQJJJGGGJJDDDDDDDUUUUUUJJJJJJJJJXXX");
            AddMapRow(67, ref map, "EEEEEEEEEEEEFFFFFFRVRRRRRRRROOOOOODDDDDDDXXXXXXXWWCCCCCCCCCCCWCWWWWVVMMMMMMMCCXXXXXXXXXNNNNNNVVVVVVVVVVVVVJJJJJJJJJDDDDDDDUUUUUUMJJJJJJXXXXX");
            AddMapRow(68, ref map, "BEEEEEEEEEEFFFFFFFFFZRRRRVRROOOOOODDDDDDIWXXXXXXXWCCCCCCCCCCCCCCWWWWVMMMMMMMMMSXXXXXXXTTNNNVVVVVVVVVVVVVVVWWWJJJJJJDDDDDDDDDMMMMMJMXJJJXXXXX");
            AddMapRow(69, ref map, "EEEEEEEEEEFFFFFFFFFFZZRRRRRROOOAOODDDDDDWWXXXXWWWWWWCCCCCCCCCCCWWWWWVMMGGGMMMSSXXXLLLLTTNNVVVVVVVVVVVVVVVVWQQQQJJJJDDDDDDWDDMMMMMXXXDJJXXXXX");
            AddMapRow(70, ref map, "EEEEEELLEEFFFFFFFFFFZZZZZRZROODDDDDDDDDDWWXWWWWWWWUCCCCCCCCVVVVVVVWVVWWGGGMMMSSCXXLLLLLTNNVVVVVVVVVVVVVVVVWQQQQJJJJDDDDDDDBBMXXXMXXDDJXXXXXX");
            AddMapRow(71, ref map, "OEEEYYYLEFFFFYFFFFFFZZZZZZZZOEDDDDDDDDDWUWWWWWWWWWWCCMCCCCCVVVVVVVWWWWWGGGMSSSSLLLLLLLTTTTTVVTVVVVVVVVVVVVVQQQQQQJJJJDDDDBBBBXXXXXXXXJXXXXXX");
            AddMapRow(72, ref map, "OOOOYYYYFFFYYYFYFFFZZZZZZZZZOEDDDDDDDDDWWWWWWWWWWWWMMMCWCCCVVVVVVVWWWWWQGGSSSSSLLLLLLTTTTTTTTTVVVTVVVCVUUQQQQQQJJJJJIIDDUBBBXXXXXXXXXJJXXXXX");
            AddMapRow(73, ref map, "OVVOOYYYYYYYYYYYFFFZZZZZZZZZZEDDDDDDDDDWUWWWWWWWWWWWWWWWLCCVVVVVVVWWWWWGGGSSSSSSSSLLLLTTTTTTTTTTTTCCVVVCUUQQQQQQQJJJIIDDUUBBBXXXXXXXXXXXXXXX");
            AddMapRow(74, ref map, "OVVOOOYYYYYYYYYYYZFZZZZZZZZZZEDDDDDDDDDDDWWWWWWWWWWWWWWWWCVVVVVVVVWWWWWWGGSQSSSSLLLLLLTWTTTTTTTTTTCKCCCCUUUQQQQPJJJJJIUUUUBBBBXXXXXXXXXXXXXX");
            AddMapRow(75, ref map, "VVBVVVVVYYYYYYYYYZZZZZZZZZZZZZZZZDDDDDDZDWWWWWWWWWWWWWWWWVVVVVVVVVWWWWWWWGSQQQLLLLLLLTTTTTTTTTTTCCCCCCCCCCUQQQQJJJJJJJUUUBBBBBXXXXXXXXXXXXXX");
            AddMapRow(76, ref map, "KVVVVVVVYYYYYYYYYZZZZZZZZZZZZZZZZDZDSSZZZWWWWWWWWWWWWWWWVVVVVVVVVVWWWWWWLLOQQQQQQLLLLTTTTTTTTTTCCCCCCCCCCUUUQJJJJJJJUUUUUUUBBBBMMXXXXXXXXXXX");
            AddMapRow(77, ref map, "VVVVVVVVYYYYYYYYYYZZZZZZZZZZZZZZZDZZSSSZZZZZWWWWWWWWWWWWVVXVVVVVVVVVWWWLLLQQQQLLLLLLLLTTTTTTTTCCCCCCCCCCCUUUUUJJUUJJUUUUUUUUBBBBMMXQXXXXXXXX");
            AddMapRow(78, ref map, "VVVVVVVVYYYYYYYYZYYYZZZZZZZZZZZZZZZZZZZZZZZZZWWWWWWLWLWLLVXXXVVVVVVVWWWLLLLQQQQLLLLLTTTTTTTTWTTCCCCCCCCCCCCCUUJUUUUUUUUUUUBBBBBBMQQQXQQQQXXX");
            AddMapRow(79, ref map, "VVVVVVVYYYYYYYYYZZZZZZZZZZZZZZZZZZZZZZZZZZZZZWWZWWWLLLLLLVXXXVVVVVVVVVVVLLLLLLLLLLLLTTTTTTTTTTTLLCCCCCCCCCCCUUUUUUUUUUUUUVVBFBBOQQQQQQQQQQQQ");
            AddMapRow(80, ref map, "VVVVVVVYYYYGYYYYYZZQZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZLLLLLLXXXXVVVVVVVVVVVDDLLLLLLLLLTTTTTTTTTTTTLLCCCCCCCCCCCUUUUUUUUUFUFVVVFFCBOQQQQQQQQQQQQ");
            AddMapRow(81, ref map, "VVGVVVVGYYGGYYYYYZQQQQQZZZZZZZZZZZZZZZZZZZZZZZZLZZLLLLLLLXXXXXVVVVVVVVVVDDDLLLLLLLTTTTTTTTTTTTTUURCCCCCCCCUUUUUUUUUUUFFFFFFFYQQQQQQQQQQQQQQQ");
            AddMapRow(82, ref map, "GGGGGVGGYGGYYYYYZZQQQQQQQZQZZZZZZZZZZZZZZZZZZZZLLLLLLLLLLXXXXXVVVVVVVVVVDDLLLLLLLLTTTTTTTTTTTUUUUCCCCCCCCCCCUUUUUUUUUUFFFFMFYQQQQQQQQQQQQQQQ");
            AddMapRow(83, ref map, "GGGGGGGGGGGGGZZZZZZQQQQQQQQQZZZZZZZZZZZZZZZZZZLLLLLLLLLLLLXXXXVVVVVVVVVVVVVLLLLLLLTTTTWCJTUUUUUUUCCCCCCCCCCCUUUUUUUUUFNFFFFFFQFQQQQQQQQQQQQQ");
            AddMapRow(84, ref map, "GGGGGGGGGGGGGZZZZQQQQQQQQQQQZZZZZZZZZZZZZZZZZZLLLLLLLLLLLXXXXXVVVVVVVVVVVVVDLLLLLTTCCCCCCCCCUUUUUPZPCCCCCCCCUUUUUUUUFFFFFEFFFFFQQQQQQQQQQQQQ");
            AddMapRow(85, ref map, "GGGGGGGGGGGZZZZZZZQQQQQQQQQQZZZZEEEZJJJJJZZZZLLLLLLLLLLLLXXXXXXXVVVVVVVVVVVLLLLLCCCCCCCCCCCCCUUUPPPPPCCCCCCCCUUUUUUUUUFFFFFFFFFFQQQQQYQQQQQQ");
            AddMapRow(86, ref map, "NGGGNNGGGGZZWWWZZZQQQQQQQQQOTTTTREEEJJJJJJZZZZLLLLLLLLLLLXXXXXXDVVVVVVVVVVVLLLLCCCCCCCCCCCCCCUPPPPPPPCCCCCCCCUUUUUUUUUFFFFFFFFFFYYYYYYQQQQQQ");
            AddMapRow(87, ref map, "NNNNNNGGGGGZWWWZZZTTQQQQQQQQTTTTRRREEJJJJJJJZLLLLLLLLLLLXXKXXXXXVVVVVVVVVVVLLLLCCCCCCCCCCCCFCPPPPPPPLCCCCCCCUUUUUUUCUUFFFFFFFFXYYYYYYYYQQQQQ");
            AddMapRow(88, ref map, "NNNNNGGGGGWWWWWWZZZQQQQQQQQQTTTTTTTTTJJJJJJZZLLLLLLLLLOOXXKXXXXXXVVVVVVVVVVLTTCCCCCCCCCCCCPPPPPPPPPPWWWWWCCCCSSSUSSCCCCCCFFFFFYYYYYYYYNYQXXU");
            AddMapRow(89, ref map, "NNNNNGGGGGWWWWWWZZZQQQQQQQQQQTTTTTTTTTJJJJJJZLLLLLLLLLLOXKKKXXXXXVVVVDDDTLLTTTCCCCCCCCCCCCPPPPPPPPPPWWWWWCCJJSSSSSSSSCCCCFFFFFFYYYYYYYYYXXXU");
            AddMapRow(90, ref map, "NNNNNNNGGGWWWWWWZMMQQQQQQQQQQTTTTTTTRRMEJOJZZZZZLLLLLKKOKKKKXXXXKDDDDDDTTTTTTROCCCCCCCCCCCCSSPPPPPPPWWWWWJJJJJSSSSSSSCUUUUFFFFYYYYYYYYYYUXXU");
            AddMapRow(91, ref map, "NNNNNNNNGGGWWWWWWMQQQQQQQQQQTTTTTTTTTTMMJJZZZZZZKLKKKKKKKKKRRKKXKWWDDDDDTTTTRRRRCCCCCCCCCCCMSPTPPWWWWWWWWJJJJJNSSSSSSSUUUUUYYYYYYYYYYYYYUUUU");
            AddMapRow(92, ref map, "NNNNNNNGGYGGGGGGWIVQQQQQQQQQTTTTTTTTTTMMZZZZZZZZKKKKKKKKKKKKKKKKKKWDDDTTTRTRRRRRCCCCCCCCCCMMMMMPPWWWWWWWWWWWWWWNSSSSSSSUUUUWUUYYYYYYYYYZUUUU");
            AddMapRow(93, ref map, "NNNNNNOGGYYYYGGGIIIIHIQQQQQQTTTTTTTTTTMMMMZZZZZZKKKKKKKKKKKKKKKKKWWWGGTTTRRRRRRRCCCCCCCCMMMMVVVVPWWWWWWWWWWWWWWNSSSSSSSSUUUUUUUYYYYYYYYUUUUU");
            AddMapRow(94, ref map, "NNNNHHOYYYYYYGGGIIIIIIQQITTTTTTTTTTBMMMMMMMZZZZZKKKKKKKKKKKKKKVVGWGGGGTTGRRRRRRRCCCCCCCMMMMMMMMVVWWWWWWWWWWWWWWSSSSYSSSSUUUUUUUUYYYYYHHHHUUH");
            AddMapRow(95, ref map, "NNNOOOOOOOYYYYYGIIIIIIIIITTTTTTTTTMMMMMMMMMMZZZZZMKKKKKKKKKKKKVVGGGGGGGGGHRRRRRCCCCCCCCMMMMMMMMMWWWWWWWWWWWWWWWNCCYYYLYUUUUUUUUUYYYYNHHHUUHH");
            AddMapRow(96, ref map, "NNNNOOOOOOYYJJIIIIIIIIIIIJTTTEEETTTMMMMMMMMMMMMMZMKKKKKKKKKKKKGVGGGGGGGGGHHHRRCCHHCCCCCMMMMMMMMMWWWWWWWWWWWWWWWNNCCYYLYUUUUUUUUYYYYYHHHHHUHH");
            AddMapRow(97, ref map, "NNNNOOOOOOOYJJIIIIIIIIIEJJEEEEECEHMMMMMMMMMMMMMMMMMKKKKKKKKVVVVVVVVVVVVGGHHHHHHHHHHHCCCMMMMMMMMMWWWWWWWWWWWWWWWNCCCYXYYYYUUUUUPYYYHHHHHHHHHH");
            AddMapRow(98, ref map, "NNNNNOOOOOOOIIIIIIIIIIIEJJEEEEEEEHHMMMMMMMMMMMMMMMMKKKKKKKFVVVVVVVVVVVVGGHHHHHHHHHHHCCMMMMMMMMMMWWWWWWWWWWWWWWWYCCYYYYYYYUUUUUUPPPHHHHHHHHHH");
            AddMapRow(99, ref map, "NNNNOOOOOOOOIIIIIIIIIIIEJJEEEEEEHHHHMMMMMMMMMMMYMMMKKKKKKSFFVVVVVVVVVVVGGGHHHHHHHHHHHCCMMMMMMMMMWWWWWWWWWWWWWWWYYYYYYYYYYUUUUUUUPPPHHHHHHHHH");
            AddMapRow(100, ref map, "OOOOOOOOOOOOOIIIIIIIIEEEEEEEEEEEEHHMMMMMMMMMGMYYMMKKKKKKKSFFVVVVVVVVVVVGGNGHHHHXXXXXXXMMMMMMMMMVWWWWWWWWWWYKPYYYYYYYYYPYYUUUPPPPPPPPPHHHHHHH");
            AddMapRow(101, ref map, "IOOOOOOOOOOOIIIIIIIIIIIEEEEEEEEEEHHHMMMMMMMMMMYYYMKKKKKKSSFFVVVVVVVVVVVGGXXXXXXXXXXXXXMMMMMMMMVVVWWWWWWWWWKKKKKYYYYYYNNNNPPUPPPPPPPPPPHHHHHH");
            AddMapRow(102, ref map, "IOOOOOOOOOOOIIIIIIIIIEEEEEEEEEEEEEHHHMMMMMQMMMYMYMMTKKKKSFFFVVVVVVVVVVVGGXXXXXXXXXXXXXMMMMMMMCVVVWWWWWWWWWKKGYYYYYYYNNNPPPPPPPPPPPPPPPPPHHHH");
            AddMapRow(103, ref map, "IIIOOOOOOOOOIIIIIIIIIMEEEEEEEEEEEEHHHHMMMMMMMMMMMMMKKKKKFFFFFFVVVVVVVVVGGXXXXXXXXXXXXXXXXXMMMCVVVWWWWWWWWWKKKYYYYYNNNNNPNNPNPPNPPPPPPPPPPHHH");
            AddMapRow(104, ref map, "IIIIOOOOOOOOIIIIIIIIIMEYEEEEEEEEEEEHHHMHMMMMMMMMMMMKKKKFFFFFFFVVVVVVVVVGGXXXXXXXXXXXXXXXXXMMMVVVVWWWWWWWWWKKKKKYYYNYYNNNNDNNPPNNNNPPPPOPPHHP");
            AddMapRow(105, ref map, "IIEIOOOOOOIIIIIIIIMMMMMMMEEEEEEQEEEHHHHHHMYMMMMMMMMMMKKZFFFFFFVVVVVVVVVGGXXXXXXXXXXXXXXXXXXMMMVVVWWWWWWWWWKKKKKYYYYYYNNNNNNNNNNNNNPPPOOPPPHP");
            AddMapRow(106, ref map, "IIEEOEOOAOOOIIIIIIIMMMMMMEEEEEEEEEEHHHHHIMYYYMMMMMMMMMMFFFFFFFFVVVVVVGGGEXXXXXXXXXXXXXXXXXXMMMVVVWWWWWWWWWKKKKKYYYYYVNNNNNNNNNNNNNPPPOOOOPPP");
            AddMapRow(107, ref map, "IIEEEEEOOOOOIIIMMMMMMMMMMEEEEEEEEEEHHHHHHHYYYMAMMMMMMMMFFFFFFFFVVVVVVGNNNEBJJJJJXXXXXXXXXXXXXMVMVWWWWWWWWWKKKKYYYYYYYTNNNNNNNNNNNPPPPPOOPPPP");
            AddMapRow(108, ref map, "IIIIEEEEOOOIIIIMMMMMMMMMMEEEEEEQEEHHHHHHHHHYYMMMMMMMMMMMMFFFFFFFFOOONNNNNNBJJJJJJJJXXXXXXXXXXMMMVVVVVVKKKPKKKKTTTYYTTTFTNNNNNNNNNNPPPPPPPPPP");
            AddMapRow(109, ref map, "IIIEEEEEEEEEYYYMMMMMMMMMMEEQEEEEHHHHHHHHHHHHHMMMMMMMMMMMFFFFFFFFFOOONNNNNPBBJJJJJJJXXXXXXXXXXMMMVVOVVVVVKKKKKKTTTYYTTTTTTNNNNNNNNNPPPPPPPPPP");
            AddMapRow(110, ref map, "IILLEEEEEYYYYYYMMMMMMMMMMMEQQEYYHHHHHHHHHHHYYMMMMMMMMMMMFFFFFFOOOOOOONNNNPBBBJJJJJJXXXXXXXXMMMVVVVVVVVVUKKKKOOTTTTTTTTTNNNNNNNNNNNNPPPPPPPPP");
            AddMapRow(111, ref map, "IILLEJJYYYYYYYYMYMMMMMMMMMMMEEYHHYYYYYHHHYHYYYYMMMMMMMMMMFFFOFOOOOOOONNOOBBBBJJJJJYYYYNYYYYYYMVUUVVUUUVUKUSSSSTTTTTTTTTNNNNNNNNNNNNNPPPPPPPP");
            AddMapRow(112, ref map, "IUULLJJYYYYYYYYRYWYMMMMMMMMMMEYYYYYYYYYYHYYYYYMMMMMMMMMMMMFFOOOOOOOOOOOOOYBYYJJJJJYYYYYYYYYYYYYUUUUUUUUUUUSSSSTTSTTTTTTTNNNNNNNNNNHPPPPPPPPP");
            AddMapRow(113, ref map, "LLLLLJLYYYYYYYYYYYYMYMMMMMMMMYYYNYYYYYYYYYYYYYMMMMMMMMMMMMFOOOOOOOOOOOOOYYYYYYYYJJYYYYYYYYYYYYUUUUUUUUUUUSSSSSTSSSTTTTTIINNIIINNNNPPPPPPPPPP");
            AddMapRow(114, ref map, "LLLLLJLYYYYYYYYYYYYYYMMMMMMMMYNNNNNNNYYYYYYYYYYYYYMMMMMMMMOOOOOOOOOOOOOOLYYYYYYYJYYYYYYYYYYYYUUUUUUUUUUUNSSSSSSSSSTTIITIINIIIISINNPPPPPPPPPP");
            AddMapRow(115, ref map, "LLLLLLLYYYYYYYYYYYYYMMOMMMMMMMNNNNNNNYNNYYYYYHYYYYMBMMMMOOOOOOOOOOOOOOOGLYYYYYYYYYYYYYYYYYYYYUUUUUUUUUUUNNNNSSSSSSSSIIIIINNIIIIINNPPPPPPPPPP");
            AddMapRow(116, ref map, "LLLLLLLYYYYYYYYYYMMMMMOOMOMNMCNNNNNNNNNNYYYYYHYYYYBBBMBMBOOOOOOOOOOOOLLLLYYYYYYYYYYYYYYYYYYYYUUUUFFUUUNNZNNNSSSSSBSSIMIIIIIIIIIIPPPPPVPPPPPP");
            AddMapRow(117, ref map, "LLLLLLLLYYYYYYYYMMMMMMOOOOONMMNNNNNNNNNNYYYYYHYYYYYBBBBBBBOJOOOOOOOOOOLLLYYYYYYYYYYYYYYYYYYYUUUUUUFFFFFNNNNASSSSSSSMMMMIIIIIIIIIXPPPPPPPPPPP");
            AddMapRow(118, ref map, "LLLLLLLYYYSSSSSSSMMMOOOOOOONNNNNNNNNNNTYYYYYHHYBBBBBBBBNNNNOOOOOOOLLLLLLLLYYYYYYYYYYYYYYYYYUUUUUUUFFFFFNNNNNSSSSSSSMMMMMMMMMIIIIPPPPPPPPPPPP");
            AddMapRow(119, ref map, "LLLLLLLYYYSSSSSSSMMMOOOOOONNNNNNNRRRNNYYYYYHHHHHBBBHBBBNVNNNOOOOOLLLLLLLLLLYYYYYYYYYYYYYYYUUBBBBFFFFFFFNNNNNRRRSSSSMMMMMMMMMMDDDDPPLPPPPPPPP");
            AddMapRow(120, ref map, "LLLLLLLLYYSSSSSSSXMXOOOOOOOONNNNNNRZRIIYYYHHHHHHBHHHHHHHHNNNNLOOOLLLLLLLLLLLYYYYYYYYYYYOOYYYZBBBBFFFFFFNNRRRRRRRSSNMMMMMMMMMMMMMPPIIPPPPPPPP");
            AddMapRow(121, ref map, "LLLLLLLLYYSSSSSSSXXXOOOOOOOOOONNNRRRRRYYYYHHHHHHHHHHHHHHHHNNNLOLLLULLLLLLLLYYYIYYYYYYYYOOOOYBBBBBBBFFYYNRRRRRRRNNNNMMMMMMMMMMMMMMIAIPPPPPPPP");
            AddMapRow(122, ref map, "LLLLLLLLYYSSSSSSSSSSSOOOOOOOOONNNRRRRRDDDDHWWJJJHHHHHHHHHHNTTLLLLLLLLLLLLLLLLIIYYLYYYYBOOOBBBBBBBBBRRRRRRRRRRRRRNNNMMMMMMMMMMMMIIIIIIIIPPPPP");
            AddMapRow(123, ref map, "LLLLLLRLYYSSSSSSSSSSSXXOOOOOONNNNRRRRRBDDWWWWWJJHHHHHHHHHHVVVLLLLLLLLLLLLLLLIIILLLLYYYBBBBBBBBBBBBBBRRRRRRRRRRRRNNMMKKMMMMMMMMMMIIIIIIIIIPPP");
            AddMapRow(124, ref map, "BBLLLYYYYYSSSSSSSSSSSXXOXOOOONNNNRRRRRRFFFFWWWJJJWHHHHHHBBVVVVVLLLLLLLLLLLLIIIIILLLYYYBBBBBBBBBBBBBBBRRRRRRRRRRRRNMMKKKKMMMMJMIIIIIIIIIIIIKP");
            AddMapRow(125, ref map, "BBLLMYYYYYSSSSSSSSSSSXXXXXVOOONNNRRRRRRRRRFWWJJWWWHHHHHVBBVVVWWWWWLLLLLLLLLIIIIILLYYYYYBBBBBBBBBBBBBBBRRRRRRRRRRRRKKKKKMMMMJJJIIIIIIIIIIIIKP");
            AddMapRow(126, ref map, "BMLLYYYYYYSSSSSSSSSSSXXXXVVONNNNNNFRRRRRRRWWWWWWWWHHHHHVVVVVVVVVLLLLLLLLLLLLLIIILLLLYYKBBBKBBBBBBBBBBBRRRRRRRRRZZRKKKKKMMMMJJJJJIIIIIIIIIIKK");
            AddMapRow(127, ref map, "MMMMMYYYYYSSSSSSSSSSSXVVVVVVNNNNNFFRRRRRRRWWWWWWWWHHHHVVVVVVVVVRLKKLQLUUULLUIILLLLLLYKKBBKKKBBBBBBBBRBRRRRRRIRRREKKKKKKMMMJJJKJIIIIIIIIIIIKK");
            AddMapRow(128, ref map, "MMMMMMMMYYVVSSSSSSSSSVVVVVVVNNNNFFFFRRRRVWWWWWWWWWUUUUVVVVVVVVVRLUKLQLUUUUUUIILZZZZZKKKKKKKKKBBBBBBBBBSQRRRREREEEKKKKKKMMMJXJKJIIIIIIIIIIKKK");
            AddMapRow(129, ref map, "MMMMMMMMYMMVSSSSSSSSSVVVVVVVVNNFFFFRRRRRRRWWWWWWWUUUUUUVVVVVVVUUUULLLUUUUUUUUUUUZZZZZZZKOKKKKKBBBBBBBSSQQEEEEEEEEEKKKKKKMMUJJJJJIIIIIIIIKKKK");
            AddMapRow(130, ref map, "MMMMMMMMMMMVVXXPSSSSSXVVVVVVVVFFFTTTRRRRRRRWWWWWWWUUUUUVVVVVVVVVUUUUUUUUUUUUUUUUUZZZZZZOOKKKKKJBBBBHHSHQEEEEEEEEEEEEKKJJJJUJJJJJIIIIIIIKKKKK");
            AddMapRow(131, ref map, "MMMMMMMMMMMVXXXXSSSSSAVVVVVVVVVFFFFFRRNNRNNWWWWWWUUUUVVVVVVVVVVVVVTUUTTTUUDDUUUUUZZZOZOOOOKKJJJBBBBHHHHQEEEEEEEEEEEEKKJJJJJJJJJJIIIIIIIKKKKK");
            AddMapRow(132, ref map, "MMMMMMMMMMMMMAAXSSSSSAVVVVVVVNNNFFFFFNNNNNTTKWWWWWUUUVVVVVVJVVVVVTTTTTTUUDDDDUUUUZZZOOOOOOKKKJHHKBBHEHEEEEEEEEEEEEEKKKKJJJJJJJJIIIIIIFKKKKKK");
            AddMapRow(133, ref map, "DMMMMMMMMMMMMMAASSSSSAZZZVVVVNNNFFFFNNNNTTFTTTDDDWUUVVVVVVVJVVVVTTTTTTTUUUDDDUUZZZZZOOOOOOKKJJHHHBHHEEEEEEEEEEEEEEEEEKJJJJJJJJJNIIIBIFFKKKKK");
            AddMapRow(134, ref map, "MMMMMMMMMMMMMMAAAAAAZAAZZNNNNNNNFFFNNNNNTTTTTTTDDWWUVVVVVVVJVVVVTTTTTTUUUUDDDDUZZZZZBBOOOOOOJJHHHHHEEEEEEEEEEEEEEEEJJJJJJJJJJIJJJIIBYFYKKKKK");
            AddMapRow(135, ref map, "MMMMMMMMMMMMMAAAAAZZZZZZZNNNNNNNNNNNNNNNTTTTTTTDDWUUJJJJJJJJVVVVVTTTTTUUUUUUBBBBZZBBBBBOBBJJJJHHHHHHEEEEEEEEEEEEBEECJJJJJJJJJJZZJIYBYYYKKKKK");
            AddMapRow(136, ref map, "CMMMMMMMMMMMMMAAAAZZZZZZNNNNNNNNNNNNNNNTTTTTTTTDDJJJJJJJJJJJVVVVVVVOTUUUUUUUUBBBBBBBBBBBBBJJHHHHHHHHEEEEEEEEEEEEEQEEQQQJJJJJJJZZZYYYYYYYKKKK");
            AddMapRow(137, ref map, "CMMMMMMMMMAAAMAAAAZZAZZZZNNNNNNNNNNTTNNTTTTTTTVTDDDJJJJJJJJJJVVVVVBBBBUUUUUUUBBBBBBBBBBBBEHHHHHHHHHHHHHEEEEEEEEEQQQQQQQQQJJJJJZZZYYYYYYMMKMM");
            AddMapRow(138, ref map, "CCCMMMLLLMMAAAAAAAAZAZZZZNNNNNNNNNNNTTTTTTTTTTTTTTJJJJJJJJUJVVVVVVBBBBUUUUUUUUUBBBBBBBBBBBHHHHHHHHHHHHHHEREEEEEEEQQQQQQQQQJQJJZZZYYYYYYYMMMM");
            AddMapRow(139, ref map, "CCCCMLLLAAAAAAAAAAAAAZZZZZNNNNNNNNNNTTTTTTTTTTTTJTJJJJJJUUUUVVVVVBBBBBUUUUUUUUUBBBBBBBBBHHHHVHHHHHHHHHHHHHEAAEEEEQQQQQQQQQQQJZZZZZYYYYYYMMMM");
        }

        void DumpMap(ref char[,] map)
        {
            for (var y = 0; y < mapSize; y++)
            {
                for (var x = 0; x < mapSize; x++)
                {
                    Console.Write(map[y, x]);
                }
                Console.WriteLine();
            }
        }
    }

    class Region
    {
        public char plant { get; set; }

        public int originX { get; set; }
        public int originY { get; set; }

        public int area { get; set; }
        public int perimiter { get; set; }
    }
}
