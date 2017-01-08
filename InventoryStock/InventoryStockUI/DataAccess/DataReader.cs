using InventoryStockUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace InventoryStockUI.DataAccess
{
    class DataReader
    {
        private string FileName = "InventoryData.XML";

        public DataContainer ReadData()
        {
            var fullName = GetFullFIleName();

            if (!System.IO.File.Exists(fullName))
            {
                return new DataContainer()
                {
                    Expenses = new List<Expense>(),
                    InventoryItems = new List<InventoryItem>()
                };
            }
            else
            {
                XmlSerializer ser = new XmlSerializer(typeof(DataContainer));
                using (FileStream fs = File.OpenRead(fullName))
                {
                    return (DataContainer)ser.Deserialize(fs);
                }
            }
        }

        public bool SaveData(DataContainer data)
        {
            var fullName = GetFullFIleName();

            using (StreamWriter sw = new StreamWriter(fullName))
            {
                XmlSerializer ser = new XmlSerializer(typeof(DataContainer));
                ser.Serialize(sw, data);

                return true;
            }
        }

        public bool BackupData(string backupLocation)
        {
            var fullName = GetFullFIleName();

            var backupfilename = string.Format("InventoryData{0}{1}{2}-{3}{4}.XML", DateTime.Now.Year.ToString("D4"), DateTime.Now.Month.ToString("D2"), DateTime.Now.Day.ToString("D2"), DateTime.Now.Hour.ToString("D2"), DateTime.Now.Minute.ToString("D2"));
            var backupfullName = Path.Combine(backupLocation, backupfilename);

            File.Copy(fullName, backupfullName);

            return true;
        }

        private string GetFullFIleName()
        {
            var currentDir = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            return Path.Combine(currentDir, FileName);
        }

    }
}
