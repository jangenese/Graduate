using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using Graduate.Core.Data.Models;
using Graduate.Core;
using Graduate.Core.View.Model;
using Graduate.Droid.ListAdapters;


namespace Graduate.Droid.Fragments
{
    public class EditDialogFragment : DialogFragment
    {

        private TextView title;
        private TextView parentType;
        private AutoCompleteTextView parentEntry;
        private TextView entryType;
        private EditText entry;
        private TextView grade;
        private AutoCompleteTextView gradeEntry;
        private TextView credits;
        private EditText creditsEntry;
        private CheckBox checkbox;
        private Button cancelButton;
        private Button editButton;
        private Planner planner;


        private LinearLayout parentRow;
        private LinearLayout classRow;
        private LinearLayout checkBoxRow;

        private int parentPosition = 0;
        private Boolean status = false;
        public int parentId { get; set; } = 0;


        private View fragmentView;
        public int type { get; set; } = 0;

        public int entityID { get; set; } = 0;
        public Boolean fromParent { get; set; } = false;

        private int originalWeight = 0;

        private String inputMessage = "";

        public static EditDialogFragment NewInstance(Bundle bundle)
        {
            EditDialogFragment fragment = new EditDialogFragment();
            fragment.Arguments = bundle;
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle editdInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            fragmentView = inflater.Inflate(Resource.Layout.NewEntryDialogFragment, container, false);
            planner = GraduateApp.Current.planner;


            findViews();

            checkFormType(type);
            handleEvents();


            return fragmentView;
        }

        public override void OnDismiss(IDialogInterface dialog)
        {
            base.OnDismiss(dialog);
            Activity activity = this.Activity;
            ((IDialogInterfaceOnDismissListener)activity).OnDismiss(dialog);

        }

        private void findViews()
        {


            title = fragmentView.FindViewById<TextView>(Resource.Id.textViewTitle);
            parentType = fragmentView.FindViewById<TextView>(Resource.Id.textViewParent);
            parentEntry = fragmentView.FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextViewParentEntry);
            entryType = fragmentView.FindViewById<TextView>(Resource.Id.textViewLabel);
            entry = fragmentView.FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextViewLabelEntry);
            grade = fragmentView.FindViewById<TextView>(Resource.Id.textViewGrade);
            gradeEntry = fragmentView.FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextViewGradeEntry);
            credits = fragmentView.FindViewById<TextView>(Resource.Id.textViewCredits);
            creditsEntry = fragmentView.FindViewById<EditText>(Resource.Id.editTextCreditsEntry);
            checkbox = fragmentView.FindViewById<CheckBox>(Resource.Id.checkBoxCompleted);
            cancelButton = fragmentView.FindViewById<Button>(Resource.Id.buttonCancel);
            editButton = fragmentView.FindViewById<Button>(Resource.Id.buttonSave);

            parentRow = fragmentView.FindViewById<LinearLayout>(Resource.Id.linearLayoutParentRow);
            classRow = fragmentView.FindViewById<LinearLayout>(Resource.Id.linearLayoutMainClass);
            checkBoxRow = fragmentView.FindViewById<LinearLayout>(Resource.Id.linearLayoutCheckBoxRow);


        }



        private void handleEvents()
        {
            editButton.Click += SaveButton_Click;
            cancelButton.Click += CancelButton_Click;
            checkbox.Click += Checkbox_Click;

        }

        private void Checkbox_Click(object sender, EventArgs e)
        {
            if (checkbox.Checked)
            {
                status = true;
                grade.Text = "Grade";
            }
            else
            {
                status = false;
                grade.Text = "Goal";
            }
        }


        private void CancelButton_Click(object sender, EventArgs e)
        {
            Dismiss();
            Toast.MakeText(Activity, "Cancelled", ToastLength.Short).Show();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Boolean successful = editEntry(type);

           
            if (successful)
            {
                Dismiss();
                Toast.MakeText(Activity, "Saved", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(Activity, inputMessage, ToastLength.Short).Show();
            }


        }

        private Boolean editSchoolYear()
        {

            planner.editSchoolYear(entityID.ToString(),entry.Text.ToString());
                

            return true;
        }

        private Boolean editSemester()
        {
            
            
                String fid = getSchoolYearParentID(parentEntry.Text, planner.getAllSchoolYears());
                planner.editSemester(entityID.ToString(), fid, entry.Text.ToString());
            

            return true;
        }

        private Boolean editClass()
        {
            Boolean successful = true;

            if (isInputValidForClassSave(gradeEntry.Text, creditsEntry.Text))
            {
               
              
                    String fid = getSemesterParentID(parentEntry.Text, planner.getAllSemesters());
                    planner.editClass(entityID.ToString(), fid, entry.Text, gradeEntry.Text, creditsEntry.Text, status);
                
            }
            else
            {
                successful = false;
            }

            return successful;
        }

        private Boolean editClassActivity()
        {
            Boolean successful = true;

            if (isInputValidForActivitySave(gradeEntry.Text, creditsEntry.Text, originalWeight))
            {
                planner.editClassActivity(entityID.ToString(), parentId.ToString(), entry.Text, gradeEntry.Text, creditsEntry.Text, true);
            }
            else
            {
                successful = false;
            }

            return successful;
        }

        private Boolean editEntry(int type)
        {
            Boolean successful = true;
            switch (type)
            {
                case 1:

                    successful = editSchoolYear();

                    break;
                case 2:

                    successful = editSemester();

                    break;
                case 3:

                    successful = editClass();
                    break;
                case 4:
                    successful = editClassActivity();
                    break;
                default:

                    break;
            }
            return successful;
        }

        private void checkFormType(int type)
        {
            switch (type)
            {
                case 1:

                    modifySchoolYearForm();

                    break;
                case 2:

                    modifySemesterForm();

                    break;
                case 3:

                    modifyClassForm();
                    break;
                case 4:
                    modifyClassActivityForm();
                    break;
                default:

                    break;
            }
        }



        private void modifySchoolYearForm()
        {
            SchoolYearView syView = planner.getSchoolYear(entityID.ToString());

            title.Text = "Edit: SchoolYear";


            entryType.Text = "School Year";
            entry.Text = syView.label;
            parentRow.Visibility = ViewStates.Gone;
            classRow.Visibility = ViewStates.Gone;
            checkBoxRow.Visibility = ViewStates.Gone;
        }

        private void modifySemesterForm()
        {
            SemesterView semView = planner.getSemester(entityID.ToString());

            title.Text = "Edit: Semester";

            var parentEntryOptions = planner.getAllSchoolYearLabels();
            ArrayAdapter parentEntryAdapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleDropDownItem1Line, parentEntryOptions);
            parentEntry.Adapter = parentEntryAdapter;
            parentType.Text = "School Year";           
            parentEntry.Text = semView.parentLabel;
            entryType.Text = "Semester";
            entry.Text = semView.label;            
            classRow.Visibility = ViewStates.Gone;
            checkBoxRow.Visibility = ViewStates.Gone;
        }

        private void modifyClassForm()
        {
            ClassView classView = planner.getClass(entityID.ToString());
            title.Text = "Edit: Class";

            var parentEntryOptions = planner.getAllSemesterLabels();
            ArrayAdapter parentEntryAdapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleDropDownItem1Line, parentEntryOptions);
            parentEntry.Adapter = parentEntryAdapter;
            parentType.Text = "Semester";
            parentEntry.Text = classView.parentLabel;
            entryType.Text = "Class";
            entry.Text = classView.label;
            creditsEntry.Text = classView.credits;
            var gradeEntryOptions = planner.getAllLetterGrades();
            ArrayAdapter gradeEntryAdapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleDropDownItem1Line, gradeEntryOptions);
            gradeEntry.Adapter = gradeEntryAdapter;            
            gradeEntry.Text = classView.goalLetterGrade;


            if (classView.completed) {
                checkbox.Checked = true;
                grade.Text = "Grade";
            }


        }

        private void modifyClassActivityForm()
        {
            gradeEntry.InputType = Android.Text.InputTypes.ClassNumber;

            checkBoxRow.Visibility = ViewStates.Gone;

            


            ClassActivityView classActivity = planner.getClassActivity(entityID.ToString());
            title.Text = "Edit : Class";
            hideParentEntry();

            originalWeight = Convert.ToInt32(classActivity.weight);

            var gradeEntryOptions = planner.getAllPercentGrades();
            ArrayAdapter gradeEntryAdapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleDropDownItem1Line, gradeEntryOptions);
            gradeEntry.Adapter = gradeEntryAdapter;

            title.Text = "Edit: Activity";
            entryType.Text = "Activity";
            entry.Text = classActivity.label;
            credits.Text = "Weight";
            creditsEntry.Text = classActivity.weight;
            grade.Text = "Grade";
            gradeEntry.Text = classActivity.percentGrade;
        }

        private String getParentPosition()
        {


            int i = parentPosition + 1;

            return i.ToString();
        }

        private String getSchoolYearParentID(String label, IList<SchoolYear> list)
        {
            int i = 0;
            foreach (SchoolYear sy in list)
            {
                if (label == sy.label)
                {
                    i = sy.Id;
                }
            }

            return i.ToString();
        }

        private String getSemesterParentID(String label, IList<Semester> list)
        {
            int i = 0;
            foreach (Semester sem in list)
            {
                if (label == sem.label)
                {
                    i = sem.Id;
                }
            }

            return i.ToString();
        }


        private void hideParentEntry()
        {
            parentRow.Visibility = ViewStates.Gone;
        }

        private Boolean isInputValidForClassSave(String letterGrade, String credits)
        {
            Boolean isValid = true;
            IList<String> letterGrades = planner.getAllLetterGrades();
            try
            {
                Convert.ToInt32(credits);
            }
            catch
            {
                inputMessage = "Credit input must be a number";
                isValid = false;
            }

            if (!letterGrades.Contains(letterGrade))
            {
                inputMessage = "Letter grade not found";
                isValid = false;
            }

            return isValid;
        }

        private Boolean isInputValidForActivitySave(String percentGrade, String weight, int originalWeight)
        {


            Boolean isValid = true;
            int inputWeight = 200;

            IList<String> percentGrades = planner.getAllPercentGrades();
            try
            {
                inputWeight = Convert.ToInt32(weight);

            }
            catch
            {
                inputMessage = "Weight input must be a number";
                isValid = false;
            }

            int remainingWeight = getRemainingWeight() + originalWeight;

            if (inputWeight > remainingWeight && inputWeight != 0)
            {
                inputMessage = "Weight entry invalid! " + remainingWeight.ToString() + " Max weight";
                isValid = false;
            }



            if (!percentGrades.Contains(percentGrade))
            {
                inputMessage = "Percent grade not found";
                isValid = false;
            }

            return isValid;

        }

        private int getRemainingWeight()
        {
            ClassView c = planner.getClass(entityID.ToString());
            return Convert.ToInt32(c.remainingWeight);
        }



    }
}