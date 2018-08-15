using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;
using SQLite.Net.Platform.WinRT;
using SQLite.Net;

namespace Udalosti.Udaje.Data
{
    class SQLiteDatabaza
    {
        public void VyvorDatabazu()
        {
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
            using (SQLiteConnection databaza = new SQLiteConnection(new SQLitePlatformWinRT(), App.databaza))
            {
                var existujuciPouzivatel = databaza.Query<Pouzivatelia>("SELECT * FROM Pouzivatelia WHERE email =" + pouzivatelia.email).FirstOrDefault();
                if (existujuciPouzivatel != null)
                {
                    databaza.RunInTransaction(() =>
                    {
                        databaza.Update(pouzivatelia);
                    });
                }
            }
        }

        public void odstranPouzivatelskeUdaje(String email)
        {
            using (SQLiteConnection databaza = new SQLiteConnection(new SQLitePlatformWinRT(), App.databaza))
            {
                var existujuciPouzivatel = databaza.Query<Pouzivatelia>("SELECT * FROM Pouzivatelia WHERE email =" + email).FirstOrDefault();
                if (existujuciPouzivatel != null)
                {
                    databaza.RunInTransaction(() =>
                    {
                        databaza.Delete(existujuciPouzivatel);
                    });
                }
            }
        }

        public bool pouzivatelskeUdaje()
        {
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
            Dictionary<string, string> pouzivatelskeUdaje;
            using (SQLiteConnection databaza = new SQLiteConnection(new SQLitePlatformWinRT(), App.databaza))
            {
                var pouzivatelia = databaza.Query<Pouzivatelia>("SELECT email, heslo FROM Pouzivatelia").FirstOrDefault();
                if (pouzivatelia != null)
                {
                    pouzivatelskeUdaje = new Dictionary<string, string>();
                    pouzivatelskeUdaje.Add("email", pouzivatelia.email);
                    pouzivatelskeUdaje.Add("heslo", pouzivatelia.heslo);
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
            using (SQLiteConnection databaza = new SQLiteConnection(new SQLitePlatformWinRT(), App.databaza))
            {
                var existujuceMiesto = databaza.Query<Miesto>("SELECT * FROM Miesto WHERE idMiesto =" + miesto.idMiesto).FirstOrDefault();
                if (existujuceMiesto != null)
                {
                    databaza.RunInTransaction(() =>
                    {
                        databaza.Update(miesto);
                    });
                }
            }
        }

        public void odstranMiestoPrihlasenia(int idMiesto)
        {
            using (SQLiteConnection databaza = new SQLiteConnection(new SQLitePlatformWinRT(), App.databaza))
            {
                var existujuceMiesto = databaza.Query<Miesto>("SELECT * FROM Miesto WHERE idMiesto =" + idMiesto).FirstOrDefault();
                if (existujuceMiesto != null)
                {
                    databaza.RunInTransaction(() =>
                    {
                        databaza.Delete(existujuceMiesto);
                    });
                }
            }
        }

        public bool miestoPrihlasenia()
        {
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