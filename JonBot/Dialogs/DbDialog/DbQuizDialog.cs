using System;

namespace JonBot.Model
{
    [Serializable]
    public class DbQuizDialog
    {
        public DbQuizDialog()
        {
            Points = 0;
            ConversationStatus = 0;
            Answers1 = new string[] { "6", "24", "12", "8", "3" };
            Answers2 = new string[] { "O homem que a viu", "A camisa do homem que era vermelha", "O dedo do homem que apontou para ela quando a viu", "Um vulto difuso, uma mancha, já que a toupeira é quase cega", "Nenhuma das respostas acima" };
            Answers3 = new string[] { "Na África", "Na Oceania", "Às margens do Rio Negro", "Na Ásia", "Nas Américas" };
            Answers4 = new string[] { "A velociade do som é igual a da luz", "Golfinhos são mamíferos", "Os passáros são invertebrados", "O mar vermelho fica na China", "O Brasil é o maior país do mundo" };
            Answers5 = new string[] { "Supersônico", "Ultrasônico", "Hipersônico", "Sônico", "Teco teco" };
            Answers6 = new string[] { "Na Mongólia", "No Japão", "Na Atlântida", "No Egito, pelos Judeus", "Na China" };
            Answers7 = new string[] { "Shelby Mustang", "Hummer H2", "Porsche 911", "Aston Martin DB5", "Audi R8" };
            Answers8 = new string[] { "O Tiranossauro Rex", "O Tiranossauro do Rex", "O Mastodonte", "O Dinossauro Rex", "O Bocassauro" };
            Answers9 = new string[] { "Leonardo da Vinci", "Sócrates", "Heródoto", "Homero", "Platão" };
            Answers10 = new string[] { "Agrale", "Cometa", "Busscar", "Scania", "Comil" };
            ConversationOption = new string[] { "Vamos conversar?", "Responder de novo" };
        }

        public int ConversationStatus { get; set; }
        public int Points { get; set; }
        public string[] Answers1 { get; set; }
        public string[] Answers2 { get; set; }
        public string[] Answers3 { get; set; }
        public string[] Answers4 { get; set; }
        public string[] Answers5 { get; set; }
        public string[] Answers6 { get; set; }
        public string[] Answers7 { get; set; }
        public string[] Answers8 { get; set; }
        public string[] Answers9 { get; set; }
        public string[] Answers10 { get; set; }
        public string[] ConversationOption { get; set; }
    }
}