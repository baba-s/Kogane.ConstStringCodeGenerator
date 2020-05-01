namespace UniConstStringCodeGenerator
{
	/// <summary>
	/// 文字列の定数のコードを生成する時のオプションを管理するクラス
	/// </summary>
	public sealed class ConstStringCodeGeneratorOptions
	{
		/// <summary>
		/// 文字列の定数の要素の情報を管理するクラス
		/// </summary>
		public sealed class Element
		{
			/// <summary>
			/// 要素の名前
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// 要素のコメント
			/// </summary>
			public string Comment { get; set; }

			/// <summary>
			/// 要素の値
			/// </summary>
			public string Value { get; set; }
		}

		/// <summary>
		/// コードのテンプレート
		/// </summary>
		public string Template { get; set; }

		/// <summary>
		/// 名前空間の名前
		/// </summary>
		public string NamespaceName { get; set; }

		/// <summary>
		/// クラスの名前
		/// </summary>
		public string ClassName { get; set; }

		/// <summary>
		/// クラスのコメント
		/// </summary>
		public string ClassComment { get; set; }

		/// <summary>
		/// 文字列の定数の要素の情報を管理する配列
		/// </summary>
		public Element[] Elements { get; set; }
	}
}