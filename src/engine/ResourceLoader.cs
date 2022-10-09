using System.Runtime.InteropServices;

class ResourceLoader
{
    private string assemblyName;

    public ResourceLoader()
    {
        this.assemblyName = this.GetType().Assembly.GetName().Name!;
    }

    public string LoadString(string resourceName)
    {
        using var stream = this.GetType().Assembly.GetManifestResourceStream($"{this.assemblyName}.{resourceName}");
        using var reader = new StreamReader(stream!);
        return reader.ReadToEnd();
    }

    public byte[] LoadBytes(string resourceName)
    {
        using var stream = this.GetType().Assembly.GetManifestResourceStream($"{this.assemblyName}.{resourceName}");
        using var memoryStream = new MemoryStream();
        stream!.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    public (IntPtr ptr, int size) LoadToIntPtr(string resourceName)
    {
        var bytes = this.LoadBytes(resourceName);
        var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        var ptr = handle.AddrOfPinnedObject();
        return (ptr, bytes.Length);
    }
}
