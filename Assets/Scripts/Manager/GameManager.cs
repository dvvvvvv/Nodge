using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    bool onPlay = false;

    public CometManager cometManager;
    public TimeManager timeManager;
    public Text prepareText;
    public PlayerCometMove player;
    public CameraRig camera;
    public Image targetPointer;
    public SpaceShipAI spaceShip;
    	
	// Update is called once per frame
	void Update () {

        if (!onPlay && Input.GetMouseButtonDown(0))
        {
            GameStart();
        }
    }

    public void Gameover()
    {
        onPlay = false;

        timeManager.Stop();
        cometManager.StopComets();
        camera.onPlay = false;
        targetPointer.enabled = false;
    }

    public void GameStart()
    {
        onPlay = true;
        prepareText.enabled = false;
        camera.onPlay = true;
        cometManager.Reset();
        timeManager.Reset();
        spaceShip.Respawn();
    }
}
