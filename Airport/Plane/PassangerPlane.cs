using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace Airport.Plane
{
    internal class PassangerPlane : BasePlane
    {
        private int PassangerCount;

        public PassangerPlane(int number) : base(number, new PictureBox())
        {
            xMoveInterval = 2;
            yMoveInterval = 6;
            PassangerCount = new Random().Next(30, 50);
            Image.Image = Properties.Resources.P1;
            NumberLabel.Location = new Point(17, 10);

            Wheels = Properties.Resources.passwheels;
        }

        public override void StartTakesOff()
        {
            NumberLabel.Location = new Point(70, 10);
            SupportImage.Location = new Point(-17, 0);
            base.StartTakesOff();
        }

        public override void StartUnloading()
        {
            SupportImage.Image = Properties.Resources.passengerentry;
            base.StartUnloading();
        }

        public override void Fix()
        {
            Thread.Sleep(3000);
        }

        public override void Unload()
        {
            var timer = new Timer();
            timer.Tick += (object sender, EventArgs e) =>
            {
                timer.Enabled = PassangerCount != 0;
                if (!timer.Enabled)
                    PassangerCount--;
            };
            timer.Interval = 1000;
            timer.Enabled = true;
        }
    }
}
