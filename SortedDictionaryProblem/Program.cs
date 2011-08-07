using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortedDictionaryProblem {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Starting good test");
            goodTest();
            Console.WriteLine();
            Console.WriteLine("Starting problematic test");
            problematicTest();
            Console.ReadLine();
        }

        private static void goodTest() {
            SortedDictionary<IComparable, string> good = new SortedDictionary<IComparable, string>();
            Pair first, second, third;

            first = new Pair(1, 1);
            second = new Pair(1, 2);
            third = new Pair(1, 3);

            good.Add(first, "First value");
            good.Add(second, "Second value");
            good.Add(third, "Third value");

            printValueInDictionary(good, first);
            printValueInDictionary(good, second);
            printValueInDictionary(good, third);
        }

        private static void problematicTest() {
            SortedDictionary<IComparable, string> problematic = new SortedDictionary<IComparable, string>();
            ProblematicPair problematicFirst, problematicSecond, problematicThird;

            problematicFirst = new ProblematicPair(1, 1);
            problematicSecond = new ProblematicPair(1, 2);
            problematicThird = new ProblematicPair(1, 3);

            problematic.Add(problematicFirst, "First value");
            problematic.Add(problematicSecond, "Second value");
            problematic.Add(problematicThird, "Third value");

            printValueInDictionary(problematic, problematicFirst);
            printValueInDictionary(problematic, problematicSecond);
            printValueInDictionary(problematic, problematicThird);
        }

        private static void printValueInDictionary(SortedDictionary<IComparable, string> dictionary, IComparable key) {
            try {
                Console.Write("Searching for "+key+", result: ");
                Console.WriteLine(dictionary[key]);
            } catch(KeyNotFoundException exp) {
                Console.WriteLine("the key was not found");
            }
        }
    }
}
