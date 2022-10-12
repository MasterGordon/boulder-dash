enum TileType : int
{
    AIR = 0,
    BORDER = 1,
    DIRT = 2,
    DIAMOND = 3,
    ROCK = 4,
    WALL = 5,
    ROCK_FALLING = 6,
    DIAMOND_FALLING = 7,
    EXPLOSION = 8,
    PLAYER = 9
}

class Level : Scene
{
    private Map? map;
    private int debugSelectedTile = 1;
    private TileSet? tileSet;
    private Dictionary<int, Tile>? tiles;

    public override void Create(Context context)
    {
        var levelJson = context.resourceLoader.LoadString("assets.level1.json");
        tiles = new Dictionary<int, Tile>();
        tileSet = new TileSet(context, "assets.sprites.png", 32);
        tiles.Add((int)TileType.DIRT, new Dirt(tileSet, 1, 7));
        tiles.Add((int)TileType.BORDER, new Tile(tileSet, 1, 6));
        tiles.Add((int)TileType.DIAMOND, new Diamond(tileSet, 0, 10));
        tiles.Add((int)TileType.ROCK, new Rock(tileSet, 0, 7));
        tiles.Add((int)TileType.ROCK_FALLING, new RockFalling(tileSet, 0, 7));
        tiles.Add((int)TileType.DIAMOND_FALLING, new DiamondFalling(tileSet, 0, 10));
        tiles.Add((int)TileType.WALL, new Tile(tileSet, 3, 6));
        tiles.Add((int)TileType.PLAYER, new Player(tileSet, 0, 0));
        tiles.Add((int)TileType.EXPLOSION, new Explosion(tileSet, 2, 0));
        this.map = new Map(levelJson, tiles);
        this.map.Dump();
    }

    public override void Draw(Context context)
    {
        this.map!.Draw(context);
#if (DEBUG)
        context.renderer.SetColor(0, 0, 0);
        context.renderer.DrawRect(0, 0, 34, 34);
        if (tiles?.ContainsKey(debugSelectedTile) == true)
        {
            tiles[debugSelectedTile]!.Draw(context, 0, 0);
        }
        else
        {
            context.renderer.SetColor(255, 0, 255);
            context.renderer.DrawRect(0, 0, 32, 32);
        }
#endif
    }

    public override void Update(Context context)
    {
        this.map!.Update(context);
    }

    public override void OnMouseDown(Context context, byte button, int x, int y)
    {
        var tileX = x / 32;
        var tileY = y / 32;
        if (button == SDL2.SDL.SDL_BUTTON_LEFT)
        {
            this.map!.SetTile(tileX, tileY, this.debugSelectedTile);
        }
        if (button == SDL2.SDL.SDL_BUTTON_MIDDLE)
        {
            this.debugSelectedTile = this.map!.GetTile(tileX, tileY);
        }
        if (button == SDL2.SDL.SDL_BUTTON_RIGHT)
        {
            this.map!.SetTile(tileX, tileY, 0);
        }
    }

    public override void OnKeyDown(Context context, SDL2.SDL.SDL_Keycode key, SDL2.SDL.SDL_Keymod mod)
    {
        var number = (int)key - (int)SDL2.SDL.SDL_Keycode.SDLK_0;
        if (number >= 0 && number <= 9)
        {
            this.debugSelectedTile = number;
        }
        if (key == SDL2.SDL.SDL_Keycode.SDLK_e && mod == SDL2.SDL.SDL_Keymod.KMOD_LCTRL)
        {
            Console.WriteLine("Saving...");
            var json = this.map!.ToJson();
            context.resourceLoader.SaveString("assets.level1.json", json);
        }
        if (key == SDL2.SDL.SDL_Keycode.SDLK_r && mod == SDL2.SDL.SDL_Keymod.KMOD_LCTRL)
        {
            Console.WriteLine("Reloading...");
            this.Create(context);
        }
    }
}
