using System;
using System.Windows.Forms;
using shift_making_man.Controllers;
using shift_making_man.Models;
using shift_making_man.Services;

namespace shift_making_man.Views
{
    public partial class LoginForm : Form
    {
        private readonly DataAccessFacade _dataAccessFacade;
        private readonly AuthController _authController;

        public LoginForm(DataAccessFacade dataAccessFacade)
        {
            InitializeComponent();
            _dataAccessFacade = dataAccessFacade;
            _authController = new AuthController(_dataAccessFacade.AdminDataAccess);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("ユーザー名とパスワードを入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Admin loggedInUser = _authController.Login(username, password);
            if (loggedInUser != null)
            {
                MessageBox.Show("ログイン成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MainForm mainForm = new MainForm(_dataAccessFacade);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("ユーザー名またはパスワードが間違っています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreateAdminAccount_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("ユーザー名とパスワードを入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Admin newAdmin = new Admin
                {
                    Username = username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                    FullName = "Default Name",
                    Email = "default@example.com"
                };
                _dataAccessFacade.AdminDataAccess.AddAdmin(newAdmin);
                MessageBox.Show("管理者アカウントが正常に作成されました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}