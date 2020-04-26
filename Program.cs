using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using static System.IO.File;

namespace haiku_generator
{
    class Stanza
    {
        public int Syllables;

        public List<string> Words;

        public Stanza()
        {
            Syllables = new int();
            Words = new List<string>();
        }

        public void AddWord(string word, int syllables)
        {
            Syllables += syllables;
            Words.Add(word);
        }

        public void MakeStanza(Dictionary<string, string> dictionary, int count)
        {
            var rand = new Random();

            while (Syllables < count)
            {
                var (word, value) = dictionary.ElementAt(rand.Next(0, dictionary.Count));
                var syllables = short.Parse(value);

                if (Syllables + syllables <= count) 
                    AddWord(word, syllables);
            }
        }

        public override string ToString()
        {
            return string.Join(" ", Words.ToArray());
        }
    }

    static class Program
    {
        private static void Main()
        {
            var dictionary =
                ReadAllLines(@"/Users/fischer/Programming/rider/haiku-generator/source.txt")
                    .Select(x => x.Split(' '))
                    .ToDictionary(x => x[0], x => x[1]);

            var stanza1 = new Stanza();
            stanza1.MakeStanza(dictionary, 5);

            var stanza2 = new Stanza();
            stanza2.MakeStanza(dictionary, 7);

            var stanza3 = new Stanza();
            stanza3.MakeStanza(dictionary, 5);

            WriteLine(stanza1);
            WriteLine(stanza2);
            WriteLine(stanza3);
        }
    }
}