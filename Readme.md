# Beschrijving project

## Button model
Hierin staat de klasse 'Button'.

**properties :**

- name = de naam van de knop
- type = soort knop bv. volume, channel, nummers 
- pressTime = de tijd waarop de knop aangeklikt is.

**methodes :**

Press → die de tijd gaat zetten in de property pressTime

## Remote model
### klasse 'NumberHistoryChecker'. 

Deze klasse gaat controleren of binnen n-seconden het aantal 
ingeduwde knoppen nog aantal gelijk is aan 
een verwachte hoeveelheid.
Als aan de verwachte hoeveelheid voldaan is, wil dit zeggen dat
de gebruiker geen extra knoppen ingeduwd heeft binnen de periode
van n-seconden.
Hieruit kunnen we afleiden dat de gebruiker klaar is met de ingave.

**properties :**

- TvSelected = de tv waarvoor het kanaal geselecteerd zal worden
- NumberHistory = de historiek van alle buttons die ingegeven zijn
- ExpectedCount = de hoeveel numberbuttons die we verwachten binnen n-seconden.
- WaitTime = de n-seconden
- ChannelNumber = het nummer waar naar genavigeerd moet worden


**methodes :**

ThreadProc → start de thread. Kijkt of er binnen de aangegeven waittime nog
extra knoppen ingegeven zijn. Bv als je minder dan 3 seconden wacht om
een extra numberbutton in te geven. Dan wordt deze toegevoegd aan de history.
wacht men wel langer dan 3 seconden dan wordt er naar het kanaal in de history
genavigeerd.

### klasse 'Remote'

Deze klasse groepeert alle buttons samen onder 1 afstandsbediening.
Via deze klasse kunnen we dan ook de buttons induwen.


**fields :**

buttons = dictionary die de link legt tussen de naam van de button en
de button objecten.

**properties :**

- TvSelected = de tv waaraan de remote gekoppeld is
- NumberHistory = lijst met alle reeds ingevoerde knoppen
- AllThreads = lijst met de verschillende lopende thread die controleren
of er extra knoppen ingevoerd worden.
- WaitTime = de n-seconden waarbinnen men kijkt of er extra knoppen ingegeven zijn
voor de threads

**methods :**

RunningThreadCount → Gaat na hoeveel thread er lopende zijn

PressButton → zorgt ervoor dat de juiste actie uitgevoerd wordt bij elke knop dat je induwt

## Tv Model

**fields :**

- powerd = tv staat uit om te beginnen
- currentVolume = houdt de volume bij
- currentChannel = houdt het kanaal bij
- minVolume = laagste volumeniveau
- maxVolume = hoogst volumeniveau
- minChannel = laagste kanaal
- maxChannel = hoogste kanaal
- messages = lijst van boodschappen die naar het scherm gestuurd worden

**properties :**

- messages = haalt alle openstaande boodschappen op of zet een nieuwe boodschap in de lijst
- powerd = zet de tv aan of uit. Als de tv aan gaat worden de default volume gezet
- currentVolume = zet of haalt het huidig volume op
- currentChannel = zet of haalt het huidig kanaal op

**methodes :**

- SwitchPower = zet de tv aan of uit
- volumeUp = verhoogt het volume met +1
- volumeDown = verlaagt het volume met -1
- ChannelUp = verhoogt het kanaal met +1
- ChannelDown = verlaagt het kanaal met -1
- GoToChannel = gaat naar het ingegeven kanaal
 

# Run Program

hier kun je combinaties van knoppen simuleren om bepaalde acties uit te voeren.
thread.sleep is een simulatie van de tijd tussen 2 knoppen.
In het echt duurt het ook een aantal msec vooraleer je op een volgende knop duwt.

