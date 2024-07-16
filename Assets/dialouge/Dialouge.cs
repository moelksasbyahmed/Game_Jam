using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Dialouge: MonoBehaviour
{

    [Header("this need an dialouge manager at the player or at the event system in order to work")]
    [SerializeField] private Text LeftName; // Reference to UI text element for character name
    [SerializeField] private Text rightName; // Reference to UI text element for character name
    [SerializeField] private Image Rightimage; // Reference to UI image element for character portrait
    [SerializeField] private Image Leftimage; // Reference to UI image element for character portrait
    [SerializeField] private Text dialogueText; // Reference to UI text element for dialogue
                                                //  [SerializeField] private RectTransform imageRect; // Reference to RectTransform of image for positioning

    Animator animator;


    private static List<DialogueLine> dialogueLines; // List to store parsed dialogue lines

    private void Start()
    {
        animator = GetComponent<Animator>();
        LoadNextLine();
    }

  
    public static void LoadAllDia(string fileName)
    {
        dialogueLines = ReadDialogueLines(fileName + ".txt"); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LoadNextLine();

        }
    }
    public float characterDelay = 0.1f;
    IEnumerator WritingAnimation(string text)
    {
      

        float PastTime = 0f;
        dialogueText.text = "";
        for (int i = 0; i < text.Length;)
        {
   
            PastTime += Time.deltaTime;
  


            while (PastTime >= characterDelay)
            {
                dialogueText.text += text[i];
               
                i++;
                PastTime -= characterDelay;
            }
            
            yield return null;

        }
        writingCoroutine = null;

    }
    Coroutine writingCoroutine = null;

    public void LoadNextLine()
    {
        if (dialogueLines.Count == 0)
        {
            gameObject.SetActive(false);
            // Handle end of dialogue 
            return;
        }

        DialogueLine currentLine = dialogueLines[0];
        dialogueLines.RemoveAt(0); // Remove the line from the list


        if (writingCoroutine != null)
        {
            StopCoroutine(writingCoroutine);
        }

        writingCoroutine = StartCoroutine(WritingAnimation(currentLine.DialogueText));

        // Load and display image

        // Position image based on side (right or left)
        if (currentLine.Side.ToLower() == "right")
        {
            rightName.gameObject.SetActive(true);
            Rightimage.gameObject.SetActive(true);
            animator.SetBool("right", true);
            rightName.text = currentLine.CharacterName;

            Rightimage.sprite = Resources.Load<Sprite>(currentLine.PhotoName);
            //Resources.Load<Sprite>("Triangle");
            Debug.Log("the photo should change");
            //imageRect.anchorMin = new Vector2(1, 0.5f);
            //imageRect.anchorMax = new Vector2(1, 0.5f);

        }
        else if (currentLine.Side.ToLower() == "left")
        {
            Leftimage.gameObject.SetActive(true);
            LeftName.gameObject.SetActive(true);
            animator.SetBool("right", false);

            LeftName.text = currentLine.CharacterName;

            Leftimage.sprite = Resources.Load<Sprite>(currentLine.PhotoName);

            //imageRect.anchorMin = new Vector2(0, 0.5f);
            //imageRect.anchorMax = new Vector2(0, 0.5f);
        }
        else
        {
            Debug.LogError("Invalid dialogue line format in   in line " + currentLine + ".!.");


        }
    }

    private static List<DialogueLine> ReadDialogueLines(string fileName)
    {
        List<DialogueLine> lines = new List<DialogueLine>();
        string filePath = Path.Combine(Application.dataPath, "Resources/" + fileName);


        if (File.Exists(filePath))
        {
            string[] linesFromFile = File.ReadAllLines(filePath);
            int LineNumber = 0;
            foreach (string line in linesFromFile)
            {
                LineNumber++;
                string[] parts = line.Split('|');
                if (parts.Length >= 4)
                {
                    lines.Add(new DialogueLine(parts[0], parts[1], parts[2], parts[3]));
                }
                else
                {
                    Debug.LogError("Invalid dialogue line format in " + Application.dataPath + "/Resources/" + fileName + " in line " + LineNumber + ".!.");
                }
            }
        }
        else
        {
            Debug.LogError("Dialogue file not found: " + Application.dataPath + "/Resources/" + fileName);

        }

        return lines;
    }

    /// <summary>
    /// photo characterName Text Side
    /// </summary>
    private class DialogueLine
    {
        public string PhotoName { get; private set; }
        public string CharacterName { get; private set; }
        public string DialogueText { get; private set; }
        public string Side { get; private set; }

        public DialogueLine(string photoName, string characterName, string dialogueText, string side)
        {
            PhotoName = photoName;
            CharacterName = characterName;
            DialogueText = dialogueText;
            Side = side;
        }
    }
}
