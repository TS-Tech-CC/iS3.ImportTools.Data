::@echo off

D:

cd D:\github\release\iS3.ImportTools.Data\DataEntryService\bin\Debug

installutil DataEntryService.exe


net start DataEntry



echo ׼��ж��
pause



net stop DataEntry

installutil /u DataEntryService.exe


echo ׼���˳�
pause