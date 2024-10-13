
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GoogleFormSubmit : MonoBehaviour
{
    // Replace <form_id> with your Google Form's ID
    private string formURL = "https://docs.google.com/forms/d/e/1FAIpQLSdQe96I6vwuUjWrW77nIZjvqpjlDf3ZkI5GtlnVV9qxmVMqfw/formResponse";

    // Use Start() to send data when the game starts
    private void Start()
    {
        // Example data to send
        string sessionId = "Session123";
        string winner = "Attacker";
        int numAttackers = 10;
        int numTurrets = 5;

        // Trigger the form submission
        StartCoroutine(PostToGoogleForm(sessionId, winner, numAttackers, numTurrets));
    }

    // Coroutine to send data to the Google Form
    private IEnumerator PostToGoogleForm(string sessionId, string winner, int numAttackers, int numTurrets)
    {
        WWWForm form = new WWWForm();

        // Add fields to the form. Ensure IDs match your form's entry IDs.
        form.AddField("entry.2040210924", sessionId);             // Session ID
        form.AddField("entry.1013643412", winner);                // Winner (Attacker/Defender)
        form.AddField("entry.1003807920", numAttackers.ToString()); // Number of attackers spawned
        form.AddField("entry.209927190", numTurrets.ToString());   // Number of turrets placed

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
