using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTraitorDueCount : MonoBehaviour
{
    public int due;
    int _dueCount;

    public bool isDue
    {
        get
        {
            var thatGetDued = ++_dueCount == due;
            if (thatGetDued)
            {
                _dueCount = 0;
            }
            return thatGetDued;
        }
    }
}
