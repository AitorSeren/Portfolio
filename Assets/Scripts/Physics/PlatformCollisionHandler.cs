using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollisionHandler : MonoBehaviour
{

    private Transform platform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            ContactPoint contact = collision.GetContact(0);
            if (contact.normal.y < 0.5f)
            {
                platform = collision.gameObject.transform;
                transform.SetParent(platform);                                                  //  If the player detects the platform touching it with a normal pointing up, this means is standing on the platform
            }                                                                                   //  We use the parenting system to replicate its movements on the player
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")                           
        {
            transform.SetParent(null);                                                          //  If the player exits the platform, the parenting is undone.
            platform = null;
        }
    }
}
