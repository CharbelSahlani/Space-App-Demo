using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class HandleRegex : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {

        IDictionary<string, string[]> regex_exp = new Dictionary<string, string[]>();
        regex_exp.Add("greetings", new string[] { "hello", "hi", "greetings", "hey" });
        regex_exp.Add("assistant", new string[] { "assistant", "guide" });
        regex_exp.Add("essVerbs", new string[] { "guiding you", "assisting you" });
        foreach (KeyValuePair<string, string[]> kvp in regex_exp)
        {
            foreach (string str in kvp.Value)
            {
                Console.WriteLine("{0} : {1}", kvp.Key, str);
            }
        }
        //testing
        System.Random rdi = new System.Random();
        int g = rdi.Next() % regex_exp["greetings"].Length;
        Console.WriteLine(g);
        string pattern = @"^hi\s|hello\s|greetings\s";
        string text = "hi hi hello greetings ";
        string pattern1 = "hi";
        Regex rg1 = new Regex(pattern1);
        Regex rg = new Regex(pattern);
        Debug.Log("HELLO");
        var M = Regex.Matches(text, pattern);
        foreach (var match in M)
        {
            Debug.Log(match);
        }
        string test = rg.Replace(text, regex_exp["greetings"][g] + " ", 1);
        Debug.Log(test);

        //test 2
        string test2 = "Hello I am AJ your AI assistant";
        string p = String.Join(@"\s|", regex_exp["greetings"]);
        Debug.Log(p);
        pattern = @p;
        string res = rg.Replace(test2.ToLower(), regex_exp["greetings"][g] + " ", 1);
        p = String.Join(@"\s|", regex_exp["assistant"]);
        pattern = p;
        Debug.Log(res);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
