using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    GameObject player = null;
    Vector3 velocity = Vector3.zero;
    public string targetTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(targetTag);
        }

        Vector3 target = player.transform.TransformPoint(new Vector3(0, 5, -10));

        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, 0.3f);
    }
}
