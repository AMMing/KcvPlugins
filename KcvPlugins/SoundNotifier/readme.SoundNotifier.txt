﻿author/developer :AMing
website:http://kcvp.logs.moe/
Github:https://github.com/AMMing
SourceCode:https://github.com/AMMing/KcvPlugins




##提督很忙的拓展插件(KanColleViewer Plugins)
==========

KanColleViewer 在3.0版本之后增加了插件功能，可以通过添加插件完善kcv的功能。

本插件在只kcv3.4下测试，其他旧版本理论上可以，不过没有测试，使用时最好是用最新的kcv。


### 主要功能

* 有通知的时候发出声音提示

### 使用方法

	将解压出来的dll放到kcv的Plugins目录下然后重启kcv，如果失败的话右键dll属性看看有个没有个什么什么锁定的 把那个解锁掉再重启kcv

	更改声音请修改 Plugins/sounds 里面的音频文件，支持 wav和mp3格式，但是必须按照固定的文件名 notify.wav 或notify.mp3
	
### 注意事项

	“AMing.Plugins.Core.dll”为核心组件不可删除

### 参考项目

* [KanColleViewer](https://github.com/Grabacr07/KanColleViewer)


#### 许可证

* MIT License





更新日志

1.2 -- 2015.02.03

	调用core模块实现接收其他插件发出的通知，添加警告音效“warning”跟“notify”一样提督们可以自行修改，目前在大破插件“AMing.Warning.dll”舰娘大破的时候会发出警报的音效


1.1 -- 2015.01.19

	修复不能静音的bug？（我几台电脑都没问题，不知道具体有没有解决）

	PS：_ (:3」∠)_好像是MediaPlayer太早实例化导致kcv将他识别成游戏声音控制了 导致游戏静音不了？

1.0 -- 2015.01.18

	有通知的时候发出声音提示在win7下可以和另一个通知插件组合成win8的推送效果