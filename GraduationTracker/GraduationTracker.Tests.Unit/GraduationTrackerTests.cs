using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using GraduationTracker.Models;
using Should;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        GraduationTracker _tracker;
        
        [TestInitialize]
        public void Init()
        {
            _tracker = new GraduationTracker();            
        }


        [TestMethod]
        public void TestHasCredits()
        {
            var diploma = DataSeeder.SeedDiploma();
            var students = DataSeeder.SeedStudents();

            var graduated = new List<DiplomaResult>();

            foreach(var student in students)
            {
                graduated.Add(_tracker.HasGraduated(diploma, student));      
            }

            //one of the student didn't pass due to low marks
            (graduated.Count(g => g.Status == true) == students.Count()).ShouldBeFalse();

        }


    }
}
