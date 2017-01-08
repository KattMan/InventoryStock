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
            var currentDir = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            var fullName = Path.Combine(currentDir, FileName);

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
            var currentDir = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            var fullName = Path.Combine(currentDir, FileName);

            using (StreamWriter sw = new StreamWriter(fullName))
            {
                XmlSerializer ser = new XmlSerializer(typeof(DataContainer));
                ser.Serialize(sw, data);

                return true;
            }
        }

    }
}
