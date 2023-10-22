using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public CardData[] cardDataArray;
    public GameObject cardPrefab;
    public GameObject handGameObject;
    public GameObject rewardGameObject;
    public int handSize;
    public int rewardSize;

    // Start is called before the first frame update
    void OnEnable()
    {
        //Subscribe to events
        GameManager.drawCard += DrawCard;
        GameManager.startGame += DrawHand;
        GameManager.startGame += DrawReward;
    }

    private void OnDisable()
    {
        //Subscribe to events
        GameManager.drawCard -= DrawCard;
        GameManager.startGame -= DrawHand;
        GameManager.startGame -= DrawReward;
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

    public void DrawcardReward()
    {
        //get select random CardData
        int index = Random.Range(0, cardDataArray.Length);
        //Instantiate prefab
        GameObject newCard = Instantiate(cardPrefab, rewardGameObject.transform, false);
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

    public void DrawReward()
    {
        for (int i = 0; i < rewardSize; i++)
            DrawcardReward();
    }


}