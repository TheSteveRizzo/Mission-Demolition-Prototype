using UnityEngine;
using System.Collections;
public class Goal : MonoBehaviour
{
    static public bool goalMet = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Goal.goalMet = true;
            Material mat = GetComponent<Renderer>().material;
            mat.SetColor("_Color", Color.green);
        }
    }
}