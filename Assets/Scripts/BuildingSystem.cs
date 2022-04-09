using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject blockObject;
    public Transform parent;

    public void Update()
    {
        if (Input.GetButtonDown("LockMouse")){Cursor.lockState = CursorLockMode.Locked;}
        if (Input.GetButtonDown("UnlockMouse")){Cursor.lockState = CursorLockMode.None;}
        if (Input.GetMouseButtonDown(0)){BuildBlock(blockObject);}
        if (Input.GetMouseButtonDown(1)){DestroyBlock();}
        HighlightBlock();
    }

    void BuildBlock(GameObject block)
    {
        if(Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
        {
          Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(hitInfo.point.x + hitInfo.normal.x/2), Mathf.RoundToInt(hitInfo.point.y + hitInfo.normal.y / 2), Mathf.RoundToInt(hitInfo.point.z + hitInfo.normal.z /2));
          Instantiate(block, spawnPosition, Quaternion.identity, parent);
        }
    }

    void DestroyBlock()
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
        {
          if(hitInfo.transform.gameObject.tag=="Blocks"){
            Destroy(hitInfo.transform.gameObject);
          }
        }
    }
    void HighlightBlock()
    {
      if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
      {
        if(hitInfo.transform.gameObject.tag=="Blocks"){

        }
      }
    }
}
