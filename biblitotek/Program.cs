using System;
using System.Collections.Generic;
using System.IO;

namespace Bibliotek
{
    // En bok har namn och sånt
    class MinBok
    {
        public string bokNamn;
        public string personSomSkrev;
        public bool ärBortaJustNu;

        // Gör en ny bok
        public MinBok(string n, string p, bool b = false)
        {
            bokNamn = n;
            personSomSkrev = p;
            ärBortaJustNu = b;
        }
    }

    class Program
    {
        // Alla böcker finns här
        static List<MinBok> allaBöcker = new List<MinBok>();
        static string filNamn = "minaBöcker.txt";

        // Detta körs när programmet startar
        static void Main(string[] args)
        {
            Console.WriteLine("*** MITT BOK PROGRAM ***");

            // Försök ladda böcker
            bool laddningsResultat = LaddaBöcker();
            if (!laddningsResultat)
            {
                Console.WriteLine("AJDÅ! Något gick fel!");
            }

            bool kör = true;
            while (kör == true)
            {
                // Visa menyn
                Console.WriteLine("\nVAD VILL DU GÖRA???");
                Console.WriteLine("1. Visa mina böcker");
                Console.WriteLine("2. Lägg till bok i listan");
                Console.WriteLine("3. Hitta nån bok");
                Console.WriteLine("4. Ta en bok");
                Console.WriteLine("5. Ge tillbaks boken");
                Console.WriteLine("6. Ta bort bok för alltid");
                Console.WriteLine("7. Stäng av");
                Console.Write("Skriv nummer här --> ");

                string användarVal = Console.ReadLine();

                // Kolla vad användaren vill göra
                if (användarVal == "1")
                {
                    // Visa böcker
                    SkrivUtBöcker();
                }
                else if (användarVal == "2")
                {
                    // Lägg till bok
                    LäggTillNyBok();
                }
                else if (användarVal == "3")
                {
                    // Hitta en bok
                    HittaBok();
                }
                else if (användarVal == "4")
                {
                    // Låna bok
                    LånaBok();
                }
                else if (användarVal == "5")
                {
                    // Lämna tillbaka
                    GeTillbaka();
                }
                else if (användarVal == "6")
                {
                    // Ta bort bok
                    TaBortBok();
                }
                else if (användarVal == "7")
                {
                    kör = false;
                    Console.WriteLine("HEJDÅ OCH TACK!!!!!");
                }
                else
                {
                    Console.WriteLine("DU GJORDE FEL!!! Välj 1-7 snälla!!");
                }
            }
        }

        // Den här laddar böcker
        static bool LaddaBöcker()
        {
            try
            {
                // Kolla om filen finns
                if (File.Exists(filNamn))
                {
                    // Läs filen
                    string[] allaRader = File.ReadAllLines(filNamn);

                    // För varje rad i filen
                    for (int i = 0; i < allaRader.Length; i++)
                    {
                        string rad = allaRader[i];
                        string[] delar = rad.Split(',');

                        // Kolla att det är rätt antal delar
                        if (delar.Length >= 3)
                        {
                            // Lägg till boken
                            string b = delar[0];
                            string f = delar[1];
                            bool utlånad = false;
                            if (delar[2] == "ja")
                            {
                                utlånad = true;
                            }

                            // Lägg till den
                            MinBok nyBok = new MinBok(b, f, utlånad);
                            allaBöcker.Add(nyBok);
                        }
                    }
                }
                else
                {
                    // Om ingen fil, lägg till exempel-böcker
                    LäggTillExempelBöcker();
                    SparaBöcker();
                }
                return true;
            }
            catch
            {
                Console.WriteLine("AJDÅ DET BLEV FEL MED FILERNA!!!");
                return false;
            }
        }

        // Lägger till några start-böcker
        static void LäggTillExempelBöcker()
        {
            allaBöcker.Add(new MinBok("Liftarens guide till galaxen", "Douglas Adams"));
            allaBöcker.Add(new MinBok("Pippi långstrump", "Astrid lindgren"));
            allaBöcker.Add(new MinBok("A clockwork orange", "Anthony burger"));
            allaBöcker.Add(new MinBok("Bocchi the rock", "Kessoku band"));
            allaBöcker.Add(new MinBok("Symfoni Nr:41", "Mozart"));
            allaBöcker.Add(new MinBok("The god delusion", "Richard dawkins"));
            allaBöcker.Add(new MinBok("år 2001", "Arthur c. clarke"));
            allaBöcker.Add(new MinBok("Anne franks dagbok", "Anne frank"));
            allaBöcker.Add(new MinBok("Papillon", "Henri Charriere"));
            allaBöcker.Add(new MinBok("Sapiens", "Yuval nova harari"));
        }

        // Spara alla böcker till fil
        static void SparaBöcker()
        {
            try
            {
                List<string> allaRader = new List<string>();

                // För varje bok
                for (int i = 0; i < allaBöcker.Count; i++)
                {
                    MinBok b = allaBöcker[i];
                    string utlånadText = "nej";
                    if (b.ärBortaJustNu == true)
                    {
                        utlånadText = "ja";
                    }

                    // Skapa rad
                    string radAttSpara = b.bokNamn + "," + b.personSomSkrev + "," + utlånadText;
                    allaRader.Add(radAttSpara);
                }

                // Spara alla rader
                File.WriteAllLines(filNamn, allaRader);
            }
            catch
            {
                Console.WriteLine("AJDÅ KUNDE INTE SPARA!!!");
            }
        }

        // Visa alla böcker
        static void SkrivUtBöcker()
        {
            if (allaBöcker.Count == 0)
            {
                Console.WriteLine("DET FINNS INGA BÖCKER :(");
                return;
            }

            Console.WriteLine("\n----- DINA BÖCKER -----");
            for (int i = 0; i < allaBöcker.Count; i++)
            {
                MinBok b = allaBöcker[i];
                string utlånadText = "";
                if (b.ärBortaJustNu == true)
                {
                    utlånadText = " (UTLÅNAD JUST NU)";
                }

                Console.WriteLine((i + 1) + ". " + b.personSomSkrev + " - " + b.bokNamn + utlånadText);
            }
        }

        // Lägg till ny bok
        static void LäggTillNyBok()
        {
            Console.WriteLine("\n----- LÄGG TILL NY BOK -----");

            Console.Write("Vad heter boken? --> ");
            string bokens_namn = Console.ReadLine();

            Console.Write("Vem skrev boken? --> ");
            string författarens_namn = Console.ReadLine();

            // Skapa ny bok och lägg till
            MinBok nyBok = new MinBok(bokens_namn, författarens_namn);
            allaBöcker.Add(nyBok);

            // Spara
            SparaBöcker();
            Console.WriteLine("YAY! BOKEN ÄR TILLAGD!");
        }

        // Hitta en bok
        static void HittaBok()
        {
            Console.WriteLine("\n----- HITTA BOK -----");

            Console.Write("Vad letar du efter? --> ");
            string sökText = Console.ReadLine().ToLower();

            bool hittadeNågon = false;
            Console.WriteLine("\nJAG HITTADE DESSA BÖCKER:");

            for (int i = 0; i < allaBöcker.Count; i++)
            {
                MinBok b = allaBöcker[i];

                // Kolla om boken matchar sökningen
                if (b.bokNamn.ToLower().Contains(sökText) || b.personSomSkrev.ToLower().Contains(sökText))
                {
                    string utlånadText = "";
                    if (b.ärBortaJustNu == true)
                    {
                        utlånadText = " (UTLÅNAD JUST NU)";
                    }

                    Console.WriteLine((i + 1) + ". " + b.personSomSkrev + " - " + b.bokNamn + utlånadText);
                    hittadeNågon = true;
                }
            }

            if (hittadeNågon == false)
            {
                Console.WriteLine("INGA BÖCKER MATCHADE SÖKNINGEN :(");
            }
        }

        // Låna en bok
        static void LånaBok()
        {
            Console.WriteLine("\n----- LÅNA BOK -----");

            // Visa alla böcker först
            SkrivUtBöcker();

            Console.Write("\nVilken bok vill du låna? (skriv nummer) --> ");
            string inmatning = Console.ReadLine();

            // Försök konvertera till nummer
            bool konverteringen_funkar = int.TryParse(inmatning, out int val);

            if (konverteringen_funkar == true && val > 0 && val <= allaBöcker.Count)
            {
                // Boken finns!
                MinBok vald_bok = allaBöcker[val - 1];

                // Kolla om redan utlånad
                if (vald_bok.ärBortaJustNu == true)
                {
                    Console.WriteLine("NEJ! BOKEN ÄR REDAN LÅNAD AV NÅGON ANNAN!!!");
                }
                else
                {
                    // Markera som utlånad
                    vald_bok.ärBortaJustNu = true;
                    SparaBöcker();
                    Console.WriteLine("OK! DU HAR LÅNAT BOKEN!");
                }
            }
            else
            {
                Console.WriteLine("FEL NUMMER! FINNS INGEN SÅDAN BOK!!!");
            }
        }

        // Lämna tillbaka en bok
        static void GeTillbaka()
        {
            Console.WriteLine("\n----- LÄMNA TILLBAKA BOK -----");

            // Visa alla böcker först
            SkrivUtBöcker();

            Console.Write("\nVilken bok vill du lämna tillbaka? (skriv nummer) --> ");
            string inmatning = Console.ReadLine();

            // Försök konvertera till nummer
            bool konverteringen_funkar = int.TryParse(inmatning, out int val);

            if (konverteringen_funkar == true && val > 0 && val <= allaBöcker.Count)
            {
                // Boken finns!
                MinBok vald_bok = allaBöcker[val - 1];

                // Kolla om den verkligen är utlånad
                if (vald_bok.ärBortaJustNu == false)
                {
                    Console.WriteLine("KONSTIGT! DEN BOKEN ÄR INTE UTLÅNAD!!!");
                }
                else
                {
                    // Markera som tillbakalämnad
                    vald_bok.ärBortaJustNu = false;
                    SparaBöcker();
                    Console.WriteLine("TACK FÖR ATT DU LÄMNADE TILLBAKA BOKEN!!!");
                }
            }
            else
            {
                Console.WriteLine("FEL NUMMER! FINNS INGEN SÅDAN BOK!!!");
            }
        }

        // Ta bort en bok helt
        static void TaBortBok()
        {
            Console.WriteLine("\n----- TA BORT BOK HELT -----");

            // Visa alla böcker först
            SkrivUtBöcker();

            Console.Write("\nVilken bok vill du ta bort? (skriv nummer) --> ");
            string inmatning = Console.ReadLine();

            // Försök konvertera till nummer
            bool konverteringen_funkar = int.TryParse(inmatning, out int val);

            if (konverteringen_funkar == true && val > 0 && val <= allaBöcker.Count)
            {
                // Boken finns!
                Console.Write("ÄR DU SÄKER??? BOKEN FÖRSVINNER FÖR ALLTID!!! (j/n) --> ");
                string svar = Console.ReadLine().ToLower();

                if (svar == "j")
                {
                    // Ta bort boken
                    allaBöcker.RemoveAt(val - 1);
                    SparaBöcker();
                    Console.WriteLine("BOKEN ÄR BORTA FÖR ALLTID NU!!!");
                }
                else
                {
                    Console.WriteLine("OK, INGET HÄNDE MED BOKEN!");
                }
            }
            else
            {
                Console.WriteLine("FEL NUMMER! FINNS INGEN SÅDAN BOK!!!");
            }
        }
    }
}
