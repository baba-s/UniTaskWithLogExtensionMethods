# UniTaskWithLogExtensionMethods

UniTask で処理の開始時と終了時にログを出力できる拡張メソッド

## 使用例

```cs
using Cysharp.Threading.Tasks;
using Kogane;
using UnityEngine;

public sealed class Example : MonoBehaviour
{
    private async void Start()
    {
#if ENABLE_RELEASE
        // リリースビルドの時はログ出力を無効化
        UniTaskWithLogExtensionMethods.OnStartLog      = null;
        UniTaskWithLogExtensionMethods.OnFinishLog     = null;
        UniTaskWithLogExtensionMethods.OnStartTimeLog  = null;
        UniTaskWithLogExtensionMethods.OnFinishTimeLog = null;
#else
        UniTaskWithLogExtensionMethods.OnStartLog      = message => Debug.Log( $"{message} 開始" );
        UniTaskWithLogExtensionMethods.OnFinishLog     = message => Debug.Log( $"{message} 終了" );
        UniTaskWithLogExtensionMethods.OnStartTimeLog  = message => Debug.Log( $"{message} 開始" );
        UniTaskWithLogExtensionMethods.OnFinishTimeLog = ( message, elapsed ) => Debug.Log( $"{message} 終了 {elapsed.TotalSeconds} 秒" );
#endif

        await Test1().WithLog( "Test1" );

        var str1 = await Test2().WithLog( "Test2" );

        await Test1().WithTimeLog( "Test1" );

        var str2 = await Test2().WithTimeLog( "Test2" );
    }

    private static async UniTask Test1()
    {
        await UniTask.Delay( 1000 );
    }

    private static async UniTask<string> Test2()
    {
        await UniTask.Delay( 1000 );
        return "ピカチュウ";
    }
}
```