using System;
using System.Threading.Tasks;

namespace Utilities
{
    public static class DelayAction
    {
        public static async void Execute(Action action, float delaySeconds)
        {
            if (action == null) return;

            await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
            action.Invoke();
        }
    }
}

