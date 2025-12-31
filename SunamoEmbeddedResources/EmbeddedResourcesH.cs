namespace SunamoEmbeddedResources;

/// <summary>
///     Require assembly and default namespace.
///     Content is referred like with ResourcesH - with fs path
/// </summary>
public class EmbeddedResourcesH //: IResourceHelper
{
    /*usage:
uri = new Uri("Wpf.Tests.Resources.EmbeddedResource.txt", UriKind.Relative);
GetString(uri.ToString()) - the same string as passed in ctor Uri
     */

    /// <summary>
    ///     For entry assembly
    /// </summary>
    public static EmbeddedResourcesH Instance = null;

    protected string DefaultNamespace { get; set; }

    protected Assembly EntryAssembly { get; set; }

    protected EmbeddedResourcesH()
    {
    }

    /// <summary>
    ///     public to use in assembly like SunamoNTextCat
    ///     A2 is name of project, therefore don't insert typeResourcesSunamo.Namespace
    /// </summary>
    /// <param name="entryAssembly"></param>
    public EmbeddedResourcesH(Assembly entryAssembly, string defaultNamespace)
    {
        this.EntryAssembly = entryAssembly;
        DefaultNamespace = defaultNamespace;
    }

    protected Assembly CurrentEntryAssembly
    {
        get
        {
            if (EntryAssembly == null) EntryAssembly = Assembly.GetEntryAssembly();
            return EntryAssembly;
        }
    }

    public string GetResourceName(string name)
    {
        name = string.Join(".", DefaultNamespace,
            name.TrimStart('/').Replace("/", "."));
        return name;
    }

    /// <summary>
    ///     If it's file, return its content
    ///     Its for getting string from file, never from resx or another in code variable
    /// </summary>
    /// <param name="name"></param>
    public string GetString(string name)
    {
        var stream = GetStream(name);

        return Encoding.UTF8.GetString(FS.StreamToArrayBytes(stream));
    }

    /// <summary>
    ///     Resources/tidy_config.txt (no assembly)
    /// </summary>
    /// <param name="name"></param>
    public Stream GetStream(string name)
    {
        var resourceName = GetResourceName(name);
        var stream = CurrentEntryAssembly.GetManifestResourceStream(resourceName);
        return stream;
    }
}