md update
echo copy new file
Xcopy .\build\Release .\update /E/H/C/I
echo delete ignore file
del .\update\Update.pdb
del .\update\Update.dll
del .\update\Update.exe
del *.zip
echo complete successfully
7z a update.zip .\update\**
rmdir /s/q update