using Luban.Job.Cfg.Datas;
using Luban.Job.Cfg.Defs;
using Luban.Job.Cfg.Utils;
using System.Collections.Generic;
using System.Text;

namespace Luban.Job.Cfg.DataVisitors
{
    class ToJsonLiteralVisitor : ToLiteralVisitorBase
    {
        public static ToJsonLiteralVisitor Ins { get; } = new();

        public override string Accept(DText type)
        {
            var ass = DefAssembly.LocalAssebmly;
            return $"{{\"{DText.KEY_NAME}\":\"{type.Key}\",\"{DText.TEXT_NAME}\":\"{DataUtil.EscapeString(type.GetText(ass.ExportTextTable, ass.NotConvertTextSet))}\"}}";
        }

        public override string Accept(DBean type)
        {
            var x = new StringBuilder();
            if (type.Type.IsAbstractType)
            {
                x.Append($"{{ \"_name\":\"{type.ImplType.Name}\"");
                if (type.Fields.Count > 0)
                {
                    x.Append(',');
                }
            }
            else
            {
                x.Append('{');
            }

            int index = 0;
            foreach (var f in type.Fields)
            {
                var defField = type.ImplType.HierarchyExportFields[index++];
                if (f == null)
                {
                    continue;
                }
                if (index > 1)
                {
                    x.Append(',');
                }
                x.Append('\"').Append(defField.Name).Append('\"').Append(':');
                x.Append(f.Apply(this));
            }
            x.Append('}');
            return x.ToString();
        }


        protected virtual void Append(List<DType> datas, StringBuilder x)
        {
            x.Append('[');
            int index = 0;
            foreach (var e in datas)
            {
                if (index++ > 0)
                {
                    x.Append(',');
                }
                x.Append(e.Apply(this));
            }
            x.Append(']');
        }

        public override string Accept(DArray type)
        {
            var x = new StringBuilder();
            Append(type.Datas, x);
            return x.ToString();
        }

        public override string Accept(DList type)
        {
            var x = new StringBuilder();
            Append(type.Datas, x);
            return x.ToString();
        }

        public override string Accept(DSet type)
        {
            var x = new StringBuilder();
            Append(type.Datas, x);
            return x.ToString();
        }

        public override string Accept(DMap type)
        {
            var x = new StringBuilder();
            x.Append('{');
            int index = 0;
            foreach (var e in type.Datas)
            {
                if (index++ > 0)
                {
                    x.Append(',');
                }
                x.Append('"').Append(e.Key.Apply(ToJsonPropertyNameVisitor.Ins)).Append('"');
                x.Append(':');
                x.Append(e.Value.Apply(this));
            }
            x.Append('}');
            return x.ToString();
        }

        public override string Accept(DVector2 type)
        {
            var v = type.Value;
            return $"{{\"x\":{v.X},\"y\":{v.Y}}}";
        }

        public override string Accept(DVector3 type)
        {
            var v = type.Value;
            return $"{{\"x\":{v.X},\"y\":{v.Y},\"z\":{v.Z}}}";
        }

        public override string Accept(DVector4 type)
        {
            var v = type.Value;
            return $"{{\"x\":{v.X},\"y\":{v.Y},\"z\":{v.Z},\"w\":{v.W}}}";
        }
    }
}
