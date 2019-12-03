using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    GameObject ally = null;
    GameObject player = null;

    Vector3 velocity = Vector3.zero;
    public string targetTag = "Ally";
    public string orientationTag = "Player";
    public int maxDistance = 5;

    // Update is called once per frame
    void LateUpdate()
    {
        if (ally == null)
        {
            ally = GameObject.FindGameObjectWithTag(targetTag);
        }

        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag(orientationTag);
        }

        Vector3 direction = player.transform.position - ally.transform.position;

        direction.x = Mathf.Clamp(direction.x, -maxDistance, maxDistance);
        direction.y = Mathf.Clamp(direction.y, -maxDistance, maxDistance);

        direction.z = -10;

        Vector3 target = ally.transform.TransformPoint(direction);

        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, 0.3f);
    }
}
