namespace ReolMarkedet
{
    // Kundeinfo
    class Lejer
    {
        public int LejerId;
        public string Navn;
        public string Telefon;
        public string Email;

        public string ReolType;      // A eller B
        public int AntalReoler;
        public decimal PrisPrReol;
        public decimal SpecialRabat; // i procent
        public decimal TotalPrMd;
    }

    class Program
    {
        static List<Lejer> alleLejere = new List<Lejer>();
        static int naesteId = 1;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(" REOLMARKED ");
                Console.WriteLine("1) Registrer ny lejer");
                Console.WriteLine("2) Vis alle registreringer");
                Console.WriteLine("0) Afslut");
                Console.Write("Vælg: ");

                string valg = Console.ReadLine();
                Console.WriteLine();

                if (valg == "1")
                {
                    RegistrerNyLejer();
                }
                else if (valg == "2")
                {
                    VisAlle();
                }
                else if (valg == "0")
                {
                    Console.WriteLine("afslut");
                    break;
                }
                else
                {
                    Console.WriteLine("Ugyldigt valg\n");
                }
            }
        }

        static void RegistrerNyLejer()
        {
            Console.WriteLine("--- Registrering af ny lejer ---");

            // Indtast oplysninger
            Console.Write("Navn: ");
            string navn = Console.ReadLine();
            Console.Write("Telefon: ");
            string telefon = Console.ReadLine();
            Console.Write("E-mail: ");
            string email = Console.ReadLine();

            
            if (string.IsNullOrWhiteSpace(navn) | string.IsNullOrWhiteSpace(telefon) | string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Der mangler oplysninger. Udfyld venligst navn, telefon og e-mail.\n");
                return;
            }

            // Vælg reoltype
            Console.WriteLine("\nReoltype:");
            Console.WriteLine("A) Type A (6 hylder)");
            Console.WriteLine("B) Type B (3 hylder + bøjlestang)");
            Console.Write("Vælg A eller B: ");
            string reolType = (Console.ReadLine() ?? "").Trim().ToUpper();
            if (reolType != "A" && reolType != "B")
            
            // Antal reoler
            Console.Write("\nAntal ønskede reoler: ");
            int antalReoler = int.Parse(Console.ReadLine());
            if (antalReoler <= 0)
            {
                Console.WriteLine("Antal skal være større end 0.\n");
                return;
            }

            // Beregner pris
            decimal prisPrReol = BeregnPrisPrReol(antalReoler);
            Console.WriteLine($"Pris pr. reol: {prisPrReol:0.00} kr. pr. måned");

            // Spørger om rabat
            decimal rabatProcent = 0;
            Console.Write("Har lejeren specialrabat? (ja/nej): ");
            string svar = (Console.ReadLine() ?? "").Trim().ToLower();
            if (svar == "ja")
            {
                Console.Write(" Rabat i %: ");
                rabatProcent = decimal.Parse(Console.ReadLine());
                if (rabatProcent < 0)
                {
                    Console.WriteLine("Rabat kan ikke være negativ.\n");
                    return;
                }
            }

            // Beregner samlede pris
            decimal total = BeregnTotal(antalReoler, prisPrReol, rabatProcent);

            // Bekræftelse
            Console.WriteLine("\n--- Bekræftelse ---");
            Console.WriteLine($"Navn: {navn}");
            Console.WriteLine($"Telefon: {telefon}");
            Console.WriteLine($"E-mail: {email}");
            Console.WriteLine($"Reoltype: {(reolType == "A" ? "Type A (6 hylder)" : "Type B (3 hylder + bøjlestang)")}");
            Console.WriteLine($"Antal reoler: {antalReoler}");
            Console.WriteLine($"Pris pr. reol: {prisPrReol:0.00} kr.");
            Console.WriteLine($"Specialrabat: {(rabatProcent > 0 ? rabatProcent + "%" : "Ingen")}");
            Console.WriteLine($"Total pr. måned: {total:0.00} kr.");
            Console.Write("\nBekræft registrering? (ja/nej): ");

            string ok = (Console.ReadLine() ?? "").Trim().ToLower();
            if (ok == "ja")
            {
                // Gemmer oplysninger
                Lejer ny = new Lejer();
                ny.LejerId = naesteId++;
                ny.Navn = navn;
                ny.Telefon = telefon;
                ny.Email = email;
                ny.ReolType = reolType;
                ny.AntalReoler = antalReoler;
                ny.PrisPrReol = prisPrReol;
                ny.SpecialRabat = rabatProcent;
                ny.TotalPrMd = total;

                alleLejere.Add(ny);

                Console.WriteLine("\nLejer registreret med ID: " + ny.LejerId + "\n");
            }
            else
            {
                Console.WriteLine("\nRegistrering annulleret\n");
            }
        }

        static void VisAlle()
        {
            Console.WriteLine("--- Alle registreringer ---");

            if (alleLejere.Count == 0)
            {
                Console.WriteLine("Ingen registreringer endnu\n");
                return;
            }

            foreach (var l in alleLejere)
            {
                Console.WriteLine($"Lejer #{l.LejerId}: {l.Navn}");
                Console.WriteLine($" - Tlf: {l.Telefon}");
                Console.WriteLine($" - Email: {l.Email}");
                Console.WriteLine($" - Reoltype: {(l.ReolType == "A" ? "A (6 hylder)" : "B (3 hylder + bøjlestang)")}");
                Console.WriteLine($" - Antal reoler: {l.AntalReoler}");
                Console.WriteLine($" - Pris pr. reol: {l.PrisPrReol:0.00} kr.");
                Console.WriteLine($" - Specialrabat: {(l.SpecialRabat > 0 ? l.SpecialRabat + "%" : "Ingen")}");
                Console.WriteLine($" - Total pr. måned: {l.TotalPrMd:0.00} kr.");
                Console.WriteLine("----------------------------------------");
            }

            Console.WriteLine();
        }

        static decimal BeregnPrisPrReol(int antal)
        {
            if (antal <= 1)
                return 850m;
            else if (antal <= 3)
                return 825m;
            else
                return 800m;
        }

        static decimal BeregnTotal(int antal, decimal prisPrReol, decimal rabatProcent)
        {
            decimal subtotal = antal * prisPrReol;

            if (rabatProcent > 0)
            {
                decimal rabatBelob = subtotal * (rabatProcent / 100m);
                return subtotal - rabatBelob;
            }

            return subtotal;
        }
    }
}
