using Airport.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace Airport.Plane
{
    internal abstract class BasePlane
    {
        public delegate void PlaneHandler();
        public event PlaneHandler OutAir;
        public event PlaneHandler Landed;

        private Label NumberLabel;
        private int maxX;
        private int maxY;
        private int xMoveIntervalTimer;
        private int xMoveInterval;
        private int yMoveIntervalTimer;
        private int yMoveInterval;
        private int dx;
        private int dy;

        public BasePlane(int number, PictureBox image)
        {
            Number = number;
            Image = image;

            State = StateEnum.InAir;

            NumberLabel = new Label();
            NumberLabel.BackColor = Color.Transparent;
            NumberLabel.ForeColor = Color.White;
            NumberLabel.Location = new Point(17, 10);
            NumberLabel.Text = number.ToString();
            NumberLabel.Visible = true;
            NumberLabel.Parent = Image;

            maxX = 391;
            maxY = 40;
            xMoveIntervalTimer = 0;
            xMoveInterval = 1;
            yMoveIntervalTimer = 0;
            yMoveInterval = 3;
            dx = 0;
            dy = 0;


        }

        public int Number { get; private set; }
        public PictureBox Image { get; set; }
        public StateEnum State { get; private set; }

        private Employee employee;
        public Employee Employee
        { 
            get => employee; 
            set
            {
                employee = value;

                Employee.Image.Parent = Image;

                Employee.WorkEnded += StartTakesOff;
                Employee.WorkStarted += StartUnloading;
            } 
        }

        public abstract void Unload();
        public abstract void Fix();

        public void StartLanding() 
        {
            State = StateEnum.Landing;

            var timer = new Timer();
            timer.Tick += (object sender, EventArgs e) =>
            {
                timer.Enabled = PlaneMove();
                if (!timer.Enabled)
                {
                    State = StateEnum.WaitAfterLand;
                    Landed?.Invoke();
                }
            };
            timer.Interval = 1;
            timer.Enabled = true;
        }

        public void StartUnloading() 
        {
            State = StateEnum.Unloading;
        }

        public void StartTakesOff()
        {
            State = StateEnum.TakesOff;

            var timer = new Timer();
            Image.Location = new Point(0, 40);
            timer.Tick += (object sender, EventArgs e) =>
            {
                timer.Enabled = PlaneMove();
                if (!timer.Enabled)
                {
                    State = StateEnum.OutAir;
                    OutAir?.Invoke();
                }
            };
            timer.Interval = 1;
            timer.Enabled = true;
        }

        private bool PlaneMove()
        {
            dx = 0;
            dy = 0;

            if (++xMoveIntervalTimer >= xMoveInterval)
            {
                if (Image.Location.X + 1 <= maxX)
                {
                    dx = 1;
                }
                else
                {
                    if (State == StateEnum.TakesOff)
                        State = StateEnum.OutAir;

                    return false;
                }

                xMoveIntervalTimer = 0;
            }

            if (Image.Location.X >= maxX / 2 && ++yMoveIntervalTimer >= yMoveInterval)
            {
                if (State == StateEnum.TakesOff && Image.Location.Y - 1 >= 0) 
                    dy = -1;
                else if ((State == StateEnum.Landing && Image.Location.Y + 1 <= maxY)) 
                    dy = 1;

                yMoveIntervalTimer = 0;
            }



            if (dx == 1 || dy == 1 || dy == -1)
                Image.Location = new Point(Image.Location.X + dx, Image.Location.Y + dy);

            return true;
        }
    }
}
