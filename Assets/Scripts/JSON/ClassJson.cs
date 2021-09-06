

using System.Collections.Generic;

[System.Serializable]
public class Class
{
    public string _id;
    public int max_age;
    public int min_age;
    public string name;
}

[System.Serializable]
public class ClassJson
{
    public int code;
    public string message;
    public List<Class> data;
}

