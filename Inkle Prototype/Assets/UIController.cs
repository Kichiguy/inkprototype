using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class UIController : MonoBehaviour {

    public Text foodCountText;
    public int foodCount;
    public Text woodCountText;
    public int woodCount;
    public Text stoneCountText;
    public int stoneCount;
    public Text ironCountText;
    public int ironCount;
    public TextAsset storyJSON;
    private Story story;
    public GameObject dialogueContainer;
    public Text dialogueWindow;
    public GameObject answerContainer;
    public Text[] answers;
    private List<string> tags;
    

	// Use this for initialization
	void Start () {
        dialogueContainer.SetActive(false);
        answerContainer.SetActive(false);
        foodCountText.text = foodCount.ToString();
        woodCountText.text = woodCount.ToString();
        stoneCountText.text = stoneCount.ToString();
        ironCountText.text = ironCount.ToString();
        story = new Story(storyJSON.ToString());
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            dialogueContainer.SetActive(true);
            if (story.canContinue) advanceStory();
            else populateAnswers();
        }
	}

    void advanceStory()
    {
        story.Continue();
        dialogueWindow.text = story.currentText;
    }

    void populateAnswers()
    {
        answerContainer.SetActive(true);
        for(int i = 0; i < story.currentChoices.Count; i++)
        {
            answers[i].text = story.currentChoices[i].text;
        }
    }

    public void ChooseAnswer(int choice)
    {
        story.ChooseChoiceIndex(choice);
        for(int i = 0; i < answers.Length; i++)
        {
            if (i != choice) answers[i].transform.parent.gameObject.SetActive(false);
        }
        story.Continue();
        tags = story.currentTags;
        switch (tags[0])
        {
            case "food":
                UpdateFood(int.Parse(tags[1]));
                break;
            case "wood":
                UpdateWood(int.Parse(tags[1]));
                break;
            case "stone":
                UpdateStone(int.Parse(tags[1]));
                break;
            case "iron":
                UpdateIron(int.Parse(tags[1]));
                break;
            default:
                break;
        }
        dialogueWindow.text = story.currentText;
    }

    public void UpdateFood(int amount)
    {
        foodCount += amount;
        foodCountText.text = foodCount.ToString();
    }

    public void UpdateWood(int amount)
    {
        woodCount += amount;
        woodCountText.text = woodCount.ToString();
    }

    public void UpdateStone(int amount)
    {
        stoneCount += amount;
        stoneCountText.text = stoneCount.ToString();
    }

    public void UpdateIron(int amount)
    {
        ironCount += amount;
        ironCountText.text = ironCount.ToString();
    }
}
