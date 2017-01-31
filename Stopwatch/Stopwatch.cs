using System;
using System.Collections;
using UnityEngine;

public class Stopwatch {
    
    /// <summary>
    /// The stopwatch duration
    /// </summary>
    private float duration = 1;
    
    /// <summary>
    /// The moment where the stopwatch started to count the time
    /// </summary>
    private float startingTime;
    
    /// <summary>
    /// Generate a stopwatch
    /// </summary>
    /// <param name="duration">The duration of its time counter</param>
    public Stopwatch(float duration) {
        this.duration = duration;

        Reset();
    }
    
    /// <summary>
    /// when the stopwatch is suposed to end
    /// </summary>
    public float EndingTime { get { return startingTime + duration; } }

    /// <summary>
    /// If stopwatch ended its cicle
    /// </summary>
    public bool IsReady { get { return Time.time - startingTime > duration; } }
    
    /// <summary>
    /// Coroutine that sets an action to happen continuously while the stopwatch is not ready
    /// </summary>
    /// <param name="duration">the duration that the action will take to be done</param>
    /// <param name="action">the action what will be executed</param>
    public static IEnumerator PlayUntilReady(float duration, Action<float> action, Action Finale = null) {
        Stopwatch sw = new Stopwatch(duration);

        yield return new WaitUntil(() => {
            action(sw.Get());

            return sw.IsReady;
        });

        if(Finale != null)
            Finale();
    }
   
    /// <summary>
    /// Get the percentage of the time counted over its duration
    /// </summary>
    /// <returns>the percentage</returns>
    public float Get() {
        return Mathf.Clamp((Time.time - startingTime) / duration, 0.0f, 1.0f);
    }
   
    /// <summary>
    /// Reset the starting time
    /// </summary>
    /// <param name="dur">optional parameter that changes the duration of the stopwatch</param>
    public void Reset(float? dur = null) {
        if(dur.HasValue)
            duration = dur.Value;
        startingTime = Time.time;
    }
}