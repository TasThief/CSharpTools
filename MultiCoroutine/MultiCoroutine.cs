using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate IEnumerator CoroutineDel();

/// <summary>
/// Multiroutines are made to split a coroutine into several sub routines, when all those subroutines
/// are finished the main coroutine is resumed.
/// </summary>
public static class MultiRoutine {
    /// <summary>
    /// Simply class that wraps a integer to be used as a counter for the coroutine stream
    /// </summary>
    private class Counter {
        //the counter
        public int amount;

        //constructor
        public Counter(int limit) {
            amount = limit;
        }
    }

    /// <summary>
    /// Linq like interface that stores all IEnumerators in a given colection of objects
    /// </summary>
    /// <param name="colectRoutine">Method that extracts all coroutines</param>
    /// <returns>a list of coroutines</returns>
    public static CoroutineDel[] Select<T>(this IList<T> self, Func<T, CoroutineDel> colectRoutine) {
        //Initialize the result array
        CoroutineDel[] result = new CoroutineDel[self.Count];

        //Colect all routines and store them inside the result array
        for(int i = 0; i < self.Count; i++)
            result[i] = colectRoutine(self[i]);

        //return the result array
        return result;
    }

    /// <summary>
    /// Wraps a coroutine inside a method that triggers a callback when its done
    /// </summary>
    /// <param name="coroutine">the method that will be wrapped</param>
    /// <param name="callback">the callback</param>
    /// <returns>the new coroutined wrapped inside the coroutine</returns>
    private static IEnumerator CallbackWraper(CoroutineDel coroutine, Action callback) {
        //wait the coroutine to end
        yield return coroutine();

        //invoke the callback
        callback();
    }

    /// <summary>
    /// Start a stream of paralel coroutines and yield return until all of them are done
    /// </summary>
    /// <param name="coroutineList">the coroutine stream</param>
    /// <returns>the new coroutine</returns>
    public static IEnumerator StartMultiCoroutine(this MonoBehaviour self, CoroutineDel[] coroutineList) {
        //start the coroutine counter
        Counter count = new Counter(coroutineList.Length);

        //for each coroutine in the stream
        foreach(CoroutineDel coroutine in coroutineList)

            //start it and wraps it inside the callbackwrapper
            self.StartCoroutine(CallbackWraper(coroutine, () => count.amount--));

        //yield until all of them are done
        yield return new WaitUntil(() => count.amount == 0);
    }
}