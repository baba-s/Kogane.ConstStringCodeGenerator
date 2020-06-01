using System.IO;
using System.Text;

namespace Kogane
{
	/// <summary>
	/// 文字列の定数のコードを生成するクラス
	/// </summary>
	public static class ConstStringCodeGenerator
	{
		//================================================================================
		// 定数
		//================================================================================
		public const string TAG_NAMESPACE_NAME      = "#NAMESPACE_NAME#";
		public const string TAG_CLASS_NAME          = "#CLASS_NAME#";
		public const string TAG_CLASS_COMMENT       = "#CLASS_COMMENT#";
		public const string TAG_VALUES              = "#VALUES#";
		public const string TAG_LENGTH              = "#LENGTH#";
		public const string TAG_GET_VALUES_CONTENTS = "#GET_VALUES_CONTENTS#";

		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// 指定されたパスに文字列の定数のコードを書き込みます
		/// </summary>
		public static void Write( string path, ConstStringCodeGeneratorOptions options )
		{
			var code = Generate( options );
			Write( path, code );
		}

		/// <summary>
		/// 指定されたパスに指定された文字列を書き込みます
		/// </summary>
		public static void Write( string path, string code )
		{
			var dir = Path.GetDirectoryName( path );

			if ( string.IsNullOrWhiteSpace( dir ) ) return;

			if ( !Directory.Exists( dir ) )
			{
				Directory.CreateDirectory( dir );
			}

			File.WriteAllText( path, code );
		}

		/// <summary>
		/// 文字列の定数のコードを表現する文字列を生成して返します
		/// </summary>
		public static string Generate( ConstStringCodeGeneratorOptions options )
		{
			var template          = options.Template;
			var namespaceName     = options.NamespaceName;
			var className         = options.ClassName;
			var classComment      = options.ClassComment;
			var length            = options.Elements.Length;
			var values            = GetValuesText( options );
			var getValuesContents = GetGetValuesContents( options );
			var output            = template;

			output = output.Replace( TAG_NAMESPACE_NAME, namespaceName );
			output = output.Replace( TAG_CLASS_NAME, className );
			output = output.Replace( TAG_CLASS_COMMENT, classComment );
			output = output.Replace( TAG_VALUES, values );
			output = output.Replace( TAG_LENGTH, length.ToString() );
			output = output.Replace( TAG_GET_VALUES_CONTENTS, getValuesContents );

			return output;
		}

		/// <summary>
		/// 文字列の定数の要素を定義するコードを生成して返します
		/// </summary>
		private static string GetValuesText( ConstStringCodeGeneratorOptions options )
		{
			var builder = new StringBuilder();

			foreach ( var element in options.Elements )
			{
				var comment = element.Comment;
				var name    = element.Name;
				var value   = element.Value;

				// コメントが複数行の場合も考慮
				builder.Append( "\t\t" ).AppendLine( "///<summary>" );
				foreach ( var n in comment.Split( '\n' ) )
				{
					builder.Append( "\t\t" ).AppendFormat( "///<para>{0}</para>", n ).AppendLine();
				}

				builder.Append( "\t\t" ).AppendLine( "///</summary>" );
				builder.Append( "\t\t" ).AppendFormat( @"public const string {0} = ""{1}"";", name, value ).AppendLine();
			}

			return builder.ToString().TrimEnd();
		}

		/// <summary>
		/// 文字列の定数の一覧を返す関数の中身のコードを生成して返します
		/// </summary>
		private static string GetGetValuesContents( ConstStringCodeGeneratorOptions options )
		{
			var builder = new StringBuilder();

			foreach ( var element in options.Elements )
			{
				var name = element.Name;

				builder.Append( "\t\t\t" ).AppendFormat( @"yield return {0};", name ).AppendLine();
			}

			return builder.ToString().TrimEnd();
		}
	}
}