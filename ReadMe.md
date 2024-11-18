# 《PCK Viewer》软件源代码   

欢迎学习《PCK Viewer》软件源代码！本源码遵循Creative Commons Attribution - NonCommercial许可协议，你可以在保留源码署名并保证不用于商业用途的前提下自由的使用、分发、修改本源码，但坚决不允许用于商业用途！   

### 基本信息  
源码名称：PCK Viewer   
源码版本：1.1.0  
源码作者：52pojie.cn  
遵循协议：Creative Commons Attribution - NonCommercial    
源码语言：C#  
.NET Framework框架版本：4.5  


### 基本介绍
本工具用于演示PCK格式的游戏资源文件的解压缩功能，支持部分格式为PCK国产老游戏的资源文件，同时还引入了第三方类库FreeImage和NAudio，实现图像和声音浏览功能。本软件演示了PCK文件算法的文件偏移、大小、文件目录信息的存储方式和数据区的内容存储规则，对研究资源逆向基础提供了重要参考资料。  

### 更新日志  
--------------------------------------------------  
2024.11.18 ——V 1.1.0  
1、项目更名为《PCK Viewer》  。
2、更换了Zlib解压库，改用zlib.dll提供的API来执行解压，同时移除了DotNetZip类库。  
3、优化了PCK文件目录区解析流程。  
4、新增文件浏览功能，支持浏览文本、图像和音频文件。  
  
--------------------------------------------------  
2024.10.30 ——V 1.0.0  
1、PCK解压算法讨论帖发布。  
2、初代图形界面开发完成。  
  
--------------------------------------------------  
2024.10.27 ——V 0.5.0  
1、PCK解压算法研究完成，关键代码完成编写，进入立项阶段。  

### 如何编译  
本软件使用了Tuple多元List，因此依赖于.NET Framework V4.5运行，原则上Visual Studio 2015就可以编译，但是本人是在Visual Studio 2022中编译的，因此建议在Visual Studio 2022中编译。  

### 意见或建议
可通过论坛回帖留言的方式反馈，也可私信该帖楼主也就是我来反馈。禁止留QQ、微信等联系方式，对利用私信留联系方式的行为将从重处罚！  
 

This project is licensed under Creative Commons Attribution - NonCommercial (CC BY - NC). You are free to use the source code for non - commercial purposes only.
