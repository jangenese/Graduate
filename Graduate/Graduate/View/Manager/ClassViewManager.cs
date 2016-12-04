﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graduate.Core.Data.Models;
using Graduate.Core.View.Model;
using Graduate.Core.Manager;
using SQLite;

namespace Graduate.Core.View.Manager
{
   public class ClassViewManager
    {
        SchoolYearManager schoolYearManager;
        SemesterManager semesterManager;
        ClassManager classManager;
        ClassActivityManager cActivityManager;
        public ClassViewManager(SchoolYearManager schoolYearManager, SemesterManager semesterManager, ClassManager classManager, ClassActivityManager cActivityManager)
        {
            this.schoolYearManager = schoolYearManager;
            this.semesterManager = semesterManager;
            this.classManager = classManager;
            this.cActivityManager = cActivityManager;
        }

        public ClassView getClassView(String id)
        {
            Class c = classManager.getClassByID(id);

            return populateClassView(c);
        }

        private IList<ClassActivity> getChildren(String fid)
        {
            return cActivityManager.getClassActivitiesByFID(fid).ToList<ClassActivity>();
        }

        private ClassView populateClassView(Class c)
        {
            ClassView classView = new ClassView();
            classView.id = c.Id;
            classView.label = c.label;
            classView.credits = c.credits.ToString();
            classView.goalGrade = c.goalGrade.ToString();
            classView.gpaGrade = c.gpaGrade.ToString();
            
            classView.letterGrade = "A+";
            classView.parentLabel = getParentLabel(c.FId.ToString());
            classView.status = getStatus(c.completed);
            classView.children = getChildren(c.Id.ToString());
            classView.completed = c.completed;
            classView.remainingWeight = getRemainingWeight(c.Id.ToString()) + "%";
            if (getRemainingWeight(c.Id.ToString()) > 0)
            {
                classView.neededGrade = calculateNeededGrade(c.Id.ToString(), c.goalGrade).ToString() + "%";
            }
            else {
                classView.neededGrade = "-";
            }

            if (c.completed)
            {
                classView.percentGrade = c.percentGrade + "%";
            }
            else {
                classView.percentGrade = calculatePercentGrade(c.Id.ToString()).ToString() + "%";
            }
            

            
            return classView;
        }
               
        private String getParentLabel(String fid)
        {
            Semester sem = semesterManager.getSemesterByID(fid);
            return sem.label;
        }

        private int getRemainingWeight(String fid) {
            int remainingWeight = 100;
            int completedWeight = 0;
            IList<ClassActivity> children = getChildren(fid);

            foreach (ClassActivity c in children) {
                completedWeight += c.weight;
            }

            return remainingWeight - completedWeight;
        }

        private double calculateNeededGrade(String fid, int goalGradeByuser) {
            double goalGrade = goalGradeByuser;
            double neededGrade = 0;
            double unearnedWeight = 0;
            double completedWeight = 0;
            double earnedWeight = 0;
            double remainingWeight = getRemainingWeight(fid);
            double activityGrade = 1;

            IList<ClassActivity> children = getChildren(fid);

            foreach (ClassActivity c in children)
            {
                activityGrade = c.grade * 0.01;

                completedWeight += c.weight;
                earnedWeight += activityGrade * c.weight;
            }

            unearnedWeight = goalGrade - earnedWeight;
            neededGrade = unearnedWeight / remainingWeight;

            neededGrade = neededGrade * 100;
            neededGrade = Math.Round(neededGrade, 0);
            
            return neededGrade;

        }

        private int calculatePercentGrade(String fid) {
            double grade = 0;
            double completedWeight = 0;
            double earnedWeight = 0;
            double activityGrade = 1;

            IList<ClassActivity> children = getChildren(fid);

            foreach (ClassActivity c in children)
            {
                activityGrade = c.grade * 0.01;

                completedWeight += c.weight;
                earnedWeight += activityGrade * c.weight;
            }

            if (completedWeight > 0) {
                grade = earnedWeight / completedWeight;
            } 
                

            

            return Convert.ToInt32(grade * 100);
               
        }

        

        private String getStatus(Boolean b)
        {
            String status = "C";
                       
                if (!b)
                {
                    status = "INP";
                }  
            return status;
        }

    }
}
