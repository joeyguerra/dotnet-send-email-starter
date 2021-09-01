using System;
using System.Collections.Generic;
using Mustachio;
namespace EmailSender {
    public class EmailTemplate{

        public string Template {get;}
        public dynamic Model {get;}
        public string Content {get;set;}
        public EmailTemplate(string template, dynamic model){
            this.Template = template;
            this.Model = model;
            this.Content = EmailTemplate.Transform(template, model);
        }
        public static string Transform(string sourceTemplate, dynamic model){
            var output = Mustachio.Parser.Parse(sourceTemplate)(model);
            return output;
        }
    }
}