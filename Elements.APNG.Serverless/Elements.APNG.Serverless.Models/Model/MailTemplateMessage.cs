using System.Collections.Generic;

namespace Elements.APNG.Serverless.Models.Model
{
    public class MailTemplateMessage : Message
    {
        public MailTemplate Template { get; private set; }
        public Dictionary<string, object> TemplateParameters { get; protected set; }
        public MailTemplateMessage(string to, string subject, MailTemplate template, Dictionary<string, object> templateParameters = null)
          : base(to, subject, string.Empty)
        {
            Template = template;
            TemplateParameters = templateParameters;
        }
        public MailTemplateMessage(IEnumerable<string> to,
            string subject, MailTemplate template, Dictionary<string, object> templateParameters = null)
         : base(to, subject, string.Empty)
        {
            Template = template;
            TemplateParameters = templateParameters;
        }

        public void AddParameter(string key, object value)
        {
            if (TemplateParameters is null)
            {
                TemplateParameters = new Dictionary<string, object>();
            }

            TemplateParameters.Add(key, value);
        }

        public void AddLink(string webPath, string key = "link")
        {
            AddParameter(key, webPath);
        }
    }
}


