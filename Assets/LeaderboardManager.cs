using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Assets;

public class LeaderboardManager : MonoBehaviour {
    //sileoloz_cannon 5LDZ*u8#0GAV
    private string secretKey = "1j2hh5fg9"; // Edit this value and make sure it's the same as the one stored on the server
    private string addScoreURL = "http://silentdogstudios.com/gamehelpers/CannonGame/addscore.php?"; //be sure to add a ? to your url
    private string highscoreURL = "http://silentdogstudios.com/gamehelpers/CannonGame/display.php";
    private string highscorePositionsURL = "http://silentdogstudios.com/gamehelpers/CannonGame/getpositions.php";
    private ManageGame manageGameScript;
    private TombGenerator tombGenerateScript;

    public int lowestScore = 0;
    public Text HighScoreBoardText;
    public Text PlayerBestScoreText;
    public Text PlayerName;
    public Canvas ValidationStringCanvas;
    public Canvas SuccessStringCanvas;
    public Canvas SubmitHSCanvas;
    public Canvas SubmitButtonCanvas;


    void Start(){
		RefreshLeaderboard();
        manageGameScript = (ManageGame)GameObject.Find("_GM").GetComponent<ManageGame>();
        tombGenerateScript = (TombGenerator)GameObject.Find("_GM").GetComponent<TombGenerator>();
    }

	public void RefreshLeaderboard(){
		StartCoroutine(GetScores());
        StartCoroutine(GetPositions());
	}


    public void PostToLeaderboard()
    {
        if (isNameValid(PlayerName.text.TrimEnd()))
        {
            SubmitButtonCanvas.enabled = false;
            ValidationStringCanvas.enabled = false;
            SuccessStringCanvas.enabled = false;
            string name = PlayerName.text.TrimEnd().ToString();
            int score = Int32.Parse(PlayerBestScoreText.text);
            float xpos = manageGameScript.BestPosition.position.x;
            float ypos = manageGameScript.BestPosition.position.y;
            StartCoroutine(PostScores(name, score, xpos, ypos));
            SuccessStringCanvas.enabled = true;
            SubmitHSCanvas.enabled = false;
            SubmitButtonCanvas.enabled = false;
        }
        else
        {
            ValidationStringCanvas.enabled = true;
        }
    }

	private bool isNameValid(string name){
		if(name.Length <= 0)
			return false;

		Regex r = new Regex("^[a-zA-Z]*$");
		if (!r.IsMatch(name)) {
			return false;
		}

		r = new Regex(@"FUCK|NIGGER|FAG|NIG|ASS|CUNT|BITCH|SHIT|WHORE|NIGER", RegexOptions.IgnoreCase);
		if (r.IsMatch(name.ToUpper())) {
			return false;
		}

		return true;
	}

	// remember to use StartCoroutine when calling this function!
	IEnumerator PostScores(string name, int score, float xpos, float ypos)
	{
		//This connects to a server side php script that will add the name and score to a MySQL DB.
		// Supply it with a string representing the players name and the players score.
		string hash = Md5Sum(name + score + xpos + ypos + secretKey);
		
		string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&xpos=" + xpos + "&ypos=" + ypos + "&hash=" + hash;
		
		// Post the URL to the site and create a download object to get the result.
		WWW hs_post = new WWW(post_url);
		yield return hs_post; // Wait until the download is done
		
		if (hs_post.error != null)
		{
			Debug.Log("There was an error posting the high score: " + hs_post.error);
		} else {
			RefreshLeaderboard();
		}
	}

	// Get the scores from the MySQL DB to display in a GUIText.
	// remember to use StartCoroutine when calling this function!
	IEnumerator GetScores()
	{
		WWW hs_get = new WWW(highscoreURL);
		yield return hs_get;
		
		if (hs_get.error != null)
		{
			Debug.Log("There was an error getting the high score: " + hs_get.error);
		}
		else
		{
            HighScoreBoardText.text = hs_get.text; // this is a GUIText that will display the scores in game.
		}
	}

    /// <summary>
    /// get the positions from the MySQL DB to display as tombstones or something
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetPositions()
    {
        WWW hs_get = new WWW(highscorePositionsURL);
        yield return hs_get;

        List<Tomb> tombs = new List<Tomb>();

        if (hs_get.error != null)
        {
            Debug.Log("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
            string[] tempPosArray = hs_get.text.Split(new string[] { "\n" }, StringSplitOptions.None);
            if (tempPosArray.Length > 0)
            {
                foreach(string s in tempPosArray)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        string[] temp = s.Split(',');

                        if (temp[1] == "0" && temp[2] == "0")
                            continue;

                        Tomb tomb = new Tomb();
                        tomb.Name = temp[0];
                        tomb.XPos = Convert.ToSingle(temp[1]);
                        tomb.YPos = Convert.ToSingle(temp[2]);

                        tombs.Add(tomb);
                    }
                }
            }
            //send tombs to get generated on map
            if (tombs.Count > 0)
            {
                tombGenerateScript.Tombs = tombs;
                tombGenerateScript.Generate();
            }
        }
    }

    string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);
		
		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
		
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
		
		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}
		
		return hashString.PadLeft(32, '0');
	}
}
