using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System;


public class Levelmanager : MonoBehaviour {
	public Transform mainMenu, optionMenu, loginMenu, registerMenu;
	public InputField usernameRegister, passwordRegister, usernameLogin, passwordLogin;
	public Text loginText, registerText, usernameDisplay, rankDisplay;
	private Player p = new Player ();
	private string serverAddress = "115.146.88.121";

	void Start(){
		Debug.LogWarning ("user = " + GlobalControl.Instance.username);
		if (GlobalControl.Instance.username != "") {
			p.LoadPlayer ();
			usernameDisplay.text = p.get_username();
			rankDisplay.text = p.get_rank ().ToString ();
			mainMenu.gameObject.SetActive (true);
			loginMenu.gameObject.SetActive (false);
		}
	}

	// change the scene
	public void LoadScene(string name){
		p.SavePlayer ();
		Application.LoadLevel(name);

	}

	public void QuitGame(){
		Application.Quit();
	}

	// open and close the function menu
	public void OptionMenu(bool clicked){
		if (clicked == true) {
			optionMenu.gameObject.SetActive(clicked);
			mainMenu.gameObject.SetActive(false);
		} else {
			optionMenu.gameObject.SetActive(clicked);
			mainMenu.gameObject.SetActive(true);		
		}
	}

	// switch between login menu and main menu
	// handle user login and display the user info
	public void LoginMenu(bool clicked){
		if (clicked == true) {
			if (isVaildInput ()) {
				try{
					userLogin();
					usernameDisplay.text = p.get_username();
					rankDisplay.text = p.get_rank(usernameLogin.textComponent.text, passwordLogin.text);
					mainMenu.gameObject.SetActive(clicked);
					loginMenu.gameObject.SetActive(false);
				}
				catch(Exception e){
					loginText.text = "incorrect username or password.";
				}
			}
		} else {
			// log out and reset the previous input data
			loginText.text = "";
			usernameDisplay.text = "";
			p.reset ();
			mainMenu.gameObject.SetActive(clicked);
			registerMenu.gameObject.SetActive(clicked);
			loginMenu.gameObject.SetActive(true);	
		}
	}

	// switch between register menu and main menu
	// handle user register and display the user info
	public void RegisterMenu(bool clicked){
		if (clicked == true) {
			if (isVaildInput()) {
				try{
					registeRequest();
					usernameDisplay.text = p.get_username();
					rankDisplay.text = p.get_rank(usernameRegister.textComponent.text, passwordRegister.text);
					mainMenu.gameObject.SetActive(clicked);
					registerMenu.gameObject.SetActive(false);
				}
				catch(Exception e){
					registerText.text = "Username used.";
				}
			}
			else{
				registerText.text = "Invalid username or password,\n" +
					"only [0-9, A-Z, a-z] allowed.";
			}
		} else {
			registerText.text = "";
			loginMenu.gameObject.SetActive(clicked);
			registerMenu.gameObject.SetActive(true);
		}
	}

	// check the validity of the value avoid any malfunction inupt
	public bool isVaildInput(){
		Regex regex = new Regex(@"\W");
		return !regex.IsMatch(usernameRegister.textComponent.text) && !regex.IsMatch(passwordRegister.text);
	}

	// make the HTTP POST REQUEST to register a new user
	// HTTP 200 will be returned if successed and HTTP 400 error will be return if failed
	public bool registeRequest(){
		String un = usernameRegister.textComponent.text;
		String pw = passwordRegister.text;

		var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://"+serverAddress+"/users/register/");
		httpWebRequest.ContentType = "application/json";
		httpWebRequest.Method = "POST";

		using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
		{
			
			JSONObject json = new JSONObject ();
			json.AddField ("username", un);
			json.AddField ("password", pw);
			streamWriter.Write(json.ToString());
			streamWriter.Flush();
			streamWriter.Close();
		}

		var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
		{
			var result = streamReader.ReadToEnd();
			JSONObject pJson = new JSONObject (result);
			this.p.set_username (pJson.GetField("username").ToString());
			this.p.set_password (pw);
		}
		return true;
	}

	// make the HTTP GRT REQUEST to login a new user
	// HTTP 200 will be returned if successed and HTTP 400 error will be return if failed
	public void userLogin(){
		String un = usernameLogin.textComponent.text;
		String pw = passwordLogin.text;
		String url = "http://"+serverAddress+"/users/" + un + "/";
		var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
		httpWebRequest.ContentType = "application/json";
		httpWebRequest.Method = "GET";
		String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(un + ":" + pw));
		httpWebRequest.Headers.Add("Authorization", "Basic " + encoded);

		var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
		{
			var result = streamReader.ReadToEnd();
			JSONObject pJson = new JSONObject (result);
			this.p.set_username (pJson.GetField("username").ToString());
			this.p.set_password (pw);
		}
	}
}
