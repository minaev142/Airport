using Airport.Plane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Image.Location = new Point(0, 27);
            Image.Image = Properties.Resources.employee_blue;
            //Image.Image = (Image)Properties.Resources.ResourceManager.GetObject($"employee_{color}");
            Image.Visible = false;
            Image.Enabled = false;
            Image.BackColor = System.Drawing.Color.Transparent;
        }

        public bool Work(BasePlane plane)
        {
            if (plane.State != StateEnum.WaitAfterLand)
                return false;

            Image.Parent = plane.Image;
            Image.Visible = true;
            Image.Enabled = true;

            WorkStarted?.Invoke();

            plane.Unload();
            plane.Fix();

            WorkEnded?.Invoke();

            Image.Parent = null;
            Image.Visible = false;
            Image.Enabled = false;

            return true;
        }
    }
}
