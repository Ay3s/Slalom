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
        int radius;
        int racerSize;
        int xStarting, yStarting;
        Graphics g;
        bool slopeEditing;
        List<Gates> undefinedGates;
        IEnumerable<Gates> orderedGates;
        List<Gates> racingGates;
        Brush settingColor; 
        Racer racer;
        Stopwatch stopwatch;
        
        public Form1()
        {
            InitializeComponent();
            radius = 7;
            racerSize = 6;
            xStarting = SkiSlope.Width / 2;
            yStarting = 0;
            racerSize = 5;
            slopeEditing = false;
            firstSet = true;
            g = SkiSlope.CreateGraphics();
            undefinedGates = new List<Gates>();
            racingGates = new List<Gates>();
            settingColor = Brushes.Black;
            FinishLine.BackgroundImage = null;
            racer = new Racer(xStarting, yStarting);
            stopwatch = new Stopwatch();
        }

        //Takes input from user's mouse to draw an undefined gate.
        private void SkiSlope_MouseClick(object sender, MouseEventArgs e)
        {
            if (slopeEditing)
            {
                undefinedGates.Add(new Gates(e.X, e.Y, settingColor));
                orderedGates = undefinedGates.OrderBy(Gates => Gates.y);
                g.FillEllipse(settingColor, e.X, e.Y, radius, radius);
            }
        }

        #region Buttons

        // A button that allows or disallows creating undefined gates. Also disables starting a race when editing the track is allowed.
        private void ButtonSlopeSetting_Click(object sender, EventArgs e)
        {
            if (ButtonSlopeSetting.Text == "Začít kreslení dráhy")
            {
                firstSet = (undefinedGates.Count > 0) ? false : true;
                if (!firstSet)
                {
                    FinishLine.BackgroundImage = null;
                    racingGates.Clear();

                    if (undefinedGates[0].colorGate != settingColor)
                    {
                        SkiSlope.Refresh();
                        foreach (Gates gate in orderedGates)
                        {
                            gate.colorGate = settingColor;
                            g.FillEllipse(settingColor, gate.x, gate.y, radius, radius);
                        }
                        foreach(Gates gate in undefinedGates)
                        {
                            gate.colorGate = settingColor;
                            g.FillEllipse(settingColor, gate.x, gate.y, radius, radius);
                        }
                    }
                }
                ResetRacer();
                slopeEditing = true;
                ButtonSlopeSetting.Text = "Ukončit kreslení dráhy";
            }
            else
            {
                slopeEditing = false;
                ButtonSlopeSetting.Text = "Začít kreslení dráhy";
            }
        }

        // A button which starts the algorithm for finding the track and then starts the whole race.
        private void ButtonStarter_Click(object sender, EventArgs e)
        {
            if (undefinedGates.Count == 0) MessageBox.Show("Musíte postavit alespoň jednu branku!");
            else if (!slopeEditing)
            {
                var nonTrackGateColor = Brushes.LightGray;
                FinishLine.BackgroundImage = Properties.Resources.finish_line1;

                RacingTrackFinder(orderedGates);

                foreach (Gates gate in undefinedGates)
                {
                    g.FillEllipse(nonTrackGateColor, gate.x, gate.y, radius, radius);
                }

                foreach (Gates gate in racingGates)
                {
                    g.FillEllipse(gate.colorGate, gate.x, gate.y, radius, radius);
                }

                g.FillEllipse(Brushes.Black, racer.x, racer.y, racerSize, racerSize);

                TimerRacer.Enabled = true;
                stopwatch.Start();
                KeyPreview = true;
                ActiveControl = SkiSlope;
            }
        }

        // A button which deletes all gates and also stops the race.
        private void ButtonReset_Click(object sender, EventArgs e)
        {
            ResetRacer();
            undefinedGates.Clear();
            racingGates.Clear();
            Refresh();
        }

        // A button which starts the player on the track. This is possible anytime, even when the track is being edited. 
        private void ButtonStartRacer_Click(object sender, EventArgs e)
        {
            TimerRacer.Enabled = true;
            stopwatch.Start();
            KeyPreview = true;
            ActiveControl = SkiSlope;
        }

        #endregion

        #region Track Algorithm
        // Finds the right gates which would be defined the track.
        // The algorithm chooses only those Gates, which are possible to ride around.
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
                    // Checks the distance between the top of the track and the momentary picked gate. Runs only when there is no defined (colorized/racing) gate.
                    var difUD = 30;
                    if (gate.y >= difUD)
                    {
                        if (gate.LRChecker(gate.x,SkiSlope.Width)) gate.colorGate = leftTurningGate;
                        else gate.colorGate = rightTurningGate;
                        racingGates.Add(gate);
                        isFirst = false;
                    }
                }
                else
                {
                    if (GateChecker(racingGates[i], gate))
                    {
                        if (racingGates[i].colorGate == rightTurningGate) gate.colorGate = leftTurningGate;
                        else gate.colorGate = rightTurningGate;
                        racingGates.Add(gate);
                        i++;
                    }
                }
            }
        }

        // Checks the X and Y distance between the latest defined (colorized/racing) gate and the momentary picked gate.
        private bool GateChecker(Gates prevGate, Gates checkGate)
        {
            // The distance must not be smaller then the listed values. Otherwise it would be impossible to ride around those two gates without violation of the rules.
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

        // A clock that represents the motion of the player.
        private void TimerRacer_Tick(object sender, EventArgs e)
        {
            g.FillEllipse(Brushes.MintCream, racer.x, racer.y, racerSize, racerSize);
            racer.x += racer.xSpeed;
            racer.y += racer.ySpeed;
            CollisionChecker(racer.x, racer.y);
            g.FillEllipse(Brushes.Black, racer.x, racer.y, racerSize, racerSize);
        }

        // Takes the input from the arrows on the player's keyboard and changes the speed in different directions.
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

        // Checks if player violated the rules.
        private void CollisionChecker(int x, int y)
        {

            //Checking if player colided with a gate.
            foreach (Gates gate in racingGates)
            {
                if (Math.Abs(x - gate.x) <= racerSize/2 + radius/2 && Math.Abs(y - gate.y) <= racerSize/2 + radius/2)
                {
                    ResetRacer();
                    MessageBox.Show("Narazili jste do branky!");
                    TimerRacer.Enabled = true;
                    stopwatch.Start();
                }
            }

            // Checking if player went out of the SkiSlope.
            if (x <= 0 || x >= SkiSlope.Width)
            {
                ResetRacer();
                MessageBox.Show("Vyjeli jste ze sjezdovky!");
                TimerRacer.Enabled = true;
                stopwatch.Start();
            }

            // Checking if player went around the right way.
            if (racingGates.Count == 1)
            {
                if (racingGates[0].y - 3 < y && racingGates[0].y + 3 > y)
                {
                    if (racingGates[0].colorGate == Brushes.Red)
                    {
                        if (racingGates[0].x < x)
                        {
                            ResetRacer();
                            MessageBox.Show("Objeli jste branku ze špatné strany!");
                            TimerRacer.Enabled = true;
                            stopwatch.Start();
                        }
                    }
                    else
                    {
                        if (racingGates[0].x > x)
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
                for (int i = 0; i <= racingGates.Count - 1; i++)
                {
                    if (racingGates[i].y - 3 < y && racingGates[i].y + 3 > y)
                    {
                        if (racingGates[i].colorGate == Brushes.Red)
                        {
                            if (racingGates[i].x < x)
                            {
                                ResetRacer();
                                MessageBox.Show("Objeli jste branku ze špatné strany!");
                                TimerRacer.Enabled = true;
                                stopwatch.Start();
                            }
                        }
                        else
                        {
                            if (racingGates[i].x > x)
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
            
            // Checking if player went through the finish line or went around.
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
                    ActiveControl = null;
                    KeyPreview = false;
                    ResetRacer();
                }
            }
        }

        // Resets the player values to default.
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
