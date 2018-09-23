using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        public ItemsEnum.Items item;
    }

    public Text historyText;
    public Transform answerParent;
    public Transform inventoryParent;
    public GameObject buttonAnswer;
    public GameObject inventoryPrefab;
    public GameObject pauseMenu;
    public GameObject blurryEffect;
    public Sprite key;
    public Sprite scissors;
    public Sprite book;
    private StoryNode current;
    private bool gameOver;
    private float itemHeight = 225;

    private List<ItemsEnum.Items> currentItems = new List<ItemsEnum.Items>();

    private void Start()
    {
        gameOver = false;
        current = StoryFiller.FillStory();
        historyText.text = string.Empty;
        foreach (Transform child in inventoryParent.transform)
        {
            Destroy(child.gameObject);
        }
        FillUI();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !gameOver)
        {
            UpdateGameMenu();
        }
    }

    void UpdateGameMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        blurryEffect.SetActive(!blurryEffect.activeSelf);
    }

    void answerSelected(int index)
    {
        historyText.text += "\n" + current.answers[index];

        if (!current.isFinal)
        {
            current = current.nextNode[index];
            //mirem si en aquest node tenim mètode delegat assignat
            if (current.nodeVisited != null)
            {
                //executem el mètode delegat que en aquest cas reemplaçarà el node
                current.nodeVisited();
            }

            //si el item no es el default i encara no el tenim, l'afegim a l'inventari
            if(current.item != ItemsEnum.Items.DEFAULT && !currentItems.Contains(current.item))
            {
                GetItem(current.item);
                currentItems.Add(current.item);
            }

            FillUI();
        }
        else
        {
            //final del joc
            gameOver = true;
            UpdateGameMenu();
            //modifiquem el titol de "Pausa" a "GameOver"
            GameObject popUpTitle = GameObject.FindGameObjectWithTag("pauseTitle");
            if (popUpTitle != null)
            {
                popUpTitle.GetComponent<Text>().text = "GameOver";

            }
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
        float height = 35;
        int index = 0;

        foreach (string answer in current.answers)
        {
            GameObject buttonAnswerCopy = Instantiate(buttonAnswer);
            buttonAnswerCopy.transform.SetParent(answerParent);
            float x = buttonAnswerCopy.GetComponent<RectTransform>().rect.x * 1.1f;
            buttonAnswerCopy.GetComponent<RectTransform>().localPosition = new Vector3(isLeft ? x : -x, height, 0);

            if (!isLeft)
            {
                height += buttonAnswerCopy.GetComponent<RectTransform>().rect.y * 3f;
            }
            
            buttonAnswerCopy.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            isLeft = !isLeft;
            FillListener(buttonAnswerCopy.GetComponentInChildren<Button>(), index);
            buttonAnswerCopy.GetComponentInChildren<Text>().text = answer;
            index++;
        }
    }

    void FillListener(Button button, int index)
    {
        button.onClick.AddListener(() =>
        {
            answerSelected(index);
        });
    }

    public void GetItem(ItemsEnum.Items item)
    {
        GameObject itemCopy = Instantiate(inventoryPrefab);
        itemCopy.transform.SetParent(inventoryParent);
        float x = 0;// itemCopy.GetComponent<RectTransform>().rect.x * 1.1f;
        itemCopy.GetComponent<RectTransform>().localPosition = new Vector3(x, itemHeight, 0);

        itemHeight += itemCopy.GetComponent<RectTransform>().rect.y * 3f;

        Image[] images = itemCopy.GetComponentsInChildren<Image>();

        Image imgComponent = images.Where(i => i.name == "ImageInventory").FirstOrDefault();

        switch (item)
        {
            case ItemsEnum.Items.KEY:
                imgComponent.sprite = key;
                break;
            case ItemsEnum.Items.SCISSORS:
                imgComponent.sprite = scissors;
                break;
            case ItemsEnum.Items.BOOK:
                imgComponent.sprite = book;
                break;
            default:
                break;
        }
    }
}
