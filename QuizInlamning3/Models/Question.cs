using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace QuizInlamning3.Models
{
    //public enum Category
    //{

    //    GeneralKnowledge,
    //    FoodAndDrink,
    //    Sports,
    //    Animals,
    //    Geography,
    //    ScienceAndNature
    //}
    public enum Category
    {
        Datatyper,
        Operatorer,
        Variabler,
        KlasserOchObjekt,
        Arv,
        Polymorfism,
        Interface,
        Abstraktion,
        Collections,
        LINQ,
        Delegater,
        Events,
        AsyncAwait,
        Felhantering,
        FilerOchIO,
        Generics,
        Static,
        PartialClasses,
        Records,
        Properties,
        Enums,
        MinneOchGC,
        Reflektion,
        Attributes
    }

    public enum QType
    {
        Text, Image 
    }

    public class Question
    {
        public Category Category { get; set; }

        public QType QType { get; set; }
        public string QuestionText { get; set; }

        public string ImagePath { get; set; }

        public string[] Answers { get; set; }

        public int CorrectAnswerIndex {  get; set; }

        

    }


}
