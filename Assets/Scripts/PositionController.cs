using UnityEngine;
using Leap;
using Leap.Unity;
using System;

public class PositionController : MonoBehaviour {

    public LeapServiceProvider provider;
    private Controller controller;
    private Vector3 initPosCam, initPosPalm;
    private Vector3 currentPosPalms, absPosPalms;
    public float scaleX, scaleY, scaleZ;
    private bool isFist;
    private bool init;


    // Use this for initialization
    void Start() {
        controller = provider.GetLeapController();
        initPosCam = GameObject.Find("Camera").transform.position;
    }

    // Update is called once per frame
    void Update() {

        if(isFist) {
            return;
        }

        Hand currentHand;
        try {
            Frame frame = controller.Frame();
            currentHand = frame.Hands[0];
            currentPosPalms = new Vector3(currentHand.PalmPosition.x,
                                            currentHand.PalmPosition.y,
                                                currentHand.PalmPosition.z);
            if (init) {
                initPosPalm = currentPosPalms;
                Debug.Log(initPosPalm);
                init = false;
            }
            absPosPalms = new Vector3(transform.position.x + (currentPosPalms.z / 1000.0f),
                                        transform.position.y + (currentPosPalms.y / 1000.0f),
                                            transform.position.z + (currentPosPalms.x / 1000.0f));

        } catch (Exception e) {
            //If no hand is detected
            init = true;
            return;
        }
    }

    public void setFist(bool boo) {
        isFist = boo;
    }
}
