using System;

namespace Shared.JsonObjects
{
    public class PilotJson
    {
        public int Id { get; set; }

        public int CrewId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Exp { get; set; }

        public DateTime birthDate { get; set; }
    }
}