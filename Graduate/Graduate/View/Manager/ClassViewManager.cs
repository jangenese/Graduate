using System;
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

        GradeManager gradeManager;
        public ClassViewManager(SchoolYearManager schoolYearManager, SemesterManager semesterManager, ClassManager classManager, ClassActivityManager cActivityManager, GradeManager gradeManager)
        {
            this.schoolYearManager = schoolYearManager;
            this.semesterManager = semesterManager;
            this.classManager = classManager;
            this.cActivityManager = cActivityManager;

            this.gradeManager = gradeManager;
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
            classView.parentLabel = getParentLabel(c.FId.ToString());
            classView.status = getStatus(c.completed);
            classView.children = getChildren(c.Id.ToString());
            classView.completed = c.completed;
            classView.remainingWeight = getRemainingWeight(c.Id.ToString()) + "%";



           

            

            
            
            //checks if there are remaining weight to calculate 
            if (getRemainingWeight(c.Id.ToString()) > 0)
            {
                classView.neededGrade = calculateNeededGrade(c.Id.ToString(), c.percentGoalGrade).ToString() + "%";
            }
            else {
                classView.neededGrade = "-";
            }


            System.Diagnostics.Debug.WriteLine("Checking if Completed");

            if (c.completed)
            {

                System.Diagnostics.Debug.WriteLine("Class is completed");
                classView.percentGrade = c.percentGrade + "%";
                classView.gpaGrade = c.gpaGrade.ToString();
                classView.letterGrade = c.letterGrade;

                classView.goalPercentGrade = c.percentGoalGrade.ToString();
                classView.goalLetterGrade = c.letterGoalGrade;

            }
            else {

                System.Diagnostics.Debug.WriteLine("Class is not completed");

                classView.goalPercentGrade = c.percentGoalGrade.ToString();
                classView.goalLetterGrade = c.letterGoalGrade;


                System.Diagnostics.Debug.WriteLine("Checking if it has activites");
                if (getChildren(c.Id.ToString()).Count > 0 ) {

                   

                    System.Diagnostics.Debug.WriteLine("It has Activities");

                    classView.percentGrade = calculatePercentGrade(c.Id.ToString()).ToString() + "%";
                   
                    classView.letterGrade = getLetterFromSchema(calculatePercentGrade(c.Id.ToString()));
                    classView.gpaGrade = getGPAGradeFromSchema(classView.letterGrade).ToString();

                    //save updated grade values
                    c.percentGrade = calculatePercentGrade(c.Id.ToString());
                    c.letterGrade = getLetterFromSchema(calculatePercentGrade(c.Id.ToString()));
                    c.gpaGrade = getGPAGradeFromSchema(classView.letterGrade);

                   
                } else {

                    System.Diagnostics.Debug.WriteLine("It doesnt have activities");

                    c.percentGrade = c.percentGoalGrade;
                    c.letterGrade = c.letterGoalGrade;
                    c.gpaGrade = c.gpaGoalGrade;

                    classView.percentGrade = c.percentGrade + "%";
                    classView.gpaGrade = c.gpaGrade.ToString();
                    classView.letterGrade = c.letterGrade;                
                    

                    //save updated grade values
                    c.percentGrade = c.percentGoalGrade;
                    c.letterGrade = c.letterGoalGrade;
                    c.gpaGrade = c.gpaGoalGrade;

                    
                }

                System.Diagnostics.Debug.WriteLine("Saving Calcualeted grades");

                classManager.SaveItem(c);

                

                
               
            }


            System.Diagnostics.Debug.WriteLine("Hello World");





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

        private Class prepareGrades(Class c)
        {

            Class originalClass = classManager.getClassByID(c.Id.ToString());

            if (c.percentGrade != originalClass.percentGrade)
            {
                c.letterGrade = getLetterFromSchema(c.percentGrade);
                c.gpaGrade = getGPAGradeFromSchema(c.letterGrade);
            }


            return c;
        }


        private int getPercentGradeFromSchema(String letter)
        {


            Grade g = gradeManager.getByLetter(letter);

            return g.Percent;
        }

        private double getGPAGradeFromSchema(String letter)
        {


            Grade g = gradeManager.getByLetter(letter);

            return g.GPA;
        }

        private String getLetterFromSchema(int percent)
        {
            return gradeManager.getByPercent(percent.ToString()).Letter;
        }

    }
}
