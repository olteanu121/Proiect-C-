using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class MainClass
{
    public static void Main(string[] args)
    {
        bool exit = false;

        Console.WriteLine();
        Console.WriteLine("Meniu:");
        Console.WriteLine("1. Citire din fisier.");
        Console.WriteLine("2. Citire de la tastatura.");
        Console.WriteLine("3. Iesire.");
        Console.WriteLine();
        Console.Write("Alegeti optiunea: ");

        string optiune;

        while ((optiune = Console.ReadLine()) != "3" && !exit)
        {
            switch (optiune)
            {
                case "1":
                    string sirCititDinFisierMain = CitesteFisier("C:\\Users\\crist\\Desktop\\PriectLFC");
                    AnalizeazaSir(sirCititDinFisierMain);
                    Console.WriteLine();
                    Console.Write("Alegeti alta optiune: ");
                    break;

                case "2":
                    Console.WriteLine("Introduceti gramatica:");
                    string gramaticaCititaDeLaTastatura = Console.ReadLine();
                    AnalizeazaSir(gramaticaCititaDeLaTastatura);
                    Console.WriteLine();
                    Console.Write("Alegeti alta optiune: ");
                    break;

                case "3":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Optiune invalida. Alegeti alta optiune: ");
                    break;
            }
        }

        Console.WriteLine();
    }

    public static string CitesteFisier(string caleFisier)
    {
        StringBuilder stringBuilder = new StringBuilder();

        try
        {
            using (StreamReader streamReader = new StreamReader(caleFisier))
            {
                string linie;

                Console.WriteLine("Continutul fisierului este: ");

                while ((linie = streamReader.ReadLine()) != null)
                {
                    Console.WriteLine(linie);
                    stringBuilder.Append(linie);
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Eroare la citirea fisierului:");
            Console.WriteLine(e.StackTrace);
        }

        return stringBuilder.ToString();
    }

    public static void AnalizeazaSir(string input)
    {
        string[] subSiruri;
        bool hasStartSymbol = false;

        if (input == null)
        {
            Console.WriteLine("Gramatica este vida.");
            return;
        }
        else
        {
            subSiruri = input.Split('$');

            if (subSiruri.Length == 1 && string.IsNullOrEmpty(subSiruri[0]))
            {
                Console.WriteLine("Gramatica este vida.");
                return;
            }

            if (subSiruri.Any(string.IsNullOrEmpty))
            {
                Console.WriteLine("Una dintre productii este goala.");
                return;
            }
        }

        if (input[0] == '&')
        {
            Console.WriteLine("Simbolul de start lipseste din gramatica.");
            return;
        }

        if (input.EndsWith("&"))
        {
            hasStartSymbol = true;
        }
        else
        {
            Console.WriteLine("Simbolul de final nu este '&'.");
            return;
        }

        HashSet<char> VN = new HashSet<char>();
        HashSet<char> VT = new HashSet<char>();

        bool firstProduction = true;

        foreach (string subSirOriginal in subSiruri)
        {
            string subSir = subSirOriginal.Trim();

            if (!Char.IsUpper(subSir[0]))
            {
                Console.WriteLine("Una din productii nu incepe cu litera mare.");
                return;
            }

            if (subSir.Length <= 2)
            {
                Console.WriteLine("Fiecare productie trebuie sa contina cel putin 3 caractere");
                return;
            }

            if (!firstProduction)
            {
                Console.Write("; ");
            }

            Console.Write(subSir[0] + "->" + subSir.Substring(1));
            firstProduction = false;

            for (int i = 0; i < subSir.Length; i++)
            {
                char character = subSir[i];

                if (char.IsUpper(character))
                {
                    VN.Add(character);
                }
                else
                {
                    VT.Add(character);
                }
            }
        }

        Console.WriteLine();

        if (input.Length > 0 && hasStartSymbol)
        {
            Console.WriteLine("Simbolul de start este: " + input[0]);
        }

        Console.Write("VN = { ");
        bool firstVN = true;
        foreach (char c in VN)
        {
            if (!firstVN)
            {
                Console.Write(", ");
            }
            Console.Write(c);
            firstVN = false;
        }
        Console.Write(" } ");
        Console.WriteLine();

        Console.Write("VT = { ");
        bool firstVT = true;
        foreach (char c in VT)
        {
            if (!firstVT)
            {
                Console.Write(", ");
            }
            Console.Write(c);
            firstVT = false;
        }
        Console.Write(" } ");
        Console.WriteLine();
    }
}