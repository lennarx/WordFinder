// See https://aka.ms/new-console-template for more information

using Qubeyond.WordFinder;

var matrix = new List<string>
{
    { "adsgadsgadsfgadfghsfdghdfgjwh" },
    { "asdgsdfhdfgnfghmghkjmsdfhdfoh" },
    { "dfgasdgasdgasdfgsdfgsdfgsdfrg" },
    { "aasdfgsdfhgdfghdfgjtukjiuyodi" },
    { "gsgsdfghdfyhnmyimopopsdfhbdig" },
    { "adgsdfhgsfhjuhoujoijgoijijfsf" },
    { "uieooauiehjdlpukllusiueoekkfn" },
    { "afjdglksjgiumabmiuuiasiouroro" },
    { "bombastickhjalsjkdhgkjanrojgn" },
    { "klajskljdfyeyunomkladjgklsdjf" },
    { "kljsadflkhjsdflkhjhorafdagsdf" },
    { "lknjolijouinlbjinlkuixhjouaae" }

};

var wordFinder = new WordFinder(matrix);

var wordsToSearch = new List<string>
{
    { "bombastic" },
    { "hora" },
    { "gmh" },
    { "frgjda" },
    { "bmiuu" }
};

var wordsFound = wordFinder.Find(wordsToSearch);

Console.WriteLine(wordsFound);
