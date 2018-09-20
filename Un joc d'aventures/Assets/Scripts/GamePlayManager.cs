using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    public class StoryNode
    {
        public string history;
        public string[] answers;
        public StoryNode[] nextNode;
        public bool isFinal = false;
        public delegate void NodeVisited();
        public NodeVisited nodeVisited;
    }

    public Text historyText;
    public Transform answerParent;
    public GameObject buttonAnswer;
    private StoryNode current;

    private void Start()
    {
        current = StoryFiller.FillStory();
        historyText.text = string.Empty;
        FillUI();
    }

    void answerSelected(int index)
    {
        print(index);
        historyText.text += "\n" + current.answers[index];

        if (!current.isFinal)
        {
            current = current.nextNode[index];
            FillUI();
        }
        else
        {
            //final del juego
        }
    }

    void FillUI()
    {
        historyText.text += "\n" + current.history;

        foreach (Transform child in answerParent.transform)
        {
            Destroy(child.gameObject);
        }

        bool isLeft = true;
        float height = 50;
        int index = 0;

        foreach (string answer in current.answers)
        {
            GameObject buttonAnswerCopy = Instantiate(buttonAnswer);
            buttonAnswerCopy.transform.parent = answerParent;
            float x = buttonAnswerCopy.GetComponent<RectTransform>().rect.x * 1.1f;
            buttonAnswerCopy.GetComponent<RectTransform>().localPosition = new Vector3(isLeft ? x : -x, height, 0);

            if (!isLeft)
            {
                height += buttonAnswerCopy.GetComponent<RectTransform>().rect.y * 2.5f;

                isLeft = !isLeft;
                FillListener(buttonAnswerCopy.GetComponent<Button>(), index);
                buttonAnswerCopy.GetComponentInChildren<Text>().text = answer;

                index++;
            }
        }
    }

    void FillListener(Button button, int index)
    {
        button.onClick.AddListener(() =>
        {
            answerSelected(index);
        });
    }
}
