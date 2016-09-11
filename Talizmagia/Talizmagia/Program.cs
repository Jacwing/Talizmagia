using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talizmagia
{
    class Program
    {
        const int rozmiarKwadratu = 4;

        static Sasiedzi[] sasiedziDlaKwadratuMagicznego = new Sasiedzi[]
        {
            new Sasiedzi(new Wspolrzedna(0,0), new Wspolrzedna(0,1)),
            new Sasiedzi(new Wspolrzedna(0,0), new Wspolrzedna(1,0)),
            new Sasiedzi(new Wspolrzedna(0,0), new Wspolrzedna(1,1)),
            new Sasiedzi(new Wspolrzedna(0,1), new Wspolrzedna(0,2)),
            new Sasiedzi(new Wspolrzedna(0,1), new Wspolrzedna(1,0)),
            new Sasiedzi(new Wspolrzedna(0,1), new Wspolrzedna(1,1)),
            new Sasiedzi(new Wspolrzedna(0,1), new Wspolrzedna(1,2)),
            new Sasiedzi(new Wspolrzedna(0,2), new Wspolrzedna(0,3)),
            new Sasiedzi(new Wspolrzedna(0,2), new Wspolrzedna(1,1)),
            new Sasiedzi(new Wspolrzedna(0,2), new Wspolrzedna(1,2)),
            new Sasiedzi(new Wspolrzedna(0,2), new Wspolrzedna(1,3)),
            new Sasiedzi(new Wspolrzedna(0,3), new Wspolrzedna(1,2)),
            new Sasiedzi(new Wspolrzedna(0,3), new Wspolrzedna(1,3)),
            new Sasiedzi(new Wspolrzedna(1,0), new Wspolrzedna(1,1)),
            new Sasiedzi(new Wspolrzedna(1,0), new Wspolrzedna(2,0)),
            new Sasiedzi(new Wspolrzedna(1,0), new Wspolrzedna(2,1)),
            new Sasiedzi(new Wspolrzedna(1,1), new Wspolrzedna(1,2)),
            new Sasiedzi(new Wspolrzedna(1,1), new Wspolrzedna(2,0)),
            new Sasiedzi(new Wspolrzedna(1,1), new Wspolrzedna(2,1)),
            new Sasiedzi(new Wspolrzedna(1,1), new Wspolrzedna(2,2)),
            new Sasiedzi(new Wspolrzedna(1,2), new Wspolrzedna(1,3)),
            new Sasiedzi(new Wspolrzedna(1,2), new Wspolrzedna(2,1)),
            new Sasiedzi(new Wspolrzedna(1,2), new Wspolrzedna(2,2)),
            new Sasiedzi(new Wspolrzedna(1,2), new Wspolrzedna(2,3)),
            new Sasiedzi(new Wspolrzedna(1,3), new Wspolrzedna(2,2)),
            new Sasiedzi(new Wspolrzedna(1,3), new Wspolrzedna(2,3)),
            new Sasiedzi(new Wspolrzedna(2,0), new Wspolrzedna(2,1)),
            new Sasiedzi(new Wspolrzedna(2,0), new Wspolrzedna(3,0)),
            new Sasiedzi(new Wspolrzedna(2,0), new Wspolrzedna(3,1)),
            new Sasiedzi(new Wspolrzedna(2,1), new Wspolrzedna(2,2)),
            new Sasiedzi(new Wspolrzedna(2,1), new Wspolrzedna(3,0)),
            new Sasiedzi(new Wspolrzedna(2,1), new Wspolrzedna(3,1)),
            new Sasiedzi(new Wspolrzedna(2,1), new Wspolrzedna(3,2)),
            new Sasiedzi(new Wspolrzedna(2,2), new Wspolrzedna(2,3)),
            new Sasiedzi(new Wspolrzedna(2,2), new Wspolrzedna(3,1)),
            new Sasiedzi(new Wspolrzedna(2,2), new Wspolrzedna(3,2)),
            new Sasiedzi(new Wspolrzedna(2,2), new Wspolrzedna(3,3)),
            new Sasiedzi(new Wspolrzedna(2,3), new Wspolrzedna(3,2)),
            new Sasiedzi(new Wspolrzedna(2,3), new Wspolrzedna(3,3)),
            new Sasiedzi(new Wspolrzedna(3,0), new Wspolrzedna(3,1)),
            new Sasiedzi(new Wspolrzedna(3,1), new Wspolrzedna(3,2)),
            new Sasiedzi(new Wspolrzedna(3,2), new Wspolrzedna(3,3))
        };



        static void Main(string[] args)
        {
            string[] liniePliku = File.ReadAllLines(@"WszytkieMagiczneKwadraty.txt");
            KwadratMagiczny[] wszystkieMagiczneKwadraty = new KwadratMagiczny[liniePliku.Length / (rozmiarKwadratu + 1)];

            for (int i = 0; i < liniePliku.Length; i += 5)
            {
                KwadratMagiczny kwadratTymczacowy = new KwadratMagiczny(rozmiarKwadratu);
                int offsetDrugiejTablicy = 0;

                for (int j = 0; j < rozmiarKwadratu; j++)
                {
                    int[] lina = liniePliku[i + j].Split(',').Select(int.Parse).ToArray();
                    Buffer.BlockCopy(lina, 0, kwadratTymczacowy.Struktura, offsetDrugiejTablicy, lina.Length * sizeof(int));
                    offsetDrugiejTablicy += lina.Length * sizeof(int);
                }
                wszystkieMagiczneKwadraty[i / 5] = kwadratTymczacowy;
            }

            int lp = 1;
            for (int i = 0; i < wszystkieMagiczneKwadraty.Length; i++)
            {
                int rMin = RMinKwadratu(wszystkieMagiczneKwadraty[i]);
                if (rMin > 1)
                {
                    Console.WriteLine("Kwadrat numer " + lp + ". Rmin = " + rMin + ".");
                    wszystkieMagiczneKwadraty[i].WypiszKwadrat();
                    Console.WriteLine();
                    lp++;
                }

            }

            Console.ReadKey();
        }

        static bool CzyMagiczny(KwadratMagiczny kwadratMagiczny)
        {
            bool wynik = false;
            int liczbaTakichSamychSum = 0;
            int suma = kwadratMagiczny.Struktura[0, 0] + kwadratMagiczny.Struktura[0, 1] + kwadratMagiczny.Struktura[0, 2] + kwadratMagiczny.Struktura[0, 3];

            if (kwadratMagiczny.Struktura[1, 0] + kwadratMagiczny.Struktura[1, 1] + kwadratMagiczny.Struktura[1, 2] + kwadratMagiczny.Struktura[1, 3] == suma)
                liczbaTakichSamychSum++;
            if (kwadratMagiczny.Struktura[2, 0] + kwadratMagiczny.Struktura[2, 1] + kwadratMagiczny.Struktura[2, 2] + kwadratMagiczny.Struktura[2, 3] == suma)
                liczbaTakichSamychSum++;
            if (kwadratMagiczny.Struktura[3, 0] + kwadratMagiczny.Struktura[3, 1] + kwadratMagiczny.Struktura[3, 2] + kwadratMagiczny.Struktura[3, 3] == suma)
                liczbaTakichSamychSum++;
            if (kwadratMagiczny.Struktura[0, 0] + kwadratMagiczny.Struktura[1, 0] + kwadratMagiczny.Struktura[2, 0] + kwadratMagiczny.Struktura[3, 0] == suma)
                liczbaTakichSamychSum++;
            if (kwadratMagiczny.Struktura[0, 1] + kwadratMagiczny.Struktura[1, 1] + kwadratMagiczny.Struktura[2, 1] + kwadratMagiczny.Struktura[3, 1] == suma)
                liczbaTakichSamychSum++;
            if (kwadratMagiczny.Struktura[0, 2] + kwadratMagiczny.Struktura[1, 2] + kwadratMagiczny.Struktura[2, 2] + kwadratMagiczny.Struktura[3, 2] == suma)
                liczbaTakichSamychSum++;
            if (kwadratMagiczny.Struktura[0, 3] + kwadratMagiczny.Struktura[1, 3] + kwadratMagiczny.Struktura[2, 3] + kwadratMagiczny.Struktura[3, 3] == suma)
                liczbaTakichSamychSum++;
            if (kwadratMagiczny.Struktura[0, 0] + kwadratMagiczny.Struktura[1, 1] + kwadratMagiczny.Struktura[2, 2] + kwadratMagiczny.Struktura[3, 3] == suma)
                liczbaTakichSamychSum++;
            if (kwadratMagiczny.Struktura[0, 3] + kwadratMagiczny.Struktura[1, 2] + kwadratMagiczny.Struktura[2, 1] + kwadratMagiczny.Struktura[3, 0] == suma)
                liczbaTakichSamychSum++;

            if (liczbaTakichSamychSum == 9)
                wynik = true;

            return wynik;
        }

        static int RMinKwadratu(KwadratMagiczny kwadratMagiczny)
        {
            int rMin = 10;

            for (int i = 0; i < sasiedziDlaKwadratuMagicznego.Length; i++)
            {
                int min = Math.Abs(kwadratMagiczny.Struktura[sasiedziDlaKwadratuMagicznego[i].PierwszySasiad.X, sasiedziDlaKwadratuMagicznego[i].PierwszySasiad.Y] - kwadratMagiczny.Struktura[sasiedziDlaKwadratuMagicznego[i].DrugiSasiad.X, sasiedziDlaKwadratuMagicznego[i].DrugiSasiad.Y]);
                if (min < rMin)
                    rMin = min;
            }
            return rMin;
        }


    }

    class KwadratMagiczny
    {
        int rozmiar;
        public int[,] Struktura;

        public KwadratMagiczny(int rozmiar)
        {
            this.rozmiar = rozmiar;
            Struktura = new int[rozmiar, rozmiar];
        }

        public void WypiszKwadrat()
        {
            for (int i = 0; i < rozmiar; i++)
            {
                for (int j = 0; j < rozmiar; j++)
                {
                    Console.Write(this.Struktura[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }

    class Wspolrzedna
    {
        public int X, Y;

        public Wspolrzedna(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class Sasiedzi
    {
        public Wspolrzedna PierwszySasiad, DrugiSasiad;

        public Sasiedzi(Wspolrzedna pierwszySasiad, Wspolrzedna drugiSasiad)
        {
            PierwszySasiad = pierwszySasiad;
            DrugiSasiad = drugiSasiad;
        }
    }
}
