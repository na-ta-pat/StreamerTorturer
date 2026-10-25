using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChatMsg{
    public float time;
    public string name;
    public string message;
}

public abstract class Chat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

}
