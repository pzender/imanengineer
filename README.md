#Założenia dot. użytkownika#
* ogólne podejście typu "mam wolne 2 godziny, co mogę teraz obejrzeć?"
* kilka wybranych programów dla których chciałby znaleźć czas, choć też w granicach rozsądku - środek nocy czy praca nie mają sensu

#Funkcjonalność#
* rejestracja użytkowników
* bez logowania:
	* przegląd programu TV na dzisiaj
	* filtrowanie kanałów
	* wyszukiwanie programów
* zalogowany:
	* zapamiętywanie ulubionych kanałów
	* konfigurowalne powiadomienia
		* poziom (tylko ręcznie włączane, cykliczne, polecane)
		* czas (nie ma sensu powiadamianie użytkownika o programie lecącym o 10 rano, kiedy prawdopodobnie jest w pracy)
	* **rekomendacje** - content based w oparciu o opisy, wspierany collaborative? dane o preferencjach na podstawie zapisanych ulubionych i wyszukiwań
		* tf-idf do analizy tekstu
* od strony serwera - cykliczne uruchamianie grabbera do EPG

#Podobne rozwiązania#
* [https://www.filmweb.pl/]() - program TV z rekomendacjami dla filmów. 
* [https://tastedive.com/shows]() - ogólne rekomendacje dotyczące telewizji, muzyki, książek. nie ma programu

#Źródła danych#
* [http://www.webgrabplus.com/download]("WebGrab+Plus") - zbiera program z 10 różnych stron (dla Polski); wynikiem jest plik xml zawierający listę dostępnych kanałów i nadawanych przez nie programów
	* Przykładowy zapis kanału:
```xml
	<channel id="Polsat">
		<display-name lang="pl">Polsat</display-name>
    	<icon src="https://1.fwcdn.pl/channels/3.1.png" />
    	<url>http://www.filmweb.pl</url>
	</channel>
```
	* Przykładowy zapis programu:
```xml
  <programme start="20181016220500 +0200" stop="20181017003500 +0200" channel="Polsat">
    <title lang="pl">Liga niezwykłych dżentelmenów</title>
    <title lang="en">The League of Extraordinary Gentlemen</title>
    <desc lang="pl">Alan Moore jest uważany za najlepszego scenarzystę komiksowego na świecie, a jego "Liga niezwykłych dżentelmenów" cieszy się opinią jednej z najciekawszych i najoryginalniejszych serii ostatnich lat. Nic, więc dziwnego, że w dobie panującej obecnie ... czytaj dalej. Ligę Niezwykłych Dżentelmenów tworzą bohaterowie, jakich nikt dotąd nie widział, a na jej czele stoi największy łowca i awanturnik na świecie, Allan Quatermain - odkrywca legendarnych kopalni króla Salomona. Towarzyszą mu: kapitan Nemo, wampirzyca Mina Harker, niewidzialny człowiek Rodney Skinner, amerykański agent do zadań specjalnych Tom Sawyer, nieśmiertelny Dorian Gray i nieobliczalny dr Jekyll/pan Hyde. Brytyjski wywiad, który powołał do życia Ligę, reprezentuje enigmatyczny M. Członkowie Ligi nie uznają żadnych autorytetów, są indywidualistami, których los zmusza do współdziałania. Muszą przełamać dzielące ich różnice i nauczyć się ufać sobie, bo w nich cała nadzieja świata: zamaskowany szaleniec, który każe się nazywać "Fantomem" grozi zatopieniem Wenecji, gdzie odbywa się konferencja przywódców największych mocarstw. Członkowie Ligi wyruszają do Włoch na pokładzie Nautilusa, okrętu podwodnego kapitana Nemo. Mają dziewięć godzin, by ocalić świat... ... więcejitemprop="description"&gt;(n)</desc>
    <credits>
      <director>Stephen Norrington</director>
      <actor>Sean Connery</actor>
      <actor>Naseeruddin Shah</actor>
      <actor>Peta Wilson</actor>
      <actor>Tony Curran</actor>
    </credits>
    <date>2003</date>
    <category lang="pl">film przygodowy</category>
    <category lang="pl">Fantasy</category>
    <category lang="pl">Przygodowy</category>
    <icon src="https://1.fwcdn.pl/po/42/04/34204/7152507.5.jpg" />
    <country>Czechy</country>
    <rating>
      <value>6,6</value>
    </rating>
  </programme>
```
```xml
  <programme start="20181016190000 +0200" stop="20181016193500 +0200" channel="TVN">
    <title lang="pl">Fakty</title>
    <category lang="pl">program informacyjny</category>
  </programme>
```
```xml
  <programme start="20181016205500 +0200" stop="20181016213000 +0200" channel="TVN">
    <title lang="pl">Milionerzy</title>
    <desc lang="pl">Kultowy teleturniej powraca na antenę TVN! Na fotelu prowadzącego niezastąpiony Hubert Urbański. Ilu milionerów zobaczymy w nowym sezonie, przekonamy się już na wiosnę! Ten najpopularniejszy teleturniej na świecie trzyma w napięciu jak niejeden thriller i budzi w widzach ogromne emocje. Nie dziwi zatem fakt, że od momentu premiery teleturnieju "Who Wants to Be a Millionaire?" w Wielkiej Brytanii lokalne wersje ukazały się w 120 krajach, a jego ponadczasowa formuła urzekła widzów od Stanów Zjednoczonych po Wybrzeże Kości Słoniowej. Na fotelu gracza we wszystkich światowych edycjach usiadło około miliona osób. W odcinkach specjalnych programu uczestnikami są gwiazdy show-biznesu. W polskim wydaniu teleturnieju gościnnie wystąpiła m.in. Ewa Drzyzga, Kinga Rusin oraz Marcin Prokop. Program stał się inspiracją dla oskarowego filmu "Slumdog. Milioner z ulicy", a legendarny zwrot "czy to twoja ostateczna odpowiedź?" wszedł do języka potocznego na stałe. "Milionerzy" to jednak nie tylko fantastyczna, a wręcz kultowa rozrywka dla całej rodziny. Jest to również program, który odmienił życie wielu zwykłych ludzi. Na całym świecie jest ponad 150 zwycięzców głównej nagrody. Jeden wygrany milion okazał się dla nich milionem możliwości i pozwolił na spełnienie, czasem bardzo skromnych, marzeń. Na Islandii pierwszym "milionerem" został młody ksiądz, który przeznaczył wygraną na renowację lokalnego kościoła. W Kolumbii natomiast, w edycji specjalnej z udziałem dzieci, dziesięcioletni chłopiec wygrał autobus dla swojej szkoły. Pierwsza główna wygrana w indyjskiej edycji powędrowała do młodego pracownika IT, którego miesięczna pensja nie przekraczała 150 dolarów. Lokalna adaptacja "Milionerów" biła rekordy popularności również w Polsce. Wyemitowano aż 688 odcinków, z których każdy gromadził niemal 3 miliony widzów. Pytanie o milion zostało zadane w całej historii polskiej wersji teleturnieju dwunastokrotnie, ale główna wygrana padła tylko raz - 28 marca 2010 roku. Wtedy poprawnej odpowiedzi na pytanie za największą stawkę udzielił Krzysztof Wójcik. Na wiosnę "Milionerzy" powrócą w klasycznej i lubianej przez widzów formule. Prawidłowa odpowiedź na pytanie drugie i siódme zapewnia uczestnikowi kwotę gwarantowaną (odpowiednio wysokości 1000 zł i 40 000 zł). Aby wygrać główną nagrodę należy odpowiedzieć prawidłowo na 12 pytań. Pytania mają różnorodną tematykę. Każde z pytań ma oznaczony stopień trudności, a także kategorię tematyczną i opracowane jest na podstawie co najmniej dwóch prawidłowych źródeł. W drodze do zwycięstwa uczestnik będzie mógł skorzystać z trzech kół ratunkowych: "pytanie do przyjaciela", "pół na pół" i "pytanie do publiczności". Na fotelu prowadzącego ponownie zasiądzie Hubert Urbański, aktor i prezenter, postać związania z polską edycją od pierwszego odcinka.  Charyzmatyczny i przenikliwy prowadzący zelektryzuje całą Polskę słynnym pytaniem: "Czy to jest twoja ostateczna odpowiedź?" ... więcejitemprop="description"&gt;(n)</desc>
    <category lang="pl">teleturniej</category>
    <category lang="pl">Teleturniej</category>
    <icon src="https://1.fwcdn.pl/po/07/90/770790/7749701.5.jpg" />
    <country>Polska</country>
    <rating>
      <value>6,5</value>
    </rating>
  </programme>
```

* grabber z Filmweb udostępnia 396 kanałów, z Interii 404
* opisy z kilku? pod content based recommendations


#Narzędzia