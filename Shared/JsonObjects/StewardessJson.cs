using System;

namespace Shared.JsonObjects
{
    public class StewardessJson
    {
        public int Id { get; set; }

        public int CrewId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}