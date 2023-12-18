using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour, IDataPersistence
{
    //Fields
    //Window
    public GameObject window;
    //Indicator
    public GameObject indicator;
    //Text component
    public TMP_Text dialogueText;
    //Dialogues list
    public List<string> dialogues;
    //Writing speed
    public float writingSpeed;
    //Index on dialogue
    private int index;
    //Character index
    private int charIndex;
    //Started boolean
    private bool started;
    //Wait for next boolean
    private bool waitForNext;

    private bool triggeredOnce = false;

    private void Awake()
    {
        ToggleIndicator(false);
        ToggleWindow(false);
    }

    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }
    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    //Start Dialogue
    public void StartDialogue()
    {
        if (started)
            return;

        //Boolean to indicate that we have started
        started = true;
        //Show the window
        ToggleWindow(true);
        //hide the indicator
        ToggleIndicator(false);
        //Start with first dialogue or random index if triggered once
        if(triggeredOnce == true)
        {
            GetDialogue(1);
            return;
        }
        GetDialogue(0);
    }

    private void GetDialogue(int i)
    {
        //start index at zero
        index = i;
        //Reset the character index
        charIndex = 0;
        //clear the dialogue component text
        dialogueText.text = string.Empty;
        //Start writing
        StartCoroutine(Writing());
    }

    //End Dialogue
    public void EndDialogue()
    {
        //Stared is disabled
        started = false;
        //Disable wait for next as well
        waitForNext = false;
        //Stop all Ienumerators
        StopAllCoroutines();
        //Hide the window
        ToggleWindow(false);       
        this.triggeredOnce = true;
    }
    //Writing logic
    IEnumerator Writing()
    {
        yield return new WaitForSeconds(writingSpeed);

        string currentDialogue = dialogues[index];
        //Write the character
        dialogueText.text += currentDialogue[charIndex];
        //increase the character index 
        charIndex++;
        //Make sure you have reached the end of the sentence
        if(charIndex < currentDialogue.Length)
        {
            //Wait x seconds 
            yield return new WaitForSeconds(writingSpeed);
            //Restart the same process
            StartCoroutine(Writing());
        }
        else
        {
            //End this sentence and wait for the next one
            waitForNext = true;
            UpdateDialogueFrameSize();
        }        
    }

    public bool IsDialogueRunning()
    {
        return started;
    }
    private void Update()
    {
        if (!started)
            return;

        if(waitForNext && Input.GetKeyDown(KeyCode.E))
        {
            waitForNext = false;
            index++;

            //Check if we are in the scope fo dialogues List
            if(index < dialogues.Count)
            {
                //If so fetch the next dialogue
                GetDialogue(index);
            }
            else
            {
                //If not end the dialogue process
                ToggleIndicator(true);
                EndDialogue();
            }            
        }
    }
    private void UpdateDialogueFrameSize()
    {
        // Get the preferred height of the text based on its content
        float preferredHeight = dialogueText.preferredHeight;
    
        // Get the RectTransform of the dialogueText
        RectTransform textRectTransform = dialogueText.GetComponent<RectTransform>();
    
        // Adjust the size of the dialogue frame based on the preferred height
        textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x, preferredHeight);
    
        // You may also want to adjust the size of the window based on the preferred height
        RectTransform windowRectTransform = window.GetComponent<RectTransform>();
        windowRectTransform.sizeDelta = new Vector2(windowRectTransform.sizeDelta.x, preferredHeight);
    }
    
    public void LoadData(GameData data)
    {
        this.triggeredOnce = data.oldManDialogueTriggered;
    }
    public void SaveData(ref GameData data)
    {   
        data.oldManDialogueTriggered = this.triggeredOnce;
    }
}