using System;
using System.Net;
using System.IO;
using UnityEngine;

public class Player
{
	private String username = null;
	private String rank = null;
	private String password = null;

	public Player (){
	}

	public void set_username(String un){
		this.username = un.Trim('\"');
	}

	public String get_username(){
		return username;
	}

	public String set_password(String pw){
		return this.password = pw;
	}

	public String get_password(){
		return this.password;
	}

	public void reset(){
		username = null;
	}

	// make the HTTP GET REQUEST to get player's rank
	// HTTP 200 will be returned if successed and HTTP 400 error will be return if failed
	private void get_rank_request(String un, String pw){
		String url = "http://115.146.88.121/users/rank/" + un + "/";
		var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
		httpWebRequest.ContentType = "application/json";
		httpWebRequest.Method = "GET";
		String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(un + ":" + pw));
		httpWebRequest.Headers.Add("Authorization", "Basic " + encoded);

		var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
		{
			var result = streamReader.ReadToEnd();
			JSONObject rJson = new JSONObject (result);
			this.rank = rJson.GetField ("rank").ToString ();
		}
	}

	public int get_rank(){
		return int.Parse (rank.Trim('\"'));
	} 

	// Get player's rank otherwise return the error msg
	public String get_rank(String un, String pw){
		try{
			get_rank_request (un, pw);
		}
		catch(Exception e){
			return "Rank sync error.";
		}
		return rank.Trim('\"');
	}

	// make the HTTP GET REQUEST to update player's rank
	// HTTP 200 will be returned if successed and HTTP 400 error will be return if failed
	private void update_rank_request(String un, String pw){
		String url = "http://115.146.88.121/users/rank/" + un + "/";
		var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
		httpWebRequest.ContentType = "application/json";
		httpWebRequest.Method = "PUT";
		String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(un + ":" + pw));
		httpWebRequest.Headers.Add("Authorization", "Basic " + encoded);

		using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
		{

			JSONObject json = new JSONObject ();
			json.AddField ("rank", this.rank);
			streamWriter.Write(json.ToString());
			streamWriter.Flush();
			streamWriter.Close();
		}

		var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
		{
			var result = streamReader.ReadToEnd();
		}
	}

	//update player's rank otherwise return the error msg
	public String update_rank(int new_rank){
		this.rank = new_rank.ToString();


		try{
			update_rank_request (this.username, this.password);
			SavePlayer();
		}
		catch(Exception e){
			return "Rank sync error.";
		}
		return this.rank.Trim('\"');
	}

	//Save data to global control   
	public void SavePlayer()
	{
		GlobalControl.Instance.username = this.username;
		GlobalControl.Instance.password = this.password;
		GlobalControl.Instance.rank = this.rank;
	}

	public void LoadPlayer() 
	{   
		this.username = GlobalControl.Instance.username;
		this.password = GlobalControl.Instance.password;
		this.rank = GlobalControl.Instance.rank;
	}

}


