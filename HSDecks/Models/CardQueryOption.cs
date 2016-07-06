using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSDecks.Models
{
    public class CardQueryOption
    {
        public string name { get; set; }
        public List<string> heroClassList { get; set; }
        public List<string> costList { get; set; }
        public List<string> cardTypeList { get; set; }
        public List<string> cardRarityList { get; set; }
        public List<string> modeList { get; set; }
        public List<string> expansionList { get; set; }

        public void AddCost(int i)
        {
            costList.Add(i.ToString());
        }
        public void AddHero(string heroClass)
        {
            heroClassList.Add(heroClass);
        }
        public void AddExpansion(string expansion)
        {
            expansionList.Add(expansion);
        }

        public CardQueryOption()
        {
            heroClassList = new List<string>();
            costList = new List<string>();
            cardTypeList = new List<string>();
            cardRarityList = new List<string>();
            modeList = new List<string>();
            expansionList = new List<string>();

    
        }
    }
}
