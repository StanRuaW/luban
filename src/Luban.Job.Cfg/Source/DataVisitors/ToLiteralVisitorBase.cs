using Luban.Job.Cfg.Datas;
using Luban.Job.Cfg.Defs;
using Luban.Job.Cfg.Utils;
using System.Collections.Generic;
using System.Text;

namespace Luban.Job.Cfg.DataVisitors
{
    abstract class ToLiteralVisitorBase : IDataFuncVisitor<string>
    {
        public string Accept(DBool type)
        {
            return type.Value ? "true" : "false";
        }

        public string Accept(DByte type)
        {
            return type.Value.ToString();
        }

        public string Accept(DShort type)
        {
            return type.Value.ToString();
        }

        public string Accept(DFshort type)
        {
            return type.Value.ToString();
        }

        public string Accept(DInt type)
        {
            return type.Value.ToString();
        }

        public string Accept(DFint type)
        {
            return type.Value.ToString();
        }

        public string Accept(DLong type)
        {
            return type.Value.ToString();
        }

        public string Accept(DFlong type)
        {
            return type.Value.ToString();
        }

        public string Accept(DFloat type)
        {
            return type.Value.ToString();
        }

        public string Accept(DDouble type)
        {
            return type.Value.ToString();
        }

        public string Accept(DEnum type)
        {
            return type.Value.ToString();
        }

        public virtual string Accept(DString type)
        {
            return "\"" + DataUtil.EscapeString(type.Value) + "\"";
        }

        public string Accept(DBytes type)
        {
            throw new System.NotSupportedException();
        }

        public abstract string Accept(DText type);

        public abstract string Accept(DBean type);

        public abstract string Accept(DArray type);

        public abstract string Accept(DList type);

        public abstract string Accept(DSet type);

        public abstract string Accept(DMap type);

        public abstract string Accept(DVector2 type);

        public abstract string Accept(DVector3 type);

        public abstract string Accept(DVector4 type);

        public string Accept(DDateTime type)
        {
            var ass = DefAssembly.LocalAssebmly as DefAssembly;
            return type.GetUnixTime(ass.TimeZone).ToString();
        }
    }
}
