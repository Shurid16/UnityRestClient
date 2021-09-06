// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 


[System.Serializable]
public class Data
{
    public string api_token ;
    public string type ;
}

[System.Serializable]
public class VerifyJson
{
    public int code ;
    public string message ;
    public Data data ;
}

