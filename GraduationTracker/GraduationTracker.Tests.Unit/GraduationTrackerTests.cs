using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using GraduationTracker.Models;
using Should;
using Moq;
using GraduationTracker.Repositories;

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

            //few of the student didn't pass due to low marks or low credits
            (graduated.Count(g => g.Status == true) == students.Count()).ShouldBeFalse();

        }


        [TestMethod]
        [DataRow(1, STANDING.MagnaCumLaude)]
        [DataRow(2, STANDING.MagnaCumLaude)]
        [DataRow(3, STANDING.Average)]
        [DataRow(4, STANDING.Remedial)]
        [DataRow(5, STANDING.Remedial)]
        [DataRow(6, STANDING.Remedial)]
        public void TestHasGraduated_Returns_ValidType(int id, STANDING standing)
        {
            //Arrange
            var diploma = DataSeeder.SeedDiploma();
            var students = DataSeeder.SeedStudents();

            var student = students.First(s => s.Id == id);

            //Act
            DiplomaResult result = _tracker.HasGraduated(diploma, student);

            //Assert
            result.Standing.ShouldEqual(standing);
        }

        [TestMethod]
        public void TestHasGraduated_Returns_None()
        {
            //Arrange
            var diploma = DataSeeder.SeedDiploma();
            var students = DataSeeder.SeedStudents();

            var student = students.First(s => s.Id == 6);

            var mockDiplomaService = new Mock<IDiplomaService>();

            mockDiplomaService.Setup(r => r.GetDiplomaCoursesByRequirement(It.IsAny<Course[]>(), It.IsAny<Requirement>())).Returns(new Course[0]);

            _tracker = new GraduationTracker(mockDiplomaService.Object, new RequirmentRepo());


            //Act
            var result = _tracker.HasGraduated(diploma, student);


            //Assert
            result.Standing.ShouldEqual(STANDING.Remedial);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHasGraduated_Throws_ArgumentException_WhenDiplomaIsNull()
        {
            //Arrange
            Diploma diploma = null;
            var students = DataSeeder.SeedStudents();

            var student = students.First(s => s.Id == 6);

            //Act
            var result = _tracker.HasGraduated(diploma, student);

            //Assert
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHasGraduated_Throws_ArgumentException_WhenStudentIsNull()
        {
            //Arrange
            Diploma diploma = DataSeeder.SeedDiploma();
            var students = DataSeeder.SeedStudents();

            Student student = null;

            //Act
            var result = _tracker.HasGraduated(diploma, student);

            //Assert
        }
    }
}
