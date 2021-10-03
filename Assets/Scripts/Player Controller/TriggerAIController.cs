using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerAIController : MonoBehaviour
{
    public string action;

    // Start is called before the first frame update
    //get the table of regex
    //IDictionary<string, string[]> regex_array = new Dictionary<string, string[]>();
    HandleRegex regex_script;
    IDictionary<string, string[]> regex_arr;
    string last_planet_name = "";
    int last_random = 0;
    [SerializeField] GameObject ai_panel;
    [SerializeField] Text ai_text;
    int rdx = 0;

    void Start()
    {
        regex_script = FindObjectOfType<HandleRegex>();
        regex_arr = regex_script.get_regex_array();
    }

    public void UpdateAIText()
    {
        ai_panel.SetActive(true);

        foreach (KeyValuePair<string, string[]> kvp in regex_arr)
        {

            //Debug.Log(kvp.Key + str);
            if (action == kvp.Key)
            {
                ai_panel.SetActive(true);
                Debug.Log(kvp.Value);
                ai_text.text = kvp.Value[0];
            }

        }
    }

    public void UpdateAIText(int index)
    {
        ai_panel.SetActive(true);

        foreach (KeyValuePair<string, string[]> kvp in regex_arr)
        {

            //Debug.Log(kvp.Key + str);
            if (action == kvp.Key)
            {
                ai_panel.SetActive(true);
                Debug.Log(kvp.Value[index]);
                ai_text.text = kvp.Value[index];
            }

        }
    }
}
