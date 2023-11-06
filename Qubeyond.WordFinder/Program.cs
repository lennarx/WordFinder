// See https://aka.ms/new-console-template for more information

using Qubeyond.WordFinder;

var matrix = new List<string>
{
    { "Caverna" },
    { "Taberna" },
    { "Palerma" },
    { "Alarmas" }

};

var wordFinder = new WordFinder(matrix);

var wordsToSearch = new List<string>
{
    { "Alarma" },
    { "Jaja" },
    { "Caverna" },
    { "Alarma" },
    { "AJC" }
};

var wordsFound = await wordFinder.Find(wordsToSearch);

Console.WriteLine(wordsFound);
