using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAB04_01.Model;

namespace LAB04_01.Controller
{
    public class StudentController
    {
        public static List<Student> GetStudent()
        {
            using (var context = new SMContext())
            {
                return context.Students.ToList();
            }
        }

        public static bool AddStudent(Student student, out string error)
        {
            using (var context = new SMContext())
            {
                error = string.Empty;
                try
                {
                    context.Students.Add(student);
                    context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    error = ex.ToString();
                    return false;
                }
            }
        }

        public static bool UpdateStudent(string ID, Student NewStudent, out string error)
        {
            using (var context = new SMContext())
            {
                error = string.Empty;
                try
                {
                    var student = context.Students.First(p => p.StudentID == ID);
                    student.FullName = NewStudent.FullName;
                    student.AverageScore = NewStudent.AverageScore;
                    student.FacultyID = NewStudent.FacultyID;
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                    return false;
                }
            }
        }

        public static bool DeleteStudent(string ID, out string error)
        {
            using (var context = new SMContext())
            {
                error = string.Empty;
                try
                {
                    var student = context.Students.First(p => p.StudentID == ID);
                    context.Students.Remove(student);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                    return false;
                }
            }
        }
    }
}
