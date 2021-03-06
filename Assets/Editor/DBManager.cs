using Newtonsoft.Json;
using Pacient;
using System.IO;
using UnityEngine;

namespace EditorScripts
{
    public class MenuItems
    {
        [UnityEditor.MenuItem("Tools/Clear DataBase")]
        private static void ClearDataBase()
        {
            string path = Application.persistentDataPath + "/HistoriaClinica.json";

            if (File.Exists(path))
            {
                File.WriteAllText(path, null);
            }

        }

        [UnityEditor.MenuItem("Tools/Create DataBase")]
        private static void CreateDataBase()
        {
            string path = Application.persistentDataPath + "/HistoriaClinica.json";

            PacientData[] data = new PacientData[10];

            data[0] = new PacientData(1, "Luciano", "Donati", "35194976", "24/01/1991", 'M');
            data[1] = new PacientData(2, "Luciana", "Fedeli", "38207007", "10/05/1994", 'F');
            data[2] = new PacientData(3, "Fabian", "Fedeli", "22059595", "08/05/1971", 'M');
            data[3] = new PacientData(4, "Etel", "Scokin", "22903280", "09/12/1972", 'F');
            data[4] = new PacientData(5, "Florencia", "Fedeli", "39516987", "19/06/1996", 'F');
            data[5] = new PacientData(6, "Victoria", "Fedeli", "46161088", "20/01/2005", 'F');
            data[6] = new PacientData(7, "Carolina", "Illanes", "12345678", "24/01/1991", 'F');
            data[7] = new PacientData(8, "Oriana", "Donati", "98765432", "08/04/1988", 'F');
            data[8] = new PacientData(9, "Pablo", "Fedeli", "4123678", "11/02/1940", 'M');
            data[9] = new PacientData(10, "Maria", "Moris", "6415234", "17/08/1945", 'F');

            File.WriteAllText(path, JsonConvert.SerializeObject(data));

        }
    }
}
