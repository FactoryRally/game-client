using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterDisplay : MonoBehaviour {
    public int NextRegister = 0;

    public void AddCard(GameObject card) {
        card.gameObject.transform.parent = this.transform;
        NextRegister++;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
