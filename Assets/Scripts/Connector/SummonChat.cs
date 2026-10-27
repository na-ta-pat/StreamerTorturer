using System.Collections.Generic;
class SummonCommand{
    public string spawnableName;
    public string spawnableLocation;
    public SummonCommand(string spawnableName, string spawnableLocation)
    {
        this.spawnableName = spawnableName;
        this.spawnableLocation = spawnableLocation;
    }
}
class SummonDB{
    public List<string> userQ = new();
    public Dictionary<string,SummonCommand> userDict = new();
    int indexQ;
    public bool isCommandEmpty=>indexQ>=userQ.Count;
    public void Reset(){
        userQ.Clear();
        userDict.Clear();
        indexQ=0;
    }
    public void Add(string name,SummonCommand command){
        if(!userDict.ContainsKey(name)) userQ.Add(name);
        userDict[name] = command;
    }
    public SummonCommand Pop(){
        if(isCommandEmpty)return null;
        return userDict[userQ[indexQ++]];
    }
}