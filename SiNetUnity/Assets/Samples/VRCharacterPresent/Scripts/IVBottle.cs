using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IVBottle : MonoBehaviour
{
    public int id;
    public bool isOnGrab = false;

    public static IVBottle FindIVBottle(int id)
    {
        foreach (var bottle in FindObjectsOfType<IVBottle>())
        {
            if (bottle.id == id) {
                return bottle;
            }
        }
        return null;
    }
}
