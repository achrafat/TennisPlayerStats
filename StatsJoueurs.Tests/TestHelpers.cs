using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsJoueurs.Tests
{
    public  static class TestHelpers
    {
        public static string GetFilePath(string fileName)
        {
            // Combine le répertoire de base avec le nom du fichier
            return Path.Combine(Directory.GetCurrentDirectory(), fileName);
        }
    }
}
