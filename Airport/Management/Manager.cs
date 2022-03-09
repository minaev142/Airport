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
        private Employee Employee;
        private BasePlane Plane;
        private Form1 Form;

        public Manager(Form1 form)
        {
            Form = form;
        }

        public void Start()
        { 
            Employee = new Employee("red");
            Plane = new PassangerPlane(123);
            Plane.Landed += AfterLanding;
            Plane.OutAir += OutAir;
            Plane.Image.Parent = Form.GetRunway();
            Plane.StartLanding();
            Plane.Employee = Employee;
        }

        private void AfterLanding() => Employee.Work(Plane);
        private void OutAir()
        {
            Plane.Image.Visible = false;
            Plane.Image.Enabled = false;
            Plane = null;
            Employee = new Employee("green");

            Start();
        }
    }
}
