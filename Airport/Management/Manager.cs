using Airport.Plane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Management
{
    internal class Manager
    {
        public bool Started { get; set; } = false;

        private Employee Employee;
        private BasePlane Plane;
        private Form1 Form;
        private WeatherForecast Weather;

        public Manager(Form1 form, WeatherForecast weather)
        {
            Form = form;
            Weather = weather;

            Weather.OnDayTimes += ChangeDayTime;
            Weather.OnWeather += ChangeWeather;

            Weather.StartDayTimes();
            Weather.StartWeather();
        }

        public void Start()
        {
            if (Started)
                return;

            Started = true;
            var colors = new string[] { "red", "green", "blue" };
            int index = new Random().Next(colors.Length);
            
            Employee = new Employee(colors[index]);
            Employee.Image.Parent = Form.GetRunway();

            Plane = GetPlane();
            Plane.Landed += AfterLanding;
            Plane.OutAir += OutAir;

            Plane.Image.Parent = Form.GetRunway();

            Plane.StartLanding();
            Plane.Employee = Employee;
        }

        private BasePlane GetPlane()
        {
            var r = new Random();
            var pi = r.Next(0, 2);

            if (pi == 1)
                return new MilitaryPlane(r.Next(1, 10));

            return new PassangerPlane(r.Next(1, 10));
        }

        private void AfterLanding() => Employee.Work(Plane);
        private void OutAir()
        {
            Plane.Image.Visible = false;
            Plane.Image.Enabled = false;

            Started = false;
            Start();
        }

        private void ChangeDayTime()
        {
            if (Weather.DayTime == WeatherForecast.DayTimes.Day)
                LightOff();
            else
                LightOn();
        }

        private void ChangeWeather()
        {
            var w = Weather.Weather.ToString().ToLower();
            if (Weather.Weather == WeatherForecast.Weathers.Sun && Weather.DayTime == WeatherForecast.DayTimes.Nitht)
                w = "moon";

            Form.GetWeatherBox().Image = (Image)Properties.Resources.ResourceManager.GetObject(w) ?? Properties.Resources.sun;
        }

        private void LightOn()
        {
            Form.GetRunway().BackColor = Color.FromArgb(129, 142, 235);
            ChangeLight("passtart_night"); 
        }

        private void LightOff()
        {
            Form.GetRunway().BackColor = Color.FromArgb(168, 237, 247);
            ChangeLight("passtart");
        }

        private void ChangeLight(string light) 
            => Form.GetRunway().BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(light) ?? Properties.Resources.passtart;
    }
}
