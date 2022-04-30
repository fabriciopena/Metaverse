using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockConnection : MonoBehaviour {

    public void connectBlocks(Transform connectorBlock, Transform newBlock) {
        Transform connectorBlockInstance = connectorBlock.GetChild(0);
        float newYPos = (connectorBlockInstance.localPosition.y - connectorBlock.childCount) / 2;

        newBlock.SetParent(connectorBlock);
        newBlock.localPosition = new Vector3(connectorBlockInstance.localPosition.x + 0.3f, newYPos, 0);
        newBlock.localScale = Vector3.one;

        Quaternion newBlockRotation = newBlock.localRotation;
        newBlockRotation.eulerAngles = Vector3.zero;
        newBlock.localRotation = newBlockRotation;
        // TODO: Figure out maximum y-value (maximum amount of blocks placed in one column)
        // When column is full, begin at initial y-position and continue downwards
    }
}
