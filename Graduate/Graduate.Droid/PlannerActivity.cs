
using Android.App;

using Android.OS;

using Android.Views;


using Graduate.Droid.Fragments;

namespace Graduate.Droid
{
    [Activity(Label = "PlannerActivity_Main")]
    public class PlannerActivity_Main : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.Title = "Planner";

            SetContentView(Resource.Layout.PlannerActivity_Main);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            AddTab("SchoolYear", new SchoolYearFragment());
            AddTab("Semester", new SemesterFragment());
            AddTab("Class", new ClassFragment());
        }

        private void AddTab(string tabText, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);


            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
            };
            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(view);
            };

            this.ActionBar.AddTab(tab);

        }


        public void findViews() {
        }

        public void handleEvents() {
        }
    }
}