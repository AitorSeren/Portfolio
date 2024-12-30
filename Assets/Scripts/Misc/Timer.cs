using System;
using Utilities;

namespace Utilities
{
    public abstract class Timer
    {
        protected float initialTime;
        protected float Time
        {
            get; set;
        }

        public bool isRunning { get; protected set; }
        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };
        public float Progress()
        {
            return (Time / initialTime);
        }

        protected Timer(float value)
        {
            initialTime = value;
            isRunning = false;
        }


        public void Start()
        {
            Time = initialTime;
            if (!isRunning)
            {
                isRunning = true;
                OnTimerStart.Invoke();
            }
        }

        public void Stop()
        {
            if (isRunning)
            {
                isRunning = false;
                OnTimerStop.Invoke();
            }
        }



        public void Resume()
        {
            isRunning = true;
        }

        public void Pause()
        {
            isRunning = false;
        }


        public abstract void Tick(float deltaTime);
    }
}

// Temporizador por cuenta atrás

public class CountdownTimer : Timer
{
    public CountdownTimer(float value) : base(value) { }


    public override void Tick(float deltaTime)
    {
        if (isRunning && Time > 0)
        {
            Time -= deltaTime;
        }


        if (isRunning && Time <= 0)
        {
            Stop();
        }
    }


    public bool IsFinished()
    {
        return (Time <= 0);
    }

    public void Reset()
    {
        Time = initialTime;
    }


    public void Reset(float newTime)
    {
        Time = newTime;
        Reset();
    }
}

// Temporizador por cuenta hacia delante


public class StopwatchTimer : Timer
{
    public StopwatchTimer() : base(0) { }

    public override void Tick(float deltaTime)
    {
        if (isRunning)
        {
            Time += deltaTime;
        }
    }

    public void Reset()
    {
        Time = 0;
    }

    public float GetTime()
    {
        return Time;
    }
}