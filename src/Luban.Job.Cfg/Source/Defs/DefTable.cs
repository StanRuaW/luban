using Luban.Job.Cfg.RawDefs;
using Luban.Job.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luban.Job.Cfg.Defs
{
    public class DefTable : CfgDefTypeBase
    {
        private static readonly NLog.Logger s_logger = NLog.LogManager.GetCurrentClassLogger();

        public DefTable(Table b)
        {
            Name = b.Name;
            Namespace = b.Namespace;
            Index = b.Index;
            ValueType = b.ValueType;
            Mode = b.Mode;
            InputFiles = b.InputFiles;
            Groups = b.Groups;
            _branchInputFiles = b.BranchInputFiles;
            Comment = b.Comment;
        }


        public string Index { get; private set; }

        public string ValueType { get; }

        public ETableMode Mode { get; }

        public bool IsMapTable => Mode == ETableMode.MAP;

        public bool IsOneValueTable => Mode == ETableMode.ONE;

        public List<string> InputFiles { get; }

        private readonly Dictionary<string, List<string>> _branchInputFiles;

        public List<string> Groups { get; }

        public TType KeyTType { get; private set; }

        public TBean ValueTType { get; private set; }

        public DefField IndexField { get; private set; }

        public int IndexFieldIdIndex { get; private set; }

        public bool NeedExport => Assembly.NeedExport(this.Groups);

        public string OutputDataFile => FullName;

        public string InnerName => "_" + this.Name;

        public string OutputDataFileEscapeDot => FullName.Replace('.', '_');

        public List<string> GetBranchInputFiles(string branchName)
        {
            return _branchInputFiles.GetValueOrDefault(branchName);
        }

        public override void Compile()
        {
            var ass = Assembly;

            foreach (var branchName in _branchInputFiles.Keys)
            {
                if (ass.GetBranch(branchName) == null)
                {
                    throw new Exception($"table:'{FullName}' branch_input branch:'{branchName}' 不存在");
                }
            }

            if ((ValueTType = (TBean)ass.CreateType(Namespace, ValueType)) == null)
            {
                throw new Exception($"table:'{FullName}' 的 value类型:'{ValueType}' 不存在");
            }

            switch (Mode)
            {
                case ETableMode.ONE:
                {
                    KeyTType = null;
                    break;
                }
                case ETableMode.MAP:
                {
                    if (!string.IsNullOrWhiteSpace(Index))
                    {
                        if (ValueTType.GetBeanAs<DefBean>().TryGetField(Index, out var f, out var i))
                        {
                            IndexField = f;
                            IndexFieldIdIndex = i;
                        }
                        else
                        {
                            throw new Exception($"table:'{FullName}' index:'{Index}' 字段不存在");
                        }
                    }
                    else if (ValueTType.Bean.HierarchyFields.Count == 0)
                    {
                        throw new Exception($"table:'{FullName}' 必须定义至少一个字段");
                    }
                    else
                    {
                        IndexField = (DefField)ValueTType.Bean.HierarchyFields[0];
                        Index = IndexField.Name;
                        IndexFieldIdIndex = 0;
                    }
                    KeyTType = IndexField.CType;
                    break;
                }
                default: throw new Exception($"unknown mode:'{Mode}'");
            }
        }
    }
}
