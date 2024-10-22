
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

public class GoogleFormSubmit : MonoBehaviour
{
    private string formURL = "https://docs.google.com/forms/d/e/1FAIpQLSdQe96I6vwuUjWrW77nIZjvqpjlDf3ZkI5GtlnVV9qxmVMqfw/formResponse";

    // Make the method public so it can be accessed from another script
    public void SubmitData(string sessionId, string winner, int numAttackers, int numTurrets)
    {
        StartCoroutine(PostToGoogleForm(sessionId, winner, numAttackers, numTurrets));
    }

    private IEnumerator PostToGoogleForm(string sessionId, string winner, int numAttackers, int numTurrets)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.2040210924", sessionId);
        form.AddField("entry.1013643412", winner);
        form.AddField("entry.1003807920", numAttackers.ToString());
        form.AddField("entry.209927190", numTurrets.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(formURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Form submission failed: " + www.error);
            }
            else
            {
                Debug.Log("Form submission complete!");
            }
        }
    }
}

//public class GoogleFormSubmit : MonoBehaviour
//{
//    // Replace <form_id> with your Google Form's ID
//    private string formURL = "https://docs.google.com/forms/d/e/1FAIpQLSdQe96I6vwuUjWrW77nIZjvqpjlDf3ZkI5GtlnVV9qxmVMqfw/formResponse";

//    // This method is called when the game ends
//    public void OnGameEnd(string sessionId, string winner, int numAttackers, int numTurrets)
//    {
//        StartCoroutine(PostToGoogleForm(sessionId, winner, numAttackers, numTurrets));
//    }

//    private IEnumerator PostToGoogleForm(string sessionId, string winner, int numAttackers, int numTurrets)
//    {

//    }
//}
