using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChatType{
    summon,
    vote,
    work,
    invalid
}

[System.Serializable]
public class ChatMsg{
    public float time;
    public string name;
    public string message;
}

public abstract class Chat : MonoBehaviour
{
    public SummonDB summondb;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public ChatType Categorize(string message){
        string msg = message.ToLowerInvariant();
        string[] words = msg.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
        int len = words.Length;
        int w0len = words[0].Length;
        switch(words[0][0]){
            case 's': //s(ummon) enemy (north)
            if(len<=3 && w0len<=6 && words[0] == "summon"[..w0len])return ChatType.summon;
            break;
            case 'v': //v(ote) leftdoor
            if(len==2 && w0len<=4 && words[0] == "vote"[..w0len]) return ChatType.vote;
            break;
            case 'w': //w(ork) smith
            if(len==2 && w0len<=4 && words[0] == "work"[..w0len]) return ChatType.work;
            break;
        }
        return ChatType.invalid;
    }

}
