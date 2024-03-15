using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    string sirCititDinFisierMain = CitesteFisier("C:\\Users\\crist\\Desktop\\PriectLFC\\Proiect\\Gramatici.txt");
                    string sirGeneratDinFisier = GenerareSirCuProductii(sirCititDinFisierMain);
                    if (!string.IsNullOrEmpty(sirGeneratDinFisier))
                    {
                        Console.WriteLine("Sirul generat este: " + sirGeneratDinFisier);
                    }
                    Console.WriteLine();
                    Console.Write("Alegeti alta optiune: ");
                    break;

                case "2":
                    Console.WriteLine("Introduceti secventa de productii: ");
                    string secventaProductii = Console.ReadLine();
                    Console.WriteLine("Secventa introdusa este: " + secventaProductii);
                    string sirGeneratDeLaTastaturaMain = GenerareSirCuProductii(secventaProductii);
                    if (!string.IsNullOrEmpty(sirGeneratDeLaTastaturaMain))
                    {
                        Console.WriteLine("Sirul generat este: " + sirGeneratDeLaTastaturaMain);
                    }
                    Console.WriteLine();
                    Console.Write("Alegeti alta optiune: ");
                    break;

                case "3":
                    exit = true;
                    break;

                default:
                    if (OptiuneValida(optiune))
                    {
                        Console.WriteLine("Optiune valida. Alegeti alta optiune: ");
                    }
                    else
                    {
                        Console.WriteLine("Optiune invalida. Alegeti alta optiune: ");
                    }
                    break;
            }
        }

        Console.WriteLine();
    }

    public static void CitireSiGenerareSirCuProductii(string caleFisier)
    {
        string sirCititDinFisierMain = CitesteFisier(caleFisier);
        GenerareSirCuProductii(sirCititDinFisierMain);
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

    public static string GenerareSirCuProductii(string input)
    {
        StringBuilder sirGenerat = new StringBuilder();

        try
        {
            string[] subSiruri;
            bool hasStartSymbol = false;

            if (input == null)
            {
                Console.WriteLine("Sirul este gol.");
                return "";
            }
            else
            {
                subSiruri = input.Split('$');

                if (subSiruri.Length == 1 && string.IsNullOrEmpty(subSiruri[0]))
                {
                    Console.WriteLine("Sirul este gol.");
                    return "";
                }
            }

            if (input.Contains("@") && Char.IsUpper(input[0]) && input.EndsWith("&"))
            {
                hasStartSymbol = true;
            }
            else
            {
                if (!input.Contains("@"))
                {
                    Console.WriteLine("Simbolul '@' lipseste din sir.");
                }
                if (!Char.IsUpper(input[0]))
                {
                    Console.WriteLine("Primul simbol nu este o litera mare.");
                }
                if (!input.EndsWith("&"))
                {
                    Console.WriteLine("Simbolul de final nu este '&'.");
                }
                return "";
            }

            HashSet<char> VN = new HashSet<char>();
            HashSet<char> VT = new HashSet<char>();

            bool firstSubsir = true;

            Random random = new Random();
            string variabilaCurenta = subSiruri[0][0].ToString();

            foreach (string productie in subSiruri)
            {
                if (!sirGenerat.ToString().Equals("") && !firstSubsir)
                {
                    sirGenerat.Append("$");
                }

                sirGenerat.Append(variabilaCurenta + productie.Substring(1));

                variabilaCurenta = productie.Substring(1);
                firstSubsir = false;
            }

            sirGenerat.Append("&");

            string rezultatFiltrat = new string(sirGenerat.ToString().Where(char.IsLetter).ToArray());

            return rezultatFiltrat;
        }
        catch (Exception e)
        {
            Console.WriteLine("Eroare la generarea sirului:");
            Console.WriteLine(e.StackTrace);
            return "";
        }
    }

    public static bool OptiuneValida(string optiune)
    {
        // Verifica daca optiunea este un numar intreg pozitiv
        if (int.TryParse(optiune, out int result) && result > 0)
        {
            return true;
        }
        return false;
    }
}