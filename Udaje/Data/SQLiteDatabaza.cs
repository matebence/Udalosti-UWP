using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Udaje.Data
{
    class SQLiteDatabaza
    {
        public void VyvorDatabazu()
        {
            if (!tabulkaExistuje(App.databaza).Result)
            {
                using (SQLite.Net.SQLiteConnection tabulka = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.databaza))
                {
                    tabulka.CreateTable<Pouzivatel>();
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

        public void novePouzivatelskeUdaje(Pouzivatel pouzivatel)
        {
            using (SQLite.Net.SQLiteConnection databaza = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.databaza))
            {
                databaza.RunInTransaction(() =>
                {
                    databaza.Insert(pouzivatel);
                });
            }
        }

        public void aktualizujPouzivatelskeUdaje(Pouzivatel pouzivatel)
        {
            using (SQLite.Net.SQLiteConnection databaza = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.databaza))
            {
                var existujuciPouzivatel = databaza.Query<Pouzivatel>("SELECT * FROM Pouzivatel WHERE email =" + pouzivatel.email).FirstOrDefault();
                if (existujuciPouzivatel != null)
                {
                    databaza.RunInTransaction(() =>
                    {
                        databaza.Update(pouzivatel);
                    });
                }
            }
        }

        public void odstranPouzivatelskeUdaje(String email)
        {
            using (SQLite.Net.SQLiteConnection databaza = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.databaza))
            {
                var existujuciPouzivatel = databaza.Query<Pouzivatel>("SELECT * FROM Pouzivatel WHERE email =" + email).FirstOrDefault();
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
            using (SQLite.Net.SQLiteConnection databaza = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.databaza))
            {
                var pouzivatel = databaza.Query<Pouzivatel>("SELECT email FROM Pouzivatel").FirstOrDefault();
                if (pouzivatel != null)
                {
                    if (pouzivatel.email != null)
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
            using (SQLite.Net.SQLiteConnection databaza = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.databaza))
            {
                var pouzivatel = databaza.Query<Pouzivatel>("SELECT email, heslo FROM Pouzivatel").FirstOrDefault();
                if (pouzivatel != null)
                {
                    pouzivatelskeUdaje = new Dictionary<string, string>();
                    pouzivatelskeUdaje.Add("email", pouzivatel.email);
                    pouzivatelskeUdaje.Add("heslo", pouzivatel.heslo);
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
            using (SQLite.Net.SQLiteConnection databaza = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.databaza))
            {
                databaza.RunInTransaction(() =>
                {
                    databaza.Insert(miesto);
                });
            }
        }

        public void aktualizujMiestoPrihlasenia(Miesto miesto)
        {
            using (SQLite.Net.SQLiteConnection databaza = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.databaza))
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
            using (SQLite.Net.SQLiteConnection databaza = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.databaza))
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
            using (SQLite.Net.SQLiteConnection databaza = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.databaza))
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
            using (SQLite.Net.SQLiteConnection databaza = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.databaza))
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