using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAB04_01.Model;

namespace LAB04_01.Controller
{
    public class FacultyController
    {
        public static List<Faculty> GetFacultyList()
        {
            using (var context = new SMContext())
            {
                return context.Faculties.ToList();
            }
        }

        public static string GetFacultyName(int FacultyID)
        {
            using (var context = new SMContext())
            {
                return context.Faculties.FirstOrDefault(p => p.FacultyID == FacultyID).FacultyName;
            }
        }

        public static bool AddFaculty(Faculty faculty, out string error)
        {
            using (var context = new SMContext())
            {
                error = string.Empty;
                try
                {
                    context.Faculties.Add(faculty);
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

        public static bool UpdateFaculty(int FacultyID, Faculty NewFaculty, out string error)
        {
            using (var context = new SMContext())
            {
                error = string.Empty;
                try
                {
                    var faculty = context.Faculties.FirstOrDefault(p => p.FacultyID == FacultyID);
                    faculty.FacultyName = NewFaculty.FacultyName;
                    faculty.TotalProfessor = NewFaculty.TotalProfessor;
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

        public static bool DeleteFaculty(int FacultyID, out string error)
        {
            using (var context = new SMContext())
            {
                error = string.Empty;
                try
                {
                    var faculty = context.Faculties.FirstOrDefault(p => p.FacultyID == FacultyID);
                    context.Faculties.Remove(faculty);
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
