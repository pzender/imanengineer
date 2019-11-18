using System;
using System.Collections.Generic;
using System.Text;
using TV_App.Models;

namespace TV_AppTests
{
    public class TestData
    {

        public static Feature[] SampleFeatures = new Feature[]
        {
            new Feature() { Id =  0, Value = "USA", Type = 1 },
            new Feature() { Id =  1, Value = "Japan", Type = 1 },
            new Feature() { Id =  2, Value = "2016", Type = 2 },
            new Feature() { Id =  3, Value = "2006", Type = 2 },
            new Feature() { Id =  4, Value = "Sophie Turner", Type = 4 },
            new Feature() { Id =  5, Value = "Jennifer Lawrence", Type = 4 },
            new Feature() { Id =  6, Value = "Michael Fassbender", Type = 4 },
            new Feature() { Id =  7, Value = "Bruce Willis", Type = 4 },
            new Feature() { Id =  8, Value = "Bryan Singer", Type = 5 },
            new Feature() { Id =  9, Value = "Quentin Tarantino", Type = 5 },
            new Feature() { Id = 10, Value = "Film", Type = 7 },
            new Feature() { Id = 11, Value = "Serial", Type = 7 },
            new Feature() { Id = 12, Value = "Sensacyjny", Type = 7 },
            new Feature() { Id = 13, Value = "Dokumentalny", Type = 7 },
        };


        public static string TestChannels = @"<?xml version = ""1.0"" encoding=""utf-8"" ?>
                <tv generator-info-name=""WebGrab+Plus/w MDB &amp; REX Postprocess -- version V2.1.9 -- Jan van Straaten"" generator-info-url=""http://www.webgrabplus.com"">
                    <channel id=""Stopklatka TV"">
                        <display-name lang=""pl"">Stopklatka TV</display-name>
                        <icon src = ""http://ocdn.eu/program-tv-transforms/1/Q7vktlEYWRtL2ExM2Y1NGNlNjQ5YmVjMjFhMTA5NzRlNGRmOTRhMTI1MDhlMzg4OWI1OGM0YzA3YjllYmVkYjk0NmY5ZTg3MjSSlQJkAMLDlQIAKMLD"" />
                        <url>http://www.programtv.onet.pl</url>
                    </channel>
                    <channel id= ""TTV"" >
                        <display-name lang=""pl""> TTV </display-name >
                        <icon src=""http://ocdn.eu/program-tv-transforms/1/F0zktlEYWRtLzZiYzNhOTZlOTdlNDBhOWE4M2I5YjMxZjA2M2UzYjY2MzEzZWI3ODk2MzRmMmVkNWJlOGY3ZDY2ZTM0ZDhlYTSSlQJkAMLDlQIAKMLD"" />
                        <url> http://www.programtv.onet.pl</url>
                    </channel>
                </tv>
            ";

        public static string TestProgrammesBasic = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
            <tv generator-info-name=""WebGrab+Plus/w MDB &amp; REX Postprocess -- version V2.1.9 -- Jan van Straaten"" generator-info-url=""http://www.webgrabplus.com"">
              <channel id=""POLSAT"">
                <display-name lang=""pl"">POLSAT</display-name>
                <icon src=""http://ocdn.eu/program-tv-transforms/1/i6TktkpTURBXy8zNDYzMmJmMGI2Y2JiMjM1NjgyNmZmNzE4YWQ1ZDdkNC5wbmeSlQJkAMLDlQIAKMLD"" />
                <url>http://www.programtv.onet.pl</url>
              </channel>
              <programme start=""20210802200500 +0200"" stop=""20210802231000 +0200"" channel=""POLSAT"">
                <title lang=""pl"">X-Men: Apocalypse</title>
                <desc lang=""pl"">
                  Film ""X-Men: Apocalypse"" jest kolejną odsłoną słynnej serii inspirowanej kultowymi komiksami. Twórcą obrazu jest Bryan Singer, reżyser ""Podejrzanych"", ""Walkirii"", ale także trzech wcześniejszych produkcji z serii ""X-Men"". Artysta zadbał o to, by widzów seans nie znudził, tak dużo jest tu dynamicznych, krwawych i oszałamiających efektami specjalnymi scen. Polskich kinomanów zainteresuje zaś z pewnością rodzimy akcent. Choć brzmi to jak żart, słynny Magneto zamieszkuje w Pruszkowie i stara się mówić po polsku.
                  Tytuł filmu ""X-Men: Apocalypse"" pochodzi od imienia pierwszego i zarazem najpotężniejszego mutanta wszech czasów, Apocalypse'a (Oscar Isaac), zwanego też En Sabah Nur. Dotąd był on uśpiony. Teraz jednak powraca do życia w Egipcie, gdzie przez wieki czczono go niczym boga. Apocalypse potrafi absorbować moce innych mutantów, dzięki czemu zyskał nieśmiertelność i stał się niepokonany.
                  Niestety, po przebudzeniu potężny mutant czuje się zawiedziony nową rzeczywistością. Postanawia więc wykorzystać swą moc do zniszczenia rodzaju ludzkiego. Kolejnym krokiem ma być przejęcie władzy nad Ziemią. Wkrótce Apocalypse werbuje trzech innych niezwykle silnych mutantów: Storm (Alexandra Shipp), Angela (Ben Hardy) i Psylock (Olivia Munn). Dociera również do Magneto (Michael Fassbender), który - rozczarowany i pozbawiony złudzeń - znalazł schronienie w Polsce, gdzie założył rodzinę. Gdy traci bliskich, dołącza do En Sabah Nura. Los Błękitnej Planety wydaje się przesądzony. Mimo to Charles Xavier (James McAvoy), Raven (Jennifer Lawrence) i młode pokolenie mutantów z akademii Profesora X postanawiają powstrzymać potężnego wroga.(n)
                </desc>
                <credits>
                  <actor>Jennifer Lawrence</actor>
                </credits>
                <date>2016</date>
                <category lang=""pl"">Film</category>
                <icon src=""https://ocdn.eu/program-tv/new-upload-images/images-84f3b3e1-2d88-4494-b1c0-14c08035f07d"" />
                <country lang=""pl"">USA</country>
                <rating>
                  <value>12</value>
                </rating>
                <star-rating>
                  <value>3</value>
                </star-rating>
              </programme>
            </tv>
        ";
        public static string TestProgrammesMissingEmission = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
            <tv generator-info-name=""WebGrab+Plus/w MDB &amp; REX Postprocess -- version V2.1.9 -- Jan van Straaten"" generator-info-url=""http://www.webgrabplus.com"">
              <channel id=""POLSAT"">
                <display-name lang=""pl"">POLSAT</display-name>
                <icon src=""http://ocdn.eu/program-tv-transforms/1/i6TktkpTURBXy8zNDYzMmJmMGI2Y2JiMjM1NjgyNmZmNzE4YWQ1ZDdkNC5wbmeSlQJkAMLDlQIAKMLD"" />
                <url>http://www.programtv.onet.pl</url>
              </channel>
              <programme >
                <title lang=""pl"">X-Men: Apocalypse</title>
                <desc lang=""pl"">
                  Film ""X-Men: Apocalypse"" jest kolejną odsłoną słynnej serii inspirowanej kultowymi komiksami. Twórcą obrazu jest Bryan Singer, reżyser ""Podejrzanych"", ""Walkirii"", ale także trzech wcześniejszych produkcji z serii ""X-Men"". Artysta zadbał o to, by widzów seans nie znudził, tak dużo jest tu dynamicznych, krwawych i oszałamiających efektami specjalnymi scen. Polskich kinomanów zainteresuje zaś z pewnością rodzimy akcent. Choć brzmi to jak żart, słynny Magneto zamieszkuje w Pruszkowie i stara się mówić po polsku.
                  Tytuł filmu ""X-Men: Apocalypse"" pochodzi od imienia pierwszego i zarazem najpotężniejszego mutanta wszech czasów, Apocalypse'a (Oscar Isaac), zwanego też En Sabah Nur. Dotąd był on uśpiony. Teraz jednak powraca do życia w Egipcie, gdzie przez wieki czczono go niczym boga. Apocalypse potrafi absorbować moce innych mutantów, dzięki czemu zyskał nieśmiertelność i stał się niepokonany.
                  Niestety, po przebudzeniu potężny mutant czuje się zawiedziony nową rzeczywistością. Postanawia więc wykorzystać swą moc do zniszczenia rodzaju ludzkiego. Kolejnym krokiem ma być przejęcie władzy nad Ziemią. Wkrótce Apocalypse werbuje trzech innych niezwykle silnych mutantów: Storm (Alexandra Shipp), Angela (Ben Hardy) i Psylock (Olivia Munn). Dociera również do Magneto (Michael Fassbender), który - rozczarowany i pozbawiony złudzeń - znalazł schronienie w Polsce, gdzie założył rodzinę. Gdy traci bliskich, dołącza do En Sabah Nura. Los Błękitnej Planety wydaje się przesądzony. Mimo to Charles Xavier (James McAvoy), Raven (Jennifer Lawrence) i młode pokolenie mutantów z akademii Profesora X postanawiają powstrzymać potężnego wroga.(n)
                </desc>
                <credits>
                  <director>Bryan Singer</director>
                  <actor>James McAvoy</actor>
                  <actor>Michael Fassbender</actor>
                  <actor>Jennifer Lawrence</actor>
                  <actor>Nicholas Hoult</actor>
                  <actor>Oscar Isaac</actor>
                  <actor>Rose Byrne</actor>
                  <actor>Evan Peters</actor>
                  <actor>Josh Helman</actor>
                  <actor>Sophie Turner</actor>
                  <actor>Tye Sheridan</actor>
                  <writer>Simon Kinberg</writer>
                  <writer>Bryan Singer</writer>
                </credits>
                <date>2016</date>
                <category lang=""pl"">Film</category>
                <category lang=""pl"">Sf</category>
                <icon src=""https://ocdn.eu/program-tv/new-upload-images/images-84f3b3e1-2d88-4494-b1c0-14c08035f07d"" />
                <country lang=""pl"">USA</country>
                <rating>
                  <value>12</value>
                </rating>
                <star-rating>
                  <value>3</value>
                </star-rating>
              </programme>
            </tv>
        ";
        public static string TestProgrammesSeparateEmission = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
            <tv generator-info-name=""WebGrab+Plus/w MDB &amp; REX Postprocess -- version V2.1.9 -- Jan van Straaten"" generator-info-url=""http://www.webgrabplus.com"">
                <channel id=""POLSAT"">
                    <display-name lang=""pl"">POLSAT</display-name>
                    <icon src=""http://ocdn.eu/program-tv-transforms/1/i6TktkpTURBXy8zNDYzMmJmMGI2Y2JiMjM1NjgyNmZmNzE4YWQ1ZDdkNC5wbmeSlQJkAMLDlQIAKMLD"" />
                    <url>http://www.programtv.onet.pl</url>
                </channel>
                <programme start=""20210802200500 +0200"" stop=""20210802231000 +0200"" channel=""POLSAT"">
                    <title lang=""pl"">X-Men: Apocalypse</title>
                    <desc lang=""pl"">
                        Film ""X-Men: Apocalypse"" jest kolejną odsłoną słynnej serii inspirowanej kultowymi komiksami. Twórcą obrazu jest Bryan Singer, reżyser ""Podejrzanych"", ""Walkirii"", ale także trzech wcześniejszych produkcji z serii ""X-Men"". Artysta zadbał o to, by widzów seans nie znudził, tak dużo jest tu dynamicznych, krwawych i oszałamiających efektami specjalnymi scen. Polskich kinomanów zainteresuje zaś z pewnością rodzimy akcent. Choć brzmi to jak żart, słynny Magneto zamieszkuje w Pruszkowie i stara się mówić po polsku.
                        Tytuł filmu ""X-Men: Apocalypse"" pochodzi od imienia pierwszego i zarazem najpotężniejszego mutanta wszech czasów, Apocalypse'a (Oscar Isaac), zwanego też En Sabah Nur. Dotąd był on uśpiony. Teraz jednak powraca do życia w Egipcie, gdzie przez wieki czczono go niczym boga. Apocalypse potrafi absorbować moce innych mutantów, dzięki czemu zyskał nieśmiertelność i stał się niepokonany.
                        Niestety, po przebudzeniu potężny mutant czuje się zawiedziony nową rzeczywistością. Postanawia więc wykorzystać swą moc do zniszczenia rodzaju ludzkiego. Kolejnym krokiem ma być przejęcie władzy nad Ziemią. Wkrótce Apocalypse werbuje trzech innych niezwykle silnych mutantów: Storm (Alexandra Shipp), Angela (Ben Hardy) i Psylock (Olivia Munn). Dociera również do Magneto (Michael Fassbender), który - rozczarowany i pozbawiony złudzeń - znalazł schronienie w Polsce, gdzie założył rodzinę. Gdy traci bliskich, dołącza do En Sabah Nura. Los Błękitnej Planety wydaje się przesądzony. Mimo to Charles Xavier (James McAvoy), Raven (Jennifer Lawrence) i młode pokolenie mutantów z akademii Profesora X postanawiają powstrzymać potężnego wroga.(n)
                    </desc>
                    <credits>
                        <director>Bryan Singer</director>
                        <actor>James McAvoy</actor>
                        <actor>Michael Fassbender</actor>
                        <actor>Jennifer Lawrence</actor>
                        <actor>Nicholas Hoult</actor>
                        <actor>Oscar Isaac</actor>
                        <actor>Rose Byrne</actor>
                        <actor>Evan Peters</actor>
                        <actor>Josh Helman</actor>
                        <actor>Sophie Turner</actor>
                        <actor>Tye Sheridan</actor>
                        <writer>Simon Kinberg</writer>
                        <writer>Bryan Singer</writer>
                    </credits>
                    <date>2016</date>
                    <category lang=""pl"">Film</category>
                    <category lang=""pl"">Sf</category>
                    <icon src=""https://ocdn.eu/program-tv/new-upload-images/images-84f3b3e1-2d88-4494-b1c0-14c08035f07d"" />
                    <country lang=""pl"">USA</country>
                    <rating>
                        <value>12</value>
                    </rating>
                    <star-rating>
                        <value>3</value>
                    </star-rating>
                </programme>
                <programme start=""20210803200500 +0200"" stop=""20210803231000 +0200"" channel=""POLSAT"">
                    <title lang=""pl"">X-Men: Apocalypse</title>
                    <desc lang=""pl"">
                        Film ""X-Men: Apocalypse"" jest kolejną odsłoną słynnej serii inspirowanej kultowymi komiksami. Twórcą obrazu jest Bryan Singer, reżyser ""Podejrzanych"", ""Walkirii"", ale także trzech wcześniejszych produkcji z serii ""X-Men"". Artysta zadbał o to, by widzów seans nie znudził, tak dużo jest tu dynamicznych, krwawych i oszałamiających efektami specjalnymi scen. Polskich kinomanów zainteresuje zaś z pewnością rodzimy akcent. Choć brzmi to jak żart, słynny Magneto zamieszkuje w Pruszkowie i stara się mówić po polsku.
                        Tytuł filmu ""X-Men: Apocalypse"" pochodzi od imienia pierwszego i zarazem najpotężniejszego mutanta wszech czasów, Apocalypse'a (Oscar Isaac), zwanego też En Sabah Nur. Dotąd był on uśpiony. Teraz jednak powraca do życia w Egipcie, gdzie przez wieki czczono go niczym boga. Apocalypse potrafi absorbować moce innych mutantów, dzięki czemu zyskał nieśmiertelność i stał się niepokonany.
                        Niestety, po przebudzeniu potężny mutant czuje się zawiedziony nową rzeczywistością. Postanawia więc wykorzystać swą moc do zniszczenia rodzaju ludzkiego. Kolejnym krokiem ma być przejęcie władzy nad Ziemią. Wkrótce Apocalypse werbuje trzech innych niezwykle silnych mutantów: Storm (Alexandra Shipp), Angela (Ben Hardy) i Psylock (Olivia Munn). Dociera również do Magneto (Michael Fassbender), który - rozczarowany i pozbawiony złudzeń - znalazł schronienie w Polsce, gdzie założył rodzinę. Gdy traci bliskich, dołącza do En Sabah Nura. Los Błękitnej Planety wydaje się przesądzony. Mimo to Charles Xavier (James McAvoy), Raven (Jennifer Lawrence) i młode pokolenie mutantów z akademii Profesora X postanawiają powstrzymać potężnego wroga.(n)
                    </desc>
                    <credits>
                        <director>Bryan Singer</director>
                        <actor>James McAvoy</actor>
                        <actor>Michael Fassbender</actor>
                        <actor>Jennifer Lawrence</actor>
                        <actor>Nicholas Hoult</actor>
                        <actor>Oscar Isaac</actor>
                        <actor>Rose Byrne</actor>
                        <actor>Evan Peters</actor>
                        <actor>Josh Helman</actor>
                        <actor>Sophie Turner</actor>
                        <actor>Tye Sheridan</actor>
                        <writer>Simon Kinberg</writer>
                        <writer>Bryan Singer</writer>
                    </credits>
                    <date>2016</date>
                    <category lang=""pl"">Film</category>
                    <category lang=""pl"">Sf</category>
                    <icon src=""https://ocdn.eu/program-tv/new-upload-images/images-84f3b3e1-2d88-4494-b1c0-14c08035f07d"" />
                    <country lang=""pl"">USA</country>
                    <rating>
                        <value>12</value>
                    </rating>
                    <star-rating>
                        <value>3</value>
                    </star-rating>
                </programme>
            </tv>
        ";
        public static string TestSeparateProgrammes = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
        <tv generator-info-name=""WebGrab+Plus/w MDB &amp; REX Postprocess -- version V2.1.9 -- Jan van Straaten"" generator-info-url=""http://www.webgrabplus.com"">
            <channel id=""POLSAT"">
                <display-name lang=""pl"">POLSAT</display-name>
                <icon src=""http://ocdn.eu/program-tv-transforms/1/i6TktkpTURBXy8zNDYzMmJmMGI2Y2JiMjM1NjgyNmZmNzE4YWQ1ZDdkNC5wbmeSlQJkAMLDlQIAKMLD"" />
                <url>http://www.programtv.onet.pl</url>
            </channel>
            <programme start=""20210802200500 +0200"" stop=""20210802231000 +0200"" channel=""POLSAT"">
                <title lang=""pl"">X-Men: Apocalypse</title>
                <desc lang=""pl"">
                    Film ""X-Men: Apocalypse"" jest kolejną odsłoną słynnej serii inspirowanej kultowymi komiksami. Twórcą obrazu jest Bryan Singer, reżyser ""Podejrzanych"", ""Walkirii"", ale także trzech wcześniejszych produkcji z serii ""X-Men"". Artysta zadbał o to, by widzów seans nie znudził, tak dużo jest tu dynamicznych, krwawych i oszałamiających efektami specjalnymi scen. Polskich kinomanów zainteresuje zaś z pewnością rodzimy akcent. Choć brzmi to jak żart, słynny Magneto zamieszkuje w Pruszkowie i stara się mówić po polsku.
                    Tytuł filmu ""X-Men: Apocalypse"" pochodzi od imienia pierwszego i zarazem najpotężniejszego mutanta wszech czasów, Apocalypse'a (Oscar Isaac), zwanego też En Sabah Nur. Dotąd był on uśpiony. Teraz jednak powraca do życia w Egipcie, gdzie przez wieki czczono go niczym boga. Apocalypse potrafi absorbować moce innych mutantów, dzięki czemu zyskał nieśmiertelność i stał się niepokonany.
                    Niestety, po przebudzeniu potężny mutant czuje się zawiedziony nową rzeczywistością. Postanawia więc wykorzystać swą moc do zniszczenia rodzaju ludzkiego. Kolejnym krokiem ma być przejęcie władzy nad Ziemią. Wkrótce Apocalypse werbuje trzech innych niezwykle silnych mutantów: Storm (Alexandra Shipp), Angela (Ben Hardy) i Psylock (Olivia Munn). Dociera również do Magneto (Michael Fassbender), który - rozczarowany i pozbawiony złudzeń - znalazł schronienie w Polsce, gdzie założył rodzinę. Gdy traci bliskich, dołącza do En Sabah Nura. Los Błękitnej Planety wydaje się przesądzony. Mimo to Charles Xavier (James McAvoy), Raven (Jennifer Lawrence) i młode pokolenie mutantów z akademii Profesora X postanawiają powstrzymać potężnego wroga.(n)
                </desc>
                <credits>
                    <actor>Jennifer Lawrence</actor>
                </credits>
                <date>2016</date>
                <category lang=""pl"">Film</category>
                <icon src=""https://ocdn.eu/program-tv/new-upload-images/images-84f3b3e1-2d88-4494-b1c0-14c08035f07d"" />
                <country lang=""pl"">USA</country>
                <rating>
                    <value>12</value>
                </rating>
                <star-rating>
                    <value>3</value>
                </star-rating>
            </programme>
            <programme start=""20210803200500 +0200"" stop=""20210803231000 +0200"" channel=""POLSAT"">
                <title lang=""pl"">Test</title>
                <desc lang=""pl"">
                    Test test Test test Test test Test test Test test Test test Test test Test test Test test Test test 
                </desc>
                <credits>
                    <actor>Test Actor</actor>
                </credits>
                <date>2016</date>
                <category lang=""pl"">Film</category>
                <icon src=""https://ocdn.eu/program-tv/new-upload-images/images-84f3b3e1-2d88-4494-b1c0-14c08035f07d"" />
                <country lang=""pl"">USA</country>
                <rating>
                    <value>12</value>
                </rating>
                <star-rating>
                    <value>3</value>
                </star-rating>
            </programme>
        </tv>
        ";
        public static string ProblemDelBoca = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<tv generator-info-name=""WebGrab+Plus/w MDB &amp; REX Postprocess -- version V2.1.9 -- Jan van Straaten"" generator-info-url=""http://www.webgrabplus.com"">
  <channel id=""Novela tv"">
    <display-name lang=""pl"">Novela tv</display-name>
    <icon src=""http://ocdn.eu/program-tv-transforms/1/zlpktkpTURBXy9hYTA3OWRmMWQ1MDFjYjNkODM2ZWUzZGNjNzQ2MmI3NC5wbmeSlQJkAMLDlQIAKMLD"" />
    <url>http://www.programtv.onet.pl</url>
  </channel>
  <programme start=""20210816045000 +0200"" stop=""20210816060000 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Wola życia</title>
    <title lang=""xx"">Voglia di vivere</title>
    <desc lang=""pl"">W dniu 17 urodzin Any jej ojciec, Anibal organizuje wielkie przyjęcie dla rodziny i przyjaciół. Wieczorem Ana i jej znajomi idą na dyskotekę. Dziewczyna poznaje tam Renzo, nieśmiałego chłopaka, którego pasją jest muzyka. Młodzi zakochują się w sobie. Ojciec Any, mężczyzna zaborczy i zazdrosny, jest przeciwny temu związkowi. Zabrania młodym spotykać się ze sobą. Ana popada w apatię, nie widzi sensu życia. Ze względu na zły stan zdrowia córki Anibal zgadza się na spotkania Any z Renzo. W przyszłości, kiedy stan zdrowia dziewczyny poprawi się, planuje znowu rozdzielić parę. Jednak nic nie powstrzyma miłości Any i Renzo. Młodzi postanawiają za wszelką cenę być razem i decydują się na ślub...(n)</desc>
    <credits>
      <director>Nicolas del Boca</director>
      <actor>Andrea del Boca</actor>
      <actor>Duilio Marzio</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 56</episode-num>
    <episode-num system=""onscreen"">E56</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210816080000 +0200"" stop=""20210816085500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 43</episode-num>
    <episode-num system=""onscreen"">E43</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210816134000 +0200"" stop=""20210816145500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 43</episode-num>
    <episode-num system=""onscreen"">E43</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210816200000 +0200"" stop=""20210816205500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 44</episode-num>
    <episode-num system=""onscreen"">E44</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210817020000 +0200"" stop=""20210817025500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 44</episode-num>
    <episode-num system=""onscreen"">E44</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210817045000 +0200"" stop=""20210817060000 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Wola życia</title>
    <title lang=""xx"">Voglia di vivere</title>
    <desc lang=""pl"">W dniu 17 urodzin Any jej ojciec, Anibal organizuje wielkie przyjęcie dla rodziny i przyjaciół. Wieczorem Ana i jej znajomi idą na dyskotekę. Dziewczyna poznaje tam Renzo, nieśmiałego chłopaka, którego pasją jest muzyka. Młodzi zakochują się w sobie. Ojciec Any, mężczyzna zaborczy i zazdrosny, jest przeciwny temu związkowi. Zabrania młodym spotykać się ze sobą. Ana popada w apatię, nie widzi sensu życia. Ze względu na zły stan zdrowia córki Anibal zgadza się na spotkania Any z Renzo. W przyszłości, kiedy stan zdrowia dziewczyny poprawi się, planuje znowu rozdzielić parę. Jednak nic nie powstrzyma miłości Any i Renzo. Młodzi postanawiają za wszelką cenę być razem i decydują się na ślub...(n)</desc>
    <credits>
      <director>Nicolas del Boca</director>
      <actor>Andrea del Boca</actor>
      <actor>Duilio Marzio</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 57</episode-num>
    <episode-num system=""onscreen"">E57</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210817080000 +0200"" stop=""20210817085500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 44</episode-num>
    <episode-num system=""onscreen"">E44</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210817200000 +0200"" stop=""20210817205500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 45</episode-num>
    <episode-num system=""onscreen"">E45</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210818020000 +0200"" stop=""20210818025500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 45</episode-num>
    <episode-num system=""onscreen"">E45</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210818045000 +0200"" stop=""20210818060000 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Wola życia</title>
    <title lang=""xx"">Voglia di vivere</title>
    <desc lang=""pl"">W dniu 17 urodzin Any jej ojciec, Anibal organizuje wielkie przyjęcie dla rodziny i przyjaciół. Wieczorem Ana i jej znajomi idą na dyskotekę. Dziewczyna poznaje tam Renzo, nieśmiałego chłopaka, którego pasją jest muzyka. Młodzi zakochują się w sobie. Ojciec Any, mężczyzna zaborczy i zazdrosny, jest przeciwny temu związkowi. Zabrania młodym spotykać się ze sobą. Ana popada w apatię, nie widzi sensu życia. Ze względu na zły stan zdrowia córki Anibal zgadza się na spotkania Any z Renzo. W przyszłości, kiedy stan zdrowia dziewczyny poprawi się, planuje znowu rozdzielić parę. Jednak nic nie powstrzyma miłości Any i Renzo. Młodzi postanawiają za wszelką cenę być razem i decydują się na ślub...(n)</desc>
    <credits>
      <director>Nicolas del Boca</director>
      <actor>Andrea del Boca</actor>
      <actor>Duilio Marzio</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 58</episode-num>
    <episode-num system=""onscreen"">E58</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210818080000 +0200"" stop=""20210818085500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 45</episode-num>
    <episode-num system=""onscreen"">E45</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210818134000 +0200"" stop=""20210818145500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 45</episode-num>
    <episode-num system=""onscreen"">E45</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210818200000 +0200"" stop=""20210818205500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 46</episode-num>
    <episode-num system=""onscreen"">E46</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210819020000 +0200"" stop=""20210819025500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 46</episode-num>
    <episode-num system=""onscreen"">E46</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210819045000 +0200"" stop=""20210819060000 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Wola życia</title>
    <title lang=""xx"">Voglia di vivere</title>
    <desc lang=""pl"">W dniu 17 urodzin Any jej ojciec, Anibal organizuje wielkie przyjęcie dla rodziny i przyjaciół. Wieczorem Ana i jej znajomi idą na dyskotekę. Dziewczyna poznaje tam Renzo, nieśmiałego chłopaka, którego pasją jest muzyka. Młodzi zakochują się w sobie. Ojciec Any, mężczyzna zaborczy i zazdrosny, jest przeciwny temu związkowi. Zabrania młodym spotykać się ze sobą. Ana popada w apatię, nie widzi sensu życia. Ze względu na zły stan zdrowia córki Anibal zgadza się na spotkania Any z Renzo. W przyszłości, kiedy stan zdrowia dziewczyny poprawi się, planuje znowu rozdzielić parę. Jednak nic nie powstrzyma miłości Any i Renzo. Młodzi postanawiają za wszelką cenę być razem i decydują się na ślub...(n)</desc>
    <credits>
      <director>Nicolas del Boca</director>
      <actor>Andrea del Boca</actor>
      <actor>Duilio Marzio</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 59</episode-num>
    <episode-num system=""onscreen"">E59</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210819080000 +0200"" stop=""20210819085500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 46</episode-num>
    <episode-num system=""onscreen"">E46</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210819134000 +0200"" stop=""20210819145500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 46</episode-num>
    <episode-num system=""onscreen"">E46</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210819200000 +0200"" stop=""20210819205500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 47</episode-num>
    <episode-num system=""onscreen"">E47</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210820020000 +0200"" stop=""20210820025500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Czarna perła</title>
    <title lang=""xx"">Perla Negra</title>
    <desc lang=""pl"">Tajemnicza kobieta zostawia dziecko w ekskluzywnej szkole dla dziewcząt. Na pokrycie kosztów utrzymania i wykształcenia przeznacza 22 niezwykle cenne czarne perły. Dziewczynka, której nadano imię Perła, dorasta. W dniu swoich 21. urodzin otrzymuje od dyrektorki szkoły jedną z czarnych pereł. W szkole Perła zaprzyjaźnia się z rówieśniczką - Evą. Pewnego dnia w życiu dziewcząt pojawia się młody mężczyzna - Tomas. Najpierw próbuje oczarować Perłę, ale gdy dziewczyna nie odwzajemnia jego zainteresowania, kieruje swą uwagę w stronę Evy. Perła próbuje ostrzec przyjaciółkę przed chłopakiem, którego przyłapała na niejednym kłamstwie, ale dziewczyna jest ślepo zakochana. Eva zachodzi w ciążę, a wtedy Tomas opuszcza ją. Przyjaciółki postanawiają razem wychować dziecko. Po narodzinach synka Eva otrzymuje informację, że może przejąć znaczną część udziałów w rodzinnej firmie kosmetycznej, które zapisał jej w testamencie dziadek. Gdy dziewczęta jadą do domu rodzinnego Evy, ma miejsce tragiczny wypadek samochodowy. Eva ginie na miejscu. Perła wychodzi z kraksy cało. W szpitalu Perła orientuje się, że w czasie wypadku trzymała torebkę z dokumentami przyjaciółki i teraz wszyscy biorą ją za Evę. Postanawia zachować nową tożsamość, aby móc wychowywać synka Evy i zapewnić mu dostatnią przyszłość. Rozpoczyna się walka o utrzymanie firmy kosmetycznej i dalsze perypetie sercowe głównej bohaterki.(n)</desc>
    <credits>
      <director>Gaita Aragona</director>
      <director>Nicolas Del Boca</director>
      <actor>Andrea Del Boca</actor>
      <actor>Gabriel Corrado</actor>
      <actor>Maria Rosa Gallo</actor>
      <actor>Norberto Diaz</actor>
      <actor>Facungo Arana</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 47</episode-num>
    <episode-num system=""onscreen"">E47</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210820045000 +0200"" stop=""20210820060000 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Wola życia</title>
    <title lang=""xx"">Voglia di vivere</title>
    <desc lang=""pl"">W dniu 17 urodzin Any jej ojciec, Anibal organizuje wielkie przyjęcie dla rodziny i przyjaciół. Wieczorem Ana i jej znajomi idą na dyskotekę. Dziewczyna poznaje tam Renzo, nieśmiałego chłopaka, którego pasją jest muzyka. Młodzi zakochują się w sobie. Ojciec Any, mężczyzna zaborczy i zazdrosny, jest przeciwny temu związkowi. Zabrania młodym spotykać się ze sobą. Ana popada w apatię, nie widzi sensu życia. Ze względu na zły stan zdrowia córki Anibal zgadza się na spotkania Any z Renzo. W przyszłości, kiedy stan zdrowia dziewczyny poprawi się, planuje znowu rozdzielić parę. Jednak nic nie powstrzyma miłości Any i Renzo. Młodzi postanawiają za wszelką cenę być razem i decydują się na ślub...(n)</desc>
    <credits>
      <director>Nicolas del Boca</director>
      <actor>Andrea del Boca</actor>
      <actor>Duilio Marzio</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 60</episode-num>
    <episode-num system=""onscreen"">E60</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210820080000 +0200"" stop=""20210820085500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 47</episode-num>
    <episode-num system=""onscreen"">E47</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210820140000 +0200"" stop=""20210820145500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 47</episode-num>
    <episode-num system=""onscreen"">E47</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
  <programme start=""20210820200000 +0200"" stop=""20210820205500 +0200"" channel=""Novela tv"">
    <title lang=""pl"">Stellina</title>
    <title lang=""xx"">Stelina</title>
    <desc lang=""pl"">Stellina i jej matka, Rosa, mieszkają w niewielkiej wiosce w Argentynie. Żyją bardzo skromnie, ale nie narzekają na swój los. Stellina jest już prawie dorosła i pomaga matce w prowadzeniu gospodarstwa. Pewnego dnia okazuje się, że matka jest poważnie chora. Stellina bardzo się o nią martwi. Rosa postanawia jechać do Buenos Aires, aby znaleźć dobrego lekarza. Ma tam oddaną przyjaciółkę, Teresę, której nie widziała od wielu lat. Teresa jest gosposią bogatej rodziny Mendoza. Zaprasza Rosę i Stellinę do swojego domu. Obie wyruszają w długą podróż do Buenos Aires. Wkrótce po ich przyjeździe okazuje się, że stan Rosy jest bardzo ciężki i jej dni są policzone. Kobieta martwi się, że jej córka zostanie na świecie całkiem sama, bez środków do życia. Zastanawia się nad wyjawieniem wielkiej tajemnicy: Stellina jest dzieckiem zamożnego człowieka, który porzucił Rosę, kiedy była w ciąży.(n)</desc>
    <credits>
      <director>Diana Alvarez</director>
      <actor>Andrea del Boca</actor>
      <actor>Ricardo Darin</actor>
      <actor>Osvaldo Laport</actor>
    </credits>
    <category lang=""pl"">Telenowela</category>
    <episode-num system=""onscreen"">, odc. 48</episode-num>
    <episode-num system=""onscreen"">E48</episode-num>
    <rating>
      <value>12</value>
    </rating>
  </programme>
</tv>
        ";

    }
}
