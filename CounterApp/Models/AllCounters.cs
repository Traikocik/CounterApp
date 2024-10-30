using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CounterApp.Models
{
    public class AllCounters
    {
        public ObservableCollection<Counter> Counters { get; set; } = new ObservableCollection<Counter>();

        private string FileName;

        public AllCounters()
        {
            FileName = Path.Combine(FileSystem.AppDataDirectory, "counters.xml");
            LoadCounters();
        }

        public void SaveCounters()
        {
            var document = new XDocument(new XElement("Counters",
                Counters.Select(counter => new XElement("Counter",
                    new XAttribute("Name", counter.Name),
                    new XAttribute("Value", counter.Value),
                    new XAttribute("InitialValue", counter.InitialValue),
                    new XAttribute("Color", counter.CounterColor.ToHex())))));

            document.Save(FileName);
        }

        public void LoadCounters()
        {
            Counters.Clear();
            if (File.Exists(FileName))
            {
                var document = XDocument.Load(FileName);
                var counters = document.Root.Elements("Counter")
                    .Select(x => new Counter(
                        (string)x.Attribute("Name"),
                        (int)x.Attribute("InitialValue"),
                        Color.FromHex((string)x.Attribute("Color")))
                    {
                        Value = (int)x.Attribute("Value")
                    });

                foreach (var counter in counters)
                    Counters.Add(counter);
            }
        }
    }
}
