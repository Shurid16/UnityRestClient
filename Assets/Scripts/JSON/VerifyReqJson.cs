// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

[System.Serializable]
public class VerifyReqJson
{
    public string email ;
    public string phone ;
    public string otp ;
    public string type ;
}