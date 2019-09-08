using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Transitions;


/* To test this out from your app:
      Transitions trans = new Transitions();
      trans.TransitionsTest();
 */

namespace EZAPI.Toolbox
{
    /// <summary>
    /// Check out the dot-net-transitions project on Google Code:
    /// http://code.google.com/p/dot-net-transitions/
    /// </summary>
    public class Transitions
    {
        public event EventHandler TransitionCompleted;

        Transition transition;

        public Transitions()
        {

        }

        void transition_TransitionCompletedEvent(object sender, Transition.Args e)
        {
            if (TransitionCompleted != null)
                TransitionCompleted(this, e);
        }

        public void ResizeControls(Dictionary<Control, Point> controlSize, int Milliseconds = 1000)
        {
            // Modify the Size properties using a transition.
            transition = new Transition(new TransitionType_EaseInEaseOut(Milliseconds));
            transition.TransitionCompletedEvent += transition_TransitionCompletedEvent;
            foreach (Control control in controlSize.Keys)
            {
                Point size = controlSize[control];
                transition.add(control, "Width", size.X);
                transition.add(control, "Height", size.Y);
            }
            transition.run();
        }

        public void MoveControls(Dictionary<Control, Point> controlDestination, int Milliseconds = 1000)
        {
            // Modify the Left and Top properties using a transition.
            transition = new Transition(new TransitionType_EaseInEaseOut(Milliseconds));
            transition.TransitionCompletedEvent += transition_TransitionCompletedEvent;
            foreach (Control control in controlDestination.Keys)
            {
                Point location = controlDestination[control];
                transition.add(control, "Left", location.X);
                transition.add(control, "Top", location.Y);
            }
            transition.run();
        }

        public void TransitionsTest()
        {
            var pictureBox = new PictureBox
            {
                ImageLocation = "http://bit.ly/aTsxKI",
                SizeMode = PictureBoxSizeMode.AutoSize
            };
            var textBox = new TextBox
            {
                Text = "Hello World",
                Location = new Point(140, 140)
            };
            var form = new Form
            {
                Controls =
                {
                    textBox,
                    pictureBox
                }
            };
            form.Click += (sender, e) =>
            {
                // swap the Left and Top properties using a transition
                var t = new Transition(new TransitionType_EaseInEaseOut(1000));
                t.add(pictureBox, "Left", textBox.Left);
                t.add(pictureBox, "Top", textBox.Top);
                t.add(textBox, "Left", pictureBox.Left);
                t.add(textBox, "Top", pictureBox.Top);
                t.run();
            };
            form.ShowDialog();

        }

    } // class
} // namespace
