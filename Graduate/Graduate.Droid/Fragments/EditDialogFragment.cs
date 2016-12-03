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
        private Button saveButton;
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
            saveButton = fragmentView.FindViewById<Button>(Resource.Id.buttonSave);

            parentRow = fragmentView.FindViewById<LinearLayout>(Resource.Id.linearLayoutParentRow);
            classRow = fragmentView.FindViewById<LinearLayout>(Resource.Id.linearLayoutMainClass);
            checkBoxRow = fragmentView.FindViewById<LinearLayout>(Resource.Id.linearLayoutCheckBoxRow);


        }



        private void handleEvents()
        {
            saveButton.Click += SaveButton_Click;
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
            editEntry(type);
            Dismiss();
            Toast.MakeText(Activity, "Saved", ToastLength.Short).Show();
        }

        private void editSchoolYear()
        {
            planner.editSchoolYear(entityID.ToString(), entry.Text.ToString());
        }

        private void editSemester()
        {
            String fid = getSchoolYearParentID(parentEntry.Text, planner.getAllSchoolYears());
            planner.editSemester(entityID.ToString(), fid, entry.Text.ToString());
        }

        private void editClass()
        {
            String fid = getSemesterParentID(parentEntry.Text, planner.getAllSemesters());
            planner.editClass(entityID.ToString(), fid, entry.Text, gradeEntry.Text, creditsEntry.Text, status);
        }

        private void editClassActivity()
        {
            planner.editClassActivity(entityID.ToString(), parentId.ToString(), entry.Text, gradeEntry.Text, creditsEntry.Text, status);
        }

        private void editEntry(int type)
        {
            switch (type)
            {
                case 1:

                    editSchoolYear();

                    break;
                case 2:

                    editSemester();

                    break;
                case 3:

                    editClass();
                    break;
                case 4:
                    editClassActivity();
                    break;
                default:

                    break;
            }
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
            var gradeEntryOptions = planner.getAllLetterGrades();
            ArrayAdapter gradeEntryAdapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleDropDownItem1Line, gradeEntryOptions);
            gradeEntry.Adapter = gradeEntryAdapter;            
            gradeEntry.Text = classView.grade;


            if (classView.completed) {
                checkbox.Checked = true;
                grade.Text = "Grade";
            }


        }

        private void modifyClassActivityForm()
        {
            

            ClassActivityView classActivity = planner.getClassActivity(entityID.ToString());
            title.Text = "Edit : Class";
            hideParentEntry();


            var gradeEntryOptions = planner.getAllPercentGrades();
            ArrayAdapter gradeEntryAdapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleDropDownItem1Line, gradeEntryOptions);
            gradeEntry.Adapter = gradeEntryAdapter;

            title.Text = "Edit: Activity";
            entryType.Text = "Activity";
            entry.Text = classActivity.label;
            credits.Text = "Weight";
            creditsEntry.Text = classActivity.weight;
            gradeEntry.Text = classActivity.grade;
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



    }
}