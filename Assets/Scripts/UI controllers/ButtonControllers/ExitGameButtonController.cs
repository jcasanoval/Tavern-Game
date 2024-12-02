using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameButtonController : IButtonController

{
    protected override void OnClick()
    {
        FindAnyObjectByType<GameManager>().EndGame();
    }
}
