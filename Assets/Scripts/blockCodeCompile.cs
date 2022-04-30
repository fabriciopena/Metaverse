using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockCodeCompile : MonoBehaviour {
    
    public void compileBlockCode(Transform startBlock) {
        for (int childCounter = 1; childCounter < startBlock.childCount; childCounter ++) {
            Transform currentBlockRun = startBlock.GetChild(childCounter).GetChild(0);
            MonoBehaviour blockScriptName = currentBlockRun.GetComponent<MonoBehaviour>();

            blockScriptName.BroadcastMessage("runCode");
            // Calls the inner function of the script using a built-in method of MonoBehaviour
            // When making new Block Scripts, the compiler will look for the following function:
            // public void runCode() {}
        }
    }
}
