#WinUP Adventure Game Engine

Light weight 2D game engine on windows, and optimized for adventure games.
Windows上的轻量级2D游戏框架，为开发文字冒险游戏而优化。
This message driven engine has an abstraction layer to change its render engine anytime, but render engine is still being developed.
这是一个消息驱动的游戏引擎，且具有为任何时间更换渲染引擎而提供的独立抽象层，但关键的渲染引擎还在开发中。
Functions of this engine is not completed yet, it still has many commits in the future.
引擎功能仍未开发完毕，未来仍会有大量新代码加入。

##功能 / Functions

* 可限制帧率的游戏主循环 / Game loop with max fps limitation.
* 轻量的消息循环 / Message loop
* 计时器 / Timer
* 可随时替换的渲染引擎（基于渲染抽象层） / Render engine which can be changed anytime(based on Rendering Abstraction Layer)
* 插件化结构 / Plugins
* 基于脚本的游戏逻辑 / Game logic in scripts
* 资源管理API / Resource management API

##它已经有了哪些模块？ / Modules which aleardy have

* 成就模块 / Achievement
* 用于文字冒险游戏的CG显示模块 / CG module for AVG
* 用于文字冒险游戏的选择支模块 / Choice module for AVG
* 音视频模块 / Media module
* 用于文字冒险游戏的对话显示模块 / Text module for AVG
* 基本CG特效 / Shader effects(required WPF)
* 基本选择支特效 / Choice effects for choice module
* 游戏页面示例 / Sample game scenes
* 成就示例 / Sample achievements
* 基本对话文本特效 / Text effects for text module