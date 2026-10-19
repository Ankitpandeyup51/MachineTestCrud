using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MachineTestCrud.Models
{
    public class Student
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public string Email { get; set; }

        public int Password { get; set; }

        public DateTime Dob {  get; set; }

        public string Address { get; set; }
    }
}