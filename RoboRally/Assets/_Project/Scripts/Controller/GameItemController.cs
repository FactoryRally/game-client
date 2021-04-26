using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using RoboRally.Utils;
using Tgm.Roborally.Api.Model;
using UnityEngine;
using UnityEngine.Networking;

public class GameItemController : MonoBehaviour {
    private readonly Dictionary<int, RobotCommand> programmingCards = new Dictionary<int, RobotCommand>();
    public void GetCommand(int id, Action<RobotCommand> callback) {
        if (programmingCards.ContainsKey(id)) callback(programmingCards[id]);
        else StartCoroutine(RequestProgrammingCardAsync(IngameData.ID, id, command => {
            programmingCards[id] = command;
            callback(command);
        }));
    }

    public IEnumerator RequestProgrammingCardAsync(int game, int cardId, Action<RobotCommand> callback) {
        UnityWebRequest uwp = Http.CreateGet($"games/{game}/statements/{cardId}",Http.AuthOnlyParams);
        return Http.SendWithCallback(uwp, callback);
    }
}
