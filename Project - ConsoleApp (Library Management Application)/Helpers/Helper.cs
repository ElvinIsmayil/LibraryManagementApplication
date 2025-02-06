namespace Project___ConsoleApp__Library_Management_Application_.Helpers
{
    public class Helper
    {
        public static void ChangeTextColor(ConsoleColor consoleColor, string word)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(word);
            Console.ResetColor();
        }


        public static void MainMenu()
        {
            Helper.ChangeTextColor(ConsoleColor.Green,
                           "-----------Main Menu-----------\n" +
                           "\nChoose Operation:\n" +
                           "1 - Author Actions Menu\n" +
                           "2 - Book Actions Menu\n" +
                           "3 - Borrower Actions Menu\n" +
                           "4 - Borrow Book\n" +
                           "5 - Return Book\n" +
                           "6 - Return Most Borrowed Book\n" +
                           "7 - Return Late Borrowers\n" +
                           "8 - Show All Borrowers\n" +
                           "9 - Filter Books by Title/Author\n" +
                           "Escape - Exit\n");
        }

        public static void AuthorMenu()
        {
            Helper.ChangeTextColor(ConsoleColor.Green,
                "-----------Author Menu-----------\n" +
                           "\nChoose Operation:\n" +
                           "1 - Show All Authors\n" +
                           "2 - Add new Author\n" +
                           "3 - Update Author\n" +
                           "4 - Delete Author\n" +
                           "Escape - Return to Main Menu\n");
        }

        public static void BookMenu()
        {
            Helper.ChangeTextColor(ConsoleColor.Green,
                "-----------Book Menu-----------\n" +
                           "\nChoose Operation:\n" +
                           "1 - Show All Books\n" +
                           "2 - Add new Book\n" +
                           "3 - Update Book\n" +
                           "4 - Delete Book\n" +
                           "Escape - Return to Main Menu\n");
        }

        public static void BorrowerMenu()
        {
            Helper.ChangeTextColor(ConsoleColor.Green,
                "-----------Borrower Menu-----------\n" +
                           "\nChoose Operation:\n" +
                           "1 - Show All Borrowers\n" +
                           "2 - Add new Borrower\n" +
                           "3 - Update Borrower\n" +
                           "4 - Delete Borrower\n" +
                           "Escape - Return to Main Menu\n");
        }
    }
}
