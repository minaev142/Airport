using Airport.Plane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace Airport.Management
{
    internal class Employee
    {
        public delegate void EmployeeHandler();
        public event EmployeeHandler WorkEnded;
        public event EmployeeHandler WorkStarted;
        public PictureBox Image { get; set; }
        public string Color;

        public Employee(string color)
        {
            Color = color;
            Image = new PictureBox();
            Image.Location = new Point(390, 70);
            Image.Image = (Image)Properties.Resources.ResourceManager.GetObject($"employee_{color}");
            Image.Size = new Size(19, 30);
            Image.Visible = false;
            Image.Enabled = false;
        }

        public bool Work(BasePlane plane)
        {
            if (plane.State != StateEnum.WaitAfterLand)
                return false;

            WorkStarted?.Invoke();
            
            Image.BackColor = System.Drawing.Color.Transparent;
            Image.Visible = true;
            Image.Enabled = true;

            plane.Unload();
            plane.Fix();

            Leave();

            return true;
        }

        private void Leave()
        {
            int x = 0;

            var timer = new Timer();
            timer.Tick += (object sender, EventArgs e) =>
            {
                x++;
                Image.Location = new Point(Image.Location.X + 1, Image.Location.Y);
                timer.Enabled = x != 100;
                if (!timer.Enabled)
                {
                    WorkEnded?.Invoke();

                    Image.Parent = null;
                    Image.Visible = false;
                    Image.Enabled = false;
                }
            };
            timer.Interval = 25;
            timer.Enabled = true;
        }
    }
}
