using System;

namespace FunctionalityEvents
{
    class Events
    {
        // Most followed Convention for Dlegation used as a Event.
        public delegate void EventDelegation(object sender, EventArgs args);

        // Event keyword adds some restriction on usage of EventTrigger, capabilities and makes safe to use.
        public event EventDelegation EventTrigger;

        // Event Update to perform further operations
        private void EventHandler(object caller, EventArgs args) { Console.WriteLine("Event Triggrerd after action");}

        // Assign Event Action to the Event Trigger
        public Events() { EventTrigger = new EventDelegation(EventHandler); }

        public void CallAction()
        {
            // Action Code to Perform action //
            Console.WriteLine("Action Performed");

            // To trigger event after performing action.
            if (EventTrigger != null)
            {
                new ActionGenerator1(EventTrigger);
                new ActionGenerator2(EventTrigger);
            }
        }
    }

    public interface HandlerInterface
    {
        void EventHandler(object caller, EventArgs args);
    }

    class ActionGenerator1 : HandlerInterface
    {
        public ActionGenerator1(Events.EventDelegation dele)
        {
            dele = EventHandler;
            dele(new object(), new EventArgs());
        }

        public void EventHandler(object caller, EventArgs args)
        {
            Console.WriteLine("Handler Called from Class 1");
        }
    }

    class ActionGenerator2 : HandlerInterface
    {
        public ActionGenerator2(Events.EventDelegation dele)
        {
            dele = EventHandler;
            dele(new object(), new EventArgs());
        }

        public void EventHandler(object caller, EventArgs args)
        {
            Console.WriteLine("Handler Called from Class 2");
        }
    }
}
