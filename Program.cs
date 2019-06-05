using System;
using System.Collections.Generic;
using System.Linq;

namespace PianoExam
{
    class Program
    {
        private static Random _rand = new Random();

        private static IList<string> _appregios = (new List<(string, int)>
        {
            ("A major", 1),
            ("G minor", 1),
            ("B minor", 1),
            ("B flat major", 1),
            ("B major", 1),
            ("E flat major", 1),
            ("C minor", 1)
        }).AsStrings().ToList();

        private static IList<string> _scales = (new List<(string, int)>
        {
            ("A major", 1),
            ("E major", 1),
            ("B major (Cross Smoothly)", 1),
            ("B flat major", 1),
            ("E flat major", 1),
            ("B minor", 1),
            ("C minor", 1),
            ("G minor", 1)
        }).AsStrings().ToList();

        private static IEnumerable<string> _hands = (new List<string>
        {
            "L hand",
            "Both hands",
            "R hand",
            "Both hands"
        }).AsPaddedRight();

        private static List<int> _octaves = new List<int>
        {
            2,
            3,
            4
        };

        static void Main(string[] args)
        {
            SayHi();
            Console.WriteLine();
            Console.WriteLine("Press any key to start!");
            Console.ReadKey();
            Console.WriteLine();

            var toPlayAppreagio = "Apreggio";
            var toPlayScale = "Scale".PadRight(toPlayAppreagio.Length);

            var maxLength = Math.Max(_appregios.Max(s => s.Length), _scales.Max(s => s.Length));

            var toPlay = new List<string>(_appregios.AsPaddedRight(maxLength: maxLength).Select(a => $"{toPlayAppreagio} | {a}"));
            toPlay.AddRange(_scales.AsPaddedRight(maxLength: maxLength).Select(s => $"{toPlayScale} | {s}"));
            toPlay = Shuffle(toPlay).ToList();

            var octavesHandsCombinations = new List<string>();
            foreach (var hand in _hands)
            {
                foreach (var octave in _octaves)
                {
                    octavesHandsCombinations.Add($"{hand} | {octave} octaves");
                }
            }
            octavesHandsCombinations = Shuffle(octavesHandsCombinations).ToList();

            int almostDoneNumber = (int)(toPlay.Count * 0.9);
            var randoms = GetRandN(1, almostDoneNumber, 3);
            var pikachyNumber = randoms[0];
            var bunnyNumber = randoms[1];
            var snailNumber = randoms[2];
            for (var i = 0; i < toPlay.Count; i++)
            {
                var number = $"{i + 1}.";
                var line = $"| {number.PadRight(3)} | {toPlay[i]} | {octavesHandsCombinations[i % octavesHandsCombinations.Count]} |";
                if (i == 0)
                {
                    Console.WriteLine("-".PadRight(line.Length, '-'));
                }

                Console.WriteLine(line);

                var splitLine = "-".PadRight(line.Length, '-');
                Console.WriteLine(splitLine);
                var key = Console.ReadKey();

                if (i == pikachyNumber)
                {
                    Console.WriteLine(PikachySays("Please, no mistakes :)"));
                    Console.WriteLine(splitLine);
                }
                else if (i == bunnyNumber)
                {
                    Console.WriteLine(BunnySays("You are doing good, keep it up!"));
                    Console.WriteLine(splitLine);
                }
                else if (i == snailNumber)
                {
                    Console.WriteLine(SnailSays("Do not rush, take your time..."));
                    Console.WriteLine(splitLine);
                }
                else if (i == almostDoneNumber)
                {
                    Console.WriteLine(DogSays("Almost Done!"));
                    Console.WriteLine(splitLine);
                    // Set it to 0 so it woun't match any more.
                    almostDoneNumber = 0;
                }
            }

            SayAllDone();
        }

        private static IEnumerable<T> Shuffle<T>(IEnumerable<T> collection)
        {
            return collection.OrderBy(x => _rand.NextDouble());
        }

        private static void SayAllDone()
        {
            var allDone = @"
 $$$$$$\  $$\ $$\       $$$$$$$\                                $$\ 
$$  __$$\ $$ |$$ |      $$  __$$\                               $$ |
$$ /  $$ |$$ |$$ |      $$ |  $$ | $$$$$$\  $$$$$$$\   $$$$$$\  $$ |
$$$$$$$$ |$$ |$$ |      $$ |  $$ |$$  __$$\ $$  __$$\ $$  __$$\ $$ |
$$  __$$ |$$ |$$ |      $$ |  $$ |$$ /  $$ |$$ |  $$ |$$$$$$$$ |\__|
$$ |  $$ |$$ |$$ |      $$ |  $$ |$$ |  $$ |$$ |  $$ |$$   ____|    
$$ |  $$ |$$ |$$ |      $$$$$$$  |\$$$$$$  |$$ |  $$ |\$$$$$$$\ $$\ 
\__|  \__|\__|\__|      \_______/  \______/ \__|  \__| \_______|\__|

";
            Console.Write(allDone);
        }

        private static void SayHi()
        {
            var greeting = @"
$$\   $$\ $$\           $$\    $$\                                                              $$\ 
$$ |  $$ |\__|          $$ |   $$ |                                                             $$ |
$$ |  $$ |$$\           $$ |   $$ | $$$$$$\   $$$$$$\  $$\    $$\  $$$$$$\   $$$$$$\   $$$$$$\  $$ |
$$$$$$$$ |$$ |          \$$\  $$  | \____$$\ $$  __$$\ \$$\  $$  | \____$$\ $$  __$$\  \____$$\ $$ |
$$  __$$ |$$ |           \$$\$$  /  $$$$$$$ |$$ |  \__| \$$\$$  /  $$$$$$$ |$$ |  \__| $$$$$$$ |\__|
$$ |  $$ |$$ |            \$$$  /  $$  __$$ |$$ |        \$$$  /  $$  __$$ |$$ |      $$  __$$ |    
$$ |  $$ |$$ |$$\          \$  /   \$$$$$$$ |$$ |         \$  /   \$$$$$$$ |$$ |      \$$$$$$$ |$$\ 
\__|  \__|\__|$  |          \_/     \_______|\__|          \_/     \_______|\__|       \_______|\__|
              \_/                                                                                   
";
            Console.Write(greeting);
        }

        private static string PikachySays(string speach)
        {
            var pikachy = @"
 ( )( )
(>*.*<) {0}
 ("")("")
";
            return string.Format(pikachy, speach);
        }
        private static string BunnySays(string speach)
        {
            return @"
   _     _
   \`\ /`/
    \ V /               
    /. .\   " + speach + @"      
   =\ T /=                  
    / ^ \     
 {}/\\ //\
 __\ "" "" /__           
(____/^\____)
";
        }

        private static string DogSays(string speach)
        {
            return @"
,-.___,-.
\_/_ _\_/
  )O_O(
 { (_) }  " + speach + @"
  `-^-'
";
        }

        private static string SnailSays(string speach)
        {
            return @"
o       o
 \_____/   " + speach + @"
 /=O=O=\     _______ 
/   ^   \   /\\\\\\\\
\ \___/ /  /\   ___  \
 \_ V _/  /\   /\\\\  \
   \  \__/\   /\ @_/  /
    \____\____\______/
";
        }

        private static int[] GetRandN(int min, int max, int count)
        {
            var rand = new Random(379);
            return Enumerable.Range(min, max).Select(n => rand.Next(min, max)).Distinct().Take(count).ToArray();
        }
    }


    static class CollectionExtensions
    {
        public static IEnumerable<string> AsPaddedRight(this ICollection<string> strings, char padCharacter = ' ', int maxLength = 0)
        {
            if (strings == null)
            {
                yield break;
            }

            maxLength = Math.Max(maxLength, strings.Max(s => s.Length));
            foreach (var s in strings)
            {
                yield return s.PadRight(maxLength, padCharacter);
            }
        }

        public static IEnumerable<string> AsStrings(this IEnumerable<(string, int)> stringInts)
        {
            foreach (var strInt in stringInts)
            {
                for (var i = 0; i < strInt.Item2; i++)
                {
                    yield return strInt.Item1;
                }
            }
        }
    }
}