using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ablakos = System.Windows.Forms;

namespace Objektumok
{
    internal class Program
    {
        static Jarmu[] jarmuvek;
        static ablakos.Timer idozito;
        static void Main(string[] args)
        {
            Vezerles();
            string auto = "A-1";
            int vizsz = 67;
            int fugg = 28;
            Console.SetCursorPosition(vizsz, fugg);
            Console.Write(auto);
            ConsoleKeyInfo cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.Enter) 
            {
               while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        cki = Console.ReadKey(true);
                        if (cki.Key == ConsoleKey.Escape)
                        {
                            break;
                        }
                    }
                    if (vizsz > 3)
                    {
                        vizsz -= 3;
                    }
                    Console.Clear();
                    Console.SetCursorPosition(vizsz, fugg);
                    Console.Write(auto);
                    Thread.Sleep(1000/16); //Thread, mintszál, egy önlló folyamat része
                    /* az egyszerű animációk másodpecenkénet egymástól valamennyire eltérő
                     * 16 képet tesznek láthatóvá, ami az érzékelésünk következtében 
                     * összemosódik, folytonos mozgás érzetét keltve */
                }
            }
            Auto a = new Auto(); //amennyiben egy osztaly dinamikus, abból előbb egy példányt kell létrehozni
            a.Mozog();
        }
        static void Vezerles()
        {
            /*for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write(i % 10);
            } */
            jarmuvek = new Jarmu[30];
            for (int i = 0; i < jarmuvek.Length; i++)
            {
                jarmuvek[i] = new Jarmu("A-1");
                jarmuvek[i].Mutat();
            }
            idozito = new ablakos.Timer();
            idozito.Interval = 1000;
            idozito.Enabled = true;
            idozito.Tick += new EventHandler(Mozgatas);
            //idozito.Start();
            while (true)
            {
                //Thread.Sleep(10);
                ablakos.Application.DoEvents();
                if(Console.KeyAvailable)
                {
                    if(Console.ReadKey(true).Key == ConsoleKey.Escape) 
                    { 
                        break; 
                    }
                }
            }
        }
        static void Mozgatas(object sender, EventArgs e)
        {
            idozito.Enabled = false;
            Console.Clear();
            Console.SetCursorPosition(1, 1);
            Console.Write("*");
            for (int i = 0;i < jarmuvek.Length; i++)
            {
                jarmuvek[i].Mozgat();
                jarmuvek[i].Mutat();
            }
            idozito.Enabled = true;
        }
    }
    class Auto // mivel nincs sehol static szó ez mindenképp dinamikus
    {
        int sebesseg = 3;   // amennyibe sem public, sem private, sem protected minősítés nem szerepel, akkor C...
        string rendszam = "A-1";  //ugyanez a javaban package public, vagyis az egy package-be...
        public void Mozog()
        {
            Console.WriteLine("mozgok...");
        }
    }
    struct Helyzet //Értéktípus - a Stack/Verem memóriában tárolódik
    {
        int x;
        int y;
        public Helyzet(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int X // Az ilyen ún. property az osztályoknál is használható
        {
            get // ellenőrzött módon tesszük lehetővé a privatr tartalom lekérdezését (get)
            {
                return x;
            }
        }
        public int Y // A java csak ún. seter és getter metódusokat enged alkalmazni
        {
            get
            {
                return y;
            }
        }
    }
    class Jarmu //Címtípus/Referenciatípus - a Heap/Halom/szabad  meória területen kerül elhelyezésre
    {
        Helyzet helyzet;
        int sebesseg;
        string rendszam;
        static int peldanyszamlalo = 0;
        static Random vszg = new Random();
        public Jarmu(string rendszam) //konstruktor amit a new műveletnél hsználunk fel a z initializásra és operációs rendszerrel megvalósított memóriafoglalásra
        {
            helyzet = new Helyzet(vszg.Next(1, Console.WindowWidth), vszg.Next(1, 30));
            sebesseg = vszg.Next(1, 5);
            //peldanyszamlalo++;
            this.rendszam = rendszam + peldanyszamlalo;
        }
        public void Mutat()
        {
            Console.SetCursorPosition(helyzet.X, helyzet.Y);
            Console.Write(rendszam);
        }
        public void Mozgat()
        {
            if(helyzet.X > sebesseg)
            {
                helyzet = new Helyzet(helyzet.X - sebesseg, helyzet.Y);
            }
            else
            {
                helyzet = new Helyzet(Console.WindowWidth - rendszam.Length, helyzet.Y);
            }
        }
    }
}
