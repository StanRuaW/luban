using Plugin.Bright.Serialization;

namespace {{namespace}}
{
   
public static class {{name}}
{
    public static System.Collections.Generic.Dictionary<int, Plugin.Bright.Net.IProtocolFactory> Factories { get; } = new System.Collections.Generic.Dictionary<int, Plugin.Bright.Net.IProtocolFactory>
    {
    {{~for proto in protos ~}}
        [{{proto.full_name}}.ID] = () => new {{proto.full_name}}(false),
    {{~end~}}
    };
}

}
