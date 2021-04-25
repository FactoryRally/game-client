using System.Collections;
using System.Collections.Generic;
using Tgm.Roborally.Api.Model;
using UnityEngine;
using UnityEngine.UI;

public class ProgrammingCard : MonoBehaviour {
    public ProgrammingCard(int cardId) {
        this.cardId = cardId;
    }

    public int                cardId;
    public Text               title;
    public Image              img;
    public Text               description;
    public GameItemController controller;
    
    // Start is called before the first frame update
    void Start() {
        controller = FindObjectOfType<GameItemController>();
        controller.GetCommand(cardId, card => {
            title.text  = card.Name;
            description.text = card.Description;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
