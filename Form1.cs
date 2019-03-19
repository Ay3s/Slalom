using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slalom
{
    public partial class Form1 : Form
    {
        bool firstSet;
        int radius = 7;
        int racerSize = 6;
        int xStarting, yStarting;
        Graphics g;
        bool slopeSetting;
        List<Gates> listGates;
        IEnumerable<Gates> orderedGates;
        List<Gates> listRacingGates;
        Brush settingColor; 
        Racer racer;
        Stopwatch stopwatch;
        
        public Form1()
        {
            InitializeComponent();
            xStarting = SkiSlope.Width / 2;
            yStarting = 0;
            racerSize = 5;
            slopeSetting = false;
            firstSet = true;
            g = SkiSlope.CreateGraphics();
            listGates = new List<Gates>();
            listRacingGates = new List<Gates>();
            settingColor = Brushes.Black;
            FinishLine.BackgroundImage = null;
            racer = new Racer(xStarting, yStarting);
            stopwatch = new Stopwatch();
        }

        private void SkiSlope_MouseClick(object sender, MouseEventArgs e)
        {
            if (slopeSetting)
            {
                listGates.Add(new Gates(e.X, e.Y, settingColor));
                orderedGates = listGates.OrderBy(Gates => Gates.y);
                g.FillEllipse(settingColor, e.X, e.Y, radius, radius);
            }
        }

        #region Buttons
        private void ButtonSlopeSetting_Click(object sender, EventArgs e)
        {
            if (ButtonSlopeSetting.Text == "Začít kreslení dráhy")
            {
                firstSet = (listGates.Count > 0) ? false : true;
                if (!firstSet)
                {
                    FinishLine.BackgroundImage = null;
                    listRacingGates.Clear();

                    if (listGates[0].colorGate != settingColor)
                    {
                        SkiSlope.Refresh();
                        foreach (Gates gate in orderedGates)
                        {
                            gate.colorGate = settingColor;
                            g.FillEllipse(settingColor, gate.x, gate.y, radius, radius);
                        }
                        foreach(Gates gate in listGates)
                        {
                            gate.colorGate = settingColor;
                            g.FillEllipse(settingColor, gate.x, gate.y, radius, radius);
                        }
                    }
                }
                ResetRacer();
                slopeSetting = true;
                ButtonSlopeSetting.Text = "Ukončit kreslení dráhy";
            }
            else
            {
                slopeSetting = false;
                ButtonSlopeSetting.Text = "Začít kreslení dráhy";
            }
        }

        private void ButtonStarter_Click(object sender, EventArgs e)
        {
            if (listGates.Count == 0) MessageBox.Show("Musíte postavit alespoň jednu branku!");
            else if (!slopeSetting)
            {
                var nonTrackGateColor = Brushes.LightGray;
                FinishLine.BackgroundImage = Properties.Resources.finish_line1;

                RacingTrackFinder(orderedGates);

                foreach (Gates gate in listGates)
                {
                    g.FillEllipse(nonTrackGateColor, gate.x, gate.y, radius, radius);
                }

                foreach (Gates gate in listRacingGates)
                {
                    g.FillEllipse(gate.colorGate, gate.x, gate.y, radius, radius);
                }

                g.FillEllipse(Brushes.Black, racer.x, racer.y, racerSize, racerSize);

                TimerRacer.Enabled = true;
                stopwatch.Start();
                this.KeyPreview = true;
                this.ActiveControl = SkiSlope;
            }
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            ResetRacer();
            listGates.Clear();
            listRacingGates.Clear();
            Refresh();
        }

        private void ButtonStartRacer_Click(object sender, EventArgs e)
        {
            TimerRacer.Enabled = true;
            this.KeyPreview = true;
            this.ActiveControl = SkiSlope;
        }

        #endregion

        #region Track Algoritm
        // Finds the right gates which would be defined the Track.
        // The algorythm chooses only those Gates, which are possible to ride around.
        private void RacingTrackFinder(IEnumerable<Gates> gates)
        {
            var i = 0;
            var isFirst = true;
            var leftTurningGate = Brushes.Red;
            var rightTurningGate = Brushes.Blue;

            foreach (Gates gate in gates)
            {
                if (isFirst)
                {
                    var difUD = 30;
                    if (gate.y >= difUD)
                    {
                        if (gate.LRChecker(gate.x,SkiSlope.Width)) gate.colorGate = leftTurningGate;
                        else gate.colorGate = rightTurningGate;
                        listRacingGates.Add(gate);
                        isFirst = false;
                    }
                }
                else
                {
                    if (GateChecker(listRacingGates[i], gate))
                    {
                        if (listRacingGates[i].colorGate == rightTurningGate) gate.colorGate = leftTurningGate;
                        else gate.colorGate = rightTurningGate;
                        listRacingGates.Add(gate);
                        i++;
                    }
                }
            }
        }

        private bool GateChecker(Gates prevGate, Gates checkGate)
        {
            var difX = 30;
            var difY = 45;

            if (prevGate.colorGate == Brushes.Red)
            {
                if (checkGate.x - prevGate.x >= difX && checkGate.y - prevGate.y >= difY) return true;
                else return false;
            }
            else
            {
                if (prevGate.x - checkGate.x >= difX && checkGate.y - prevGate.y >= difY) return true;
                else return false;
            }
        }
        #endregion

        #region Racer Properties
        private void TimerRacer_Tick(object sender, EventArgs e)
        {
            g.FillEllipse(Brushes.MintCream, racer.x, racer.y, racerSize, racerSize);
            racer.x += racer.xSpeed;
            racer.y += racer.ySpeed;
            CollisionChecker(racer.x, racer.y);
            g.FillEllipse(Brushes.Black, racer.x, racer.y, racerSize, racerSize);
        }

        private void SkiSlope_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var LRdif = 2;
            var UDdif = 50;
            if (e.KeyCode == Keys.Left)
            {
                racer.xSpeed -= LRdif;
            }
            else if (e.KeyCode == Keys.Right)
            {
                racer.xSpeed += LRdif;
            }
            else if (e.KeyCode == Keys.Up)
            {
                TimerRacer.Interval += UDdif;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (TimerRacer.Interval == UDdif) TimerRacer.Interval = 1;
                else if (TimerRacer.Interval > UDdif) TimerRacer.Interval -= UDdif;
            }
            e.IsInputKey = true;
        }

        private void CollisionChecker(int x, int y)
        {

            //Checking if racer colided with a gate
            foreach (Gates gate in listRacingGates)
            {
                if (Math.Abs(x - gate.x) <= racerSize/2 + radius/2 && Math.Abs(y - gate.y) <= racerSize/2 + radius/2)
                {
                    ResetRacer();
                    MessageBox.Show("Narazili jste do branky!");
                    TimerRacer.Enabled = true;
                    stopwatch.Start();
                }
            }

            // Checking if racer went out of the SkiSlope
            if (x <= 0 || x >= SkiSlope.Width)
            {
                ResetRacer();
                MessageBox.Show("Vyjeli jste ze sjezdovky!");
                TimerRacer.Enabled = true;
                stopwatch.Start();
            }

            // Checking if racer went around the right way.
            if (listRacingGates.Count == 1)
            {
                if (listRacingGates[0].y - 3 < y && listRacingGates[0].y + 3 > y)
                {
                    if (listRacingGates[0].colorGate == Brushes.Red)
                    {
                        if (listRacingGates[0].x < x)
                        {
                            ResetRacer();
                            MessageBox.Show("Objeli jste branku ze špatné strany!");
                            TimerRacer.Enabled = true;
                            stopwatch.Start();
                        }
                    }
                    else
                    {
                        if (listRacingGates[0].x > x)
                        {
                            ResetRacer();
                            MessageBox.Show("Objeli jste branku ze špatné strany!");
                            TimerRacer.Enabled = true;
                            stopwatch.Start();
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i <= listRacingGates.Count - 1; i++)
                {
                    if (listRacingGates[i].y - 3 < y && listRacingGates[i].y + 3 > y)
                    {
                        if (listRacingGates[i].colorGate == Brushes.Red)
                        {
                            if (listRacingGates[i].x < x)
                            {
                                ResetRacer();
                                MessageBox.Show("Objeli jste branku ze špatné strany!");
                                TimerRacer.Enabled = true;
                                stopwatch.Start();
                            }
                        }
                        else
                        {
                            if (listRacingGates[i].x > x)
                            {
                                ResetRacer();
                                MessageBox.Show("Objeli jste branku ze špatné strany!");
                                TimerRacer.Enabled = true;
                                stopwatch.Start();
                            }
                        }
                    }
                }
            }
            
            // Checking if racer went through the finish line or went around
            if (y >= FinishLine.Location.Y)
            {
                if (x < FinishLine.Location.X || x > FinishLine.Location.X + FinishLine.Width)
                {
                    ResetRacer();
                    MessageBox.Show("Netrefili jste cíl!");
                    TimerRacer.Enabled = true;
                    stopwatch.Start();
                }
                else
                {
                    TimerRacer.Enabled = false;
                    MessageBox.Show("Gratulace, dokončili si závod v čase: " + Math.Round(stopwatch.Elapsed.TotalSeconds,3) + "s");
                    ResetRacer();
                }
            }
        }

        private void ResetRacer()
        {
            TimerRacer.Enabled = false;
            TimerRacer.Interval = 500;
            stopwatch.Reset();
            racer.Reset(xStarting, yStarting);
        }
        #endregion
    }
}
