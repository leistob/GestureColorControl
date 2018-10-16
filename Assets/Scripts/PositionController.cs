using UnityEngine;
using UnityEngine.UI;
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
    public UnityEngine.UI.Image rgbImage, redImage, greenImage, blueImage;


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
            LeapQuaternion rot = currentHand.Rotation;

        } catch (Exception e) {
            //If no hand is detected
            init = true;
            return;
        }

        int r, g, b;

        r = mapHeight(currentPosPalms.z);
        g = mapWidth(currentPosPalms.x);
        b = mapTilt(5);

        Color red = new Color(r, 0, 0, r);
        Color green = new Color(0, g, 0, g);
        Color blue = new Color(0, 0, b, 255);
        Color mixed = new Color(r, g, b, 255);

        Debug.Log(red);

        rgbImage.color = mixed;
        redImage.color = red;
        greenImage.color = green;
        blueImage.color = blue;
    }

    public void setFist(bool boo) {
        isFist = boo;
    }

    public int mapHeight(float val) {
        int value = 255;
        float maxVal = 500.0f;

        float percent = val / maxVal;
        value = (int) (percent * value);

        return Math.Abs(value);
    }
    public int mapWidth(float val) {
        int value = 255;
        float maxVal = 500.0f;

        float percent = val / maxVal;
        value = (int)(percent * value);

        //Debug.Log(percent + " " + value);
        return Math.Abs(value);
    }
    public int mapTilt(float val) {
        return 0;
    }
}
