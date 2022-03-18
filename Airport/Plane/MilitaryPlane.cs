using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace Airport.Plane
{
    internal class MilitaryPlane : BasePlane
    {
        public MilitaryPlane(int number) : base(number, new PictureBox())
        {
            maxY = 45;

            xMoveInterval = 1;
            yMoveInterval = 3;

            Image.Image = Properties.Resources.m1;
            NumberLabel.Location = new Point(33, 18);
        }

        public override void Fix()
        {
            Thread.Sleep(3000);
        }

        public override void Unload()
        {
            Thread.Sleep(3000);
        }

        public override void StartTakesOff()
        {
            NumberLabel.Location = new Point(55, 18);
            base.StartTakesOff();
        }
    }
}
