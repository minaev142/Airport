using Airport.Management;

namespace Airport
{
    public partial class Form1 : Form
    {
        private Manager Manager;
        public Form1()
        {
            InitializeComponent();
            Manager = new Manager(this, new WeatherForecast());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Manager.Start();
        }

        public Panel GetRunway() => RunwayPanel;
        public PictureBox GetWeatherBox() => WeatherPicture;
    }
}