using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public CardData[] cardDataArray;
    public GameObject cardPrefab;
    public GameObject handGameObject;
    public int handSize;
    
    // Start is called before the first frame update
    void Awake()
    {
        //Subscribe to events
        GameManager.drawCard += DrawCard;
        GameManager.startGame += DrawHand;
    }

    private void OnDestroy()
    {
        //Subscribe to events
        GameManager.drawCard -= DrawCard;
        GameManager.startGame -= DrawHand;
    }

    public void DrawCard()
    {
        //get select random CardData
        int index = Random.Range(0, cardDataArray.Length);
        //Instantiate prefab
        GameObject newCard = Instantiate(cardPrefab, handGameObject.transform,false);
        //Write data to card;
        Card cardComponent = newCard.GetComponent<Card>();
        cardComponent.CardData = cardDataArray[index];
        cardComponent.RenderData();

    }

    public void DrawHand()
    {
        for (int i = 0; i < handSize; i++)
            DrawCard();
    }

}