# WandererProject
# Wanderer - Az RPG játék
 
Készíts egy hős alapú, mezőkön mozgós, szörnyeket gyilkolós játékot.
Hősünket billentyűzet segítségével irányíthatjuk egy útvesztőben. A hősnek és
a szörnyeknek szintjük (level) és értékeik (stats) vannak amelyek a szinttől
függnek. A cél, hogy elérd a legmagasabb szintet a szörnyek levágásával és
a náluk rejtőző kulcs megtalálásával ami a következő szintre vezet.
 
## Miért?
 
A project fő célja hogy gyakorold az objektum orientált gondolkodást. Ennek a
legjobb módja, hogy : egy nagyobb applikációt készítesz és elgondolkodsz annak
felépítéséről. Készítsetek saját architektúrát. Nem elvárt,
hogy egyből előálljatok egy jól megtervezett felépítéssel, a cél inkább az, hogy
gondolkodjatok el rajta. Persze végeredményben egy kész, működő játékot várunk el.
 
 
## Workshop: Tervezd meg a munkád
 
### Projekt leírás (minimum követelmények)
 
![hero map](img/hero-map.png)
 
#### A játék képernyő
 
- A képernyő tartalmazza az első pályát, ami egy 10 x 10-es mező, amin a
  hős (és a szörnyek) mozoghatnak.
  - Minden pálya 10 x 10 mezőt tartalmaz.
- Vannak olyan mezők amikre semmilyen karakter nem mehet rá (hős vagy szörny).
- Minden pálya 3-6 közötti szörnyet tartalmaz.
- A szörnyek szintje a pályák száma alapján növekszik.
  - Ha ez a X-edik pálya, akkor a szörnyek szintje X (50% esély), X+1 (40%)
    vagy X+2 (10%)
- A szörnyek egyike a boss.
- A szörnyek egyikénél (nem a boss) van a kulcs, ha a hős megöli ezt a
  szörnyet és a bosst, akkor a következő pályára kerül.
- A játék képernyőn legyen egy szöveges rész ahol információk találhatóak
  a karakterekről.
  - Mutatja a hős összes értékét (stats)
  - Ha a hős egy szörnnyel egy mezőre kerül, írja ki a szörny összes értékét
    (stats) is.
 
#### Mozgás
 
- A hős a képernyőn mezőről mezőre tud haladni négy irányba a billentyűzet
  nyilaival (vagy a "WASD"-al).
- Minden két lépést követően a szörnyek is lépnek egy mezőt.
 
#### A karakterek
 
- Minden karakternek van (max és aktuális) életpontja (HP), védelme (DP) és
  sebzés pontja (SP).
- Az érték változhasson a játék során.
- Ha egy karakter életpontja 0 vagy kevesebb, akkor meghal.
  - Eltűnik a pályáról.
  - Ha ez a hős akkor vége a játéknak. :(
 
#### Kezdő értékek
 
(d6 egy véletlen szám 1 és 6 között, mint egy 6 oldalú dobókocka)
 
- Hero(hős):
  - HP: 20 + 3 \* d6
  - DP: 2 \* d6
  - SP: 5 + d6
- Monster (szörny) Szint (level) X:
  - HP: 2 \* X \* d6
  - DP: X/2 \* d6
  - SP: X \* d6
- Boss(boss) Szint (level) X:
  - HP: 2 \* X \* d6 + d6
  - DP: X/2 \* d6 + d6 / 2
  - SP: X \* d6 + X
 
#### Harc
 
- Ha a hős egy olyan mezőre lép amelyen egy szörny van, harc alakul ki.
- Az a karakter amely később érkezik a területre az a támadó fél.
- Ha a játékos megnyomja a `space`-t, a hős megtámadja a védekező felet,
  ezt követően az visszatámad.
- A támadó a védekező félre támad, majd a védekező a támadóra. Ez egészen
  addig folytatódik míg az egyik fél meg nem hal.
- Ha a csata során a hős a győztes karakter, akkor szintet lép.
 
#### Támadás
 
- Támadáskor a támadó értéket (SV) az SP és a d6 kétszeresének összege adja.
- A támadás akkor sikeres a a 2 \* d6 + SP nagyobb mint a másik karakter DP-je.
- Sikeres támadáskor a másik karakter HP-ja csökken SV - a másik karakter DP-je.
 
#### Szintlépés
 
- Egy sikeresen megnyert harcot követően a karakter szintetlép.
- A max HP-ja megnő d6-al.
- A DP-je megnő d6-al.
- Az SP-je megnő d6-al.
 
#### Következő pályára lépés
 
- Ha a hős megöli a kulcsot hordozó szörnyet és a bosst, azonnal a következő
  pályára kerül.
  - Ami olyan mint az előző csak új és nagyobb szintű szörnyekkel.
- Amikor a hős az új pályára kerül, gyógyul:
  - 10% esély az összes HP visszatöltésére.
  - 40% esély a HP harmadának a visszatöltésére.
  - 50% esély a HP 10%-nak a visszatöltésére.
- Szörny (monster) szint (level) X:
  - HP: 2 \* X \* d6
  - DP: X / 2 \* d6
  - SP: X \* d6
 
### A FENTEBB EMLÍTETT PROJEKT LEÍRÁS IRÁNYMUTATÓ ###
Ez azt jelenti hogy noha a rendszer működésére vonatkozó leírásokat követnetek kell,
a játékot saját kedvetek szerint nehezíthetitek, bővíthetitek.