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

public class HandleRegex : MonoBehaviour
{
    /*This function returns a random number between 0 and the length of the bag of word*/
    int random_word(string[] bag_of_words)
    {
        System.Random rnd = new System.Random();
        return rnd.Next() % bag_of_words.Length;
    }
    //create patterns
    string create_pattern(string options=@"\s|", string [] bag_of_words)
    {
        return String.Join(@options, bag_of_words);
    }
    //Replace words by random word from the bag of words 
    string replace_word(string word, string word_from_bag)
    {
        return "";
    }
    void Start()
    {
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
