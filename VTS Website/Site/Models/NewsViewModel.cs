using System;
using VTS.Site.DomainObjects;

namespace Html.Models
{
    public class SystemNewsViewModel
    {
        private readonly SystemNews model;

        public SystemNewsViewModel()
        {
            Date = DateTime.Now;
            model = new SystemNews();
        }

        public SystemNewsViewModel(SystemNews model)
        {
            this.model = model;
            Id = model.Id;
            Date = model.DatePublished;
            TopicEnglish = model.GetTopic(0);
            TopicRussian = model.GetTopic(1);
            TopicBelarusian = model.GetTopic(2);

            TextEnglish = model.GetTopic(0);
            TextRussian = model.GetTopic(1);
            TextBelarusian = model.GetTopic(2);
        }

        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string TopicEnglish { get; set; }
        public string TextEnglish { get; set; }
        public string TopicRussian { get; set; }
        public string TextRussian { get; set; }
        public string TopicBelarusian { get; set; }
        public string TextBelarusian { get; set; }

        public SystemNews GetModel()
        {
            return model;
        }
    }
}