/**
 *
 * @file		HandleRegex.cs
 * @brief		This module represents Regular expressions
 * @details
 * @author		Nour Bou Nasr (bounasrnour@gmail.com)
 * @date		Sep 17, 2021
 * @note        it stills uncomplete but it is runnable, also needs refactoring 
 * @see        
 * @version 	1.0.0
 * @warning    
 * @copyright
 * 2021 Team Dark Matter
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class HandleRegex : MonoBehaviour
{
    /*This function returns a random number between 0 and the length of the bag of word*/
    [SerializeField] Text label_text;
    TextTyper textTyper;
    string random_word(string[] bag_of_words)
    {
        System.Random rnd = new System.Random();
        int rdi = rnd.Next() % bag_of_words.Length;
        return bag_of_words[rdi];
    }
    //create patterns
    string create_pattern(string[] bag_of_words, string options = @"\s|")
    {
        return String.Join(@options, bag_of_words);
    }
    //Replace words by random word from the bag of words 
    string replace_word(string text, string word_from_bag, string pattern="")
    {
        Regex rg = new Regex(pattern);
        return rg.Replace(text, word_from_bag + " ", 1);
    }
    void Start()
    {
        textTyper = FindObjectOfType<TextTyper>();
        /*Usind dictionaries that hold a bag of words as key value pair */
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
        //testing in the UI
        string lbl_text = "hello I am AJ your AI Assistant";
        string ans = replace_word(lbl_text, random_word(regex_exp["greetings"]),@"hi\s|hey\s|hello\s|greetings\s");
        //label_text.text = ans;
        Debug.Log(ans);
        textTyper.UpdateText(ans);

        //END TEST
        //testing
        //int g = random_word(regex_exp["greetings"]);
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
        string test = rg.Replace(text, random_word(regex_exp["greetings"]), 1);
        Debug.Log(test);

        //test 2
        string test2 = "Hello I am AJ your AI assistant";
        string p = String.Join(@"\s|", regex_exp["greetings"]);
        Debug.Log(p);
        pattern = @p;
        string res = rg.Replace(test2.ToLower(), random_word(regex_exp["greetings"]), 1);
        p = String.Join(@"\s|", regex_exp["assistant"]);
        pattern = p;
        Debug.Log(res);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
