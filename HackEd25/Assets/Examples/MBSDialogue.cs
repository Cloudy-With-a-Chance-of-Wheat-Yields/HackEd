
using System;
using TMPro;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class ConvData
{

    public int Index;
    public int SpeakerIndex;
    public string Speaker;
    public string Words;
    public string Resp0;
    public int NextNode0;
    public string Resp1;
    public int NextNode1;
    public string Resp2;
    public int NextNode2;
    public string Resp3;
    public int NextNode3;
    public string Resp4;
    public int NextNode4;
    public string Resp5;
    public int NextNode5;

    public int Flag;
    public int FlagSet;

    public int NavOne;

}

[Serializable]
public class ConvDataLines
{
    public ConvData[] data;
}


public class MBSDialogue : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] Conversation jsnConv;
    [SerializeField] string strJsnConv;




    [Header("UI")]
    [SerializeField] GameObject gmDiagloueUI;
    [SerializeField] TextMeshProUGUI tmpSpeaker;
    [SerializeField] TextMeshProUGUI tmpWords;

    [SerializeField] TextMeshProUGUI[] tmpOpText;

    [SerializeField] GameObject[] gmOptionButton;


    [Header("Current Node")]
    [SerializeField] int intNodeIndexCurrent;
    [SerializeField] int intSpeakerIndexCurrent;
    [SerializeField] string strSpeakerCurrent;
    [SerializeField] string strSpeakerTextCurrent;
    [SerializeField] string[] strResponseCurrent = new string[50];
    [SerializeField] int[] intNewNodeCurrent = new int[50];

    // used to trigger code in a seperate script custom written for the conversation. used to set the flag on entering a node

    public int intFlagCurrent;
    public int intFlagSetCurrent;

    [Header("Nodes")]
    [SerializeField] int[] intNodeIndex = new int[50];
    [SerializeField] int[] intSpeakerIndex = new int[50];
    [SerializeField] string[] strSpeaker = new string[50];
    [SerializeField] string[] strSpeakerText = new string[50];
    [SerializeField] string[,] strResponse = new string[50, 10];
    [SerializeField] int[,] intNextNode = new int[50, 10];
    [SerializeField] int[] intNodeNavigation = new int[50];

    // 0 is normal / 1 is a one visit node / 2 is a visited one visit node

    // for calling specific action in a specific script for the conversation
    [SerializeField] int[] intFlag = new int[50];
    [SerializeField] int[] intFlagSet = new int[50];



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        intNodeIndex = new int[50];
        intSpeakerIndex = new int[50];
        strSpeaker = new string[50];
        strSpeakerText = new string[50];
        strResponse = new string[50, 10];
        intNextNode = new int[50, 10];
        intNodeNavigation = new int[50];
        intFlag = new int[50];
        intFlagSet = new int[50];
        strResponseCurrent = new string[10];
        intNewNodeCurrent = new int[10];



        if (jsnConv != null)
        {
            strJsnConv = jsnConv.strConversationText;

            Debug.Log(strJsnConv);
            ConvDataLines conNodesList = JsonUtility.FromJson<ConvDataLines>(strJsnConv);

            if (conNodesList != null && conNodesList.data != null)
            {
                foreach (ConvData node in conNodesList.data)
                {


                    Debug.Log(node.Index + ": " + node.Words);
                    int n = node.Index;

                    intNodeIndex[n] = n;
                    intSpeakerIndex[n] = node.SpeakerIndex;
                    strSpeaker[n] = node.Speaker;
                    strSpeakerText[n] = node.Words;
                    strResponse[n, 0] = node.Resp0;
                    strResponse[n, 1] = node.Resp1;
                    strResponse[n, 2] = node.Resp2;
                    strResponse[n, 3] = node.Resp3;
                    strResponse[n, 4] = node.Resp4;
                    strResponse[n, 5] = node.Resp5;
                    intNextNode[n, 0] = node.NextNode0;
                    intNextNode[n, 1] = node.NextNode1;
                    intNextNode[n, 2] = node.NextNode2;
                    intNextNode[n, 3] = node.NextNode3;
                    intNextNode[n, 4] = node.NextNode4;
                    intNextNode[n, 5] = node.NextNode5;
                    intFlag[n] = node.Flag;
                    intFlagSet[n] = node.FlagSet;
                    intNodeNavigation[n] = node.NavOne;

                }



            }






            // Now you can use the 'data' object
        }
        else
        {
            Debug.LogError("JSON file not assigned!");
        }




        FnStartDialoge(0);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("still running" + Time.time);
    }

    void FnStartDialoge(int intNodeStart)
    {
        gmDiagloueUI.SetActive(true);
        Time.timeScale = 0;
        intNodeIndexCurrent = intNodeStart;
        FnUpdateNode(intNodeIndexCurrent);
        FnUpdateUI();

    }


    void FnUpdateNode(int intNewNode)
    {
        strSpeakerCurrent = strSpeaker[intNewNode];
        intSpeakerIndexCurrent = intSpeakerIndex[intNewNode];
        strSpeakerTextCurrent = strSpeakerText[intNewNode];

        for (int i = 0; i < 6; i++)
        {
            strResponseCurrent[i] = strResponse[intNewNode, i];
            intNewNodeCurrent[i] = intNextNode[intNewNode, i];
        }



        if (intNodeNavigation[intNewNode] == 1)
        {
            intNodeNavigation[intNewNode] = 2;

        }

        intFlagCurrent = intFlag[intNewNode];
        intFlagSetCurrent = intFlagSet[intNewNode];


    }

    void FnUpdateUI()
    {
        tmpSpeaker.text = strSpeakerCurrent;
        tmpWords.text = strSpeakerTextCurrent;

        for (int i = 0; i < 6; i++)
        {
            tmpOpText[i].text = strResponseCurrent[i];

            int intNextNodeNav;

            if (intNewNodeCurrent[i] == 99 || intNewNodeCurrent[i] == -1)
            {
                intNextNodeNav = 0;
            }

            else
            {
                intNextNodeNav = intNodeNavigation[intNewNodeCurrent[i]];
            }




            if (intNewNodeCurrent[i] >= 0 && intNextNodeNav != 2)
            {
                gmOptionButton[i].SetActive(true);
            }
            else
            {
                gmOptionButton[i].SetActive(false);
            }

        }
    }

    public void FnPressCheck()
    {
        Debug.Log("Button1 pressed");
        gmDiagloueUI.SetActive(false);

    }


    public void FnPressOption(int i)
    {

        Debug.Log("Press Button " + i);

        intNodeIndexCurrent = intNewNodeCurrent[i];
        FnCheckEnd(intNodeIndexCurrent);

    }
    void FnCheckEnd(int node)
    {
        if (node == 99)
        {
            Time.timeScale = 0;
            gmDiagloueUI.SetActive(false);

        }
        else
        {
            FnUpdateNode(node);
            FnUpdateUI();

        }

    }

}
