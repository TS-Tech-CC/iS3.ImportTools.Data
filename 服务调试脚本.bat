::@echo off

D:

cd D:\github\release\iS3.ImportTools.Data\DataEntryService\bin\Debug

installutil DataEntryService.exe


net start DataEntry



echo 准备卸载
pause



net stop DataEntry

installutil /u DataEntryService.exe


echo 准备退出
pause