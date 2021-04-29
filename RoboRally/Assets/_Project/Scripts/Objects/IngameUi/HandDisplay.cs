using System.Collections;
using System.Collections.Generic;
using RoboRally.Utils;
using Tgm.Roborally.Api.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class HandDisplay : MonoBehaviour {
    public  GameObject    cardPrefab;
    public  RegisterDisplay   registers;

    private Dictionary<int, GameObject> handCards = new Dictionary<int, GameObject>();
   

    // Update is called once per frame



    public void DrawCards(DrawCardEvent ev) {
        if(ev.Robot != IngameData.MyRobotId)
            return;
        foreach (int card in ev.Cards) {
            GameObject      cardUObject = Instantiate(cardPrefab, gameObject.transform);
            ProgrammingCard cardScript  = cardUObject.GetComponent<ProgrammingCard>();
            cardScript.CardId   = card;
            cardScript.onChoose = OnChoose;
            handCards[card]  = cardUObject;
        }
    }

    private void OnChoose(int card) {
        StartCoroutine(SetRegisterAsync(IngameData.GameId, registers.NextRegister, card));
    }

    private IEnumerator SetRegisterAsync(int gameID, int registerIndex, int card) {
        //TODO: make robot ID cleaner
        UnityWebRequest uwr = Http.CreatePut(
            $"games/{gameID}/entities/robots/{IngameData.MyRobotId}/registers/{registerIndex}",
            Http.Auth(new Dictionary<string, object>() {{"statement_id",card}})
        );
        yield return Http.Send(uwr,e=> Debug.Log("Hurray card set to register"),e => Debug.Log("ALARM"));
    }

    public void OnChangeRegister(ChangeRegisterEvent ev) {
        if(ev.RobotId != IngameData.MyRobotId)
            return;
        if (ev.Action == ChangeRegisterEvent.ActionEnum.Fill) {
            registers.AddCard(handCards[ev.Card]);
            handCards.Remove(ev.Card);
        }
        else {
            Debug.Log("MISSING IMPLEMENTATION FOR NON FILL");
        }
    }
    
}
