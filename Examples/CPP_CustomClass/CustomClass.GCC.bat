
mkdir obj
mkdir obj\gcc

gcc -Wall -Wno-conversion-null -fPIC -DDEBUG -I..\Engine -c Program.cpp -o obj\gcc\obj.obj

gcc -o obj\gcc\a.exe obj\gcc\obj.obj ..\engine\engine.lib

obj\gcc\a

pause
