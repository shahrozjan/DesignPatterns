// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;
using System.IO;

namespace DesignPatterns.SOLID
{
    //Single Responsibility Principle
    //Deals with the fact each class should do too much e.g. 
    //Journal should only have add and remove, load and save should be under another class
    public class Journal
    {
        private readonly List<string> entries = new List<string>();

        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; // memento pattern!
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        // breaks single responsibility principle
        public void Save(string filename, bool overwrite = false)
        {
            File.WriteAllText(filename, ToString());
        }
    }

    // handles the responsibility of persisting objects
    public class Persistence
    {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, journal.ToString());
        }
    }

    public class Demo
    {
        public static void Runner()
        {
            var j = new Journal();
            j.AddEntry("I cried today.");
            j.AddEntry("I ate a bug.");
            WriteLine(j);

            // var p = new Persistence();
            // var filename = @"c:\temp\journal.txt";
            // p.SaveToFile(j, filename);
        }
    }
}
