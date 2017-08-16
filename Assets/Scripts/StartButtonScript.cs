using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour {

    public Button btn;
    // Use this for initialization
    void Start () {
        btn.onClick.AddListener(TaskOnClick);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void TaskOnClick()
    {
        Debug.Log("here");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Test1");
    }
}
