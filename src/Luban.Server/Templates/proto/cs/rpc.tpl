{{
    name = x.name
    full_name = x.full_name
    parent = x.parent
    fields = x.fields
    targ_type = x.targ_type
    tres_type = x.tres_type
}}
using Plugin.Bright.Serialization;

namespace {{x.namespace_with_top_module}}
{
   
{{~if x.comment != '' ~}}
    /// <summary>
    /// {{x.comment}}
    /// </summary>
{{~end~}}
    public sealed class {{name}} : Plugin.Bright.Net.Codecs.Rpc<{{cs_define_type targ_type}}, {{cs_define_type tres_type}}>
    {
        public {{name}}()
        {
        }
        
        public const int ID = {{x.id}};

        public override int GetTypeId()
        {
            return ID;
        }

        public override void Reset()
        {
            throw new System.NotImplementedException();
        }

        public override object Clone()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return $"{{full_name}}{%{ {{arg:{Arg},res:{Res} }} }%}";
        }
    }
}
