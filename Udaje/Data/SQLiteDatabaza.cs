using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;
using SQLite.Net.Platform.WinRT;
using SQLite.Net;
using System.Diagnostics;

namespace Udalosti.Udaje.Data
{
    class SQLiteDatabaza
    {
        public void VyvorDatabazu()
        {
            Debug.WriteLine("Metoda VyvorDatabazu bola vykonana");

            if (!tabulkaExistuje(App.databaza).Result)
            {
                using (SQLiteConnection tabulka = new SQLiteConnection(new SQLitePlatformWinRT(), App.databaza))
                {
                    tabulka.CreateTable<Pouzivatelia>();
                    tabulka.CreateTable<Miesto>();

                }
            }
        }

        private async Task<bool> tabulkaExistuje(string fileName)
        {
            Debug.WriteLine("Metoda tabulkaExistuje bola vykonana");

            try
            {
                var udaje = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void novePouzivatelskeUdaje(Pouzivatelia pouzivatelia)
        {
            Debug.WriteLine("Metoda novePouzivatelskeUdaje bola vykonana");

            using (SQLiteConnection databaza = new SQLiteConnection(new SQLitePlatformWinRT(), App.databaza))
            {
                databaza.RunInTransaction(() =>
                {
                    databaza.Insert(pouzivatelia);
                });
            }
        }

        public void aktualizujPouzivatelskeUdaje(Pouzivatelia pouzivatelia)
        {
            Debug.WriteLine("Metoda aktualizujPouzivatelskeUdaje bola vykonana");

            using (SQLiteConnection databaza = new SQLiteConnection(new SQLitePlatformWinRT(), App.databaza))
            {
                String dotazNaAktualizaciu = "UPDATE Pouzivatelia " +
                    "SET email = '" + pouzivatelia.email + "', " +
                    "heslo = '" + pouzivatelia.heslo + "', " +
                    "token = '" + pouzivatelia.token + "' " +
                    "WHERE email ='" + pouzivatelia.email + "'";
                databaza.Execute(dotazNaAktualizaciu);
            }
        }

        public void odstranPouzivatelskeUdaje(String email)
        {
            Debug.WriteLine("Metoda odstranPouzivatelskeUdaje bola vykonana");

            using (SQLiteConnection databaza = new SQLiteConnection(new SQLitePlatformWinRT(), App.databaza))
            {
                String dotazNaOdstranenie = "DELETE FROM Pouzivatelia " +
                    "WHERE email = '"+email+"'";
                databaza.Execute(dotazNaOdstranenie);
            }
        }

        public bool pouzivatelskeUdaje()
        {
            Debug.WriteLine("Metoda pouzivatelskeUdaje bola vykonana");

            using (SQLiteConnection databaza = new SQLiteConnection(new SQLitePlatformWinRT(), App.databaza))
            {
                var pouzivatelia = databaza.Query<Pouzivatelia>("SELECT email FROM Pouzivatelia").FirstOrDefault();
                if (pouzivatelia != null)
                {
                    if (pouzivatelia.email != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
        }

        public Dictionary<string, string> vratAktualnehoPouzivatela()
        {
            Debug.WriteLine("Metoda vratAktualnehoPouzivatela bola vykonana");

            Dictionary<string, string> pouzivatelskeUdaje;
            using (SQLiteConnection databaza = new SQLiteConnection(new SQLitePlatformWinRT(), App.databaza))
            {
                var pouzivatelia = databaza.Query<Pouzivatelia>("SELECT email, heslo, token FROM Pouzivatelia").FirstOrDefault();
                if (pouzivatelia != null)
                {
                    pouzivatelskeUdaje = new Dictionary<string, string>();
                    pouzivatelskeUdaje.Add("email", pouzivatelia.email);
                    pouzivatelskeUdaje.Add("heslo", pouzivatelia.heslo);
                    pouzivatelskeUdaje.Add("token", pouzivatelia.token);
                    return pouzivatelskeUdaje;
                }
                else
                {
                    return null;
                }
            }
        }

        public void noveMiestoPrihlasenia(Miesto miesto)
        {
            Debug.WriteLine("Metoda noveMiestoPrihlasenia bola vykonana");

            using (SQLiteConnection databaza = new SQLiteConnection(new SQLitePlatformWinRT(), App.databaza))
            {
                databaza.RunInTransaction(() =>
                {
                    databaza.Insert(miesto);
                });
            }
        }

        public void aktualizujMiestoPrihlasenia(Miesto miesto)
        {
            Debug.WriteLine("Metoda aktualizujMiestoPrihlasenia bola vykonana");

            using (SQLiteConnection databaza = new SQLiteConnection(new SQLitePlatformWinRT(), App.databaza))
            {
                String dotazNaAktualizaciu = "UPDATE Miesto " +
                    "SET stat = '" + miesto.stat + "', " +
                    "okres = '" + miesto.okres + "', " +
                    "mesto = '" + miesto.mesto + "' " +
                    "WHERE idMiesto = 1";
                databaza.Execute(dotazNaAktualizaciu);
            }
        }

        public bool miestoPrihlasenia()
        {
            Debug.WriteLine("Metoda miestoPrihlasenia bola vykonana");

            using (SQLiteConnection databaza = new SQLiteConnection(new SQLitePlatformWinRT(), App.databaza))
            {
                var miesto = databaza.Query<Miesto>("SELECT stat FROM Miesto").FirstOrDefault();
                if (miesto != null)
                {
                    if (miesto.stat != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
        }

        public Dictionary<string, string> vratMiestoPrihlasenia()
        {
            Debug.WriteLine("Metoda vratMiestoPrihlasenia bola vykonana");

            Dictionary<string, string> miestoPrihlasenia;
            using (SQLiteConnection databaza = new SQLiteConnection(new SQLitePlatformWinRT(), App.databaza))
            {
                var miesto = databaza.Query<Miesto>("SELECT stat, okres, mesto FROM Miesto").FirstOrDefault();
                if (miesto != null)
                {
                    miestoPrihlasenia = new Dictionary<string, string>();
                    miestoPrihlasenia.Add("stat", miesto.stat);
                    miestoPrihlasenia.Add("okres", miesto.okres);
                    miestoPrihlasenia.Add("mesto", miesto.mesto);
                    return miestoPrihlasenia;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}