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

        GamePlayManager.StoryNode node1 = CreateNode("Hay una silla y una mesa con una planta a la izquierda. A la derecha hay una estanteria con libros. Detrás parece que hay unas cajas.", new string[] { "Ir a la derecha", "Ir a la izquierda", "Ir hacia atrás", "Explorar habitación" });
        root.nextNode[0] = node1;

        GamePlayManager.StoryNode node2 = CreateNode("Nada interesnte... aunque hay un libro que llama la atención... botánica para astronautas?", new string[] { "Explorar el resto de objetos de la habitación", "Averiguar más del libro raro" });
        node1.nextNode[0] = node2;
        node2.nextNode[0] = node1;

        GamePlayManager.StoryNode node3 = CreateNode("Parece que habla de plantas, qué sorpresa. Hay una señalada, se llama plantus corrientis. Puede serme útil, me lo llevaré.", new string[] { "Explorar el resto de objetos de la habitación" });
        node3.nextNode[0] = node1;
        node2.nextNode[1] = node3;
        node3.item = ItemsEnum.Items.BOOK;

        GamePlayManager.StoryNode node4 = CreateNode("Nada interesante en las cajas, estan llenas de libros... deberían estar en la estantería", new string[] { "Volver y explorar el resto de objetos de la habitación" });
        node1.nextNode[2] = node4;
        node4.nextNode[0] = node1;

        GamePlayManager.StoryNode node5 = CreateNode("Humm, una silla. Te duele la cabeza, así que te sientas", new string[] { "Eso está muy bien, pero yo quiero ver lo de la mesa también" });
        node1.nextNode[1] = node5;

        GamePlayManager.StoryNode node6 = CreateNode("La mesa en sí no tiene nada de especial, tiene un poco de tierra de la planta. Los cajones de la mesa parecen entreabiertos", new string[] { "Explorar los cajones", "Volver a explorar los otros objetos" });
        GamePlayManager.StoryNode node6bis = CreateNode("La mesa en sí no tiene nada de especial, tiene un poco de tierra de la planta. La etiqueta de la planta pone plantus corrientis. Los cajones de la mesa parecen entreabiertos", new string[] { "Explorar los cajones", "Mirar la planta de cerca", "Volver a explorar los otros objetos" });
        
        node5.nextNode[0] = node6;
        node3.nodeVisited = () =>
        {
            node5.nextNode[0] = node6bis;
        };
        node6bis.nextNode[2] = node6.nextNode[1] = node1;

        GamePlayManager.StoryNode node7 = CreateNode("mmm.. hay unas tijeras, puede que tenga que defenderme", new string[] { "Volver a explorar los otros objetos" });
        node7.item = ItemsEnum.Items.SCISSORS;
        node6.nextNode[0] = node6bis.nextNode[0] = node7;
        node7.nextNode[0] = node1;

        GamePlayManager.StoryNode node8 = CreateNode("¡¡Al levantar la planta te encuentras una llave!! ¿Qué abrirá?", new string[] { "Explorar la habitación" });
        node6bis.nextNode[1] = node8;
        node8.item = ItemsEnum.Items.KEY;

        GamePlayManager.StoryNode node9 = CreateNode("La habitación tiene un par de ventanas y una puerta", new string[] { "Ir a la ventana #1", "Ir a la ventana #2", "Ir a la puerta" });
        root.nextNode[1] = node1.nextNode[3] = node8.nextNode[0] = node9;

        GamePlayManager.StoryNode node10 = CreateNode("La ventana está tapiada, no se puede abrir", new string[] { "Ir a la otra ventana", "Ir a la puerta" });
        node9.nextNode[0] = node9.nextNode[1] = node10;
        node10.nextNode[0] = node10;

        GamePlayManager.StoryNode node11 = CreateNode("La puerta está cerrada con un candado", new string[] { "Explorar los objetos de la habitación" });
        GamePlayManager.StoryNode node11bis = CreateNode("La puerta está cerrada con un candado", new string[] { "Explorar los objetos de la habitación", "Usar la llave" });
        node11.nextNode[0] = node11bis.nextNode[0] = node1;
        node9.nextNode[2] = node10.nextNode[1] = node11;
        node8.nodeVisited = () =>
        {
            node9.nextNode[2] = node10.nextNode[1] = node11bis;
        };

        GamePlayManager.StoryNode node12 = CreateNode("¡¡Has salido de la habitación!!", new string[] { "Salir del juego" });
        node11bis.nextNode[1] = node12;
        node12.isFinal = true;

        return root;
    }
    
}
