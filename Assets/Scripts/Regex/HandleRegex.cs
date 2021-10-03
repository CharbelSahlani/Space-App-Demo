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
    string replace_word(string text, string word_from_bag, string pattern)
    {
        Regex rg = new Regex(pattern);
        return rg.Replace(text, word_from_bag + " ", 1);
    }

    //Dictionary 
    IDictionary<string, string[]> regex_exp = new Dictionary<string, string[]>();
    void Start()
    {
        textTyper = FindObjectOfType<TextTyper>();
        /*Usind dictionaries that hold a bag of words as key value pair */
        regex_exp.Add("greetings", new string[] { "hello", "hi", "greetings", "hey" });
        regex_exp.Add("assistant", new string[] { "assistant", "guide", "companion" });
        regex_exp.Add("essVerbs", new string[] { "guiding you", "assisting you" });
        regex_exp.Add("adventure", new string[] { "adventure", "journey" });

        //planets
        //sun
        regex_exp.Add("sun", new string[] { "The Sun is a yellow dwarf star, a hot ball of glowing gases at the heart of our solar system. Its gravity holds everything from the biggest planets to tiny debris in its orbit.",
                                            "Electric currents in the Sun generate a magnetic field that is carried out through the solar system by the solar wind. A stream of electrically charged gas blowing outward from the Sun in all directions.",
                                            "The Sun is the largest object in our solar system, comprising 99.8% of the system’s mass. Though it seems huge to us, the Sun isn't as large as other types of stars.",
                                            "Earth orbits the Sun from a distance of about 93 million miles. The connection and interactions between the Sun and Earth drive our planet's seasons, ocean currents, weather, climate, radiation belts, and aurorae. Though it is special to us, there are billions of stars like our Sun scattered across the Milky Way galaxy."

        });
        //mercury
        regex_exp.Add("mercury", new string[] {"The smallest planet in our solar system and closest to the Sun. Itis only slightly larger than Earth's Moon. Mercury is the fastest planet, zipping around the Sun every 88 Earth days.",
                                                "From the surface of Mercury, the Sun would appear more than three times as large as it does when viewed from Earth, and the sunlight would be as much as seven times brighter.",
                                                "Despite its proximity to the Sun, Mercury is not the hottest planet in our solar system – that title belongs to nearby Venus, thanks to its dense atmosphere.",
                                                "Because of Mercury's elliptical egg-shaped orbit, and sluggish rotation, the Sun appears to rise briefly, set, and rise again from some parts of the planet's surface. The same thing happens in reverse at sunset."});

        //venus
        regex_exp.Add("venus", new string[] { "Venus spins slowly in the opposite direction from most planets. A thick atmosphere traps heat in a runaway greenhouse effect, making it the hottest planet in our solar system.",
                                               "Venus is the second planet from the Sun and is Earth’s closest planetary neighbor. It’s one of the four inner, terrestrial (or rocky) planets, and it’s often called Earth’s twin because it’s similar in size and density. These are not identical twins, however – there are radical differences between the two worlds.",
                                                "Venus has a thick, toxic atmosphere filled with carbon dioxide and it’s perpetually shrouded in thick, yellowish clouds of sulfuric acid that trap heat, causing a runaway greenhouse effect.",
                                                "Venus has crushing air pressure at its surface – more than 90 times that of Earth – similar to the pressure you'd encounter a mile below the ocean on Earth."});

        //earth
        regex_exp.Add("earth", new string[] { "Our home planet is the only place we know of so far that’s inhabited by living things. It's also the only planet in our solar system with liquid water on the surface.",
                                                "While Earth is only the fifth largest planet in the solar system, it is the only world in our solar system with liquid water on the surface. Just slightly larger than nearby Venus, Earth is the biggest of the four planets closest to the Sun, all of which are made of rock and metal.",
                                                "The name Earth is at least 1,000 years old. All of the planets, except for Earth, were named after Greek and Roman gods and goddesses. However, the name Earth is a Germanic word, which simply means \"the ground.\""});

        //mars
        regex_exp.Add("mars", new string[] { "Mars is a dusty, cold, desert world with a very thin atmosphere. There is strong evidence Mars was billions of years ago wetter and warmer, with a thicker atmosphere.",
                                                "Mars is also a dynamic planet with seasons, polar ice caps, canyons, extinct volcanoes, and evidence that it was even more active in the past.",
                                                "Mars is one of the most explored bodies in our solar system, and it's the only planet where we've sent rovers to roam the alien landscape.",
                                                "NASA currently has two rovers (Curiosity and Perseverance), one lander (InSight), and one helicopter (Ingenuity) exploring the surface of Mars."});

        //Jupiter
        regex_exp.Add("jupiter", new string[] { "Jupiter is more than twice as massive than the other planets of our solar system combined. The giant planet's Great Red spot is a centuries-old storm bigger than Earth.",
                                                "Fifth in line from the Sun, Jupiter is, by far, the largest planet in the solar system – more than twice as massive as all the other planets combined.",
                                                "Jupiter's familiar stripes and swirls are actually cold, windy clouds of ammonia and water, floating in an atmosphere of hydrogen and helium.",
                                                "Jupiter’s iconic Great Red Spot is a giant storm bigger than Earth that has raged for hundreds of years."});


        //saturn
        regex_exp.Add("saturn", new string[] { "Adorned with a dazzling, complex system of icy rings, Saturn is unique in our solar system. The other giant planets have rings, but none are as spectacular as Saturn's.",
                                         "Saturn is the sixth planet from the Sun and the second-largest planet in our solar system.",
                                          "Like fellow gas giant Jupiter, Saturn is a massive ball made mostly of hydrogen and helium."});

        //uranus
        regex_exp.Add("uranus", new string[] { "The seventh planet from the Sun rotates at a nearly 90-degree angle from the plane of its orbit. This unique tilt makes Uranus appear to spin on its side.",
                                                "It was the first planet found with the aid of a telescope, Uranus was discovered in 1781 by astronomer William Herschel, although he originally thought it was either a comet or a star.",
                                                "It was two years later that the object was universally accepted as a new planet, in part because of observations by astronomer Johann Elert Bode."});

        //nepturne
        regex_exp.Add("nepturne", new string[] { "The eighth and most distant major planet orbiting our Sun, it is dark, cold and whipped by supersonic winds. It was the first planet located through mathematical calculations, rather than by telescope.",
                                                 "More than 30 times as far from the Sun as Earth, Neptune is the only planet in our solar system not visible to the naked eye and the first predicted by mathematics before its discovery."});

        //pluto
        regex_exp.Add("pluto", new string[] { "Pluto is a complex world of ice mountains and frozen plains. Once considered the ninth planet, Pluto is the largest member of the Kuiper Belt and the best known of a new class of worlds called dwarf planets.",
                                               "Pluto, which is smaller than Earth’s Moon, has a heart-shaped glacier that’s the size of Texas and Oklahoma. This fascinating world has blue skies, spinning moons, mountains as high as the Rockies, and it snows, but the snow is red.",
                                                "On July 14, 2015, NASA’s New Horizons spacecraft made its historic flight through the Pluto system – providing the first close-up images of Pluto and its moons and collecting other data that has transformed our understanding of these mysterious worlds on the solar system’s outer frontier."});

        //general info
        regex_exp.Add("general_info", new string[] { "There are many planetary systems like ours in the universe, with planets orbiting a host star. Our planetary system is named the \"solar system\" because our Sun is named Sol, after the Latin word for Sun, \"solis,\" and anything related to the Sun we call \"solar.\"",
                                                    "Our solar system consists of our star, the Sun, and everything bound to it by gravity – the planets Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, and Neptune; dwarf planets such as Pluto; dozens of moons; and millions of asteroids, comets, and meteoroids. Beyond our own solar system, we have discovered thousands of planetary systems orbiting other stars in the Milky Way.",
                                                    "Our solar system extends much farther than the eight planets that orbit the Sun. The solar system also includes the Kuiper Belt that lies past Neptune's orbit. This is a sparsely occupied ring of icy bodies, almost all smaller than the most popular Kuiper Belt Object – dwarf planet Pluto.",
                                                    "The order and arrangement of the planets and other bodies in our solar system is due to the way the solar system formed. Nearest to the Sun, only rocky material could withstand the heat when the solar system was young. For this reason, the first four planets – Mercury, Venus, Earth, and Mars – are terrestrial planets. They are all small with solid, rocky surfaces."});

        //welcome
        regex_exp.Add("welcome", new string[] {
            "Welcome to Mars Adventure.",
            "Safe journey.\n(Time control is on)",
            "Keep your eye on the velocity.\nYou will later need the value at which it turns green."});

        //introduce the Ai
        regex_exp.Add("intro", new string[] {
            "My Name is AJ.",
            "I am AJ."});

        //introduce the job
        regex_exp.Add("job", new string[] {
            "I will be your assistant along the journey.",
            "I am going to guide you to reach Mars."});



        //time control
        regex_exp.Add("time_lock", new string[] {
            "Time has slowed down for you to focus.\nWhen the altitude reaches 210 km, slow down until the velocity is in the green.",
            "Time control is back on.\nSpeed up to see the orbit."});

        //orbit verifier
        regex_exp.Add("orbit_verifier", new string[] { "Well done! You achieved the correct trajectory.\nIt will take several orbits before landing.",
            "You slowed down more than you need.\nYou will crash.",
            "You did not slow down enough.\nYou will miss the planet." });

        //orbit verifier
        regex_exp.Add("landing", new string[] { "Congratulations!\nYou are now ready for the landing phase on the martian surface." });

        //button mapping
        regex_exp.Add("button_mapping", new string[] {
            "I will teach you about the buttons you will use.",
            "Press and hold \"Space\" for fuel burn to slow down." ,
            "Press \"V\" to switch between top view and third person cameras.",
            "Press \"-\" to slow down time.\nPress \"=\" to speed up time.",
            "Press \"S\" to stop spinning.\nPress \"H\" to expand the heat shield.",
            "Press \"P\" to deploy the parachute.\nPress \"C\" to deploy the cushion.",
            "Press the corresponding keys as soon as the actions on the bottom of the screen turns to READY.",
            "Safe landing.\n(Press \"S\" as soon this message goes away)"});

        //orbit
        regex_exp.Add("orbiting", new string[] {
            "You are now in orbit around Mars.\nThe capture (first) orbit will take almost 7 hours in realtime",
            "You are now orbit Mars.\nThe capture (first) orbit will take almost 7 hours in realtime"});

        //aerobraking
        regex_exp.Add("aero_braking", new string[] {
            "Notice that every time you complete one orbit, the apoapsis (farthest point from Mars) is getting lower.",
            "Have a look at your altitude.\nEvery time you orbit Mars, you get closer"});

        //aerobraking cause
        regex_exp.Add("aero_braking_cause", new string[] {
            "You are slowing down beacuse of aerobraking.\n(Passing by Mars's atmosphere to use air friction)",
            "Aerobraking is using the atmosphere to slow down.\nThat is what you are doing at the periapsis (lowest point) in orbit."});

        //aerobraking cause
        regex_exp.Add("start_landing", new string[] {
            "Now you are are going to land on the surface of Mars.",
            "Now the hardest phase starts."});

        //landing_end
        regex_exp.Add("end_landing", new string[] {
            "Congratulations! You are the first human on Mars.\nNow make your small step so we can have our giant leap."});




        foreach (KeyValuePair<string, string[]> kvp in regex_exp)
        {
            foreach (string str in kvp.Value)
            {
                Console.WriteLine("{0} : {1}", kvp.Key, str);
            }
        }
        //testing in the UI
        string lbl_text = "hello I am AJ your AI assistant ";
        string ans = replace_word(lbl_text, random_word(regex_exp["greetings"]), @"hi\s|hey\s|hello\s|greetings\s");
        //label_text.text = ans;
        ans = replace_word(ans, random_word(regex_exp["assistant"]), create_pattern(regex_exp["assistant"]));
        Debug.Log(create_pattern(regex_exp["assistant"]));
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

    public IDictionary<string, string[]> get_regex_array()
    {
        return regex_exp;
    }
}
