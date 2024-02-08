using UnityEngine;
using Zenject;

namespace DRAP.Objectives
{
    public class OnLevelTimerEnded : Signal
    {
    }

    public class LevelTimer : ITickable
    {
        public float TimeRemaining => targetTime - currentTime;

        float targetTime = 0f;
        float currentTime = 0f;
        bool running = true;

        SignalBus signalBus;

        public LevelTimer(SignalBus signalBus, float time)
        {
            targetTime = time;
            this.signalBus = signalBus;
        }

        public void Tick()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= targetTime && running)
            {
                Complete();
            }
        }

        private void Complete()
        {
            signalBus.Fire<OnLevelTimerEnded>();
            running = false;
        }
    }
}