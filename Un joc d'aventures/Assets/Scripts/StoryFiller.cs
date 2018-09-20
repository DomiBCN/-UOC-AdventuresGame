using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryFiller : MonoBehaviour
{
    private static GamePlayManager.StoryNode CreateNode(string history, string[] options)
    {
        GamePlayManager.StoryNode node = new GamePlayManager.StoryNode()
        {
            history = history,
            answers = options,
            nextNode = new GamePlayManager.StoryNode[options.Length]
        };

        return node;
    }

    public static GamePlayManager.StoryNode FillStory()
    {
        GamePlayManager.StoryNode root = CreateNode("Te encuentras en una habitación y no recuerdas nada. Quieres salir.", new string[] { "Explorar objetos", "Explorar la habitación" });

        GamePlayManager.StoryNode node1 = CreateNode("Hay una silla y una mesa con una planta a la izquierda. A la derecha hay una estanteria con libros. Detrás parece que hay unas cajas.", new string[] { "Ir a la derecha", "Ir a la izquierda", "Ir hacia atrás", "Explorarr habitación" });
        root.nextNode[0] = node1;

        GamePlayManager.StoryNode node2 = CreateNode("Nada interesnte... aunque hay un libro que llama la atención... botánica para astronautas?", new string[] { "Explorar el resto de objetos de la habitación", "Averiguar más del libro raro" });
        node1.nextNode[0] = node2;
        node2.nextNode[0] = node1;



        return root;
    }
}
