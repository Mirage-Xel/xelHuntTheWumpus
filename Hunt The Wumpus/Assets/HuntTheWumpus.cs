using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using rnd = UnityEngine.Random;
using KeepCoding;
using KModkit;
public class HuntTheWumpus: ModuleScript {
    public KMBombInfo bomb;
    public KMBombModule module;
    public KMAudio sound;
    bool solved;

    class Cave
    {
        public int displayName;
        public int[] adjacentIndices = new int[3];
        public Cave(int a, int b,  int c)
        {
            displayName = -1; //is assigned with a non-temp value during initalization
            adjacentIndices[0] = a; adjacentIndices[1] = b; adjacentIndices[2] = c;
        }
    }
    Cave[] caveNetwork = new Cave[] { new Cave(1, 4, 7), new Cave(0, 2, 9), new Cave(1, 3, 11), new Cave(2, 4, 13), new Cave(3, 0, 5), new Cave(4, 14, 6), new Cave(7, 5, 16), new Cave(0, 8, 6), new Cave(7, 9, 17), new Cave(8, 1, 10), new Cave(9, 11, 18), new Cave(10, 12, 2), new Cave(19, 11, 13), new Cave(12, 14, 3), new Cave(13, 5, 15), new Cave(19, 16, 14), new Cave(15, 17, 6), new Cave(16, 18, 8), new Cave(17, 19, 10), new Cave(18, 12, 15)};
    int playerLocation;

    public TextMesh indicator;
    public KMSelectable[] movementButtons;
	// Update is called once per frame
	void Start() {
        movementButtons.Assign(onInteract: s => HandleMove(s));
        InitMaze();
	}

    void InitMaze() {
        for (int i = 0; i < 20; i++) { caveNetwork[i].displayName = i + 1; caveNetwork[i].adjacentIndices = caveNetwork[i].adjacentIndices.ToList().Shuffle().ToArray(); }
        playerLocation = rnd.Range(0, 20);
        UpdatePlayerPosition();
    }

    void HandleMove(KMSelectable button)
    {
        ButtonEffect(button, 0.05f, "scanner-beep");
        playerLocation = caveNetwork[playerLocation].adjacentIndices[Array.IndexOf(movementButtons, button)];
        UpdatePlayerPosition();
    }

    void UpdatePlayerPosition()
    {
        indicator.text = caveNetwork[playerLocation].displayName.ToString();
        for (int i = 0; i  < 3; i++)
        {
            movementButtons[i].GetComponent<TextMesh>().text = caveNetwork[caveNetwork[playerLocation].adjacentIndices[i]].displayName.ToString();
        }
    }
}
