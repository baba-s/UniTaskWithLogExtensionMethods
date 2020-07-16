using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Kogane
{
	/// <summary>
	/// UniTask で処理の開始時と終了時にログを出力できる拡張メソッド
	/// </summary>
	public static class UniTaskWithLogExtensionMethods
	{
		//================================================================================
		// デリゲート(static)
		//================================================================================
		/// <summary>
		/// WithLog 関数で処理の開始時に呼び出されます
		/// </summary>
		public static Action<string> OnStartLog { get; set; } = message => Debug.Log( $"{message} 開始" );

		/// <summary>
		/// WithLog 関数で処理の終了時に呼び出されます
		/// </summary>
		public static Action<string> OnFinishLog { get; set; } = message => Debug.Log( $"{message} 終了" );

		/// <summary>
		/// WithTimeLog 関数で処理の開始時に呼び出されます
		/// </summary>
		public static Action<string> OnStartTimeLog { get; set; } = message => Debug.Log( $"{message} 開始" );

		/// <summary>
		/// WithTimeLog 関数で処理の終了時に呼び出されます
		/// </summary>
		public static Action<string, TimeSpan> OnFinishTimeLog { get; set; } = ( message, elapsed ) => Debug.Log( $"{message} 終了 {elapsed.TotalSeconds} 秒" );

		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// 処理の開始時と終了時にログを出力します
		/// </summary>
		public static async UniTask<T> WithLog<T>( this UniTask<T> task, string message )
		{
			OnStartLog?.Invoke( message );
			var result = await task;
			OnFinishLog?.Invoke( message );
			return result;
		}

		/// <summary>
		/// 処理の開始時と終了時にログを出力します
		/// </summary>
		public static async UniTask WithLog( this UniTask task, string message )
		{
			OnStartLog?.Invoke( message );
			await task;
			OnFinishLog?.Invoke( message );
		}

		/// <summary>
		/// 処理の開始時と終了時に経過時間付きのログを出力します
		/// </summary>
		public static async UniTask<T> WithTimeLog<T>( this UniTask<T> task, string message )
		{
			OnStartTimeLog?.Invoke( message );
			var now    = DateTime.Now;
			var result = await task;
			OnFinishTimeLog?.Invoke( message, DateTime.Now - now );
			return result;
		}

		/// <summary>
		/// 処理の開始時と終了時に経過時間付きのログを出力します
		/// </summary>
		public static async UniTask WithTimeLog( this UniTask task, string message )
		{
			OnStartTimeLog?.Invoke( message );
			var now = DateTime.Now;
			await task;
			OnFinishTimeLog?.Invoke( message, DateTime.Now - now );
		}
	}
}