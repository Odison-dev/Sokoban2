using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;

public class RunLevel : MonoBehaviour
{
    private List<Transform> Boxes = new List<Transform>();
    private List<Transform> CheckPoints = new List<Transform>();

    public bool isMoving = false;
    public Vector3 checkPointPosOffset;
    public LayerMask Colliderable;


    private void Start()
    {
        Listing("Pushable");
        Listing("CheckPoint");
    }
    private void Update()
    {
        
        if (isMoving)
        {

        }
        else
        {

            ReleaseBoxes();
            if (isAllBoxesOnCheckPoints())
            {
                //TODO: ¹Ø¿¨Íê³É
                //LevelComplete()
                print("Level Complete!");
            }
        }
    }

    private void Listing(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        if (tag == "Pushable")
        {
            foreach (GameObject box in gameObjects)
            {
                Boxes.Add(box.transform);
            }
        }
        else if (tag == "CheckPoint")
        {
            foreach (GameObject checkpoint in gameObjects)
            {
                CheckPoints.Add(checkpoint.transform);
            }
        }
    }


    private void ReleaseBoxes()
    {
        foreach (Transform box in Boxes)
        {
            box.SetParent(null, true);
        }
    }

    private bool isAllBoxesOnCheckPoints()
    {
        bool yes = true;
        foreach(Transform checkpoint in CheckPoints)
        {
            Collider2D hit = Physics2D.OverlapBox(checkpoint.transform.position + checkPointPosOffset, .5f * Vector3.one, 0f, Colliderable);
            if (hit != null)
            {
                yes &= hit.transform.CompareTag("Pushable");
            }
            else
            {
                yes = false;
            }
            
        }
        return yes;
    }

}
