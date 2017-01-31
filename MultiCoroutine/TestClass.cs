// Example

using System.Collections;
using UnityEngine;

internal class TestClass : MonoBehaviour {
    
    /// <summary>
    /// Array containing objects with coroutine methods
    /// </summary>
    private SubroutineHolder[] array;
    
    //!!!!!!!-----------------------------LOOK HERE-------------------------------!!!!!!!//
    /// <summary>
    /// Fire a coroutine that splits into several subroutines then resumes itself
    /// </summary>
    private IEnumerator Mainroutine() {
        Debug.Log("Split mainroutine!");
        yield return this.StartMultiCoroutine(array.Select(s => s.Subroutine));
        Debug.Log("All subroutines finished!");
    }
   
    /// <summary>
    /// initialize this object
    /// </summary>
    private void Start() {
        array = new SubroutineHolder[5];
        for(int i = 0; i < array.Length; i++)
            array[i] = new SubroutineHolder();

        StartCoroutine(Mainroutine());
    }


   
    /// <summary>
    /// Simple object holding a coroutine method
    /// </summary>
    private class SubroutineHolder {
        public IEnumerator Subroutine() {
            Debug.Log("started!");
            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 5f));
            Debug.Log("done!");
        }
    }
}