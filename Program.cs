using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatterndDesign
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherStation weatherStation = new WeatherStation();
            NewsAgency agency1 = new NewsAgency("Alpha News Agency");
            weatherStation.Attach(agency1);
            NewsAgency agency2 = new NewsAgency("Omega News Agency");
            weatherStation.Attach(agency2);

            weatherStation.Temperature = 31.2f;
            weatherStation.Temperature = 28f;
            weatherStation.Temperature = 46.8f;
            weatherStation.Temperature = 15.3f;

            Console.ReadKey();
        }
    }
    interface ISubject
    {
        void Attach(IObserver observer);
        void Notify();
    }

    class WeatherStation : ISubject
    {
        private List<IObserver> _observers;
        public float Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                Notify();
            }
        }
        private float _temperature { get; set; }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Notify()
        {
            _observers.ForEach(x => x.Update(this));
        }

        public WeatherStation()
        {
            _observers = new List<IObserver>();
        }
    }

    interface IObserver
    {
        void Update(ISubject subject);
    }
    class NewsAgency : IObserver
    {
        public string AgencyName { get; set; }
        public NewsAgency(string name)
        {
            AgencyName = name;
        }

        public void Update(ISubject subject)
        {
            if (subject is WeatherStation weatherStation)
            {
                Console.WriteLine(string.Format("{0} reporting temperature {1} degree celcius", AgencyName, weatherStation.Temperature));
                Console.WriteLine();
            }
        }
    }
}
