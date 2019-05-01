
namespace Scissors.Win.Extentions
{
    public static class ApplicationExtentions
    {
        public static IDisposable Initialize(this WinApplication winApplication)
            => ApplicationInitializer.Create(winApplication);

        private class ApplicationInitializer : IDisposable
        {
            System.ComponentModel.ISupportInitialize _App;

            private ApplicationInitializer(WinApplication winApplication)
            {
                _App = (System.ComponentModel.ISupportInitialize)winApplication;
                _App.BeginInit();
            }

            public static ApplicationInitializer Create(WinApplication winApplication)
                => new ApplicationInitializer(winApplication);

            void IDisposable.Dispose()
            {
                _App.EndInit();
            }
        }
    }
}