using FluentMigrator.Runner.VersionTableInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMigrator
{
    [VersionTableMetaData]
    public class VersionTable : DefaultVersionTableMetaData
    {
        public override string TableName => "version_info";
        public override string ColumnName => "version";
        public override string DescriptionColumnName => "description";
        public override string AppliedOnColumnName => "applied_on";

        public override string UniqueIndexName => "uc_version";
    }
}
