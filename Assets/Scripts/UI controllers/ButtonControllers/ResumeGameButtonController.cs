

public class ResumeGameButtonController : IButtonController
{
    protected override void OnClick()
    {
        FindAnyObjectByType<MenuCamera>().CameraState = CameraState.Game;
    }
}
