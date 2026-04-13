using SQLite;
using System.Xml.Linq;

namespace PizzaLend
{
    public partial class PizzaTime : Form
    {
        public PizzaTime()
        {
            InitializeComponent();
            lblLoginError.Visible = false;
            lblRegisterError.Visible = false;
            tabOrder.Enabled = false;
            tabPay.Enabled = false;
        }

        private void PizzaTime_Load(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(Varibales.filePath))
            {
                var db = new SQLiteConnection(Varibales.filePath);
                db.CreateTable<DBusers>();
                db.Close();
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string name = tBxRegisterName.Text;
            string email = tBxRegisterEmail.Text;
            string password = tBxRegisterPassword.Text;

            if (string.IsNullOrEmpty(name)) tBxRegisterName.BackColor = Color.Red;
            else tBxRegisterName.BackColor = Color.White;

            if (string.IsNullOrEmpty(email)) tBxRegisterEmail.BackColor = Color.Red;
            else tBxRegisterEmail.BackColor = Color.White;

            if (string.IsNullOrEmpty(password)) tBxRegisterPassword.BackColor = Color.Red;
            else tBxRegisterPassword.BackColor = Color.White;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return;

            DBusers newUser = new DBusers(name, email, password);

            var db = new SQLiteConnection(Varibales.filePath);

            var users = db.Table<DBusers>().ToList();

            foreach (var user in users)
            {
                if (user.Email == newUser.Email)
                {
                    lblRegisterError.Visible = true;
                    return;
                }
            }

            lblRegisterError.Visible = false;
            db.Insert(newUser);
            db.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = tBxEmail.Text;
            string password = tBxPassword.Text;

            if (string.IsNullOrEmpty(email)) tBxEmail.BackColor = Color.Red;
            else tBxEmail.BackColor = Color.White;

            if (string.IsNullOrEmpty(password)) tBxPassword.BackColor = Color.Red;
            else tBxPassword.BackColor = Color.White;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return;

            var db = new SQLiteConnection(Varibales.filePath);
            var users = db.Table<DBusers>().Where(u => u.Email == email && u.Password == password).ToList();

            if (users.Count == 0) return;
            else 
            {
                tabOrder.Enabled = true;
                tabPay.Enabled = true;
                tabControl.SelectTab(2);
            }
        }
    }
}
