''' <summary>
''' 路径类型
''' </summary>
''' <remarks></remarks>
Public Enum PathType
    Game
    Plugin
    Resource
    Skin
    Script
    UserFile
End Enum

''' <summary>
''' 转场类型
''' </summary>
''' <remarks>NavigateOperation会在导航时传递给被导航的窗体，是否处理则由该窗体决定</remarks>
Public Enum NavigateOperation
    ''' <summary>
    ''' 在无特效的情况下完成导航
    ''' </summary>
    ''' <remarks></remarks>
    NoEffect
    ''' <summary>
    ''' 不进行导航，只使用退出效果
    ''' </summary>
    ''' <remarks></remarks>
    OnlyOut
    ''' <summary>
    ''' 不进行导航，只使用进入效果
    ''' </summary>
    ''' <remarks></remarks>
    OnlyIn
    ''' <summary>
    ''' 使用退出效果然后导航，并保持新页面透明度为0
    ''' </summary>
    ''' <remarks></remarks>
    OutAndNavigate
    ''' <summary>
    ''' 使用退出效果然后导航，并直接显示新页面
    ''' </summary>
    ''' <remarks></remarks>
    OutAndShow
    ''' <summary>
    ''' 直接导航，然后使用进入效果
    ''' </summary>
    ''' <remarks></remarks>
    NavigateAndIn
    ''' <summary>
    ''' 直接导航，并保持新页面透明度为0
    ''' </summary>
    ''' <remarks></remarks>
    NavigateAndHide
    ''' <summary>
    ''' 使用默认配置，即使用所有效果且进行导航
    ''' </summary>
    ''' <remarks></remarks>
    Normal
End Enum

''' <summary>
''' 元素类型
''' </summary>
Public Enum ElementType
    Border
    Button
    Canvas
    CheckBox
    ComboBox
    Ellipse
    Expander
    Grid
    GroupBox
    Image
    Label
    ListBox
    ListView
    ProgressBar
    RadioButton
    Rectangle
    RichTextBox
    ScrollBar
    Slider
    TabControl
    TextBlock
    TextBox
    ViewBox
    Viewport3D
End Enum

''' <summary>
''' 组件绑定结果
''' </summary>
''' <remarks></remarks>
Public Enum BindingResult
    ''' <summary>
    ''' 绑定成功
    ''' </summary>
    ''' <remarks></remarks>
    Success
    ''' <summary>
    ''' 绑定已存在，不需要再次绑定
    ''' </summary>
    ''' <remarks></remarks>
    NoNeed
    ''' <summary>
    ''' 绑定被否决
    ''' </summary>
    ''' <remarks></remarks>
    Cancel
End Enum

''' <summary>
''' 组件解绑结果
''' </summary>
''' <remarks></remarks>
Public Enum UnbindingResult
    ''' <summary>
    ''' 解绑失败
    ''' </summary>
    ''' <remarks></remarks>
    Success
    ''' <summary>
    ''' 找不到需要解绑的组件
    ''' </summary>
    ''' <remarks></remarks>
    CannotFind
    ''' <summary>
    ''' 解绑被否决
    ''' </summary>
    ''' <remarks></remarks>
    Cancel
End Enum

''' <summary>
''' 组件对于游戏系统的接收器类型
''' </summary>
''' <remarks></remarks>
Public Enum ComponentReceiverType
    ''' <summary>
    ''' 没有接收器
    ''' </summary>
    ''' <remarks></remarks>
    None
    ''' <summary>
    ''' 有游戏循环接收器
    ''' </summary>
    ''' <remarks></remarks>
    LoopOnly
    ''' <summary>
    ''' 有消息循环接收器
    ''' </summary>
    ''' <remarks></remarks>
    MessageOnly
    ''' <summary>
    ''' 两个循环都有接收器
    ''' </summary>
    ''' <remarks></remarks>
    Both
End Enum