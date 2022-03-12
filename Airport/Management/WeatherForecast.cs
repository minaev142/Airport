using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace Airport.Management
{
    internal class WeatherForecast
    {
        public delegate void WeatherHandler();
        public event WeatherHandler OnDayTimes;
        public event WeatherHandler OnWeather;

        public enum DayTimes
        {
            Day,
            Nitht
        }

        public enum Weathers
        {
            Sun = 0,
            Cloud = 1,
            Rain = 2
        }

        public DayTimes DayTime { get; set; } = DayTimes.Day;
        public Weathers Weather { get; set; } = Weathers.Sun;

        public void StartDayTimes()
        {
            var timer = new Timer();
            var day = false;
            timer.Tick += (object sender, EventArgs e) =>
            {
                DayTime = day ? DayTimes.Day : DayTimes.Nitht;
                OnDayTimes?.Invoke();

                if (!day && Weather == Weathers.Sun)
                    OnWeather?.Invoke();

                day = !day;
            };
            timer.Interval = 12000;
            timer.Enabled = true;
        }

        public void StartWeather()
        {
            var timer = new Timer();
            var r = new Random();
            timer.Tick += (object sender, EventArgs e) =>
            {
                Weather = (Weathers)r.Next(0, 3);
                OnWeather?.Invoke();
            };

            timer.Interval = 1500;
            timer.Enabled = true;
        }
    }
}
