using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Text.Json;


namespace Serialize
{
    public partial class Form1 : Form
    {
        private String fileName = Directory.GetCurrentDirectory() + "\\output1.{0}";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private StudentInfo GetStudentInfo()
        {
            String name = this.name.Text;
            String address = this.address.Text;
            int id = Convert.ToInt32(studentId.Text);
            String courseInfo = this.courseInfo.Text;
            StudentInfo s = new StudentInfo(name, address, courseInfo, id);
            return s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            var s = GetStudentInfo();
            FileStream filestream = new FileStream(String.Format(fileName, "txt"), FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(filestream, s);
            filestream.Close();
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream filestream2 = new FileStream(String.Format(fileName, "txt"), FileMode.Open);
            BinaryFormatter bf2 = new BinaryFormatter();
            StudentInfo si = new StudentInfo();
            si = (StudentInfo)bf2.Deserialize(filestream2);
            this.name2.Text = si.Name;
            this.address2.Text = si.Address;
            studentId2.Text = "" + si.ID;
            courseInfo2.Text = si.CourseInfo;
            filestream2.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            var s = GetStudentInfo();
            XmlSerializer formatter = new XmlSerializer(typeof(StudentInfo));

            using (FileStream fs = new FileStream(String.Format(fileName, "xml"), FileMode.OpenOrCreate))
            {
                XmlWriter writer = new XmlTextWriter(fs, Encoding.Unicode);
                // Serialize using the XmlTextWriter.
                formatter.Serialize(writer, s);
                writer.Close();
                //formatter.Serialize(fs, s);
            }
            button3.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            XmlSerializer xmlf = new XmlSerializer(typeof(StudentInfo));
            StudentInfo si = new StudentInfo();
            using (StreamReader reader = new StreamReader(String.Format(fileName, "xml"), Encoding.UTF8, true))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(StudentInfo));
                si = (StudentInfo)xmlf.Deserialize(reader);
            }
            this.name2.Text = si.Name;
            this.address2.Text = si.Address;
            studentId2.Text = "" + si.ID;
            courseInfo2.Text = si.CourseInfo;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StudentInfo s = GetStudentInfo();
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(s);
            File.WriteAllText(String.Format(fileName, "json"), jsonString);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            StudentInfo s = new StudentInfo();
            using (StreamReader r = new StreamReader(String.Format(fileName, "json")))
            {
                string jsonString = r.ReadToEnd();
                s = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentInfo>(jsonString);
            }
            this.name2.Text = s.Name;
            this.address2.Text = s.Address;
            studentId2.Text = "" + s.ID;
            courseInfo2.Text = s.CourseInfo;
        }
    }
}
