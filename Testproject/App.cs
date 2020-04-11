using System;
using System.IO;
using Testproject.Droid.Helpers;

namespace Testproject
{
    public class App
    {

        static MainDB database;

        public static MainDB Database
        {
            get
            {
                if (database == null)
                {
                    database = new MainDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mainDB.db3"));
                }
                return database;
            }
        }
    }
}
