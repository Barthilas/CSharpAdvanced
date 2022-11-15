//Delegate = null if Invocation list 0.
//Event doesnt have invocation list.

namespace ConsoleApp2
{
    delegate void ButtonClick(Button button);
    class Button
    {
        /* This is event! Do not use public delegate, do not use these methods they are verbose.. use events.
         * Why? We dont want anybody to set delegate=null for example or Invoke it.
        private ButtonClick Click;

        public void AppendToClick(ButtonClick click)
        {
            Click += click;
        }

        public void RemoveFromClick(ButtonClick click)
        {
            // If doens't exists doesn't throw error.        
            Click -= click;
        }
        */

        /// <summary>
        /// Explicit Property, Non-automatic property. (Demonstration)
        /// Event=delegate that can only be used in certain way outside of the class.
        /// Only Button class can invoke this element.
        /// Event = protected delegate
        /// </summary>
        //public event ButtonClick Click
        //{
        //    add { _click += value; }
        //    remove { _click -= value; }
        //}

        //private event ButtonClick _click;

        public event ButtonClick Click;

        public void SimulateClick()
        {
            //Always guard with delegates.
            if (Click != null)
                Click(this);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var button = new Button();
            //button.Click = null;
            //button.Click();
            button.Click += (ButtonClickedBehaviour);
            button.Click += (OtherButtonClickedBehaviour);
            button.SimulateClick();
        }
        static void ButtonClickedBehaviour(Button button)
        {
            Console.WriteLine("Button Clicked!");
        }
        static void OtherButtonClickedBehaviour(Button button)
        {
            Console.WriteLine("More Button Clicked!");
        }
    }
}