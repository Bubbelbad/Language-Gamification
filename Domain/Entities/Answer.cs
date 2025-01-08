using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Answer : IEntity<int>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public Question question { get; set; }

        int IEntity<int>.Id
        {
            get => Id;
            set => Id = value;
        }
    }
}
