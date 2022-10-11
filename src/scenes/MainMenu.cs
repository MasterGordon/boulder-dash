class MainMenu : Scene
{
    double x = 0;
    bool dir = true;
    public override void Create(Context context)
    {
        context.fontManager.RegisterFont("MainMenu", "assets.font.ttf", 24);
    }

    public override void Update(Context context)
    {
        x += dir ? 10 : -10;
        var windowSize = context.window.GetSize();
        if (x + 30 > windowSize.width)
        {
            dir = false;
        }
        if (x < 0)
        {
            dir = true;
        }
        if (GameState.keyState.isPressed(Control.CONFIRM))
        {
            GameState.sceneManager.ChangeScene("level", context);
        }
    }

    public override void Draw(Context context)
    {
        var windowSize = context.window.GetSize();
        var font = context.fontManager.GetFont("MainMenu");
        context.renderer.SetFont(font, new Color(255, 255, 255));
        context.renderer.DrawText("Main Menu", windowSize.width / 2, windowSize.height / 2, true);
        context.renderer.SetColor(255, 255, 255);
        context.renderer.DrawRect(x, 0, 30, 30);
    }
}
