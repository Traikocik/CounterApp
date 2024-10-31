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
            var rootElement = new XElement("Counters");

            foreach (var counter in Counters)
            {
                var counterElement = new XElement("Counter",
                    new XAttribute("Name", counter.Name),
                    new XAttribute("Value", counter.Value),
                    new XAttribute("InitialValue", counter.InitialValue),
                    new XAttribute("Color", counter.CounterColor.ToHex()));

                rootElement.Add(counterElement);
            }

            var document = new XDocument(rootElement);
            document.Save(FileName);
        }

        public void LoadCounters()
        {
            Counters.Clear();
            if (File.Exists(FileName))
            {
                var document = XDocument.Load(FileName);

                foreach (var element in document.Root.Elements("Counter"))
                {
                    string name = element.Attribute("Name").Value;
                    int initialValue = int.Parse(element.Attribute("InitialValue").Value);
                    int value = int.Parse(element.Attribute("Value").Value);
                    Color color = Color.FromHex(element.Attribute("Color").Value);

                    Counter counter = new Counter(name, initialValue, value, color);

                    Counters.Add(counter);
                }
            }
        }
    }
}
