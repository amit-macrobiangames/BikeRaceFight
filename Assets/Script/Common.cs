using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common : MonoBehaviour
{
	public static int totalPlayer = 2;
	public static int totalLevels = 20;
	public static int totalPlayerCostumes = 10;
	public static int totalPlayerBikeColor = 4;
	public static int totalPlayerBat = 5;
	public static int totalPlayerPistol = 3;

	public static string sceneMenu = "menu";
	public static string sceneLevelSelection = "levelselection";
	public static string sceneGamePlay = "game";

	private static Dictionary<string, string> gameDictionary = new Dictionary<string, string>() {

		
		{"level1locked","no"},
		{"level2locked","yes"},
		{"level3locked","yes"},
		{"level4locked","yes"},
		{"level5locked","yes"},
		{"level6locked","yes"},
		{"level7locked","yes"},
		{"level8locked","yes"},
		{"level9locked","yes"},
		{"level10locked","yes"},
		{"level11locked","yes"},
		{"level12locked","yes"},
		{"level13locked","yes"},
		{"level14locked","yes"},
		{"level15locked","yes"},
		{"level16locked","yes"},
		{"level17locked","yes"},
		{"level18locked","yes"},
		{"level19locked","yes"},
		{"level20locked","yes"},

		{"player1costume0locked","no"},
		{"player1costume1locked","yes"},
		{"player1costume2locked","yes"},
		{"player1costume3locked","yes"},
		{"player1costume4locked","yes"},
		{"player1costume5locked","yes"},
		{"player1costume6locked","yes"},
		{"player1costume7locked","yes"},
		{"player1costume8locked","yes"},
		{"player1costume9locked","yes"},

		{"player1bikecolor0locked","no"},
		{"player1bikecolor1locked","yes"},
		{"player1bikecolor2locked","yes"},
		{"player1bikecolor3locked","yes"},

		
		{"player2costume0locked","no"},
		{"player2costume1locked","yes"},
		{"player2costume2locked","yes"},
		{"player2costume3locked","yes"},
		{"player2costume4locked","yes"},
		{"player2costume5locked","yes"},
		{"player2costume6locked","yes"},
		{"player2costume7locked","yes"},
		{"player2costume8locked","yes"},
		{"player2costume9locked","yes"},

		{"player2bikecolor0locked","no"},
		{"player2bikecolor1locked","yes"},
		{"player2bikecolor2locked","yes"},
		{"player2bikecolor3locked","yes"},

		{"playerbat0locked","no"},
		{"playerbat1locked","yes"},
		{"playerbat2locked","yes"},
		{"playerbat3locked","yes"},
		{"playerbat4locked","yes"},

		{"playerpistol0locked","no"},
		{"playerpistol1locked","yes"},
		{"playerpistol2locked","yes"},


	};

	public static string getGameDictionaryData(string levelKey)
	{
		return gameDictionary[levelKey];
	}

	public static void changeGameDictionary(string key, string val)
	{
		gameDictionary.Remove(key);
		gameDictionary.Add(key, val);

		PlayerPrefs.SetString(key, val);
	}

	public static bool CheckInternetConnection()
	{
		bool isConnectedToInternet = true;

		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			isConnectedToInternet = false;
		}

		return isConnectedToInternet;
	}

	public static void savePlayerData()
	{
		//		for(int i = 0; i < totalPlayerTrucks; i++)
		//		{
		//			if(!PlayerPrefs.HasKey("playertruck"+i.ToString()+"locked"))
		//				PlayerPrefs.SetString("playertruck"+i.ToString()+"locked",gameDictionary["playertruck"+i.ToString()+"locked"]);
		//		}

		for (int i = 0; i < totalLevels; i++)
		{
			if (!PlayerPrefs.HasKey("level" + i.ToString() + "locked"))
				PlayerPrefs.SetString("level" + i.ToString() + "locked", gameDictionary["level" + i.ToString() + "locked"]);

		}
		for (int i = 0; i < totalPlayerCostumes; i++)
		{
			if (!PlayerPrefs.HasKey("player1costume" + i.ToString() + "locked"))
				PlayerPrefs.SetString("player1costume" + i.ToString() + "locked", gameDictionary["player1costume" + i.ToString() + "locked"]);

			if (!PlayerPrefs.HasKey("player2costume" + i.ToString() + "locked"))
				PlayerPrefs.SetString("player2costume" + i.ToString() + "locked", gameDictionary["player2costume" + i.ToString() + "locked"]);
		}

		for (int i = 0; i < totalPlayerBikeColor; i++)
		{
			if (!PlayerPrefs.HasKey("player1bikecolor" + i.ToString() + "locked"))
				PlayerPrefs.SetString("player1bikecolor" + i.ToString() + "locked", gameDictionary["player1bikecolor" + i.ToString() + "locked"]);

			if (!PlayerPrefs.HasKey("player2bikecolor" + i.ToString() + "locked"))
				PlayerPrefs.SetString("player2bikecolor" + i.ToString() + "locked", gameDictionary["player2bikecolor" + i.ToString() + "locked"]);
		}

		for (int i = 0; i < totalPlayerPistol; i++)
		{
			if (!PlayerPrefs.HasKey("playerpistol" + i.ToString() + "locked"))
				PlayerPrefs.SetString("playerpistol" + i.ToString() + "locked", gameDictionary["playerpistol" + i.ToString() + "locked"]);
		}

		for (int i = 0; i < totalPlayerBat; i++)
		{
			if (!PlayerPrefs.HasKey("playerbat" + i.ToString() + "locked"))
				PlayerPrefs.SetString("playerbat" + i.ToString() + "locked", gameDictionary["playerbat" + i.ToString() + "locked"]);
		}
	}

	public static void getPlayerData()
	{
		//Debug.Log("yes");
		for (int i = 0; i < totalLevels; i++)
		{
			gameDictionary.Remove("level" + i.ToString() + "locked");
			gameDictionary.Add("level" + i.ToString() + "locked", PlayerPrefs.GetString("level" + i.ToString() + "locked"));
		}
		for (int i = 0; i < totalPlayerBikeColor; i++)
		{
			gameDictionary.Remove("player1bikecolor" + i.ToString() + "locked");
			gameDictionary.Add("player1bikecolor" + i.ToString() + "locked", PlayerPrefs.GetString("player1bikecolor" + i.ToString() + "locked"));

			gameDictionary.Remove("player2bikecolor" + i.ToString() + "locked");
			gameDictionary.Add("player2bikecolor" + i.ToString() + "locked", PlayerPrefs.GetString("player2bikecolor" + i.ToString() + "locked"));
		}
		for (int i = 0; i < totalPlayerCostumes; i++)
		{
			gameDictionary.Remove("player1costume" + i.ToString() + "locked");
			gameDictionary.Add("player1costume" + i.ToString() + "locked", PlayerPrefs.GetString("player1costume" + i.ToString() + "locked"));

			gameDictionary.Remove("player2costume" + i.ToString() + "locked");
			gameDictionary.Add("player2costume" + i.ToString() + "locked", PlayerPrefs.GetString("player2costume" + i.ToString() + "locked"));
		}
		for (int i = 0; i < totalPlayerBat; i++)
		{
			gameDictionary.Remove("playerbat" + i.ToString() + "locked");
			gameDictionary.Add("playerbat" + i.ToString() + "locked", PlayerPrefs.GetString("playerbat" + i.ToString() + "locked"));
		}
		for (int i = 0; i < totalPlayerPistol; i++)
		{
			gameDictionary.Remove("playerpistol" + i.ToString() + "locked");
			gameDictionary.Add("playerpistol" + i.ToString() + "locked", PlayerPrefs.GetString("playerpistol" + i.ToString() + "locked"));
		}
	}
}
 