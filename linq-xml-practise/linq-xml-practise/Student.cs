using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linq_xml_practise
{
    class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Status { get; set; }  //singe; taken

        public override string ToString()
        {
            return Name + " " + Age + " " + Status;
        }

    }
}
