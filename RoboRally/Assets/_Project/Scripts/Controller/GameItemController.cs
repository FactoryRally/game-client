using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using RoboRally.Utils;
using Tgm.Roborally.Api.Model;
using UnityEngine;
using UnityEngine.Networking;

public class GameItemController : MonoBehaviour {
    private readonly Dictionary<int, RobotCommand> programmingCards = new Dictionary<int, RobotCommand>();
    // Start is called before the first frame update
    public delegate void recive<T>(T command);
    public void GetCommand(int id, recive<RobotCommand> callback) {
        if (programmingCards.ContainsKey(id)) callback(programmingCards[id]);
        else StartCoroutine(RequestProgrammingCardAsync(IngameData.ID, id, command => {
            programmingCards[id] = command;
            callback(command);
        }));
    }

    public IEnumerator RequestProgrammingCardAsync(int game, int cardId, recive<RobotCommand> callback) {
        UnityWebRequest uwp = Http.CreateGet($"/v1/games/{game}/statements/{cardId}",new Dictionary<string, object>() {
            {"pat",IngameData.JoinData.Pat}
        });
        yield return uwp.SendWebRequest();
        if(!uwp.isHttpError && uwp.downloadHandler != null) {
            Debug.Log($"Requested card {cardId} successfull");
            callback(JsonConvert.DeserializeObject<RobotCommand>(uwp.downloadHandler.text));
        }
        else {
            Debug.LogWarning("WELP WELP FEHLER : "+uwp.responseCode);
        }
    }
}
