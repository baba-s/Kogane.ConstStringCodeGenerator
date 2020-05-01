# Uni Const String Code Generator

文字列の定数のコードを生成するエディタ拡張

## 使用例

### JobType 型のコードを生成するエディタ拡張

```cs
using UniConstStringCodeGenerator;
using UnityEditor;

public class Example
{
    [MenuItem( "Tools/Generate" )]
    private static void Generate()
    {
        var template = @"using System.Collections.Generic;

namespace #NAMESPACE_NAME#
{
    /// <summary>
    /// #CLASS_COMMENT#
    /// </summary>
    public static partial class #CLASS_NAME#
    {
        /// <summary>
        /// 要素数
        /// </summary>
        public const int LENGTH = #LENGTH#;

#VALUES#
        
        /// <summary>
        /// 文字列の定数の一覧を返します
        /// </summary>
        public static IEnumerable<string> GetValues()
        {
#GET_VALUES_CONTENTS#
        }
    }
}
";
        var values = new[]
        {
            new ConstStringCodeGeneratorOptions.Element { Name = "NONE", Comment      = "無効", Value   = "NONE" },
            new ConstStringCodeGeneratorOptions.Element { Name = "SOLDIER", Comment   = "王国兵士", Value = "SOLDIER" },
            new ConstStringCodeGeneratorOptions.Element { Name = "SORCERER", Comment  = "魔法使い", Value = "SORCERER" },
            new ConstStringCodeGeneratorOptions.Element { Name = "HUNTER", Comment    = "狩人", Value   = "HUNTER" },
            new ConstStringCodeGeneratorOptions.Element { Name = "MERCENARY", Comment = "傭兵", Value   = "MERCENARY" },
        };

        var options = new ConstStringCodeGeneratorOptions
        {
            Template      = template,
            NamespaceName = "MyProject",
            ClassName     = "JobType",
            ClassComment  = "ジョブの種類",
            Elements      = values,
        };

        var path = "Assets/JobType.cs";
        var code = ConstStringCodeGenerator.Generate( options );

        code = code
                .Replace( "\t", "    " )
                .Replace( "\r\n", "#NEW_LINE#" )
                .Replace( "\r", "#NEW_LINE#" )
                .Replace( "\n", "#NEW_LINE#" )
                .Replace( "#NEW_LINE#", "\r\n" )
            ;

        ConstStringCodeGenerator.Write( path, code );
        AssetDatabase.Refresh();
    }
}
```

### 生成された JobType 型のコード

```cs
using System.Collections.Generic;

namespace MyProject
{
    /// <summary>
    /// ジョブの種類
    /// </summary>
    public static partial class JobType
    {
        /// <summary>
        /// 要素数
        /// </summary>
        public const int LENGTH = 5;

        ///<summary>
        ///<para>無効</para>
        ///</summary>
        public const string NONE = "NONE";
        ///<summary>
        ///<para>王国兵士</para>
        ///</summary>
        public const string SOLDIER = "SOLDIER";
        ///<summary>
        ///<para>魔法使い</para>
        ///</summary>
        public const string SORCERER = "SORCERER";
        ///<summary>
        ///<para>狩人</para>
        ///</summary>
        public const string HUNTER = "HUNTER";
        ///<summary>
        ///<para>傭兵</para>
        ///</summary>
        public const string MERCENARY = "MERCENARY";
        
        /// <summary>
        /// 文字列の定数の一覧を返します
        /// </summary>
        public static IEnumerable<string> GetValues()
        {
            yield return NONE;
            yield return SOLDIER;
            yield return SORCERER;
            yield return HUNTER;
            yield return MERCENARY;
        }
    }
}
```