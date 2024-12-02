public class NewGameButtonController : IButtonController
{
    protected override void OnClick()
    {
        FindAnyObjectByType<GameManager>().StartNewGame();
    }
}
