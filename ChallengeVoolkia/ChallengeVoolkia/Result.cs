using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeVoolkia
{
    public class Result
    {
        public string id;

        public string title;

        public string category_id;

        public string name;



        public string Serialize(int index)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(index + "--------------------------------------------------");
            sb.AppendLine("ID: " + this.id);
            sb.AppendLine("Title: " + this.title);
            sb.AppendLine("Cat.ID: " + this.category_id);
            sb.AppendLine("Cat. Name: " + this.name);

            return sb.ToString();
        }
    }

   
}






