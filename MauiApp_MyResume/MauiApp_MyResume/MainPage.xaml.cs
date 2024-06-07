using MauiApp_MyResume.Models;
using Newtonsoft.Json;

namespace MauiApp_MyResume
{
    public partial class MainPage : ContentPage
    {
        public ResumeStructure resumeData;
        public string contents;

        public MainPage()
        {
            InitializeComponent();
        }

        async Task LoadMauiAsset()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("resume.json");
            using var reader = new StreamReader(stream);

            contents = reader.ReadToEnd();
        }

        private async void OnToggleResumeClicked(object sender, EventArgs e)
        {
            await LoadMauiAsset();
            await ParseResumeData();

            if (ResumeLabel.IsVisible)
            {
                ResumeLabel.IsVisible = false;
                ToggleResumeButton.Text = "Показати резюме";
            }
            else
            {
                if (resumeData != null)
                {
                    string resumeText = $"Ім'я: {resumeData.FullName}\n" +
                                        $"Дата народження: {new DateOnly(resumeData.Birthday.Year, resumeData.Birthday.Day, resumeData.Birthday.Month)}\n" +
                                        $"Освіта: {resumeData.Education}\n" +
                                        $"Навички: {resumeData.Skills}\n" +
                                        $"Контактна інформація:\nEmail: {resumeData.Email}\n" +
                                        $"Телефон: {resumeData.Phone}";

                    ResumeLabel.Text = resumeText;
                    ResumeLabel.IsVisible = true;
                    ToggleResumeButton.Text = "Приховати резюме";
                }
                else
                {
                    Console.WriteLine("Дані резюме не завантажені.");
                }
            }
        }

        async Task ParseResumeData()
        {
            if (!string.IsNullOrEmpty(contents))
            {
                try
                {
                    resumeData = JsonConvert.DeserializeObject<ResumeStructure>(contents);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
