author/developer :AMing
Github:https://github.com/AMMing
SourceCode:https://github.com/AMMing/KcvPlugins




##提督很忙的拓展插件(KanColleViewer Plugins)
==========

KanColleViewer 在3.0版本之后增加了插件功能，可以通过添加插件完善kcv的功能。

本插件在只kcv3.4下测试，其他旧版本理论上可以，不过没有测试，使用时最好是用最新的kcv。


###主要功能

* 添加到系统托盘
* 最小化时隐藏任务栏上的kcv
* 退出kcv时提示
* 热键（老板键）
* 更换kcv窗体主题
* 设置kcv的工具面板位置
* 简单的舰队信息



### 参考项目

* [KanColleViewer](https://github.com/Grabacr07/KanColleViewer)

### SVG Icons 素材

* [http://raphaeljs.com/icons](http://raphaeljs.com/icons)
* [http://thenounproject.com/](http://thenounproject.com)


#### 许可证

* MIT License





更新日志

1.7 ---2015.01.12

	###一般用户看的内容

		在MainWindow下的快捷键Ctrl+Tab 切换Tab模式的game和tool面板，Ctrl+M显示隐藏迷你舰队信息
		加大了迷你舰队信息的内容大小
		在托盘添加了显示迷你舰队信息的菜单

	###程序猿看的内容（备忘），看不懂就无视了 -，-

		修改了SimpleFleetWindow的数据源抛弃了自己重写的ViewModel 统一使用kcv 的MainViewModel 使用转换器获取一号舰队信息
		添加了Messager的模块（-，- 之前只用过mvmmlight的Messager挺简单易用的,kcv用的mvvm架构livet的Messager虽然规范得很好但是太难用了，原本是打算修改livet的最后失败了，于是自己模拟了个）
		添加了PubicModules 打算做完让各个模块之间能够动态添加相关的模块，例如托盘注册初始化PubicModules之后其他地方比如SimpleFleet添加了显示隐藏功能到PubicModules，托盘自己就添加功能到菜单上了，之后键盘事件 热键都可以用到
		SimpleFleetWindow继承的kcv那个MetroWindow 不知道哪里对宽高一直改动 导致没办法有效地控制窗体大小


1.6~1.0

	忘记记录了就无视吧