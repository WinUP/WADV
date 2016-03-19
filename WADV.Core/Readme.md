#WADV核心框架开发指南

这份文档将会讲述WADV引擎核心所提供的功能，以及二次开发的注意事项。

##00. 开始之前

如果你要进行模块开发，有一点是需要注意的，即**只有 `WADV.Core.API` 名称空间内提供的函数才是对外公开的**，其余的函数，除非对子类可访问，否则你无法在你的模块内调用。

这些是引擎提供的功能及它们所在的名称空间：

> * 游戏消息循环
    * `WADV.Core.API.Message`
> * 游戏主循环
    * `WADV.Core.API.Loop`
> * 计时器
    * `WADV.Core.API.Timer`
> * 路径管理
    * `WADV.Core.API.Path`
> * 脚本支持
    * `WADV.Core.API.Script`
> * 游戏系统本身
    * `WADV.Core.API.Game`
> * 模块支持
    * `WADV.Core.API.Plugin`

##01. 游戏消息循环

游戏消息循环是框架在运行时第一个开始运作的功能，它是其他功能的基础。游戏消息循环的唯一功能为进行消息的接收和发送。所谓的消息，即是**长度不超过50的Unicode字符串**，这些字符串被设计为遵守一个固定的命名规范，即：

* **[模块简称]功能名称_操作结果**

比如：[SYSTEM]LOOP_START。这个规范并不是强制的，但遵守这个规范可让游戏逻辑开发者少死一些脑细胞。

###消息接收器
消息循环的接收到的每一条消息都会被发送给所有已注册的接收器。也就是说，如果想要异步接收消息的话，就必须注册一个消息接收器到消息循环内。消息接收器只有一个要求，就是实现 `WADV.Core.PluginInterface.IMessageReceiver` 接口：

```vb.net
Public Interface IMessageReceiver
    Sub ReceivingMessage(message As String)
End Interface
```

每当消息循环接收到新消息时，`ReceivingMessage()` 函数都会被调用，其中参数 `message`为消息循环接收到的消息。

只有上一个接收器处理完消息后消息循环才会调用下一个接收器，当所有接收器都处理完成后，消息循环便会丢弃这条消息，并在可能的情况下开始处理下一条消息。另一个需要注意的问题是，你无法同步接收消息，因为这会导致调用线程进入无法预计时间的阻塞。你在接收器自定义代码中对消息原文造成的修改**会影响排在你之后的接收器**，因此请慎重修改消息原文。阅读接下来的“公开API”部分可以让你了解消息循环提供的全部功能，比如添加接收器的方法。

###公开API

这些API均位于 `WADV.Core.API.MessageAPI`名称空间下。代码中已经含有完整的注释，因此这里便不再赘述。详情请参阅[代码](https://github.com/WinUP/WADV/blob/master/WADV.Core/API/Message.vb)。

顺便一提，以“Sync”结尾的API为同步API，以“Async”结尾的API为异步API。所有有返回值的API，除非特别注明，否则都是同步API。

有些API不是在调用线程中执行主要逻辑的，欲知详情请参考代码的文档注释。

| 函数 | 功能 |
| ----- | ----- |
| `AddSync(IMessageReceiver)`    | 添加一个接收器           |
| `DeleteSync(IMessageReceiver)` | 删除一个接收器           |
| `SendSync(String)`             | 发送一条消息             |
| `WaitSync(String)`             | 等待下一条指定消息的出现 |
| `LastMessage() : String`       | 获取最近广播的一条信息   |
| `StartSync()`                  | 启动消息循环             |
| `StopSync()`                   | 终止消息循环             |
| `GetStatus() : Boolean`        | 获取消息循环的状态       |

###附注

- 消息循环工作在一个特殊的后台线程内，线程名称为“[系统]消息循环线程”。
- 在没有消息可被分发时，消息循环所在的线程是暂停的。新的消息被发送时它会被自动唤醒。

##02. 游戏主循环

游戏主循环为框架提供按照“每秒不超过多少次”的速度循环执行指定代码的功能，这些代码通俗地讲就是“帧内容”，而执行一次主循环又被叫做“一帧”。

###循环接收器

主循环每次执行时都会依次调用所有已注册的接收器。也就是说，如果想要使用主循环，就必须注册一个消息接收器到主循环内。循环接收器只有一个要求，就是实现 `WADV.Core.PluginInterface.ILoopReceiver` 接口：

```vb.net
Public Interface ILoopReceiver
    Function Logic(frame As Integer) As Boolean
    Sub Render()
End Interface
```

每当主循环执行时，`Logic()` 函数会先被调用，其中参数 `frame`为自游戏启动以来已经经过的帧数。当 `Logic()` 函数执行完成后， `Render()` 函数才会被调用。由于 `Render()` 函数默认在UI线程中执行，所以界面操作放置在该函数体内会比较省力。

只有上一个接收器工作完成后主循环才会调用下一个接收器，当所有接收器都处理完成后，主循环会增加一次帧计数，并开始进行下一次循环。阅读接下来的“公开API”部分可以让你了解主循环提供的全部功能，比如添加接收器的方法。

###公开API

这些API均位于 `WADV.Core.API.LoopAPI`名称空间下。代码中已经含有完整的注释，因此这里便不再赘述。详情请参阅[代码](https://github.com/WinUP/WADV/blob/master/WADV.Core/API/LoopAPI.vb)。

| 函数 | 功能 |
| ----- | ----- |
| `SetFrameSync(Integer)`                | 设置理想执行周期             |
| `GetFrame() : Integer`                 | 获取理想执行周期             |
| `AddLoopSync(ILoopReceiver)`           | 添加一个循环接收器           |
| `WaitLoopSync(ILoopReceiver)`          | 等待一个循环接收器完成并退出 |
| `WaitFrameSync(Integer)`               | 将当前线程挂起指定帧数       |
| `StartSync()`                          | 启动消息循环                 |
| `StopSync()`                           | 终止游戏循环                 |
| `CurrentFrame() : Integer`             | 获取当前的帧计数             |
| `GetStatus() : Boolean`                | 获取当前的帧计数             |
| `TranslateToTime(Integer) : TimeSpan`  | 将指定帧数转换为理想运行时间 |
| `TranslateToFrame(TimeSpan) : Integer` | 将指定时间长度转换为理想帧数 |

###附注

- 主循环工作在一个特殊的后台线程内，线程名称为“[系统]游戏循环线程”。
- 即便没有任何接收器被注册，主循环仍然会按照既定帧率执行。

##03. 计时器

计时器会按照用户的配置按照指定时间间隔发送消息 `[SYSTEM]TIMER_TICK`，因此若游戏消息循环没有工作的话，计时器也无法工作。

计时器的发送间隔可在游戏运行时调整，但是这个调整并不会立即生效，而是在下次计时器发送消息时生效，即时间调整不影响接下来的第一次发送，只影响第二次以及第二次之后的消息。

###公开API

这些API均位于 `WADV.Core.API.TimeAPI`名称空间下。代码中已经含有完整的注释，因此这里便不再赘述。详情请参阅[代码](https://github.com/WinUP/WADV/blob/master/WADV.Core/API/TimerAPI.vb)。

| 函数 | 功能 |
| ----- | ----- |
| `StartSync()`           | 启动计时器           |
| `StopSync()`            | 停止计时器           |
| `SetTickSync(Integer)`  | 设计计时器的计时间隔 |
| `GetTick() : Integer`   | 获取计时器的计时间隔 |
| `GetStatus() : Boolean` | 获取计时器的状态     |

###附注

- 计时器工作在一个特殊的后台线程内，线程名称为“[系统]游戏计时线程”。
- 计时器在启动时会先发送一次消息。
- 一旦计时器被终止，那么当计时器再次启动时，时间是重新计算的。

##04. 资源管理

框架提供了资源分类和资源加载功能。因为WPF部分API的限制，框架没有提供打包功能，这个问题可能会在以后解决。

虽然框架没有强制要求对应的资源必须严格按照分类放置，但既然分类功能都有了，那为什么不用呢？

###公开API

这些API位于 `WADV.Core.API.PathAPI` 和 `WADV.Core.API.ResourceAPI`名称空间下。你可以参阅它们的代码：
[WADV.Core.API.PathAPI](https://github.com/WinUP/WADV/blob/master/WADV.Core/API/PathAPI.vb) 和 [WADV.Core.API.ResourceAPI](https://github.com/WinUP/WADV/blob/master/WADV.Core/API/ResourceAPI.vb)

######位于 `WADV.Core.API.PathAPI` 名称空间的函数

| 函数 | 功能 |
| ----- | ----- |
| `Resource() : String`                  | 获取程序资源文件的存放路径     |
| `SetResource(String)`                  | 设置程序资源文件的存放路径     |
| `Skin() : String`                      | 获取程序皮肤文件的存放路径     |
| `SetSkin(String)`                      | 设置程序皮肤文件的存放路径     |
| `Plugin() : String`                    | 设置程序皮肤文件的存放路径     |
| `SetPlugin(String)`                    | 获取程序插件的存放路径         |
| `Script() : String`                    | 获取程序脚本文件的存放路径     |
| `SetScript(String)`                    | 设置程序脚本文件的存放路径     |
| `UserFile() : String`                  | 获取程序用户个人文件的存放路径 |
| `SetUserFile(String)`                  | 设置程序用户个人文件的存放路径 |
| `Game() : String`                      | 获取程序主存储目录             |
| `GetPath(PathType, [String]) : String` | 获取完整路径                   |
| `GetUri(PathType, [String]) : Uri`     | 获取完整路径的URI表示形式      |

######位于 `WADV.Core.API.ResourceAPI` 名称空间的函数

| 函数 | 功能 |
| ----- | ----- |
| `LoadToGameSync(String)`                   | 加载资源到游戏全局 |
| `LoadToWindowSync(String)`                 | 加载资源到主窗口   |
| `ClearGameSync()`                          | 清空全局资源       |
| `ClearWindowSync()`                        | 清空主窗口资源     |
| `RemoveFromGameSync(ResourceDictionary)`   | 清除指定全局资源   |
| `RemoveFromWindowSync(ResourceDictionary)` | 清除指定主窗口资源 |
| `GetFromGame() : ResourceDictionary`       | 获取全局资源对象   |
| `GetFromWindow() : ResourceDictionary`     | 获取全局资源对象   |

###附注

- 对主窗口的资源操作是在UI中执行的。

##05. 基础渲染

框架要求在启动时必须注册一个 `NavigationWindow` 为游戏主窗口，之后游戏的渲染均以该窗口为默认对象。框架对该窗口提供了诸多设置用API，这类API也是框架所有种类的API中功能最丰富的。许多WindowAPI都提供了同步和异步两种版本，方便开发者依据实际情况使用。

###公开API

这些API均位于 `WADV.Core.API.WindowAPI`名称空间下。详情请参阅[代码](https://github.com/WinUP/WADV/blob/master/WADV.Core/API/WindowAPI.vb)。

| 函数 | 功能 |
| ----- | ----- |
| `SetTitleSync(String)`                       | 修改窗口标题               |
| `ClearContentSync(Panel)`                    | 清空指定容器               |
| `LoadElementSync(Panel, String)`             | 为指定容器加载子元素       |
| `LoadElementAsync(Panel, String)`            | 为指定容器加载子元素       |
| `LoadPageSync(String, NavigateOperation)`    | 从文件加载指定的页面布局   |
| `LoadPageAsync(String, NavigateOperation)`   | 从文件加载指定的页面布局   |
| `LoadObjectSync(Page, NavigateOperation)`    | 从对象加载指定的页面布局   |
| `LoadObjectAsync(Page, NavigateOperation)`   | 从对象加载指定的页面布局   |
| `LoadUriSync(Uri, NavigateOperation)`        | 从路径加载制定的页面布局   |
| `LoadUriAsync(Uri, NavigateOperation)`       | 从路径加载制定的页面布局   |
| `FadeOutPageSync(Integer)`                   | 淡出当前页面               |
| `FadeOutPageAsync(Integer)`                  | 淡出当前页面               |
| `FadeInPageSync(Integer)`                    | 淡入当前页面               |
| `FadeInPageAsync(Integer)`                   | 淡入当前页面               |
| `GoBackSync()`                               | 返回上一个页面             |
| `GoForwardSync()`                            | 前进到下一个页面           |
| `RemoveOneBackSync()`                        | 移除一个最近的返回记录     |
| `RemoveBackListSync()`                       | 移除所有的返回记录         |
| `SetBackgroundByColorSync(Color)`            | 修改窗口背景色             |
| `SetBackgroundByRgbSync(Byte, Byte, Byte)`   | 修改窗口背景色             |
| `SetBackgroundByHexSync(String)`             | 修改窗口背景色             |
| `SetWidthSync(Double)`                       | 修改窗口宽度               |
| `SetHeightSync(Double)`                      | 修改窗口高度               |
| `SetResizeModeSync(Boolean)`                 | 设置窗口调整模式           |
| `SetTopmostSync(Boolean)`                    | 设置窗口置顶模式           |
| `SetIconSync(String)`                        | 设置窗口图标               |
| `SetCursorSync(String)`                      | 设置窗口指针               |
| `GetChildByName` `(Of T As FrameworkElement)` `(DependencyObject, String) : T` | 根据名称获取元素的子元素(支持多级查找) |
| `SearchObject` `(Of T As FrameworkElement)(String) : T` | 在窗口中查找具有指定名称的元素 |
| `GetRoot` `(Of T As FrameworkElement)() : T` | 获得窗口中的根元素         |
| `GetDispatcher() : Dispatcher`               | 获取窗口线程工作队列       |
| `GetWindow() : NavigationWindow`             | 获取窗口对象               |
| `InvokeSync(Action)`                         | 在UI线程上执行一个无参委托 |
| `InvokeAsync(Action)`                        | 在UI线程上执行一个无参委托 |
| `InvokeFunction` `(Func(Of Object, Object), Object) : Object` | 在UI线程上执行一个有一个参数且具有返回值的委托 |
| `GetImage() : JpegBitmapEncoder`             | 获取主窗口的截图           |
| `SaveImage(String)`                          | 将主窗口的截图保存到文件中 |
| `AddElement(String, String, Double, Double, Double, Double, [String]) : FrameworkElement` | 添加一个控件到当前页面 |

###附注

- 几乎所有的基础渲染API都是在UI线程中执行的。

##06. LUA支持

框架的LUA支持来源于项目[NeoLua](https://github.com/neolithos/neolua)，因此用法可直接查看NeoLua的文档。框架本身再次基础上进行了些微扩展，以使其更好地服务于游戏环境。

###公开API

这些API均位于 `WADV.Core.API.ScriptAPI`名称空间下。详情请参阅[代码](https://github.com/WinUP/WADV/blob/master/WADV.Core/API/ScriptAPI.vb)。

| 函数 | 功能 |
| ----- | ----- |
| `ShowSync(String, String)`                                 | 显示提示信息                       |
| `RunFileAsync(String)`                                     | 执行脚本文件中的所有代码           |
| `RunFileSync(String)`                                      | 执行脚本文件中的所有代码           |
| `RunFileWithAnswer(String)`                                | 执行脚本文件中的所有代码并返回结果 |
| `RunStringAsync(String)`                                   | 执行一段字符串脚本                 |
| `RunStringSync(String)`                                    | 执行一段字符串脚本                 |
| `SetSync(String, Object)`                                  | 设置脚本全局变量的值               |
| `GetSync(String) : Object`                                 | 获取脚本全局变量的值               |
| `RegisterInTableSync(String, String, Delegate, [Boolean])` | 注册函数到脚本主机的指定表中       |
| `GetVm() : Lua`                                            | 获取游戏脚本主机对象               |
| `GetEnv() : LuaGlobal`                                     | 获取游戏脚本执行环境对象           |

###附注

- 因为NeoLua是基于DLR执行的，因此Lua的效率可能没有原生C语言版的Lua效率高。

##07. 引擎初始化与关闭

框架提供了几个简易的API用于执行常规的框架初始化和关闭，它们是：

| 函数 | 功能 |
| ----- | ----- |
| `StartGame(NavigationWindow, Integer, Integer)` | 启动游戏系统     |
| `StopGame(CancelEventArgs)`                     | 停止游戏系统     |
| `RegisterScript()`                              | 注册系统脚本接口 |


这些API均位于 `WADV.Core.API.GameAPI`名称空间下。详情请参阅[代码](https://github.com/WinUP/WADV/blob/master/WADV.Core/API/GameAPI.vb)。其中第三个函数，即“注册系统脚本接口”会将框架API注册到默认的Lua虚拟机中，它们的映射规则请参考该函数的代码实现。

##08. 模块支持

框架是基于模块工作的，这也就是为什么框架本身并没有提供足够的游戏功能的原因——因为具体的功能可以被模块实现，只要这个模块被加载，那么框架就有了对应的功能。所以，开发合适的模块是实现游戏功能的前提。

基本上来说，模块是基于实现框架接口来实现功能的。除了实现接口外，它们也可以通过像框架本体一样提供开放API或者注册函数给Lua等形式来提供功能，它们具体如何运作完全取决于模块开发者个人的想法。模块开发是相当灵活的。

###公开API

这些API均位于 `WADV.Core.API.PluginAPI`名称空间下。详情请参阅[代码](https://github.com/WinUP/WADV/blob/master/WADV.Core/API/PluginAPI.vb)。

| 函数 | 功能 |
| ----- | ----- |
| `Add(String)`                          | 加载一个插件       |
| `Compile(String, [String]) : Assembly` | 编译一个代码文件   |
| `Load(String) : Assembly`              | 加载一个动态链接库 |

###接口

模块需要框架功能支持时，除了调用相应的API外，还可以通过实现某些接口来获得功能支持。除了上面提到过的 `WADV.Core.PluginInterface.ILoopReceiver` 和 `WADV.Core.PluginInterface.IMessageReceiver` 外，还有这几个接口是可以使用的：

| 接口 | 说明 |
| ----- | ----- |
| `WADV.Core.PluginInterface.IPluginInitialise`        | 模块加载时执行       |
| `WADV.Core.PluginInterface.INavigationReceiver`      | 主窗口页面切换前执行 |
| `WADV.Core.PluginInterface.IGameInitialiserReceiver` | 框架加载完成时执行   |
| `WADV.Core.PluginInterface.IGameDestructorReceiver`  | 框架将要结束时执行   |

这些接口的具体实现可参考代码实现。

##09. 其他

- 所有的枚举值都在文件 `Enumerations.vb` 中。
- 关于每个API的使用说明正在撰写中。
- 因为版本库里存在多个可正常使用的模块，因此将不会编写示例模块。
- 我想要支持！
