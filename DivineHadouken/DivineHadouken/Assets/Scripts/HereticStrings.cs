using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HereticStrings
{
	private static string[] s_hereticStrings = 
	{
		"Video games are for children. Grow up.",
		"Omg GUILE rulz!.",
		"I always use light themes in WebStorm.",
		"Charge characters are so cool!.",
		"Flowers are beautiful.",
		"I do not find this ethical.",
		"Metsu Hadouken!",
		"Shinkuu Hadouken!",
		"Shin-Shouryuuken!"
	};

	private static string[] s_believerStrings = 
	{
		"Messatsu",
		"LP, LP, ->, LK, HK",
		"Isshun Sengeki!",
		"May the Satsui be with you.",
		"Shakunetsu!",
		"Messatsu-Go-Shouryuu"
	};

	public static string GetRandomHereticString()
	{
		int randomID = Random.Range(0, s_hereticStrings.Length);
		return s_hereticStrings[randomID];
	}

	public static string GetRandomBelieverString()
	{
		int randomID = Random.Range(0, s_believerStrings.Length);
		return s_believerStrings[randomID];
	}
}
