# Json Schema
## Spacer
czy spacer to powinny bec kroki czy metry
- metry: prawdopodnbie lepsze przetwarzanie w backend
- kroki: 
    - mozliwe ze latwiej bedzie je oddczytac z sensorow
    - mniejsza dokladnosc w backend bo trzeba znac personalia usera zeby to przeliczyc dobrze.  To moze nie byc najwygdoniejsze i brzmi jak zadanie apki mobilnej.

## winda i schody
czy kombinowac z pietrami czy po prostu metry
- wydaje sie ze tylko backned bedzie w stanie stwierdzic wysokosc pieter

## skret
Jelsi zalozmy ze skret trwa 0 czasu to czesto bezei go trzeba polaczyc z akcja stop

## DB
#  atrybuty sensorow 
Jak przechowwywac atrybuty sensorow
    No schema po prosru json?
    Wi FI (adres mac i sygnal) i barometr po prostu 1d
# Sensor reading
Czy splaszczac sensor 3D w ten sposb ze z sanych 3d -> 3 snesory 1d
Czy zrobic record z 6 polami gddzie 5 jest nullowalne

# Timestamp / offset
Czy lepiej przechowwyac dane jako datetime czy jako offset od poczatku nagrania

a moze te sensory wyslaja to w jasi juz sensowny sposob?

# Recording Note
jak wyglada sprawa z danymi tekstowymi w db
czy jest sens robic oddzielna tabele po to zeby querowanie jakos fajniej dzialalo czy to czy jest dana tekstowa nei ma znaczenia



