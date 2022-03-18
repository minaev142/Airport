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

        private int xMoveIntervalTimer;
        private int yMoveIntervalTimer;
        private int dx;
        private int dy;

        protected Label NumberLabel;

        protected int maxX;
        protected int maxY;
        protected int xMoveInterval;
        protected int yMoveInterval;

        public BasePlane(int number, PictureBox image)
        {
            Number = number;
            Image = image;

            Image.BackColor = Color.Transparent;
            Image.Size = new Size(100, 50);
            State = StateEnum.InAir;

            NumberLabel = new Label();
            NumberLabel.BackColor = Color.Transparent;
            NumberLabel.ForeColor = Color.White;
            NumberLabel.Text = number.ToString();
            NumberLabel.Visible = true;
            NumberLabel.Parent = Image;

            maxX = 391;
            maxY = 40;
            xMoveIntervalTimer = 0;
            yMoveIntervalTimer = 0;
            dx = 0;
            dy = 0;

            SupportImage = new PictureBox();
            SupportImage.Parent = Image;
            SupportImage.Location = new Point(0, 0);
            SupportImage.Size = new Size(100, 50);
            SupportImage.Visible = true;
            SupportImage.Enabled = true;
            SupportImage.BackColor = Color.Transparent;
        }

        public int Number { get; private set; }
        public PictureBox Image { get; set; }
        public PictureBox SupportImage { get; set; }
        public StateEnum State { get; private set; }

        private Employee employee;
        public Employee Employee
        { 
            get => employee; 
            set
            {
                employee = value;

                employee.WorkEnded += StartTakesOff;
                employee.WorkStarted += StartUnloading;
            } 
        }

        public abstract void Unload();
        public abstract void Fix();

        protected Bitmap Wheels { get; set; }

        public virtual void StartLanding() 
        {
            State = StateEnum.Landing;
            SupportImage.Image = Wheels;

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

        public virtual void StartUnloading() 
        {
            State = StateEnum.Unloading;
        }

        public virtual void StartTakesOff()
        {
            State = StateEnum.TakesOff;
            SupportImage.Image = Wheels;

            var timer = new Timer();
            Image.Image.RotateFlip(RotateFlipType.Rotate180FlipY);
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
                if (Image.Location.X + 1 <= maxX && State == StateEnum.Landing)
                    dx = 1;
                else if (Image.Location.X + 1 > 0 && State == StateEnum.TakesOff)
                    dx = -1;
                else
                {
                    if (State == StateEnum.TakesOff)
                        State = StateEnum.OutAir;

                    return false;
                }

                xMoveIntervalTimer = 0;
            }

            var rule = State == StateEnum.Landing
                ? Image.Location.X >= maxX / 2
                : Image.Location.X <= maxX / 2;

            if (rule && ++yMoveIntervalTimer >= yMoveInterval)
            {
                if (State == StateEnum.TakesOff && Image.Location.Y - 1 >= 0) 
                    dy = -1;
                else if ((State == StateEnum.Landing && Image.Location.Y + 1 <= maxY)) 
                    dy = 1;

                yMoveIntervalTimer = 0;
            }



            if (dx != 0 || dy != 0)
                Image.Location = new Point(Image.Location.X + dx, Image.Location.Y + dy);

            return true;
        }
    }
}
