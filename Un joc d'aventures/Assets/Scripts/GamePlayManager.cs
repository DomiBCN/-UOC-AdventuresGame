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
        historyText.text += "\n<b>" + current.answers[index] + "</b>";

        if (!current.isFinal)
        {
            current = current.nextNode[index];
            //mirem si en aquest node tenim mètode delegat assignat
            if (current.nodeVisited != null)
            {
                //executem el mètode delegat que en aquest cas reemplaçarà el node
                current.nodeVisited();
            }
            FillUI();
        }
        else
        {
            //final del joc
        }
    }

    void FillUI()
    {
        historyText.text += "\n" + current.history;

        foreach (Transform child in answerParent.transform)
        {
            Destroy(child.gameObject);
        }
        
        float height = 80;
        int index = 0;

        foreach (string answer in current.answers)
        {
            GameObject buttonAnswerCopy = Instantiate(buttonAnswer);
            buttonAnswerCopy.transform.SetParent(answerParent);
            float x = buttonAnswerCopy.GetComponent<RectTransform>().rect.x * 1.1f;
            buttonAnswerCopy.GetComponent<RectTransform>().localPosition = new Vector3(x, height, 0);

            height += buttonAnswerCopy.GetComponent<RectTransform>().rect.y * 2.5f;
            
            //if (!isLeft)
            //{
            //height += buttonAnswerCopy.GetComponent<RectTransform>().rect.y * 2.5f;

            //isLeft = !isLeft;
            FillListener(buttonAnswerCopy.GetComponent<Button>(), index);
            buttonAnswerCopy.GetComponentInChildren<Text>().text = answer;

            index++;
            //}
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
