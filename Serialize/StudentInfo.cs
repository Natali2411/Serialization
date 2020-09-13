using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialize
{
    [Serializable]
    class StudentInfoclass
    {
    public string Name, Address, CourseInfo;
    public int ID; 
    public StudentInfo()
    { }
    public StudentInfo(String n, String a, String ci, int id)
    {
        Name = n; 
        Address = a; 
        CourseInfo = ci;
        ID = id;
    }
}
}
