using RestClient.Core;
using RestClient.Core.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // RestWebClient.Instance.Get("https://kids-app.10minuteschool.com/api/v1/class", (a) => { OnResponse(a); });

        Dictionary<string,object> postData = new Dictionary<string, object>();
        postData.Add("email", "10ms@gmail.com");
        postData.Add("phone", "+8801.....");
        postData.Add("otp", "1234");
        postData.Add("type", "email");
   
        RestWebClient.Instance.Post("https://kids-app.10minuteschool.com/api/v1/auth/otp/verify", postData,null, (a) => { OnVerifyResponse(a);});
    }


    public void OnResponse(Response e)
    {
        ClassJson a = JsonUtility.FromJson<ClassJson>(e.Data);
        Debug.Log(a.data[0]._id);
    }

    public void OnVerifyResponse(Response e)
    {

    }

    public void LoadScene()
    {
        SceneManager.LoadScene("ok");
    }
}
