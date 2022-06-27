using System.Runtime.CompilerServices;

#if DEBUG
[assembly: InternalsVisibleTo("Socketlabs.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
#endif

#if RELEASE

[assembly: InternalsVisibleTo("Socketlabs.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

#endif