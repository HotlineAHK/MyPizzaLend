namespace PizzaLend
{
    internal static class Program
    {
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new PizzaTime());
        }
    }

    static class Varibales
    {
        static public string filePath = @"C:\Users\Admin\source\repos\PizzaLend\PizzaLend\users.db";
    }
}