using static SDL2.SDL_image;
using static SDL2.SDL;

class TileSet
{
    public readonly IntPtr Texture;
    public readonly int Width, Height, Resolution;

    public TileSet(Context context, String tileSetPath, int resolution)
    {
        var res = context.resourceLoader.LoadToIntPtr(tileSetPath);
        var sdlBuffer = SDL_RWFromMem(res.ptr, res.size);
        var surface = IMG_Load_RW(sdlBuffer, 1);
        this.Texture = context.renderer.CreateTextureFromSurface(surface);
        SDL_QueryTexture(Texture, out _, out _, out Width, out Height);
        SDL_FreeSurface(surface);
        this.Resolution = resolution;
    }

    ~TileSet()
    {
        SDL_DestroyTexture(Texture);
    }
}

static class TileSetRendererExtension
{
    public static void DrawTileSet(this Renderer renderer, TileSet tileSet, int x, int y, int tileX, int tileY)
    {
        var src = new SDL_Rect
        {
            x = tileX * tileSet.Resolution,
            y = tileY * tileSet.Resolution,
            w = tileSet.Resolution,
            h = tileSet.Resolution
        };
        var dst = new SDL_Rect
        {
            x = x,
            y = y,
            w = tileSet.Resolution,
            h = tileSet.Resolution
        };
        SDL_RenderCopy(renderer.GetRaw(), tileSet.Texture, ref src, ref dst);
    }
}
