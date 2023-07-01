using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class SecondPage
    {
        public int DoctorCount { get; set; }
        public int TrainnerCount { get; set; }
        public int DonnerCount { get; set; }
        public int UserCount { get; set; }
        public IEnumerable<Person> Doctor { get; set; }
    }
}
