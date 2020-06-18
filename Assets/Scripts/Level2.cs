using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class Level2 : MonoBehaviour
{
    protected const int MAX = 21;//Total number of trials 
    public List<int> scenes = new List<int>();
    public int Block;
    public string Handedness;
    public bool KeysEnabled = false;
    private bool yes;
    private bool visible = true;
    public float WaitTimer = 0.0f;
    private bool updateTimer = false;
    public bool TestStart = false;
    public bool Testend = false;
    public bool thumbstick = false;
    public float Distance = 0.0f;
    public float Distance2 = 10.0f;
    public float Distance3 = 0.0f;
    public GameObject Canvas3;
    public GameObject ExpFinish;
    public GameObject controller;
    public int Trial;
    private ResponseTime ResponseTime;
    Vector3 tempPos;
    Vector3 tempPos2;
    Vector3 tempPos3;


    void Start()

    {
        updateTimer = true;
        WaitTimer = 00.0f;

    }
    void Update()
    {
        ResponseTime = GameObject.FindGameObjectWithTag("ResponseTime").GetComponent<ResponseTime>();
        if (ResponseTime.recording == true)
        {
            if (Testend == false)
            {
                if (TestStart == true)
                {

                    if (KeysEnabled == true)
                    {
                        if (updateTimer) //Starts the timer when the scene starts.
                        {
                            WaitTimer += Time.deltaTime * 1;
                        }
                        if (WaitTimer > 2.0f)// allows the participant to make a decition after 2 seconds

                        {
                            tempPos2 = transform.position; tempPos2.z = Distance2; transform.position = tempPos2; // moves the participant in front of the stimulus
                            if (OVRInput.GetDown(OVRInput.Button.One) || (OVRInput.GetDown(OVRInput.Button.Three)))
                            {
                                Debug.Log(Trial);
                                Trial++;
                                tempPos = transform.position; tempPos.z = Distance; transform.position = tempPos;// moves the participant to the loading stage
                                WaitTimer = 0.0f;
                                updateTimer = true;
                                if (scenes.Count == 0)
                                {
                                    if (Block > 2) // Loads the ending scene when the disired blocks of trials have been run
                                    {
                                        SceneManager.LoadScene("End");
                                        Testend = true;
                                        ResponseTime.recording = false;
                                    }
                                    TestStart = false;
                                }
                                // Get a random index from the list of remaining level 
                                int randomIndex = Random.Range(0, scenes.Count);
                                int level = scenes[randomIndex];
                                scenes.RemoveAt(randomIndex); // Removes the level from the list                       
                                SceneManager.LoadScene(level);
                            }
                        }
                    }

                    if (KeysEnabled == false)
                    {
                        if (updateTimer) //Starts the timer when the scene starts.
                        { WaitTimer += Time.deltaTime * 1; }
                    }
                    if (WaitTimer > 2.0f)
                    {
                        KeysEnabled = true;
                    }

                }

                if (TestStart == false)
                {
                    updateTimer = false;
                    if (Block > 2)// Ends the experiment after the 4th block/ Since the first block is practice this ends it after 3 recorded blocks have been completed
                    {
                        SceneManager.LoadScene("End");
                        Canvas3.SetActive(visible);
                        ExpFinish.SetActive(visible);
                        Testend = true;
                    }
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))
                    {
                        thumbstick = true;
                    }
                }
            }
            if (thumbstick == true)
            {
                tempPos = transform.position; tempPos.z = Distance; transform.position = tempPos;
                WaitTimer = 0.0f;
                scenes = new List<int>(Enumerable.Range(1, MAX));
                int randomIndex = Random.Range(0, scenes.Count);
                int level = scenes[randomIndex];
                scenes.RemoveAt(randomIndex); // Removes the level from the list
                SceneManager.LoadScene(level);
                updateTimer = true;
                TestStart = true;
                thumbstick = false;
            }
        }
    }
}





