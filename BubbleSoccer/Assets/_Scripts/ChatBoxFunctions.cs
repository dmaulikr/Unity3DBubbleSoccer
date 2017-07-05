using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChatBoxFunctions : MonoBehaviour {
	[SerializeField] ContentSizeFitter contentSizeFitter;
	[SerializeField] Text buttonText;
	[SerializeField] Transform messageParentPanel;
	[SerializeField] GameObject newMessagePrefab;

	private static int MAX = 10;
	bool isChatShowing = false;
	string message = "";
	private int messageCount;
	private GameObject[] messages;
	private Player p;

	void Start(){
		ToggleChat ();
		messageCount = 0;
		messages = new GameObject[MAX];
		p = new Player ();
	}

	public void ToggleChat(){
		isChatShowing = !isChatShowing;
		if (isChatShowing) {
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			buttonText.text = "Chat History";
		} else {
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.MinSize;
			buttonText.text = "Open Chat";
		}
	}

	public void SetMessage(string message){
		p.LoadPlayer ();
		string name = p.get_username ();
		this.message = "["+ name +"]: "+message;
	}

	public void ShowMessage(){
		if(message != ""){
			messageCount += 1;

			if (messageCount >= MAX) {
				DiscardMessage (messages);
				messageCount -= 1;
			}

			for (int i = 0; i < MAX; i++) {
				if (messages [i] == null) {
					messages [i] = (GameObject)Instantiate (newMessagePrefab);
					messages[i].transform.SetParent (messageParentPanel);
					messages[i].transform.SetSiblingIndex (messageParentPanel.childCount - 2);
					messages[i].GetComponent<MessageFunctions> ().ShowMessage (message);
					break;
				}
			}
		}
	}

	public void DiscardMessage(GameObject[] messages){
		Destroy(messages[0]);
		for(int i = 0; i< messages.Length; i++){
			if (i != messages.Length - 1) {
				messages [i] = messages [i + 1];
			} else {
				messages [i] = null;
			}
		}
	}
}