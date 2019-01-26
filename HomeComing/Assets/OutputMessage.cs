using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputMessage : MonoBehaviour
{
    public void SendInputMessage(string msg)
    {
        Debug.LogFormat("{0}", msg);
    }
}
