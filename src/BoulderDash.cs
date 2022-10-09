using static SDL2.SDL;

class BoulderDash : Game
{
    private Context context;
    private TileSet tileSet;

    public BoulderDash()
    {
        var window = new Window("Boulder Dash", 800, 600);
        var renderer = new Renderer(window);
        this.registerScenes();
        this.context = new Context(window, renderer);
        GameState.sceneManager.ChangeScene("main", context);
        this.tileSet = new TileSet(context, "assets.sprites.png", 32);
    }

    private void registerScenes()
    {
        GameState.sceneManager.AddScene("main", new MainMenu());
    }

    protected override void update(double dt)
    {
        SDL_Event e;
        while (SDL_PollEvent(out e) != 0)
        {
            if (e.type == SDL_EventType.SDL_QUIT)
            {
                System.Environment.Exit(0);
            }
            if (e.type == SDL_EventType.SDL_KEYDOWN)
            {
                if (e.key.keysym.sym == SDL_Keycode.SDLK_ESCAPE)
                {
                    System.Environment.Exit(0);
                }
            }
        }
        GameState.keyState = new KeyState();
        GameState.sceneManager.GetCurrentScene()?.Update(dt, context);
    }

    protected override void draw()
    {
#if (DEBUG)
        SDL_SetWindowTitle(this.context.window.GetWindow(), "Boulder Dash - " + GameState.fps + "FPS");
#endif
        this.context.renderer.Clear();
        GameState.sceneManager.GetCurrentScene()?.Draw(this.context);
        this.context.renderer.DrawTileSet(this.tileSet, 0, 0, 1, 6);
        this.context.renderer.DrawTileSet(this.tileSet, 32, 0, 1, 6);
        this.context.renderer.DrawTileSet(this.tileSet, 64, 0, 1, 6);
        this.context.renderer.DrawTileSet(this.tileSet, 96, 0, 1, 6);
        this.context.renderer.DrawTileSet(this.tileSet, 128, 0, 1, 6);
        this.context.renderer.DrawTileSet(this.tileSet, 160, 0, 1, 6);
        this.context.renderer.Present();
        SDL_Delay(1);
    }
}
