using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infissy.DBEntities
{
    public class Carta
    {
        public Carta(int iDCard, string title, string description, int referenceCity, string effects, int type, int progressValue, bool progress, int population, int firstMaterial, int money,int iDMazzoCarta)
        {
            IDCard = iDCard;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            ReferenceCity = referenceCity;
            Effects = effects ?? throw new ArgumentNullException(nameof(effects));
            Type = type;
            ProgressValue = progressValue;
            Progress = progress;
            Population = population;
            FirstMaterial = firstMaterial;
            Money = money;
            IDMazzoCarta = iDMazzoCarta;
        }

        public int IDCard { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReferenceCity { get; set; }
        public string Effects { get; set; }
        public int Type { get; set; }
        public int ProgressValue { get; set; }
        public bool Progress { get; set; }
        public int Population { get; set; }
        public int FirstMaterial { get; set; }
        public int Money { get; set; }
        public int IDMazzoCarta { get; set; }

        public override string ToString()
        {
            return $"{IDMazzoCarta};{IDCard};{Title};{Description};{ReferenceCity};{Effects};{Type};{ProgressValue};{Progress};{Population};{FirstMaterial};{Money};";
        }

        public static implicit operator string(Carta c)
        {
            return c.ToString();
        }
    }
}