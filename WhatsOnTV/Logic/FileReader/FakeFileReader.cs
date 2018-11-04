using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.FileReader
{
    public class FakeFileReader : IFileReader
    {
        public string ReadFile(string filename)
        {
            return "<channel id=\"Polsat\">" + 
                "< display - name lang = \"pl\" > Polsat </ display - name >" +
                "< icon src = \"https://1.fwcdn.pl/channels/3.1.png\" />" +
                "< url > http://www.filmweb.pl</url>" +
            "</ channel >" +
            "< programme start = \"20181016220500 +0200\" stop = \"20181017003500 +0200\" channel = \"Polsat\" >"+
                "< title lang = \"pl\" >Liga niezwykłych dżentelmenów</ title >" +
                "< title lang = \"en\" > The League of Extraordinary Gentlemen</ title > " +
                "< desc lang = \"pl\" > Alan Moore jest uważany za najlepszego scenarzystę komiksowego na świecie, a jego \"Liga niezwykłych dżentelmenów\" cieszy się opinią jednej z najciekawszych i najoryginalniejszych serii ostatnich lat.Nic, więc dziwnego, że w dobie panującej obecnie ... czytaj dalej. Ligę Niezwykłych Dżentelmenów tworzą bohaterowie, jakich nikt dotąd nie widział, a na jej czele stoi największy łowca i awanturnik na świecie, Allan Quatermain -odkrywca legendarnych kopalni króla Salomona. Towarzyszą mu: kapitan Nemo, wampirzyca Mina Harker, niewidzialny człowiek Rodney Skinner, amerykański agent do zadań specjalnych Tom Sawyer, nieśmiertelny Dorian Gray i nieobliczalny dr Jekyll / pan Hyde.Brytyjski wywiad, który powołał do życia Ligę, reprezentuje enigmatyczny M.Członkowie Ligi nie uznają żadnych autorytetów, są indywidualistami, których los zmusza do współdziałania.Muszą przełamać dzielące ich różnice i nauczyć się ufać sobie, bo w nich cała nadzieja świata: zamaskowany szaleniec, który każe się nazywać \"Fantomem\" grozi zatopieniem Wenecji, gdzie odbywa się konferencja przywódców największych mocarstw.Członkowie Ligi wyruszają do Włoch na pokładzie Nautilusa, okrętu podwodnego kapitana Nemo. Mają dziewięć godzin, by ocalić świat... ...więcejitemprop = \"description\" & gt; (n) </ desc >" +
                "< credits >" +
                    "< director > Stephen Norrington </ director >" +
                    "< actor > Sean Connery </ actor >" +
                    "< actor > Naseeruddin Shah </ actor >" +
                    "< actor > Peta Wilson </ actor >" + 
                    "< actor > Tony Curran </ actor >" +
                "</ credits >" +
                "< date > 2003 </ date >" +
                "< category lang = \"pl\" > film przygodowy </ category >" +
                "< category lang = \"pl\" > Fantasy </ category >" +
                "< category lang = \"pl\" > Przygodowy </ category >" +
                "< icon src = \"https://1.fwcdn.pl/po/42/04/34204/7152507.5.jpg \" />" +
                "< country > Czechy </ country >" +
                "< rating >" +
                    "< value > 6,6 </ value >" +
                "</ rating >" +
            "</ programme >";
           // throw new NotImplementedException();
        }
    }
}
