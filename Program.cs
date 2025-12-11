using System;

namespace BibliotecaDemo
{
    public class Program
    {
        public static string[] titols = new string[100];
        public static string[] autors = new string[100];
        public static int[] anys = new int[100];
        public static int comptador;

        public static void Main(string[] args)
        {
            int opcio;
            bool sortir = false, isOpcio;

            Console.WriteLine("=== SISTEMA DE GESTIÓ DE BIBLIOTECA ===");
            Console.WriteLine();

            // Afegir alguns llibres de prova
            AfegirLlibre(ref titols, ref autors, ref anys, ref comptador, "El Quixot", "Cervantes", 1605);
            AfegirLlibre(ref titols, ref autors, ref anys, ref comptador, "1984", "Orwell", 1949);
            AfegirLlibre(ref titols, ref autors, ref anys, ref comptador, "", "", 0);

            while (!sortir)
            {
                MostrarMenu();

                do
                {
                    isOpcio = Int32.TryParse(Console.ReadLine(), out opcio);
                } while (!isOpcio);
                ProcessarOpcio(opcio, ref sortir);
            }

            Console.WriteLine("Adéu!");
        }

        public static void MostrarMenu()
        {
            Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
            Console.WriteLine("1. Mostrar tots els llibres");
            Console.WriteLine("2. Cercar llibre per títol");
            Console.WriteLine("3. Afegir llibre nou");
            Console.WriteLine("4. Estadístiques");
            Console.WriteLine("5. Sortir");
            Console.Write("Tria una opció: ");
        }

        public static void ProcessarOpcio(int opcio, ref bool sortir)
        {
            bool isAny;
            string titol;

            do
            {
                switch (opcio)
                {
                    case 1:
                        MostrarTotesLlibres(titols, autors, anys, comptador);
                        break;
                    case 2:

                        Console.Write("Introdueix el títol a cercar: ");
                        titol = Console.ReadLine()!;
                        int posicio = CercarLlibre(titols, comptador, titol);

                        if (posicio >= 0)
                        {
                            Console.WriteLine($"\nLlibre trobat:");
                            Console.WriteLine($"Títol: {titols[posicio]}");
                            Console.WriteLine($"Autor: {autors[posicio]}");
                            Console.WriteLine($"Any: {anys[posicio]}");
                        }
                        else
                        {
                            Console.WriteLine("Llibre no trobat.");
                        }

                        break;
                    case 3:
                        Console.Write("Títol: ");
                        titol = Console.ReadLine()!;
                        Console.Write("Autor: ");
                        string autor = Console.ReadLine()!;
                        Console.Write("Any: ");

                        do
                        {
                            isAny = Int32.TryParse(Console.ReadLine(), out int any);
                            AfegirLlibre(ref titols, ref autors, ref anys, ref comptador, titol, autor, any);
                        } while (!isAny);
                        break;
                    case 4:

                        MostrarEstadistiques(titols, autors, anys, comptador);
                        break;
                    case 5:

                        sortir = true;
                        break;
                }
            } while (opcio < 1 && opcio > 5);
        }

        public static void AfegirLlibre(ref string[] titols, ref string[] autors, ref int[] anys, ref int comptador, string titol, string autor, int any)
        {



            if (String.IsNullOrWhiteSpace(titol) || String.IsNullOrWhiteSpace(autor) || any <= 0)
            {
                Console.WriteLine("Error: No es poden afegir dades buides o invàlides.");
            }
            else
            {
                titols[comptador] = titol;
                autors[comptador] = autor;
                anys[comptador] = any;
                comptador++;
                Console.WriteLine("Llibre afegit correctament.");
            }

        }

        public static int CercarLlibre(string[] titols, int comptador, string titolCercat)
        {
            for (int i = 0; i < comptador; i++)
            {
                if (titols[i] == titolCercat)
                {
                    return i;
                }
            }
            return -1;
        }

        public static void MostrarTotesLlibres(string[] titols, string[] autors, int[] anys, int comptador)
        {
            Console.WriteLine("\n=== CATÀLEG DE LLIBRES ===");

            for (int i = 0; i < titols.Length; i++)
            {
                if (i < comptador)
                {
                    Console.WriteLine($"{i + 1}. {titols[i]} - {autors[i]} ({anys[i]})");
                }
            }

            Console.WriteLine($"\nTotal de llibres: {comptador}");
        }

        public static void MostrarEstadistiques(string[] titols, string[] autors, int[] anys, int comptador)
        {
            if (comptador == 0)
            {
                Console.WriteLine("No hi ha llibres per analitzar.");
                return;
            }

            int suma = 0;
            for (int i = 0; i < comptador; i++)
            {
                suma += anys[i];
            }
            double mitjana = (double)suma / comptador;

            int anyMesAntic = anys[0];
            string titolMesAntic = titols[0];

            for (int i = 1; i < comptador; i++)
            {
                if (anys[i] < anyMesAntic)
                {
                    anyMesAntic = anys[i];
                    titolMesAntic = titols[i];
                }
            }

            int anyMesModern = anys[0];
            string titolMesModern = titols[0];

            for (int i = 1; i < comptador; i++)
            {
                if (anys[i] > anyMesModern)
                {
                    anyMesModern = anys[i];
                    titolMesModern = titols[i];
                }
            }

            Console.WriteLine("\n=== ESTADÍSTIQUES ===");
            Console.WriteLine($"Total de llibres: {comptador}");
            Console.WriteLine($"Any mitjà de publicació: {mitjana:F1}");
            Console.WriteLine($"Llibre més antic: {titolMesAntic} ({anyMesAntic})");
            Console.WriteLine($"Llibre més modern: {titolMesModern} ({anyMesModern})");

            ClassificarPerPeriode(anys, comptador);
        }

        public static void ClassificarPerPeriode(int[] anys, int comptador)
        {
            int medieval = 0;
            int renaixement = 0;
            int barroc = 0;
            int segleXIX = 0;
            int segleXX = 0;
            int contemporani = 0;

            for (int i = 0; i < comptador; i++)
            {
                if (anys[i] < 1500)
                {
                    medieval++;
                }
                else if (anys[i] >= 1500 && anys[i] < 1600)
                {
                    renaixement++;
                }
                else if (anys[i] >= 1600 && anys[i] < 1800)
                {
                    barroc++;
                }
                else if (anys[i] >= 1800 && anys[i] < 1900)
                {
                    segleXIX++;
                }
                else if (anys[i] >= 1900 && anys[i] < 2000)
                {
                    segleXX++;
                }
                else
                {
                    contemporani++;
                }
            }

            Console.WriteLine("\nDistribució per períodes:");
            Console.WriteLine($"Medieval: {medieval}");
            Console.WriteLine($"Renaixement: {renaixement}");
            Console.WriteLine($"Barroc: {barroc}");
            Console.WriteLine($"Segle XIX: {segleXIX}");
            Console.WriteLine($"Segle XX: {segleXX}");
            Console.WriteLine($"Contemporani: {contemporani}");
        }
    }
}