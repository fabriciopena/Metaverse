using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockCodeCompile : MonoBehaviour {
    
    public void compileBlockCode(Transform startBlock) {
        for (int childCounter = 1; childCounter < startBlock.childCount; childCounter ++) {
            Transform currentBlockRun = startBlock.GetChild(childCounter).GetChild(0);
            MonoBehaviour blockScriptName = currentBlockRun.GetComponent<MonoBehaviour>();
            // Code came from https://answers.unity.com/questions/62658/how-to-get-all-scripts-attached-to-a-gameojbect.html

            Debug.Log(blockScriptName);
            // Expected behavior: supposed to be able to call the public function runCode() within each of these scripts
            // Scripts being called: printFavoriteItem (attached on the sky blue sphere)           
            // printName (reddish sphere)
            // printRandomNumber (blue sphere)


            // var blockScript = currentBlockRun.GetComponent<blockScriptName>();
            // Spaghetti code I tried to use to try to solve it

        }
        // Debug.Log("RunBlockScript".Contains("BlockScript"));
        
    }
}
