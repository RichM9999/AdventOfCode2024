﻿//https://adventofcode.com/2023/day/3
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023
{
    class Day3
    {
        char[,] schematic;
        const int schematicSize = 140;
        const string nonSymbols = ".0123456789";

        public Day3()
        {
            schematic = new char[schematicSize, schematicSize];
        }

        public void Run()
        {
            var start = DateTime.Now;

            GetData();

            Regex test = new(@"(\d{1,3})");

            long sum = 0;

            for (var row = 0; row < schematicSize; row++)
            {
                var line = string.Empty;

                for (var col = 0; col < schematicSize; col++)
                {
                    line += schematic[col, row];
                }

                var matches = test.Matches(line);

                foreach (Match match in matches)
                {
                    var pos = match.Index;

                    if (IsPartNumber(row, pos, match.Value))
                    {
                        int.TryParse(match.Value, out var partNumber);
                        sum += partNumber;
                    }
                }
            }

            Console.WriteLine($"Sum of all part numbers: {sum}");
            //527364
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            sum = 0;

            for (var row = 0; row < schematicSize; row++)
            {
                var line = string.Empty;

                for (var col = 0; col < schematicSize; col++)
                {
                    line += schematic[col, row];
                }

                for (var pos = 0; pos < schematicSize; pos++)
                {
                    if (schematic[pos, row] == '*')
                    {
                        sum += GearRatio(row, pos);
                    }
                }
            }

            Console.WriteLine($"Sum of gear ratios: {sum}");
            //79026871
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        bool IsPartNumber(int row, int pos, string value)
        {
            for (var x = pos - 1; x <= pos + value.Length; x++)
            {
                for (var y = row - 1; y <= row + 1; y++)
                {
                    if (x >= 0 && x < schematicSize && y >= 0 && y < schematicSize)
                    {
                        var c = schematic[x, y];
                        if (!nonSymbols.Contains(schematic[x, y]))
                            return true;
                    }
                }
            }

            return false;
        }

        int GearRatio(int row, int pos)
        {
            Regex test = new(@"(\d{1,3})");

            List<int> adjacentPartNumbers = [];

            var above = string.Empty;
            var left = string.Empty;
            var right = string.Empty;
            var below = string.Empty;

            // Above
            if (row > 0)
            {
                var startAbove = pos;
                var endAbove = pos;

                if (int.TryParse(schematic[startAbove, row - 1].ToString(), out _))
                {
                    if (startAbove > 0)
                    {
                        if (startAbove == schematicSize - 1  || !int.TryParse(schematic[startAbove + 1, row - 1].ToString(), out _))
                        {
                            if (int.TryParse(schematic[startAbove - 1, row - 1].ToString(), out _))
                            {
                                startAbove--;
                            }

                            if (startAbove > 0 && int.TryParse(schematic[startAbove - 1, row - 1].ToString(), out _))
                            {
                                startAbove--;
                            }
                        }
                        else
                        {
                            if (startAbove > 0 && int.TryParse(schematic[startAbove - 1, row - 1].ToString(), out _))
                            {
                                startAbove--;
                            }

                            if (pos < schematicSize - 1 && int.TryParse(schematic[pos + 1, row - 1].ToString(), out _))
                            {
                                endAbove++;
                            }

                            if (pos + 1 < schematicSize - 1 && int.TryParse(schematic[pos + 2, row - 1].ToString(), out _))
                            {
                                endAbove++;
                            }
                        }
                    }
                }
                else
                {
                    if (startAbove > 0)
                    {
                        startAbove--;
                        if (startAbove > 0 && !int.TryParse(schematic[startAbove, row - 1].ToString(), out _))
                        {
                            startAbove = pos;
                        }
                        else if (startAbove > 0 && int.TryParse(schematic[startAbove - 1, row - 1].ToString(), out _))
                        {
                            startAbove--;
                            if (startAbove > 0 && int.TryParse(schematic[startAbove - 1, row - 1].ToString(), out _))
                                startAbove--;
                        }
                    }

                    if (!int.TryParse(schematic[pos + 1, row - 1].ToString(), out _))
                    {
                        endAbove = pos;
                    }
                    else
                    {
                        endAbove = pos + 1;

                        if (endAbove < schematicSize - 1 && int.TryParse(schematic[endAbove + 1, row - 1].ToString(), out _))
                        {
                            endAbove++;

                            if (endAbove < schematicSize - 1 && int.TryParse(schematic[endAbove + 1, row - 1].ToString(), out _))
                            {
                                endAbove++;
                            }
                        }
                    }
                }

                for (var col = startAbove; col <= endAbove; col++)
                {
                    above += schematic[col, row - 1];
                }

            }

            // Left
            if (pos > 0)
            {
                var leftPos = pos;
                if (int.TryParse(schematic[leftPos - 1, row].ToString(), out _))
                {
                    leftPos--;
                }
                if (leftPos > 0 && int.TryParse(schematic[leftPos - 1, row].ToString(), out _))
                {
                    leftPos--;
                }
                if (leftPos > 0 && int.TryParse(schematic[leftPos - 1, row].ToString(), out _))
                {
                    leftPos--;
                }

                for (var col = leftPos; col < pos; col++)
                {
                    left += schematic[col, row];
                }
            }

            // Right
            if (pos < schematicSize - 1)
            {
                var rightPos = pos;
                if (int.TryParse(schematic[rightPos + 1, row].ToString(), out _))
                {
                    rightPos++;
                }
                if (rightPos < schematicSize - 1 && int.TryParse(schematic[rightPos + 1, row].ToString(), out _))
                {
                    rightPos++;
                }
                if (rightPos < schematicSize - 1 && int.TryParse(schematic[rightPos + 1, row].ToString(), out _))
                {
                    rightPos++;
                }

                for (var col = pos + 1; col <= rightPos; col++)
                {
                    right += schematic[col, row];
                }
            }

            // Below
            if (row < schematicSize - 1)
            {
                var startBelow = pos;
                var endBelow = pos;

                if (int.TryParse(schematic[startBelow, row + 1].ToString(), out _))
                {
                    if (startBelow > 0)
                    {
                        if (startBelow == schematicSize - 1 || !int.TryParse(schematic[startBelow + 1, row + 1].ToString(), out _))
                        {
                            if (int.TryParse(schematic[startBelow - 1, row + 1].ToString(), out _))
                            {
                                startBelow--;
                            }

                            if (startBelow > 0 && int.TryParse(schematic[startBelow - 1, row + 1].ToString(), out _))
                            {
                                startBelow--;
                            }
                        }
                        else
                        {
                            if (startBelow > 0 && int.TryParse(schematic[startBelow - 1, row + 1].ToString(), out _))
                            {
                                startBelow--;
                            }

                            if (pos < schematicSize - 1 && int.TryParse(schematic[pos + 1, row + 1].ToString(), out _))
                            {
                                endBelow++;
                            }

                            if (pos + 1 < schematicSize - 1 && int.TryParse(schematic[pos + 2, row + 1].ToString(), out _))
                            {
                                endBelow++;
                            }
                        }
                    }
                }
                else
                {
                    if (startBelow > 0)
                    {
                        startBelow--;
                        if (startBelow > 0 && !int.TryParse(schematic[startBelow, row + 1].ToString(), out _))
                        {
                            startBelow = pos;
                        }
                        else if (startBelow > 0 && int.TryParse(schematic[startBelow - 1, row + 1].ToString(), out _))
                        {
                            startBelow--;
                            if (startBelow > 0 && int.TryParse(schematic[startBelow - 1, row + 1].ToString(), out _))
                                startBelow--;
                        }
                    }

                    if (!int.TryParse(schematic[pos + 1, row + 1].ToString(), out _))
                    {
                        endBelow = pos;
                    }
                    else
                    {
                        endBelow = pos + 1;

                        if (endBelow < schematicSize - 1 && int.TryParse(schematic[endBelow + 1, row + 1].ToString(), out _))
                        {
                            endBelow++;

                            if (endBelow < schematicSize - 1 && int.TryParse(schematic[endBelow + 1, row + 1].ToString(), out _))
                            {
                                endBelow++;
                            }
                        }
                    }
                }

                for (var col = startBelow; col <= endBelow; col++)
                {
                    below += schematic[col, row + 1];
                }

            }


            test.Matches(above).ToList().ForEach(m =>
            {
                int.TryParse(m.Value.ToString(), out var number);
                adjacentPartNumbers.Add(number);
            });
            test.Matches(left).ToList().ForEach(m =>
            {
                int.TryParse(m.Value.ToString(), out var number);
                adjacentPartNumbers.Add(number);
            });
            test.Matches(right).ToList().ForEach(m =>
            {
                int.TryParse(m.Value.ToString(), out var number);
                adjacentPartNumbers.Add(number);
            });
            test.Matches(below).ToList().ForEach(m =>
            {
                int.TryParse(m.Value.ToString(), out var number);
                adjacentPartNumbers.Add(number);
            });

            if (adjacentPartNumbers.Count == 2)
            {
                return adjacentPartNumbers[0] * adjacentPartNumbers[1];
            } 
            else
            {
                return 0;
            }
        }

        void AddRow(int row, string data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                var space = data[i];
                schematic[i, row] = space;
            }
        }

        void GetData()
        {
            var row = 0;

            AddRow(row++, "...................15....904...........850.................329...................13....................................871....816....697....");
            AddRow(row++, "...........53.497........................%....906...610.......*.............735#..&...*......558...68...............68..*......&....*.......");
            AddRow(row++, "..........*....$....................132.........*..........844....875................350............*...............*..336.364...649........");
            AddRow(row++, ".......726.......341..................*...186...358..................*244........57.......@.........738......*.....663.................584..");
            AddRow(row++, ".............952.*......33......660..704............949......................518*....234.967....551........971..&.......................*...");
            AddRow(row++, ".......738...*....222......................706.......*..825.............474%...........*...........*.405.........779..............542...405.");
            AddRow(row++, ".74.........366....................192..........542.737....*760...................623/..730.....718.../.....................$17......%......");
            AddRow(row++, "...*126.....................%........*.504...=..*.........................................................974...............................");
            AddRow(row++, "........331/..901.........337..........*...461.698...............*461.....814............................*............975...............165.");
            AddRow(row++, "..../.........*.....................262.......................313........*........530.56.....567.......897.....*.........*.9................");
            AddRow(row++, "....953....355...@703..................................609..............462.......%........./...............108.557...501.../...724.........");
            AddRow(row++, "..............................................=.........%.......46......................533....670...............................*..%630....");
            AddRow(row++, ".........................91...382*204.........154..%.............+.524.995..............=.....*...........7..........692.92*56...73.........");
            AddRow(row++, ".685....*....70.189.......*.........................773...830/....../....*........=.........565.............464................$....738..130");
            AddRow(row++, ".........938..*........993..............*.................................243...340...753...........*...882...............108.647....*......");
            AddRow(row++, "..............762..........348.......280...............364*526...........................*........552.....=....25..682....*.........164.....");
            AddRow(row++, ".....948..............813../....644......62........................................#.....657......................*.........................");
            AddRow(row++, "......*...........................*.................$745.......739.....*...399...30............&..@...........924.117..........309..........");
            AddRow(row++, "...883....544.....33.........585=.428.146................288.....*..853....*...........*891.409....429..460...*..........997....*........187");
            AddRow(row++, ".............*818........829..........*....846..............................248.....574..................*....789.......*.....965....-..*...");
            AddRow(row++, "......=.....................*..........370.......850+.............313...........................573....621...............268.......58..800..");
            AddRow(row++, "....67....287.............481..709...............................*.......#74...................*.................965........................");
            AddRow(row++, "............@.291.............*.........607*....950.475..309...66..932........395..374..........155.891....472..*......774.............&....");
            AddRow(row++, "....207..........%..262......543..887/......711....*.......+......*............*...*........823............*...93...........377.....764.....");
            AddRow(row++, ".....*...968.951.......#................................$.........453...366..736.972........*.............697........*618...&...............");
            AddRow(row++, "....984..*...............-.......................884..23......492.....*..*................70..523.....25........$.....................370...");
            AddRow(row++, "..................870...187.......548#.522..458.....*..........*....956......535....../.........*.525*........197....@..*266...123.....*....");
            AddRow(row++, ".........501.....*..........................&.....323........257........956..*........80..946.592.................211.........*.........311.");
            AddRow(row++, ".....380*.....867...653..347....................*........93=...........*......515..........-...................................678..........");
            AddRow(row++, "..........876......&....*.......&631.950.647...631...@...........=...705..914................739..............319......713.........*97.#918.");
            AddRow(row++, ".....805...*..........118................-.........790....559..51........../........*.........*......550......*........./.......647.........");
            AddRow(row++, "......-..428.....599......813................413..................280............840.654.......91.....*...........236.......$...............");
            AddRow(row++, ".............=...%.......*.....77...................#.492............................................96..........@.......175....%...........");
            AddRow(row++, "......*427..569..........47......................999......314.....636*253.................764.................27......-..........415..386...");
            AddRow(row++, "...142................$................@..320........969...................882*311...............#..751...........=....649...........*......");
            AddRow(row++, ".........*723..993.976....638........475................=..800.......................*764.710...574....*964....831..........473......531....");
            AddRow(row++, "...%..486...................*....#..................................831...........375........*........................=824......21..........");
            AddRow(row++, "559.........................441...316................................./....................618.407.......83.......972...............91......");
            AddRow(row++, "......*.....25..741...................%...............*63.546$..905......27........861.756........*.......*..........*...28...57............");
            AddRow(row++, ".......794....*./........782...236....47...........937...........*..755....*505..............23.948.......560.532...421....*..#.........405.");
            AddRow(row++, "............566.................*........-850..872.......90....229...+..........978..684............225..................675.....591...*....");
            AddRow(row++, ".............................296..243...........+....691...................311.........$......321......@......827....$13.......$....&..530..");
            AddRow(row++, ".....664..422$....613.....@.........%.401/.$974.....-.........%.145....729......*........./.......106............%.......=.....611..........");
            AddRow(row++, ".......$.........*.......857...147*.............591........+.46..*.....=........306.67*...818.............................261....../........");
            AddRow(row++, "............823...785..............340.........-....564..777......456.......%....................891.................560...........642..181.");
            AddRow(row++, ".....394.....*................226..........423..............................539.................*............640&.............535...........");
            AddRow(row++, ".......@.....626..317..329....*...92........+..................374....827............*.683+....771....529.........303..........*............");
            AddRow(row++, ".........172.....-........*......*.....332...........288..229..+.........*.786=....468.............*....*...........*....461...249..=.......");
            AddRow(row++, "....974...*............901..@....130..*..............$...*..........4.573................163......429....808.464*47..231....@......964......");
            AddRow(row++, "........183.................582........11...............310.....774.*.....567.............*....36...........................................");
            AddRow(row++, ".......................939.....................*826............@.....892..........481......266.*...-253.........106.830....235......*..841..");
            AddRow(row++, "727.........177#.........&.......507........301..........................550..242*.....+./.....686.......@.446.*.....*.......*....894..*....");
            AddRow(row++, "....67...............468.....&......&..596......................817.600...............85.5.............973...$.805...513......378.......388.");
            AddRow(row++, "...........*247.........*.....974.........*263....329....476....*............721............723.....................................358.....");
            AddRow(row++, "........784.....434.....667.......................*............615.195.612...............................540#........347...........*........");
            AddRow(row++, "...............*.................76......121...659........@555..........-.............201......................866............628.505.509...");
            AddRow(row++, ".............515........../.................#....................281..%...656............*670.419.......799.......*...510.786...*.......&...");
            AddRow(row++, "....993.................540.373....709.............705............*...132..#........298$......*...232....*..@..878.......*....528...#.......");
            AddRow(row++, "....$.........615.............*..........948..........*........565................%..........465..#.....497.67....................631.......");
            AddRow(row++, ".........138....#....417......123...........*.......546...821...................114..=............................258.859=.380-.............");
            AddRow(row++, ".....734*.............../.............833..399..........................+............780...288..............117..@...................925....");
            AddRow(row++, "................*860..........228........*...............................956....390*........=..........%...*..........839...373......*......");
            AddRow(row++, "..802........369........641.............763.....154.24...............302............620..............150.403.......*.........*....647.......");
            AddRow(row++, "...................................&........466*.....@..................*827....530.............................129.628.....923.........=866");
            AddRow(row++, "..........&......&.............824.573.................669.........................$.....374..627...........698.....................759.....");
            AddRow(row++, ".......499....%.812.857*653...................668..........879.......974................../......*926...852.........................-....832");
            AddRow(row++, "...646......284..............708...369...........*808................-.......705.......@...............%.............492.462............*...");
            AddRow(row++, "....................959......#.......*....872..........................102....*.........80..839....996..............*.....%........83...49..");
            AddRow(row++, ".785............363..*.............49.......*.......667.....797....224.......586..............&....*...34..673.......650....................");
            AddRow(row++, "....*..*132....*.....225...................631..350..*.........*....*.............941....=.......52....*.................406...546..........");
            AddRow(row++, ".543............760.........305..325...330.......+.................621...........*.......63..#.......268..239...................#...........");
            AddRow(row++, "........658..................*............*...........735......820.....145....419...........91..489.........*......404.......*....#.........");
            AddRow(row++, "..........=....415+.....38$....844...+..366.............*.......*......*..............................43....945.....#.....563.205..211..183.");
            AddRow(row++, "......267.......................*...519.....730........836..........277...........808...........504...&................................#....");
            AddRow(row++, ".....*..................705/....575..........*....../..........$866.........=.......*...975......#.......183.931........%.......611.........");
            AddRow(row++, ".......135.......284........................246.653.972..............365..191.....376...*.....&.........*......*.........994...@.....11.....");
            AddRow(row++, "...440../.........*.............................*..............415.....................985..838......677.......109...................*......");
            AddRow(row++, "...@............224....399.727$......68#..........................$......432.....214$.....................#.................591....686......");
            AddRow(row++, "..........&.......................................658........143....779.+....942......$.............885....567..........$...................");
            AddRow(row++, ".461.......33....277.407......108..............47.*.............*...........*.......$.811............................390....................");
            AddRow(row++, "..........................907*.....#397..307...*....197.12.....3...738....254.85..393...............713..700..741...........176.....883.....");
            AddRow(row++, "..316........180...........................*.758...................-...........................................*............*...............");
            AddRow(row++, "....*..../........871...338..165.........683............455...701*......................540....255.......$....858........277....28....+.....");
            AddRow(row++, "...660.469..912...#..........%......743.........439.....-.........206......363..........*.......*.......640............................144..");
            AddRow(row++, "..............*.........443...............610.............803.............*.............879...658............$................189...........");
            AddRow(row++, "...........172......794........98..............646......................662..............................*....49.-210.523.257....*..........");
            AddRow(row++, "...87..662.....#.......*667....%..408..........*............220...524............=.....742..39..160....64.636..........*..*....974..378.....");
            AddRow(row++, "...*.....*.....42..&.............=....371*.....804.................*..#....../.240.....*........................395%..461..818.....*........");
            AddRow(row++, "....706.398........452....500.............406..........57*380....365.42...635.......131...........18.....538.....................403........");
            AddRow(row++, "..............143........*.........80............740..........................*245......784........-...........708...................&.297..");
            AddRow(row++, ".230...........*.......373..341...........................$438.......*.................=.............874&..959*...........192......829.#....");
            AddRow(row++, "...#..........476..647.....*......................................431.728..........................................50.....$.................");
            AddRow(row++, "........334...........*731.930......=........966........*323...................301.....%....404...............879....*372.....208.134.......");
            AddRow(row++, "...........*.....................163..........*......509......................%.....&..785.......................*...............*..........");
            AddRow(row++, "...768..329....199........................................797......*368..........873..........26.565....651.....664......111..........217...");
            AddRow(row++, "................*.....26.......705*76....415.............*................................749..........*..................#......545-..%....");
            AddRow(row++, "....-....473..517......*...............5*....449..........68........................751.......*399......401.................................");
            AddRow(row++, ".730......#.........240...&..65.718.87......*.....532..............-....567............*....................675.......67..463...............");
            AddRow(row++, ".....289#....509........200......*...*....852....................885...+....663..72..507......998.&954..213....$.....*.........672..........");
            AddRow(row++, "..............................403.....129........377-....................../....&.................................473...........*...........");
            AddRow(row++, ".........855=.........250...................961........860*916.......981..........326.........633..404................676.%566..120.........");
            AddRow(row++, "892...........532.............754.......866*....790..................#........819*..............*.........%.44..=......*............29......");
            AddRow(row++, "....@527.......*..................600............../...$568.....800.....................179..528........613.....358...949...................");
            AddRow(row++, "................302...807............*..895.................+.....@.....-...........................980...........................736.......");
            AddRow(row++, "........217...=......*............879.....+.-..........%.....793.......442.......422............209*.......&.......969....240.378...*.......");
            AddRow(row++, "....340*.....145...%.596....................23....703...217................372..........610.................129.......*........../.....=....");
            AddRow(row++, ".................790.....&............904............=............795......*.......385=...+.....88*143...............891....406.......79....");
            AddRow(row++, "............*749.........31..............*....318.................+.......317.................................%419.............*392.........");
            AddRow(row++, "....854..797......*..500...............614.1...*..............803.....451............330.159.143.598.......+......./827...............717...");
            AddRow(row++, "...@............924.*........................245............-.&..........*...606................*...........461............604....#....*....");
            AddRow(row++, "....................73....160..406.150...............933.957...................%.......419.........665..*.........-.......*.....721..573....");
            AddRow(row++, "....*915.........40................*...%.............*........584.......929......377....*..404.935....&..870.....93........395..............");
            AddRow(row++, ".781.............@...........632.......710....769.323............$.........*.........904.....*...*.....................859..................");
            AddRow(row++, ".....=.......................*...646............-.........................52................149...414....................*........%.........");
            AddRow(row++, "...51....524.625..........328...*.................................295.734......@..........................856.291.695....367..218..787..8...");
            AddRow(row++, ".........../.*................339........................576/.417*...........445...@........................%....................-..........");
            AddRow(row++, "....869.85...316.....308...............345.......749.............................264.............775..777...............................89..");
            AddRow(row++, ".......*...............#.........$....*..........*....../.....786.713*52.................357.......&./.........#......971....74.305....-....");
            AddRow(row++, "............................325..770..167..@.....265..+..883................................+...57..........312.......*........*............");
            AddRow(row++, "......+.....266.....788.111....*..........342........588.......939..393*717.............488....*....................401....249...255........");
            AddRow(row++, ".......308..*............#.....816....465..........*.....492..@.............581.........-.......397..709*.................$.....*...........");
            AddRow(row++, ".............511.....881...+.........+..........178..524*..............150..../..720.......843...........964....129.../......908............");
            AddRow(row++, ".......749.............*.526.$............800............................*.........*...500....*.../.................487..........200........");
            AddRow(row++, "..668......*639......932......139.726................#...51..436$..................775...*...834..874.......%..............859+......439....");
            AddRow(row++, "........810........................*.......805.......100..*...........999.....743......169............477...961......973............$.......");
            AddRow(row++, ".574...........6*262........398....204......*.....%.......525...........*.........................412..*.........376...*.@43....=......=....");
            AddRow(row++, "....*836....................................25...619............658.....172.......................*...408...........*........%...776..802...");
            AddRow(row++, ".............307.......&........537..988..............128..-.....*..........................118...327......296.......967..991...............");
            AddRow(row++, ".....899...$...*....788.....829......=....................559.722.......519.........385$...+..................*.323*..............*123......");
            AddRow(row++, "........*.930...674...........*..........447.......801..............957....................................111......767..34....556..........");
            AddRow(row++, "..565.644..............18.....472.........*...8........................*........258........901.................296.......*..........*.......");
            AddRow(row++, "....*............995=....*................649.*...151.........437%...445.747......-.........&......#......%.....*.........800....357.240....");
            AddRow(row++, "....476.491/.931.......121..732*940..682......975..$.....*..................+.358.............../.363....152.264........$...................");
            AddRow(row++, ".............=....730*..................*584...........234.........996.................*.....701........................499............649..");
            AddRow(row++, "......................672......@.............................958.....&.+...........983.673........233.@981........760.......................");
            AddRow(row++, "..942.993@......293...........939..&....@.........867..92.....*........679.........................*.............*.......27...998...........");
            AddRow(row++, "....#.......230.$...................36.24....809..#....*...660.....$.......748..199.-.............717...@......................../..........");
            AddRow(row++, "......*95..............647...$..............-.........461..........757...........*...68...............728..210*680..708......$.....246......");
            AddRow(row++, "...355.........*..........*.538...%..............................-...............977....209*.......................*.........141.....*......");
            AddRow(row++, "...............680.....670.........784........171..799.........317..........................844........166........289.................463...");
        }
    }
}