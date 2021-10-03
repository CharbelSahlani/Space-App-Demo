using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerAIController : MonoBehaviour
{
    public string action;
    public bool autoClear;

    // Start is called before the first frame update
    //get the table of regex
    //IDictionary<string, string[]> regex_array = new Dictionary<string, string[]>();
    HandleRegex regex_script;
    IDictionary<string, string[]> regex_arr;

    [SerializeField] GameObject ai_panel;
    [SerializeField] Text ai_text;

    private float textWaitTime;

    void Start()
    {
        regex_script = FindObjectOfType<HandleRegex>();
        regex_arr = regex_script.get_regex_array();
    }

    public void UpdateAIText(int index)
    {
        ai_panel.SetActive(true);
        /*
        */

        foreach (KeyValuePair<string, string[]> kvp in regex_arr)
        {

            //Debug.Log(kvp.Key + str);
            if (action == kvp.Key)
            {
                //ai_panel.SetActive(true);
                Debug.Log(kvp.Value[index]);
                ai_text.text = kvp.Value[index];
            }
        }

        Destroy(gameObject);
    }

    public void UpdateAIText(int index, bool randomize)
    {
        //ai_panel.SetActive(true);

        foreach (KeyValuePair<string, string[]> kvp in regex_arr)
        {
            //Debug.Log(kvp.Key + str);
            if (action == kvp.Key)
            {
                if (randomize)
                {
                    System.Random rnd = new System.Random();
                    Debug.Log(kvp.Value.Length);
                    index = ((int)rnd.Next()) % kvp.Value.Length;
                }

                Debug.Log(kvp.Value[index]);
                ai_panel.SetActive(true);
                ai_text.text = kvp.Value[index];
                textWaitTime = 2.0f + Mathf.Sqrt(ai_text.text.Length) / 4.0f;
                /*if (index == 9 || index == 10)
                {
                    if (autoClear)
                    {
                        StartCoroutine(AIDisable(textWaitTime,index));
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    else
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        Destroy(gameObject);
                    }
                }*/
            }
        }
        if (autoClear)
            StartCoroutine(AIDisable(textWaitTime, index));
        else
            Destroy(gameObject);
    }

    IEnumerator AIDisable(float secondsToWait, float i)
    {
        Debug.LogWarning("Start");
        Debug.LogWarning(secondsToWait);
        yield return new WaitForSecondsRealtime(secondsToWait);
        if (i == 9 || i == 10)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.LogWarning("End");
        ai_panel.SetActive(false);
        Destroy(gameObject);
    }
}
